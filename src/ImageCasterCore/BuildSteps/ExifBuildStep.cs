using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
        private static readonly List<TagConfig<ExifTag>> DefaultExifTags = new List<TagConfig<ExifTag>>
        {
            new TagConfig<ExifTag>(ExifTag.Software         , "${IMAGECASTER}"   ),
//            new TagConfig<ExifTag>(ExifTag.ExifVersion      , "2.31"             ),
            new TagConfig<ExifTag>(ExifTag.ImageUniqueID    , "${FILE_NAME}"     ),
            new TagConfig<ExifTag>(ExifTag.DocumentName     , "${FILE_NAME}"     ),
            new TagConfig<ExifTag>(ExifTag.DateTime         , "${NOW}"           ),
            new TagConfig<ExifTag>(ExifTag.DateTimeDigitized, "${CREATION_TIME}" ),
            new TagConfig<ExifTag>(ExifTag.DateTimeOriginal , "${CREATION_TIME}" )
        }; 
        
        public ExifConfig Config { get; set; }

        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            Config = config?.Build?.Metadata?.Exif;
            return Config != null;
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            StringInterpolator interpolator = new StringInterpolator(context.ResolvedFile.FileInfo, magickImage);
            IExifProfile exifProfile = magickImage.GetExifProfile() ?? new ExifProfile();
            IEnumerable<TagConfig<ExifTag>> tags = DefaultExifTags.Concat(Config.Tags);
            
            foreach (TagConfig<ExifTag> tag in tags)
            {
                string value = interpolator.Interpolate(tag.Value);
                exifProfile.SetValue((ExifTag<string>)tag.Tag, value);
            }
            
            magickImage.AddProfile(exifProfile);
            context.Next(magickImage);
        }
    }
}
