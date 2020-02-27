using System.ComponentModel.DataAnnotations;
using ImageCasterApi.Models.Data;

namespace ImageCasterApi.Models.Request
{
    /// <summary>Data object to map formdata from request.</summary>
    public class FilterPreviewModel
    {
        /// <summary>The image itself to resize.</summary>
        [Required]
        public FrontendFile Image { get; set; }
        
        /// <summary>The geometry to set the image size to.</summary>
        [Required]
        [RegularExpression(@"(?:x\d+|\d+x?)(?:\d+|%)?[!<>^@]?")]
        public string Geometry { get; set; }
    }
}
