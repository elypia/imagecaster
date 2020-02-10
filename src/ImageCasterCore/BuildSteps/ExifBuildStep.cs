using System.Collections.Generic;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    /// <summary>The build step to add or replace Exif data on images.</summary>
    public class ExifBuildStep : IBuildStep
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The default Exif tags ImageCaster may opt to add to images unless
        /// disabled by `export.metadata.defaults`, nullified, or overridden
        /// in the user's own configuration.
        /// </summary>
        private static readonly List<TagConfig> DefaultExifTags = new List<TagConfig>
        {
            new TagConfig(ExifTag.Software         , "${IMAGECASTER}"   ),
//            new TagConfig(ExifTag.ExifVersion      , "2.31"             ),
            new TagConfig(ExifTag.ImageUniqueID    , "${FILE_NAME}"     ),
            new TagConfig(ExifTag.DocumentName     , "${FILE_NAME}"     ),
            new TagConfig(ExifTag.DateTime         , "${NOW}"           ),
            new TagConfig(ExifTag.DateTimeDigitized, "${CREATION_TIME}" ),
            new TagConfig(ExifTag.DateTimeOriginal , "${CREATION_TIME}" )
        }; 
        
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

            if (Config.Defaults)
            {
                foreach (TagConfig tag in DefaultExifTags)
                {
                    StringInterpolator interpolator = new StringInterpolator(context.ResolvedFile.FileInfo, magickImage);
                    string value = interpolator.Interpolate(tag.Value);
                    exifProfile.SetValue((ExifTag<string>)tag.Tag, value);
                }
            }
            
            foreach (TagConfig tag in Config.Tags)
            {
                StringInterpolator interpolator = new StringInterpolator(context.ResolvedFile.FileInfo, magickImage);
                string value = interpolator.Interpolate(tag.Value);
                exifProfile.SetValue((ExifTag<string>)tag.Tag, value);
            }
            
            magickImage.AddProfile(exifProfile);
            context.Next(magickImage);
        }
    }
}
