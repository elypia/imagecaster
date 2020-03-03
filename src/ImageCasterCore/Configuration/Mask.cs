using System.Collections.Generic;
using System.Text.Json.Serialization;
using ImageCasterCore.Api;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Configuration to seek all masks that can be used, and the pattern
    /// to select the appropriate mask to use for an image if it exists.
    /// </summary>
    public class Mask
    {
        /// <summary>
        /// A pattern using tokenized values from the <see cref="ICollector"/>s
        /// to select which of the sources have a mask.
        /// </summary>
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; }

        /// <summary>
        /// All locations to load or discover masks from.
        /// </summary>
        [JsonPropertyName("sources")]
        public List<DataSource> Sources { get; set; }
    }
}
