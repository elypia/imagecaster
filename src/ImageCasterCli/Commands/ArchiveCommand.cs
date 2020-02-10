using System.CommandLine;
using System.CommandLine.Invocation;
using ImageCasterCli.Api;
using ImageCasterCore.Actions;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using NLog;

namespace ImageCasterCli.Commands
{
    public class ArchiveCommand : ArchiveAction, ICliCommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public ArchiveCommand(ImageCasterConfig config) : base(config)
        {
            
        }
        
        public Command Configure()
        {
            return new Command("archive", "Archive collections of images or files into compressed archives")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }
    }
}
