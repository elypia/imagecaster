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
    /// Performs any validation to ensure the project structure adheres
    /// to all standards, without actually exporting any images.
    /// </summary>
    public class CheckCommand : CheckAction, ICliCommand
    {
        /// <summary>The NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public CheckCommand(ICollector collector, Checks checks) : base(collector, checks)
        {
            
        }
        
        public Command Configure()
        {
            return new Command("check", "Validate that the project structure and standards are maintained")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }
    }
}
