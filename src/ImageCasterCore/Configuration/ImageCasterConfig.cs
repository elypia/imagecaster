using System.Collections.Generic;
using System.Text.Json.Serialization;
using NLog;

namespace ImageCasterCore.Configuration
{
    public class ImageCasterConfig
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// A set of variables that can be referenced in other parts of the
        /// configuration, namely areas using the <see cref="StringInterpolator"/>.
        /// </summary>
        [JsonPropertyName("variables")]
        public Dictionary<string, string> Variables { get; set; }
        
        [JsonPropertyName("build")]
        public Build Build { get; set; }

        [JsonPropertyName("montages")]
        public MontageConfig Montages { get; set; }
        
        [JsonPropertyName("archives")]
        public List<Archive> Archives { get; set; }
        
        [JsonPropertyName("checks")]
        public Checks Checks { get; set; }
    }
}