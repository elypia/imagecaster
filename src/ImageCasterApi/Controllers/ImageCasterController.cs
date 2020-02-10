using ImageCasterCore.Actions;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace ImageCasterApi.Controllers
{
    [ApiController]
    public class ImageCasterController : ControllerBase
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();

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