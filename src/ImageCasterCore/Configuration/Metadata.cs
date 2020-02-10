namespace ImageCasterCore.Configuration
{
    /// <summary>Add metadata to all exported images.</summary>
    public class Metadata
    {
        /// <summary>
        /// Define Exif (Exchangable Image Format) data to add to images.
        /// <see href="https://en.wikipedia.org/wiki/Exif">Exif on Wikipedia</see>
        /// </summary>
        public ExifConfig Exif { get; set; }
        
        /// <summary>
        /// Define IPTC data to add to images.
        /// </summary>
        public ExifConfig Iptc { get; set; }
    }
}