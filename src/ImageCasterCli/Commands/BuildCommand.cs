using System.CommandLine;
using System.CommandLine.Invocation;
using ImageCasterCli.Api;
using ImageCasterCore.Actions;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using NLog;

namespace ImageCasterCli.Commands
{
    /// <summary>
    /// Using the <see cref="Export"/> configuration to export the input
    /// in all desired ouput images.
    /// </summary>
    public class BuildCommand : BuildAction, ICliCommand
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public BuildCommand(ICollector collector, ImageCasterConfig config) : base(collector, config)
        {
            
        }
        
        public Command Configure()
        {
            return new Command("build", "Export the output images from the source")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }
    }
}
