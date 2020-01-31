using System.Collections.Generic;
using System.ComponentModel;
using ImageMagick;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Define the units and a list of dimenions
    /// to export the image as.
    /// </summary>
    public class Resize
    {
        /// <summary>The Filter to use when resizing images.</summary>
        public FilterType Filter { get; set; }

        /// <summary>The unit all dimensions are relative to.</summary>
        public UnitInfo Units { get; set; }
        
        /// <summary>The different dimensions required to export.</summary>
        public List<Dimensions> Dimensions { get; set; }
    }
}
