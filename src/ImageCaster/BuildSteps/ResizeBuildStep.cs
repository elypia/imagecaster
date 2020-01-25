using System;
using ImageCaster.Api;
using ImageCaster.Configuration;
using ImageCaster.Resizers;
using ImageMagick;
using NLog;

namespace ImageCaster.BuildSteps
{
    public class ResizeBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Resize Config { get; set; }

        public IResizer Resizer { get; set; }

        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            this.Config = config?.Export?.Sizes;

            if (Config == null)
            {
                return false;
            }

            Unit unit = Config.Units?.Unit ?? Unit.Pixel;
            
            switch (unit)
            {
                case Unit.Pixel:
                    Resizer = new PixelResizer();
                    break;
                case Unit.Percentage:
                    Resizer = new PercentResizer();
                    break;
                default:
                    Logger.Fatal("Unrecognized unit found in switch case, this is a bug in ImageCaster.");
                    break;
            }
            
            return true;
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            UnitInfo unitInfo = Config.Units ?? UnitInfo.Pixel;
            string units = unitInfo.Aliases[0];

            foreach (Dimensions dimensions in Config.Dimensions)
            {
                using (IMagickImage dimensionsMagickImage = magickImage.Clone())
                {
                    PipelineContext contextClone = context.Clone();
                    contextClone.AppendPath($"@{dimensions.Height}{units}");

                    uint height = dimensions.Height;
                    uint width = dimensions.Width;

                    if (height == 0 && width == 0)
                    {
                        Logger.Fatal("Both height and width have been set to 0 in sizes configuration.");
                        Environment.Exit((int)ExitCode.MalformedConfigFields);
                    }

                    Resizer.Resize(dimensionsMagickImage, (int)dimensions.Width, (int)dimensions.Height);
                    
                    contextClone.Next(dimensionsMagickImage);
                }
            }
        }
    }
}
