using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ImageCaster.Configuration;
using ImageCaster.Interfaces;
using ImageCaster.Utilities;
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

        /// <summary>
        /// The pattern all input files must adhere to, the check fails if this doesn't match.
        /// </summary>
        public Regex Regex { get; }

        public NamingConventionChecker(string pattern) : this(new Regex(pattern))
        {
            // Do nothing
        }
        
        /// <param name="regex">The regular expression to match against when checking images.</param>
        public NamingConventionChecker(Regex regex)
        {
            this.Regex = regex.RequireNonNull();
        }
        
        public bool Check(FileInfo fileInfo)
        {
            return Regex.IsMatch(fileInfo.Name);
        }

        public Failure FailureMessage(ResolvedFile resolvedFile)
        {
            FileInfo fileInfo = resolvedFile.FileInfo;
            string message = $"filename does not adhere to the pattern /{Regex}/.";
            return new Failure(Configuration.Check.NamingConvention, fileInfo, message);
        }
    }
}
