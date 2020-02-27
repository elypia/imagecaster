using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ImageCasterCore.Checkers;
using ImageCasterCore.Collectors;

namespace ImageCasterCore.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="FileExistsChecker"/>.</summary>
    public class FileExistsConfig
    {
        /// <summary>
        /// A pattern to find initial files with pattern grouping.
        /// For example if using the <see cref="RegexCollector"/> the source could be: /src/(.+?)\.png/
        /// The brackets are stored as a variable and can be used by the target list.
        /// This would allow for you to ensure `src/$1.ora` existed if a source is found that matches.
        /// </summary>
        [Required(ErrorMessage = "Must specify a source pattern to discover files.")]
        [JsonPropertyName("source")]
        public string Source { get; set; }
        
        /// <summary>
        /// The target patterns, these patterns must resolve to existing files if a source is found.
        /// </summary>
        [Required(ErrorMessage = "Must specify target patterns to cross-check files.")]
        [JsonPropertyName("patterns")]
        public List<string> Patterns { get; set; }
    }
}
