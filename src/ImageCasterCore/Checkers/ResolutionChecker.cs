using System.Collections.Generic;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration.Checkers;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Checkers
{
    /// <summary>Check that the resolution of images match.</summary>
    public class ResolutionMatchesChecker : IChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<ResolutionMatchesConfig> Config { get; }
        
        public ResolutionMatchesChecker(List<ResolutionMatchesConfig> config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public IEnumerable<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (ResolutionMatchesConfig config in Config)
            {
                DataResolver resolver = new DataResolver(config.Source);
                resolver.ResolveAdditional("target", config.Target);
                
                foreach (ResolvedData resolvedFile in resolver.Data)
                {
                    using (IMagickImage magickSource = resolvedFile.ToMagickImage())
                    {
                        ResolvedData resolvedData = resolver.ResolvedData("target", resolvedFile, config.Pattern);

                        if (resolvedData == null)
                        {
                            continue;
                        }

                        using (IMagickImage magickTarget = resolvedData.ToMagickImage())
                        {
                            if (magickSource.Height != magickTarget.Height || magickSource.Width != magickTarget.Width)
                            {
                                failures.Add(new Failure(resolvedFile, "resolutions does not much with target."));
                            }
                        }
                    }
                }
            }

            return failures;
        }
    }
}
