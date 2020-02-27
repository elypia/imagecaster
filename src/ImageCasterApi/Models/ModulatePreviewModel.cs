using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ImageMagick;

namespace ImageCasterApi.Models
{
    /// <summary>
    /// Data object to get modulate data for preview.
    /// This doesn't include all modulate fields as some
    /// aren't required for the preview.
    /// </summary>
    public class ModulatePreviewModel
    {
        /// <summary>
        /// The default value for any of the percentages.
        /// We keep this once statically to avoid repetetively
        /// creating new objects that will all have the same value anyways.
        /// </summary>
        private static readonly Percentage DefaultPercentile = new Percentage(100);
        
        /// <summary>The image to modulate and preview.</summary>
        [Required]
        public FrontendFile Image { get; set; }

        /// <summary>The mask to determine what to modulate and what not to.</summary>
        public FrontendFile Mask { get; set; }

        /// <summary>The brightness change between 0 to 200%.</summary>
        public Percentage Brightness { get; set; } = DefaultPercentile;

        /// <summary>The saturation change between 0 to 200%.</summary>
        public Percentage Saturation { get; set; } = DefaultPercentile;

        /// <summary>The hue change between 0 to 200%.</summary>
        public Percentage Hue { get; set; } = DefaultPercentile;
    }
}
