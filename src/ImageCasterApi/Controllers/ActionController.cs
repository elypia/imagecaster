using ImageCasterCore.Actions;
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
            CheckAction check = new CheckAction(new ImageCasterConfig().Checks);
            return check.Execute();
        }
        
        [HttpPost("build")]
        public int Build()
        {
            BuildAction build = new BuildAction(new ImageCasterConfig());
            return build.Execute();
        }
        
        [HttpPost("montage")]
        public int Montage()
        {
            MontageAction montage = new MontageAction(new ImageCasterConfig());
            return montage.Execute();
        }
        
        [HttpPost("archive")]
        public int Archive()
        {
            ArchiveAction archive = new ArchiveAction(new ImageCasterConfig());
            return archive.Execute();
        }
    }
}
