using System.Text.RegularExpressions;
using ImageCaster.Api;

namespace ImageCaster.Configuration.Checkers
{
    public class NamingConventionConfig
    {
        /// <summary>Pattern for a <see cref="ICollector"/> to use to find files.</summary>
        public string Source { get; set; }
        
        /// <summary>Regular expression to validate filename.</summary>
        public Regex Pattern { get; set; }
    }
}