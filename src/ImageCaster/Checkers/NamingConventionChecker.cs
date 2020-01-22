using System.Collections.Generic;
using System.Text.RegularExpressions;
using ImageCaster.Configuration;
using ImageCaster.Configuration.Checkers;
using ImageCaster.Api;
using ImageCaster.Extensions;
using ImageMagick;
using NLog;

namespace ImageCaster.Checks
{
    /// <summary>
    /// Checks that a project wide naming convention is adhered two by the input
    /// images in the <see cref="Export"/> configuration.
    /// </summary>
    public class NamingConventionChecker : IChecker
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; }
        public List<NamingConventionConfig> Config { get; }
        
        public NamingConventionChecker(ICollector collector, List<NamingConventionConfig> config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public List<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (NamingConventionConfig config in Config)
            {
                List<ResolvedFile> resolvedFiles = Collector.Collect(config.Source);

                foreach (ResolvedFile resolvedFile in resolvedFiles)
                {
                    Regex regex = config.Pattern;
                    
                    if (!regex.IsMatch(resolvedFile.FileInfo.Name))
                    {
                        failures.Add(new Failure(resolvedFile, $"filename doesn't adhere to the naming convention (pattern) /{regex}/."));
                    }
                }
            }

            return failures;
        }
    }
}
