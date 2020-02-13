using ImageCasterCore.Actions;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    public class PreviewController : ControllerBase
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}