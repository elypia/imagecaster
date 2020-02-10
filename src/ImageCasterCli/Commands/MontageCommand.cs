using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using ImageCaster.Api;
using ImageCaster.Configuration;
using ImageCaster.Extensions;
using ImageMagick;
using NLog;

namespace ImageCaster.Commands
{
    public class MontageCommand : ICliCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>The collector implementation in use, determines how files are found.</summary>
        public ICollector Collector { get; }
        
        /// <summary>The user defined configuration to export montages of images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="collector"><see cref="Collector"/></param>
        /// <param name="config"><see cref="Config"/></param>
        public MontageCommand(ICollector collector, ImageCasterConfig config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public Command Configure()
        {
            return new Command("montage", "Export a single image comprised of all matching output images")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }

        public int Execute()
        {
            List<PatternConfig> patterns = Config.Montages;

            MontageSettings montageSettings = new MontageSettings()
            {
                BackgroundColor = MagickColors.None,
                Geometry = new MagickGeometry(2, 2, 0, 0),
                TileGeometry = new MagickGeometry(8, 0),
                Font = "DejaVu-Sans-Mono-Bold",
            };

            MagickReadSettings magickSettings = new MagickReadSettings()
            {
                Font = "DejaVu-Sans-Mono-Bold",
                FontPointsize = 14,
                BackgroundColor = MagickColors.None,
                FillColor = MagickColors.White
            };
            
            foreach (PatternConfig pattern in patterns)
            {
                string name = pattern.Name;
                List<ResolvedFile> resolvedFiles = Collector.Collect(pattern.Pattern);
                
                if (resolvedFiles.Count == 0)
                {
                    Logger.Error("The pattern for montage `{0}` doesn't match any files, exiting the application.", name);
                    return (int)ExitCode.MalformedConfigFields;
                }
                
                resolvedFiles.Sort();
                
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    foreach (ResolvedFile resolvedFile in resolvedFiles)
                    {
                        FileInfo fileInfo = resolvedFile.FileInfo;
                        string filename = fileInfo.Name;
                        string fileShortName = Path.GetFileNameWithoutExtension(filename);
                        
                        MagickImage magickImage = new MagickImage(fileInfo, magickSettings);
                        magickImage.Extent(128, 144, MagickColors.None);

                        MagickColor color = MagickColor.FromRgba(0, 0, 0, 88);
                        DrawableRectangle drawableRect = new DrawableRectangle(0, 128, 144, 144);
                        DrawableFillColor drawableColor = new DrawableFillColor(color);
                        magickImage.Draw(drawableColor, drawableRect);
                        
                        magickImage.Annotate(fileShortName, Gravity.South);
                        
                        collection.Add(magickImage);
                        Logger.Debug("Appended montage {0} with: {1}", name, resolvedFile);
                    }

                    Logger.Debug("Finished collecting input, creating a montage of {0} images.", collection.Count);
                    IMagickImage montage = collection.Montage(montageSettings);
                    
                    string filepath = Path.Combine("build", "montages", name + ".png");
                    FileInfo outputPath = new FileInfo(filepath);
                    outputPath.Directory.Create();
                    
                    montage.Write(outputPath);
                }
            }
            
            return (int)ExitCode.Normal;
        }
    }
}
