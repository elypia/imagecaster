using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Select a pattern of files.
    /// </summary>
    public class PatternConfig
    {
        /// <summary>
        /// The name of this collection of files.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The pattern pattern to match all required files.
        /// </summary>
        public string Pattern { get; set; }
    }
}
