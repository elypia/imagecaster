using System.Collections.Generic;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>Add metadata to all exported images.</summary>
    public class Metadata
    {
        /// <summary>
        /// Define Exif (Exchangable Image Format) data to add to images.
        /// <see href="https://en.wikipedia.org/wiki/Exif">Exif on Wikipedia</see>
        /// </summary>
        [JsonPropertyName("exif")]
        public ExifConfig Exif { get; set; }
        
        /// <summary>Define IPTC data to add to images.</summary>
        [JsonPropertyName("iptc")]
        public List<TagConfig<IptcTag>> Iptc { get; set; }
    }
}
