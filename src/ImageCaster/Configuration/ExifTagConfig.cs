using ImageMagick;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// An EXIF (Exchangable Image Format) key:value pair to set while
    /// exporting images.
    /// </summary>
    public class ExifTagConfig
    {
        public ExifTag Tag { get; set; }
        public string Value { get; set; }
    }
}