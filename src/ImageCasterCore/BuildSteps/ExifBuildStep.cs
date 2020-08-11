using System.Collections.Generic;
using System.Linq;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
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
            new TagConfig<ExifTag>(ExifTag.ImageUniqueID    , "${DATA_NAME}"     ),
            new TagConfig<ExifTag>(ExifTag.DocumentName     , "${DATA_NAME}"     ),
            new TagConfig<ExifTag>(ExifTag.DateTime         , "${NOW}"           )
        }; 
        
        public Metadata Config { get; set; }

        public StringInterpolator Interpolator { get; }

        public ExifBuildStep(ImageCasterConfig config) : this(config, new StringInterpolator())
        {
            
        }

        public ExifBuildStep(ImageCasterConfig config, StringInterpolator interpolator)
        {
            this.Config = config.Build.Metadata.RequireNonNull();
            Interpolator = interpolator.RequireNonNull();
            
            Dictionary<string, string> variables = config.Variables;
            
            if (config.Variables != null)
            {
                foreach ((string key, string value) in variables)
                {
                    Interpolator.Add(key, value);
                }
            }
        }
        
        public void Execute(PipelineContext context, MagickImage magickImage)
        {
            StringInterpolator interpolator = Interpolator.Branch(context.ResolvedData, magickImage);
            IExifProfile exifProfile = magickImage.GetExifProfile() ?? new ExifProfile();

            IEnumerable<TagConfig<ExifTag>> tags = new List<TagConfig<ExifTag>>();

            if (Config.Defaults)
            {
                tags = tags.Concat(DefaultExifTags);
            }

            if (Config.Exif != null)
            {
                tags = tags.Concat(Config.Exif);
            }
            
            foreach (TagConfig<ExifTag> tag in tags)
            {
                string value = interpolator.Interpolate(tag.Value);
                exifProfile.SetValue((ExifTag<string>)tag.Tag, value);
            }

            magickImage.SetProfile(exifProfile);
            context.Next(magickImage);
        }
    }
}
