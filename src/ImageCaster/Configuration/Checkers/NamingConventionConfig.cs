using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ImageCaster.Api;
using ImageCaster.Checkers;

namespace ImageCaster.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="NamingConventionChecker"/>.</summary>
    public class NamingConventionConfig
    {
        /// <summary>Pattern for a <see cref="ICollector"/> to use to find files.</summary>
        [Required(ErrorMessage = "Must specify source pattern to discover images.")]
        public string Source { get; set; }
        
        /// <summary>Regular expression to validate filename.</summary>
        [Required(ErrorMessage = "Must specify naming convension in the form of a regular expression.")]
        public Regex Pattern { get; set; }
    }
}
