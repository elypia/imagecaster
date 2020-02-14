using ImageCasterCore.Actions;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    [Route("action")]
    public class ActionController : ControllerBase
    {
        private readonly ILogger<ActionController> _logger;

        public ActionController(ILogger<ActionController> logger)
        {
            _logger = logger;
        }
        
        [HttpPost("check")]
        public int Check()
        {
            CheckAction check = new CheckAction(new RegexCollector(), ImageCasterConfig.LoadFromFile().Checks);
            return check.Execute();
        }
        
        [HttpPost("export")]
        public int Export()
        {
            BuildAction build = new BuildAction(new RegexCollector(), ImageCasterConfig.LoadFromFile());
            return build.Execute();
        }
        
        [HttpPost("montage")]
        public int Montage()
        {
            MontageAction montage = new MontageAction(new RegexCollector(), ImageCasterConfig.LoadFromFile());
            return montage.Execute();
        }
        
        [HttpPost("archive")]
        public int Archive()
        {
            ArchiveAction archive = new ArchiveAction(ImageCasterConfig.LoadFromFile());
            return archive.Execute();
        }
    }
}
