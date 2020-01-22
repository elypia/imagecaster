using System.CommandLine;
using System.CommandLine.Invocation;
using NLog;
using ICommand = ImageCaster.Api.ICommand;

namespace ImageCaster.Commands
{
    public class MontageCommand : ICommand
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public Command Configure()
        {
            return new Command("montage", "Export a single image comprised of all matching output images")
            {
                Handler = CommandHandler.Create(Execute)
            };
        }

        public int Execute()
        {
            Logger.Info("Montage hasn't been implemented yet.");
            return (int)ExitCode.Normal;
        }
    }
}
