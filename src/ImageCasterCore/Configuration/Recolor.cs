using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Configuration for the mask to use if required, and a list
    /// of all colors the image should be exported as with hue alterations.
    /// </summary>
    public class Recolor
    {
        /// <summary>The optional mask to use when modifying images.</summary>
        [JsonPropertyName("mask")]
        public Mask Mask { get; set; }

        /// <summary>Should the original color be exported? True by default.</summary>
        [JsonPropertyName("original")]
        public bool Original { get; set; } = true;

        /// <summary>A list of all colors to export the image as.</summary>
        [Required(ErrorMessage = "Must specify at least one modulation if color exists.")]
        [JsonPropertyName("modulation")]
        public List<Modulation> Modulation { get; set; }
    }
}
