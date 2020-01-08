using System.ComponentModel;
using ImageMagick;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// A single exporting color.
    /// </summary>
    /// <see cref="http://www.imagemagick.org/Usage/color_mods/#modulate_hue"/>
    public class Modulate
    {
        /// <summary>The name of this color.</summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }
        
        /// <summary>The prefix to prepend to any images when modulated to this color.</summary>
        [YamlMember(Alias = "prefix")]
        public string Prefix { get; set; }
        
        /// <summary>The brightness percentage to modify the color by.</summary>
        [YamlMember(Alias = "brightness")]
        [DefaultValue(100)]
        public Percentage Brightness { get; set; }
        
        /// <summary>The saturation percentage to modify the color by.</summary>
        [YamlMember(Alias = "saturation")]
        [DefaultValue(100)]
        public Percentage Saturation { get; set; }
        
        /// <summary>The hue percentage to modify the color by.</summary>
        [YamlMember(Alias = "hue")]
        [DefaultValue(100)]
        public Percentage Hue { get; set; }
    }
}