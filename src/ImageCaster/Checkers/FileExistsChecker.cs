using System.Collections.Generic;
using System.IO;
using ImageCaster.Api;
using ImageCaster.Configuration.Checkers;
using ImageCaster.Extensions;
using NLog;

namespace ImageCaster.Checkers
{        
    /// <summary>
    /// Check that if the source file exists, if a respective target file may exist.
    /// </summary>
    public class FileExistsChecker : IChecker
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; }
        public List<FileExistsConfig> Config { get; }
        
        public FileExistsChecker(ICollector collector, List<FileExistsConfig> config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public List<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (FileExistsConfig config in Config)
            {
                List<ResolvedFile> resolvedFiles = Collector.Collect(config.Source);

                foreach (ResolvedFile resolvedFile in resolvedFiles)
                {
                    foreach (string pattern in config.Patterns)
                    {
                        FileInfo fileInfo = Collector.Resolve(resolvedFile, pattern);
                        
                        if (!fileInfo.Exists)
                        {
                            failures.Add(new Failure(resolvedFile, $"{fileInfo} does not exist"));
                        }
                    }
                }
            }

            return failures;
        }
    }
}