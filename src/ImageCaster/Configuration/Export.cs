using System.Collections.Generic;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Export configuration, the ultimately builds the source
    /// imags into the desired output.
    /// </summary>
    public class Export
    {
        /// <summary>A glob matching all images to export.</summary>
        [YamlMember(Alias = "input")]
        public string Input { get; set; }
        
        /// <summary>A list of EXIF metadata to put on exported images.</summary>
        [YamlMember(Alias = "exif")]
        public List<ExifTagConfig> Exif { get; set; }
        
        /// <summary>A list of desired resolutions to export the image at.</summary>
        [YamlMember(Alias = "sizes")]
        public Resize Resize { get; set; }
        
        /// <summary>A list of desired colors to export the image as.</summary>
        [YamlMember(Alias = "color")]
        public ColorsConfig Colors { get; set; }
    }
}
