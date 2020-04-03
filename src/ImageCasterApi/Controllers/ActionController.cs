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
        public void Build([FromBody] ImageCasterConfig config)
        {
            BuildAction build = new BuildAction(config);
            build.Execute();
        }
        
        [HttpPost("montage")]
        public void Montage([FromBody] ImageCasterConfig config)
        {
            MontageAction montage = new MontageAction(config);
            montage.Execute();
        }
        
        [HttpPost("archive")]
        public void Archive([FromBody] ImageCasterConfig config)
        {
            ArchiveAction archive = new ArchiveAction(config);
            archive.Execute();
        }
        
        [HttpPost("check")]
        public void Check([FromBody] ImageCasterConfig config)
        {
            CheckAction check = new CheckAction(config.Checks);
            check.Execute();
        }
    }
}
