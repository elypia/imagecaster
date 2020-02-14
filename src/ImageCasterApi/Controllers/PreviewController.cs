using ImageCasterApi.Models;
using ImageMagick;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    [Route("preview")]
    public class PreviewController : ControllerBase
    {
        private readonly ILogger<PreviewController> _logger;

        public PreviewController(ILogger<PreviewController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost("resize")]
        public IActionResult ResizePreview([FromForm] ResizePreviewModel model)
        {
            FilterType filter = model.Filter;
            string geometry = model.Geometry;
            
            _logger.LogTrace("HTTP request received at preview/resize with filter of {0}, and geometery of {1}.", filter, geometry);
            
            MagickGeometry magickGeometry = new MagickGeometry(geometry);
            _logger.LogInformation("User specified geometry translates too: {0}", magickGeometry);

            using (MagickImage magickImage = new MagickImage(model.Image.OpenReadStream()))
            {
                magickImage.FilterType = filter;
                magickImage.Resize(magickGeometry);
                return File(magickImage.ToByteArray(), magickImage.FormatInfo.MimeType);
            }
        }
    }
}
