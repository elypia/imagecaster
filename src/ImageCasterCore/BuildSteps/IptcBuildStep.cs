using System.Collections.Generic;
using System.Linq;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    /// <summary>The build step to add or replace Exif data on images.</summary>
    public class IptcBuildStep : IBuildStep
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly List<TagConfig<IptcTag>> DefaultIptcTags = new List<TagConfig<IptcTag>>
        {
            new TagConfig<IptcTag>(IptcTag.CreatedDate, "${NOW_DATE}"),
            new TagConfig<IptcTag>(IptcTag.CreatedTime, "${NOW_TIME}")
        }; 
        
        public List<TagConfig<IptcTag>> Config { get; set; }

        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            Config = config?.Build?.Metadata?.Iptc;
            return Config != null;
        }

        /// <summary>
        /// TODO: Default DATE
        /// </summary>
        /// <param name="context"></param>
        /// <param name="magickImage"></param>
        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            StringInterpolator interpolator = new StringInterpolator(context.ResolvedFile.FileInfo, magickImage);
            IIptcProfile iptcProfile = magickImage.GetIptcProfile() ?? new IptcProfile();
            IEnumerable<TagConfig<IptcTag>> tags = DefaultIptcTags.Concat(Config);

            foreach (TagConfig<IptcTag> tag in tags)
            {
                string value = interpolator.Interpolate(tag.Value);
                iptcProfile.SetValue(tag.Tag, value);
            }
            
            magickImage.AddProfile(iptcProfile);
            context.Next(magickImage);
        }
    }
}
