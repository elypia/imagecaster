using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// A set of dimensions for the output.
    /// The units for this are defined elsewhere,
    /// this only manages the dimensions of each axis
    /// relative to eachother regarldess of units.
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// The height of the output image.
        /// </summary>
        [YamlMember(Alias = "height")]
        public uint Height { get; set; }
        
        /// <summary>
        /// The width of the output image.
        /// </summary>
        [YamlMember(Alias = "width")]
        public uint Width { get; set; }
    }
}