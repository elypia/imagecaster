using System.ComponentModel.DataAnnotations;
using ImageMagick;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// A single exporting color.
    /// </summary>
    /// <see cref="http://www.imagemagick.org/Usage/color_mods/#modulate_hue"/>
    public class Modulate
    {
        /// <summary>The name of this color.</summary>
        [Required(ErrorMessage = "Must specify a name for all modulations.")]
        public string Name { get; set; }
        
        /// <summary>The prefix to prepend to any images when modulated to this color.</summary>
        public string Prefix { get; set; }

        /// <summary>The brightness percentage to modify the color by.</summary>
        public Percentage Brightness { get; set; } = new Percentage(100);

        /// <summary>The saturation percentage to modify the color by.</summary>
        public Percentage Saturation { get; set; } = new Percentage(100);

        /// <summary>The hue percentage to modify the color by.</summary>
        public Percentage Hue { get; set; } = new Percentage(100);
    }
}