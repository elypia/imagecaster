using System.Collections.Generic;
using ImageCasterCore.Configuration.Checkers;
using NLog;

namespace ImageCasterCore.Configuration
{
    public class Checks
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<FileExistsConfig> FileExists { get; set; }
        public List<NamingConventionConfig> NamingConvention { get; set; }
        public List<PowerOfTwoConfig> PowerOfTwo { get; set; }
        public List<ResolutionMatchesConfig> ResolutionMatches { get; set; }
    }
}
