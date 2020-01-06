using System.Runtime.Serialization;

namespace ImageCaster.Configuration
{
    /// <summary>
    /// Compatible units for resizing images.
    /// </summary>
    public enum Unit
    {
        /// <summary>
        /// Set the number of units the ouput must be.
        /// </summary>
        Pixel,
        
        /// <summary>
        /// Set the percentage to resize the image by, relative
        /// to the input.
        /// </summary>
        Percentage
    }
}