using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Export configuration, the ultimately builds the source
    /// imags into the desired output.
    /// </summary>
    public class Build
    {
        /// <summary>A glob matching all images to export.</summary>
        [Required(ErrorMessage = "Must specify a pattern to discover input files.")]
        [JsonPropertyName("input")]
        public string[] Input { get; set; }
        
        /// <summary>Define metadata like Exif, Itcp, or XML to add to images.</summary>
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
        
        /// <summary>A list of desired resolutions to export the image at.</summary>
        [JsonPropertyName("resize")]
        public Resize Resize { get; set; }
        
        /// <summary>A list of desired colors to export the image as.</summary>
        [JsonPropertyName("colors")]
        public Colors Colors { get; set; }
    }
}
