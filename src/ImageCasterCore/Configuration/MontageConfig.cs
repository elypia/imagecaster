using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Configuration
{
    /// <summary>Generic configuration for creating montages.</summary>
    public class MontageConfig
    {
        /// <summary>The font that should name each image.</summary>
        [JsonPropertyName("font-family")]
        public string Font { get; set; }

        /// <summary>All sources to export as montages.</summary>
        [JsonPropertyName("patterns")]
        public List<PatternConfig> Patterns { get; set; }
    }
}