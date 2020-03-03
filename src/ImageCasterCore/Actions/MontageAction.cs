using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Actions
{
    public class MontageAction : IAction
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>The user defined configuration to export montages of images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="config"><see cref="Config"/></param>
        public MontageAction(ImageCasterConfig config)
        {
            this.Config = config.RequireNonNull();
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
                DataResolver resolver = new DataResolver(pattern.Source);
                List<ResolvedData> resolverData = resolver.Data;
                
                if (resolverData.Count == 0)
                {
                    Logger.Error("The pattern for montage `{0}` doesn't match any files, exiting the application.", name);
                }
                
                resolverData.Sort();
                
                using (MagickImageCollection collection = new MagickImageCollection())
                {
                    foreach (ResolvedData resolvedFile in resolverData)
                    {
                        string filename = resolvedFile.Name;
                        string fileShortName = Path.GetFileNameWithoutExtension(filename);
                        
                        IMagickImage magickImage = resolvedFile.ToMagickImage(magickSettings);
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
                        outputPath.Directory.Create();
                    
                        montage.Write(outputPath);
                    }
                }
            }
            
            return 0;
        }
    }
}
