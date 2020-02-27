using System.Collections.Generic;
using System.Text.Json.Serialization;
using NLog;

namespace ImageCasterCore.Configuration
{
    public class ImageCasterConfig
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        [JsonPropertyName("build")]
        public Build Build { get; set; }

        [JsonPropertyName("montages")]
        public List<PatternConfig> Montages { get; set; }
        
        [JsonPropertyName("archives")]
        public List<Archive> Archives { get; set; }
        
        [JsonPropertyName("checks")]
        public Checks Checks { get; set; }
    }
}