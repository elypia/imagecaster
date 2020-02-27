using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using ImageCasterCore.Api;
using ImageCasterCore.Checkers;

namespace ImageCasterCore.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="NamingConventionChecker"/>.</summary>
    public class NamingConventionConfig
    {
        /// <summary>Pattern for a <see cref="ICollector"/> to use to find files.</summary>
        [Required(ErrorMessage = "Must specify source pattern to discover images.")]
        [JsonPropertyName("source")]
        public string Source { get; set; }
        
        /// <summary>Regular expression to validate filename.</summary>
        [Required(ErrorMessage = "Must specify naming convension in the form of a regular expression.")]
        [JsonPropertyName("pattern")]
        public Regex Pattern { get; set; }
    }
}
