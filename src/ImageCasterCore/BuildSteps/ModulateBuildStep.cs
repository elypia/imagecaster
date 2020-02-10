using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    public class ModulateBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; set; }
        public Colors Config { get; set; }
        
        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config?.Export?.Colors;
            return Config != null;
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            FileInfo maskFileInfo = Collector.Resolve(context.ResolvedFile, Config.Mask);

            if (maskFileInfo != null && maskFileInfo.Exists)
            {
                using (MagickImage maskMagickImage = new MagickImage(maskFileInfo))
                {
                    magickImage.SetWriteMask(maskMagickImage);
                }
            }

            foreach (Modulate modulate in Config.Modulate)
            {
                using (IMagickImage modulatedMagickImage = magickImage.Clone())
                {
                    PipelineContext contextClone = context.Clone();
                    contextClone.AppendPath($"{modulate.Name}");
                    contextClone.AppendPrefix(modulate.Prefix);
                    
                    modulatedMagickImage.Modulate(modulate.Brightness, modulate.Saturation, modulate.Hue);
                    modulatedMagickImage.RemoveWriteMask();

                    contextClone.Next(modulatedMagickImage);
                }
            }
        }
    }
}