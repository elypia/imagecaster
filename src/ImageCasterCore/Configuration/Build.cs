using System.Collections.Generic;
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
        /// <summary>A list of all data sources.</summary>
        [Required(ErrorMessage = "Must specify at least one data source.")]
        [JsonPropertyName("input")]
        public List<DataSource> Input { get; set; }
        
        /// <summary>Define metadata like Exif, Itcp, or XML to add to images.</summary>
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
        
        /// <summary>A list of desired resolutions to export the image at.</summary>
        [JsonPropertyName("resize")]
        public Resize Resize { get; set; }
        
        /// <summary>A list of desired colors to export the image as.</summary>
        [JsonPropertyName("recolor")]
        public Recolor Recolor { get; set; }
    }
}
