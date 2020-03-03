using System.Collections.Generic;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration.Checkers;
using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore.Checkers
{        
    /// <summary>
    /// Check that if the source file exists, if a respective target file may exist.
    /// </summary>
    public class FileExistsChecker : IChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<FileExistsConfig> Config { get; }
        
        public FileExistsChecker(List<FileExistsConfig> config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public IEnumerable<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (FileExistsConfig config in Config)
            {
                DataResolver resolver = new DataResolver(config.Source);
                resolver.ResolveAdditional("target", config.Target);

                foreach (ResolvedData resolvedFile in resolver.Data)
                {
                    foreach (string pattern in config.Patterns)
                    {
                        ResolvedData resolvedData = resolver.ResolvedData("target", resolvedFile, pattern);
                        
                        if (resolvedData == null)
                        {
                            failures.Add(new Failure(resolvedFile, $"{pattern} does not exist in target data."));
                        }
                    }
                }
            }

            return failures;
        }
    }
}