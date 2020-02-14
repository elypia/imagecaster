using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace ImageCasterApi.Models
{
    /// <summary>Date object to map formdata from request.</summary>
    public class ResizePreviewModel
    {
        /// <summary>The filter to use when resizing images.</summary>
        public FilterType Filter { get; set; }
        
        /// <summary>The geometry to set the image size to.</summary>
        public string Geometry { get; set; }
        
        /// <summary>The image itself to resize.</summary>
        public IFormFile Image { get; set; }
    }
}
