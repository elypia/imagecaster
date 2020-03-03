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
        
        [HttpPost("build")]
        public int Build([FromBody] ImageCasterConfig config)
        {
            BuildAction build = new BuildAction(config);
            return build.Execute();
        }
        
        [HttpPost("montage")]
        public int Montage([FromBody] ImageCasterConfig config)
        {
            MontageAction montage = new MontageAction(config);
            return montage.Execute();
        }
        
        [HttpPost("archive")]
        public int Archive([FromBody] ImageCasterConfig config)
        {
            ArchiveAction archive = new ArchiveAction(config);
            return archive.Execute();
        }
        
        [HttpPost("check")]
        public int Check([FromBody] ImageCasterConfig config)
        {
            CheckAction check = new CheckAction(config.Checks);
            return check.Execute();
        }
    }
}
