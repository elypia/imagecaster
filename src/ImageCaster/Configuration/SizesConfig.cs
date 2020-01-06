using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Define the units and a list of dimenions
    /// to export the image as.
    /// </summary>
    public class SizesConfig
    {
        /// <summary>
        /// The unit all dimensions are relative to.
        /// </summary>
        [YamlMember(Alias = "units")]
        public UnitInfo Units { get; set; }
        
        /// <summary>
        /// The different dimensions required to export.
        /// </summary>
        [YamlMember(Alias = "dimensions")]
        public List<Dimensions> Dimensions { get; set; }
    }
}
