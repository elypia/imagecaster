using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// A single exporting color.
    /// </summary>
    /// <see cref="http://www.imagemagick.org/Usage/color_mods/#modulate_hue"/>
    public class Modulation
    {
        /// <summary>The name of this color.</summary>
        [Required(ErrorMessage = "Must specify a name for all modulations.")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        /// <summary>The prefix to prepend to any images when modulated to this color.</summary>
        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        /// <summary>The brightness percentage to modify the color by.</summary>
        [JsonPropertyName("brightness")]
        public Percentage Brightness { get; set; } = new Percentage(100);

        /// <summary>The saturation percentage to modify the color by.</summary>
        [JsonPropertyName("saturation")]
        public Percentage Saturation { get; set; } = new Percentage(100);

        /// <summary>The hue percentage to modify the color by.</summary>
        [JsonPropertyName("hue")]
        public Percentage Hue { get; set; } = new Percentage(100);
    }
}