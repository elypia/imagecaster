using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// A single exporting color.
    /// </summary>
    public class ColorConfig
    {
        /// <summary>
        /// The name of this color.
        /// </summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }
        
        /// <summary>
        /// The prefix to prepend to any images when modulated to this color.
        /// </summary>
        [YamlMember(Alias = "prefix")]
        public string Prefix { get; set; }
        
        /// <summary>
        /// The modulation to apply to the image to achieve this color.
        /// </summary>
        /// <see cref="http://www.imagemagick.org/Usage/color_mods/#modulate_hue"/>
        [YamlMember(Alias = "modulate")]
        public string Modulate { get; set; }
    }
}