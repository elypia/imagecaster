using System.ComponentModel.DataAnnotations;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Export configuration, the ultimately builds the source
    /// imags into the desired output.
    /// </summary>
    public class Export
    {
        /// <summary>A glob matching all images to export.</summary>
        [Required(ErrorMessage = "Must specify a pattern to discover input files.")]
        public string Input { get; set; }
        
        /// <summary>Define metadata like Exif, Itcp, or XML to add to images.</summary>
        public Metadata Metadata { get; set; }
        
        /// <summary>A list of desired resolutions to export the image at.</summary>
        public Resize Sizes { get; set; }
        
        /// <summary>A list of desired colors to export the image as.</summary>
        public Colors Colors { get; set; }
    }
}
