using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly ILogger<PreviewController> Logger;

        public PreviewController(ILogger<PreviewController> logger)
        {
            this.Logger = logger;
        }
        
        [HttpPost("preview/resize")]
        public IActionResult ResizePreview([FromQuery] string size, [FromBody] IFormFile image)
        {
            Logger.LogWarning("HTTP request made to preview/resize.");
            MagickGeometry geometry = new MagickGeometry(size);

            using (MagickImage magickImage = new MagickImage(image.OpenReadStream()))
            {
                magickImage.Resize(geometry);
                FileContentResult file = File(magickImage.ToByteArray(), "image/png");
                return file;
            }
        }
    }
}
