using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ImageCaster.Checks;
using ImageCaster.Configuration;
using ImageCaster.Interfaces;
using ImageCaster.Utilities;
using NLog;

namespace ImageCaster.Commands
{
    public class TestCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ICollector Collector { get; }
        public ImageCasterConfig Config { get; }

        public TestCommand(ICollector collector, ImageCasterConfig config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public void Test()
        {
            Logger.Debug("Executed Test command, running checks.");

            List<CheckConfig> checks = Config.Checks;

            if (checks == null || checks.Count == 0)
            {
                Logger.Info("Executed check command, however no checks are defined in the configuration; doing nothing.");
                return;
            }

            foreach (CheckConfig check in checks)
            {
                IChecker checker;
                Dictionary<string, string> args = check.Args;

                switch (check.Name.Check)
                {
                    case Check.FileExists:
                        checker = new FileExistsChecker();
                        break;
                    case Check.MaskResolutionMatches:
                        checker = new MaskResolutionMatchesChecker();
                        break;
                    case Check.NamingConvention:
                        if (!args.ContainsKey("pattern"))
                            throw new ArgumentException("Pattern was not passed to naming convention check.");

                        string pattern = args["pattern"];
                        Regex regex = new Regex(pattern);
                        checker = new NamingConventionChecker(regex);
                        break;
                    default:
                        throw new InvalidOperationException("An unknown or unimplemented check was passed in the checker configuartion.");
                }
            }
        }
    }
}
