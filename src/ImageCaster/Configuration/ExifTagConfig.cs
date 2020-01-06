using ImageMagick;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// An EXIF (Exchangable Image Format) key:value pair to set while
    /// exporting images.
    /// </summary>
    public class ExifTagConfig
    {
        /// <summary>
        /// The name of the valid EXIF tag according to the 2.31 standard.
        /// </summary>
        [YamlMember(Alias = "tag")]
        public string Tag { get; set; }
        
        /// <summary>
        /// The value to assign to this EXIF tag.
        /// </summary>
        [YamlMember(Alias = "value")]
        public string Value { get; set; }
    }
}