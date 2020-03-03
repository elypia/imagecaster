using System.IO;
using System.Net.Mime;
using ImageCasterCore.Api;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    public class RecolorBuildStep : IBuildStep
    {
        private const string OriginalDirectory = "original";
        
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; set; } = new RegexCollector();
        public Recolor Config { get; set; }

        public RecolorBuildStep(ImageCasterConfig config)
        {
            this.Config = config.Build.Recolor.RequireNonNull();
        }
        
        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            ResolvedData maskFileInfo = context.DataResolver.ResolvedData("masks", context.ResolvedData, Config.Mask.Pattern);

            if (maskFileInfo != null)
            {
                using (IMagickImage maskMagickImage = maskFileInfo.ToMagickImage())
                {
                    magickImage.SetWriteMask(maskMagickImage);
                }
            }

            if (Config.Original)
            {
                PipelineContext contextClone = context.Clone();
                contextClone.AppendPath(OriginalDirectory);
                contextClone.Next(magickImage);
            }
            
            foreach (Modulation modulate in Config.Modulation)
            {
                using (IMagickImage modulatedMagickImage = magickImage.Clone())
                {
                    PipelineContext contextClone = context.Clone();
                    contextClone.AppendPath($"{modulate.Name}");
                    contextClone.AppendPrefix(modulate.Prefix ?? modulate.Name.Substring(0, 1));
                    
                    modulatedMagickImage.Modulate(modulate.Brightness, modulate.Saturation, modulate.Hue);
                    modulatedMagickImage.RemoveWriteMask();

                    contextClone.Next(modulatedMagickImage);
                }
            }
        }
    }
}
