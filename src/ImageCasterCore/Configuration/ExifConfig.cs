using System.Collections.Generic;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    public class ExifConfig
    {
        /// <summary>If ImageCaster should overwrite values if they already exist.</summary>
        [JsonPropertyName("write-mode")]
        public WriteMode WriteMode { get; set; } = WriteMode.Overwrite;

        /// <summary>
        /// If ImageCaster should use it's internal default values.
        /// If set to true, you can still change values by overriding them
        /// in your configuration, or remove them entirely by setting them to null.
        /// </summary>
        [JsonPropertyName("defaults")]
        public bool Defaults { get; set; } = true;
        
        /// <summary>A list of Exif metadata to put on exported images.</summary>
        [JsonPropertyName("tags")]
        public List<TagConfig<ExifTag>> Tags { get; set; }
    }
}
