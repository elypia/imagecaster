using System.ComponentModel.DataAnnotations;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Select a pattern of files.
    /// </summary>
    public class PatternConfig
    {
        /// <summary>The name of this collection of files.</summary>
        [Required(ErrorMessage = "Must specify a name for all montages.")]
        public string Name { get; set; }
        
        /// <summary>
        /// The pattern pattern to match all required files.
        /// </summary>
        [Required(ErrorMessage = "Must specify a pattern for all montages.")]
        public string Pattern { get; set; }
    }
}
