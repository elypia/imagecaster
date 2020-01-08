using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using ImageCaster.Configuration;
using ImageCaster.Interfaces;
using ImageCaster.Utilities;
using ImageMagick;
using NLog;
using NLog.Fluent;

namespace ImageCaster.Commands
{
    /// <summary>
    /// Using the <see cref="Export"/> configuration to export the input
    /// in all desired ouput images.
    /// </summary>
    public class BuildCommand
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The collector implementation in use, determines how files are found.</summary>
        public ICollector Collector { get; }
        
        /// <summary>The user defined configuration to export images.</summary>
        public ImageCasterConfig Config { get; }

        /// <param name="collector"><see cref="Collector"/></param>
        /// <param name="config"><see cref="Config"/></param>
        public BuildCommand(ICollector collector, ImageCasterConfig config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        /// <summary>Start building all images.</summary>
        /// <exception cref="InvalidOperationException">If the configuration is malformed.</exception>
        public void Build()
        {
            Logger.Info("Executed build command, started working.");

            Export export = Config.Export;

            if (export == null)
            {
                Logger.Warn("Build command was called, but no export configuration was defined, doing nothing.");
                return;
            }
            
            string input = export.Input;

            if (input == null)
                throw new InvalidOperationException("export.input configuration is required to export images.");
            
            List<ResolvedFile> resolvedFiles = Collector.Collect(input);
            Logger.Info("Found {0} files matching collection pattern.", resolvedFiles.Count);

            foreach (ResolvedFile resolvedFile in resolvedFiles)
            {
                FileInfo fileInfo = resolvedFile.FileInfo;
                
                using (MagickImage magickImage = new MagickImage(fileInfo))
                {
                    IExifProfile exifProfile = magickImage.GetExifProfile();

                    if (exifProfile == null)
                        Logger.Warn("Resolved file {0} does not have an EXIF profile.", resolvedFile.FileInfo);
                    
                    Colors colors = export.Colors;

                    if (colors != null)
                    {
                        string maskPattern = colors.Mask;
                        FileInfo maskFileInfo = Collector.Resolve(resolvedFile, maskPattern);

                        if (maskFileInfo != null && maskFileInfo.Exists)
                        {
                            using (MagickImage maskMagickImage = new MagickImage(maskFileInfo))
                            {
                                magickImage.SetWriteMask(maskMagickImage);
                            }
                        }

                        foreach (Modulate modulate in colors.Modulation)
                        {
                            magickImage.Modulate(modulate.Brightness, modulate.Saturation, modulate.Hue);
                            magickImage.RemoveWriteMask();
                            
                            using (IMagickImage modulatedMagickImage = magickImage.Clone())
                            {
                                Resize resize  = export.Resize;
                                string units = resize.Units.Aliases[0];

                                foreach (Dimensions dimensions in resize.Dimensions)
                                {
                                    modulatedMagickImage.Resize((int)dimensions.Height, (int)dimensions.Height);
                                    
                                    string outputPath = Path.Combine("build", modulate.Name, "@" + dimensions.Height + units, fileInfo.Name);
                                    FileInfo output = new FileInfo(outputPath);
                                    
                                    output.Directory.Create();
                                    
                                    magickImage.Write(output);
                                    Logger.Info("Written file to: {0}", output);
                                }                                
                            }
                        }
                    }
                }
            }
        }
    }
}
