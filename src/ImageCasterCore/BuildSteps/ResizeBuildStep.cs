using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    public class ResizeBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Resize Config { get; set; }

        public ResizeBuildStep(ImageCasterConfig config)
        {
            this.Config = config.Build.Resize.RequireNonNull();
        }
        
        public void Execute(PipelineContext context, MagickImage magickImage)
        {
            foreach (MagickGeometry dimensions in Config.Geometries)
            {
                using (MagickImage dimensionsMagickImage = (MagickImage)magickImage.Clone())
                {
                    PipelineContext contextClone = context.Clone();
                    contextClone.AppendPath(dimensions.ToString());

                    if (dimensions.Width < 0 || dimensions.Height < 0)
                    {
                        Logger.Fatal("The height and width of dimensions can not be negative.");
                    }

                    if (dimensions.Width == 0 && dimensions.Height == 0)
                    {
                        Logger.Fatal("Both height and width in the dimensions configuration were 0, please specify at least one.");
                    }
                    
                    dimensionsMagickImage.FilterType = Config.Filter;
                    dimensionsMagickImage.Resize(dimensions);
                    
                    contextClone.Next(dimensionsMagickImage);
                }
            }
        }
    }
}
