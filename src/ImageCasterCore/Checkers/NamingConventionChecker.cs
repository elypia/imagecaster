using System.Collections.Generic;
using System.Text.RegularExpressions;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Configuration.Checkers;
using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore.Checkers
{
    /// <summary>
    /// Checks that a project wide naming convention is adhered two by the input
    /// images in the <see cref="Build"/> configuration.
    /// </summary>
    public class NamingConventionChecker : IChecker
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<NamingConventionConfig> Config { get; }
        
        public NamingConventionChecker(List<NamingConventionConfig> config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public IEnumerable<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (NamingConventionConfig config in Config)
            {
                DataResolver resolver = new DataResolver(config.Source);
                List<ResolvedData> resolvedFiles = resolver.Data;

                foreach (ResolvedData resolvedFile in resolvedFiles)
                {
                    Regex regex = config.Pattern;
                    
                    if (!regex.IsMatch(resolvedFile.Name))
                    {
                        failures.Add(new Failure(resolvedFile, $"filename doesn't adhere to the naming convention (pattern) /{regex}/."));
                    }
                }
            }

            return failures;
        }
    }
}
