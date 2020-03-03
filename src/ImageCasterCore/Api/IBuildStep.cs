using ImageCasterCore.Configuration;
using ImageMagick;

namespace ImageCasterCore.Api
{
    public interface IBuildStep
    {
        void Execute(PipelineContext context, IMagickImage magickImage);
    }
}
