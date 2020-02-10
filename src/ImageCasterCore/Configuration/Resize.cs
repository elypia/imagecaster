using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// Define the units and a list of dimenions
    /// to export the image as.
    /// </summary>
    public class Resize
    {
        /// <summary>The Filter to use when resizing images.</summary>
        public FilterType Filter { get; set; }

        /// <summary>
        /// The different dimensions required to export.
        /// See the page on ImageMagick for more information on the syntax for sizes for simple
        /// and advance usage.
        /// </summary>
        /// <see href="http://www.imagemagick.org/Usage/resize/#resize"/>
        [Required(ErrorMessage = "Must specify at least one set of dimensions if resize is configured.")]
        public List<MagickGeometry> Dimensions { get; set; }
    }
}
