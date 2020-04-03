using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImageCasterCore.Api;
using ImageCasterCore.Checkers;
using ImageCasterCore.Configuration;
using ImageCasterCore.Exceptions;
using NLog;

namespace ImageCasterCore.Actions
{
    /// <summary>
    /// Performs any validation to ensure the project structure adheres
    /// to all standards, without actually exporting any images.
    /// </summary>
    public class CheckAction : IAction
    {
        /// <summary>The NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The configured checks to perform.</summary>
        public Checks Checks { get; }

        public CheckAction(Checks checks)
        {
            this.Checks = checks;
        }
        
        public void Execute()
        {
            Logger.Debug("Executed Check command, running checks.");

            if (Checks == null)
            {
                throw new ConfigurationException("Executed check command, however no checks are defined in the configuration; doing nothing.");
            }

            List<Failure> failures = new List<Failure>();

            if (Checks.FileExists != null)
            {
                FileExistsChecker checker = new FileExistsChecker(Checks.FileExists);
                failures.AddRange(checker.Check());
            }

            if (Checks.NamingConvention != null)
            {
                NamingConventionChecker checker = new NamingConventionChecker(Checks.NamingConvention);
                failures.AddRange(checker.Check());
            }

            if (Checks.PowerOfTwo != null)
            {
                PowerOfTwoChecker checker = new PowerOfTwoChecker(Checks.PowerOfTwo);
                failures.AddRange(checker.Check());
            }

            if (Checks.ResolutionMatches != null)
            {
                ResolutionMatchesChecker matchesChecker = new ResolutionMatchesChecker(Checks.ResolutionMatches);
                failures.AddRange(matchesChecker.Check());
            }
            
            foreach (Failure failure in failures)
            {
                Logger.Warn(failure);
            }

            if (failures.Count != 0)
            {
                throw new ValidationException("Failures occured during validation.");
            }
        }
    }
}
