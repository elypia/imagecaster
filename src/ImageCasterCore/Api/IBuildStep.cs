using ImageCasterCore.Configuration;
using ImageMagick;

namespace ImageCasterCore.Api
{
    public interface IBuildStep
    {
        /// <summary>
        /// Is this build step is enabled.
        /// </summary>
        /// <param name="collector"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        bool Configure(ICollector collector, ImageCasterConfig config);
        
        void Execute(PipelineContext context, IMagickImage magickImage);
    }
}
