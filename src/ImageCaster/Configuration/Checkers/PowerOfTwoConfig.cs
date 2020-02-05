using System.ComponentModel.DataAnnotations;
using ImageCaster.Checkers;

namespace ImageCaster.Configuration.Checkers
{
    /// <summary>Configuration for the <see cref="PowerOfTwoChecker"/>.</summary>
    public class PowerOfTwoConfig
    {
        /// <summary>A pattern matching all images that must adhere to this rule.</summary>
        [Required(ErrorMessage = "Must specify a source pattern to discover images.")]
        public string Source { get; set; }
    }
}
