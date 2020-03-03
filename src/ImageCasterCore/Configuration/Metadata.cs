using System.Collections.Generic;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>Add metadata to all exported images.</summary>
    public class Metadata
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
        
        /// <summary>
        /// Define Exif (Exchangable Image Format) data to add to images.
        /// <see href="https://en.wikipedia.org/wiki/Exif">Exif on Wikipedia</see>
        /// </summary>
        [JsonPropertyName("exif")]
        public List<TagConfig<ExifTag>> Exif { get; set; }
        
        /// <summary>Define IPTC data to add to images.</summary>
        [JsonPropertyName("iptc")]
        public List<TagConfig<IptcTag>> Iptc { get; set; }
    }
}
