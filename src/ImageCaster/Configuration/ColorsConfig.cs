using System.Collections.Generic;
using System.IO;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Configuration for the mask to use if required, and a list
    /// of all colors the image should be exported as with hue alterations.
    /// </summary>
    public class ColorsConfig
    {
        /// <summary>
        /// The optional mask to use when modifying images.
        /// </summary>
        public FileInfo Mask { get; set; }
        
        /// <summary>
        /// A list of all colors to export the image as.
        /// </summary>
        public List<ColorConfig> Colors { get; set; }
    }
}
