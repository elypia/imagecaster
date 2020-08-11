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
    public class IptcBuildStep : IBuildStep
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly List<TagConfig<IptcTag>> DefaultIptcTags = new List<TagConfig<IptcTag>>
        {
            new TagConfig<IptcTag>(IptcTag.CreatedDate, "${NOW_DATE}"),
            new TagConfig<IptcTag>(IptcTag.CreatedTime, "${NOW_TIME}")
        }; 
        
        public Metadata Config { get; set; }

        public StringInterpolator Interpolator { get; }

        public IptcBuildStep(ImageCasterConfig config) : this(config, new StringInterpolator())
        {
            
        }

        public IptcBuildStep(ImageCasterConfig config, StringInterpolator interpolator)
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
        
        /// <param name="context"></param>
        /// <param name="magickImage"></param>
        public void Execute(PipelineContext context, MagickImage magickImage)
        {
            StringInterpolator interpolator = Interpolator.Branch(context.ResolvedData, magickImage);
            IIptcProfile iptcProfile = magickImage.GetIptcProfile() ?? new IptcProfile();
            
            IEnumerable<TagConfig<IptcTag>> tags = new List<TagConfig<IptcTag>>();

            if (Config.Defaults)
            {
                tags = tags.Concat(DefaultIptcTags);
            }

            if (Config.Iptc != null)
            {
                tags = tags.Concat(Config.Iptc);
            }
            
            foreach (TagConfig<IptcTag> tag in tags)
            {
                string value = interpolator.Interpolate(tag.Value);
                iptcProfile.SetValue(tag.Tag, value);
            }

            magickImage.SetProfile(iptcProfile);
            context.Next(magickImage);
        }
    }
}
