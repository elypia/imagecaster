using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
        [JsonPropertyName("filter")]
        public FilterType Filter { get; set; }

        /// <summary>
        /// The different dimensions required to export.
        /// See the page on ImageMagick for more information on the syntax for sizes for simple
        /// and advance usage.
        /// </summary>
        /// <see href="http://www.imagemagick.org/Usage/resize/#resize"/>
        [Required(ErrorMessage = "Must specify at least one geometry if resize is configured.")]
        [JsonPropertyName("geometries")]
        public List<MagickGeometry> Geometries { get; set; }
    }
}
