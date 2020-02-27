using System.Collections.Generic;
using System.Text.Json.Serialization;
using ImageCasterCore.Configuration.Checkers;
using NLog;

namespace ImageCasterCore.Configuration
{
    public class Checks
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [JsonPropertyName("file-exists")]
        public List<FileExistsConfig> FileExists { get; set; }
        
        [JsonPropertyName("naming-convention")]
        public List<NamingConventionConfig> NamingConvention { get; set; }
        
        [JsonPropertyName("power-of-two")]
        public List<PowerOfTwoConfig> PowerOfTwo { get; set; }
        
        [JsonPropertyName("resolution-matches")]
        public List<ResolutionMatchesConfig> ResolutionMatches { get; set; }
    }
}
