using System.CommandLine;
using System.CommandLine.Invocation;
using ImageCasterCli.Api;
using ImageCasterCore.Actions;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using NLog;

namespace ImageCasterCli.Commands
{
    public class MontageCommand : MontageAction, ICliCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public MontageCommand(ICollector collector, ImageCasterConfig config) : base(collector, config)
        {
            
        }
        
        public Command Configure()
        {
            return new Command("montage", "Export a single image comprised of all matching output images")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }
    }
}
