using System.ComponentModel.DataAnnotations;
using ImageCasterCore.Extensions;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// An Exif (Exchangable Image Format) key:value pair to set while
    /// exporting images.
    /// </summary>
    public class TagConfig
    {
        /// <summary>The name of the valid Exif tag according to the 2.31 standard.</summary>
        [Required(ErrorMessage = "Must specify a valid Exif tag name.")]
        public ExifTag Tag { get; set; }
        
        /// <summary>The value to assign to this Exif tag.</summary>
        public string Value { get; set; }

        public TagConfig()
        {
            // Do nothing
        }

        public TagConfig(ExifTag tag, string value = null)
        {
            this.Tag = tag.RequireNonNull();
            this.Value = value;
        }
    }
}
