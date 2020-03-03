using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ImageCasterCore.Checkers;

namespace ImageCasterCore.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="ResolutionMatchesChecker"/>.</summary>
    public class ResolutionMatchesConfig
    {
        /// <summary>The source pattern to discover images.</summary>
        [Required(ErrorMessage = "Must specify a source pattern to discover images.")]
        [JsonPropertyName("source")]
        public DataSource Source { get; set; }

        /// <summary>The target pattern to discover images to compare to.</summary>
        [Required(ErrorMessage = "Must specify a target source to compare against.")]
        [JsonPropertyName("target")]
        public DataSource Target { get; set; }

        /// <summary>A pattern to match names to compare against, or null if to use the file name.</summary>
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; }
    }
}
