using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Exceptions;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Actions
{
    public class MontageAction : IAction
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>An array of all available fonts on the system.</summary>
        private static readonly FontFamily[] FontFamilies = FontFamily.Families;

        /// <summary>The user defined configuration to export montages of images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="config"><see cref="Config"/></param>
        public MontageAction(ImageCasterConfig config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public void Execute()
        {
            MontageConfig montageConfig = Config.Montages;

            if (montageConfig == null)
            {
                throw new ConfigurationException("Montage action performed, but no montages are defined in the configuration.");
            }
            
            string font = montageConfig.Font ?? DefaultFontFamily();

            MontageSettings montageSettings = new MontageSettings()
            {
                BackgroundColor = MagickColors.None,
                Geometry = new MagickGeometry(2, 2, 0, 0),
                TileGeometry = new MagickGeometry(8, 0),
                Font = font
            };

            MagickReadSettings magickSettings = new MagickReadSettings()
            {
                FontFamily = font,
                FontPointsize = 14,
                BackgroundColor = MagickColors.None,
                FillColor = MagickColors.White
            };
            
            foreach (PatternConfig pattern in montageConfig.Patterns)
            {
                string name = pattern.Name;
                DataResolver resolver = new DataResolver(pattern.Source);
                List<ResolvedData> resolverData = resolver.Data;
                
                if (resolverData.Count == 0)
                {
                    throw new ConfigurationException($"The pattern for montage {name} doesn't match any files, exiting the application.");
                }
                
                resolverData.Sort();
                
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    foreach (ResolvedData resolvedFile in resolverData)
                    {
                        string filename = resolvedFile.Name;
                        string fileShortName = Path.GetFileNameWithoutExtension(filename);
                        
                        MagickImage magickImage = resolvedFile.ToMagickImage(magickSettings);
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

                    using (IMagickImage montage = collection.Montage(montageSettings))
                    {
                        string filepath = Path.Combine("build", "montages", name + ".png");
                        
                        FileInfo outputPath = new FileInfo(filepath);
                        outputPath.Directory?.Create();
                        
                        Logger.Debug("Writing monatage with name {0} to {1}.", name, filepath);
                        montage.Write(outputPath);
                    }
                }
            }
        }

        /// <returns>
        /// The name of a font specified in the list, or a
        /// system fault if none of the fonts specified can be found.
        /// </returns>
        private string DefaultFontFamily()
        {
            if (FontFamilies.Length == 0)
            {
                throw new Exception("There is no font available in the system, unable to create montage.");
            }
            
            if (FontFamily.GenericMonospace != null)
            {
                return FontFamily.GenericMonospace.Name;
            }
            
            if (FontFamily.GenericSansSerif != null)
            {
                return FontFamily.GenericSansSerif.Name;
            }
            
            return FontFamily.GenericSerif != null ? FontFamily.GenericSerif.Name : FontFamilies[0].Name;
        }
    }
}
