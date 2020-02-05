using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Configuration for the mask to use if required, and a list
    /// of all colors the image should be exported as with hue alterations.
    /// </summary>
    public class Colors
    {
        /// <summary>The optional mask to use when modifying images.</summary>
        public string Mask { get; set; }
        
        /// <summary>A list of all colors to export the image as.</summary>
        [Required(ErrorMessage = "Must specify at least one modulation if color exists.")]
        public List<Modulate> Modulate { get; set; }
    }
}
