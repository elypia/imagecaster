using ImageCaster.Api;
using ImageCaster.Configuration;
using ImageMagick;
using NLog;

namespace ImageCaster.BuildSteps
{
    /// <summary>
    /// The build step to add or replace Exif data on images.
    /// </summary>
    public class ExifBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ExifConfig Config { get; set; }

        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            Config = config?.Export?.Metadata?.Exif;
            return Config != null;
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            IExifProfile exifProfile = magickImage.GetExifProfile();

            if (exifProfile == null)
            {
                Logger.Debug("Resolved file {0} doesn't have an Exif profile, adding one.", context.ResolvedFile.FileInfo);
                exifProfile = new ExifProfile();
            }

            exifProfile.SetValue(ExifTag.Software, "ImageCaster 0.1.10");
            magickImage.AddProfile(exifProfile);
            
            context.Next(magickImage);
        }
    }
}
