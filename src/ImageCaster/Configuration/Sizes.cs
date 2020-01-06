using System.Collections.Generic;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Define the units and a list of dimenions
    /// to export the image as.
    /// </summary>
    public class Sizes
    {
        /// <summary>
        /// The unit all dimensions are relative to.
        /// </summary>
        public Unit Units { get; set; }
        
        /// <summary>
        /// The different dimensions required to export.
        /// </summary>
        public List<Dimensions> Dimensions { get; set; }
    }
}
