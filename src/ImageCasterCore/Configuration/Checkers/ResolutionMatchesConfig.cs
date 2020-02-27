using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="ResolutionMatchesChecker"/>.</summary>
    public class ResolutionMatchesConfig
    {
        /// <summary>The source pattern to discover images.</summary>
        [Required(ErrorMessage = "Must specify a source pattern to discover images.")]
        [JsonPropertyName("source")]
        public string Source { get; set; }
        
        /// <summary>The target pattern to compare the source pattern and groups against.</summary>
        [Required(ErrorMessage = "Must specify a target pattern to match images against.")]
        [JsonPropertyName("target")]
        public string Target { get; set; }
    }
}
