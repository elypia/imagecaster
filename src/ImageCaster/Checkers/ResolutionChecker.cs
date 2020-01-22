using System.Collections.Generic;
using System.IO;
using ImageCaster.Configuration.Checkers;
using ImageCaster.Api;
using ImageCaster.Extensions;
using ImageMagick;
using NLog;

namespace ImageCaster.Checks
{
    /// <summary>Check that the resolution of images match.</summary>
    public class MaskResolutionChecker : IChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; }
        public List<ResolutionMatchesConfig> Config { get; }
        
        public MaskResolutionChecker(ICollector collector, List<ResolutionMatchesConfig> config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public List<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (ResolutionMatchesConfig config in Config)
            {
                List<ResolvedFile> resolvedFiles = Collector.Collect(config.Source);

                foreach (ResolvedFile resolvedFile in resolvedFiles)
                {
                    using (MagickImage magickSource = new MagickImage(resolvedFile.FileInfo))
                    {
                        FileInfo fileInfo = Collector.Resolve(resolvedFile, config.Target);

                        if (!fileInfo.Exists)
                        {
                            continue;
                        }

                        using (MagickImage magickTarget = new MagickImage(fileInfo))
                        {
                            if (magickSource.Height != magickTarget.Height || magickSource.Width != magickTarget.Width)
                            {
                                failures.Add(new Failure(resolvedFile, "resolutions does not much with target"));
                            }
                        }
                    }
                }
            }

            return failures;
        }
    }
}