using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using ImageCaster.Api;
using ImageCaster.Collectors;
using ImageCaster.Commands;
using ImageCaster.Configuration;
using ImageCaster.Middleware;
using NLog;

namespace ImageCaster
{
    public class ImageCaster
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static async Task<int> Main(string[] args)
        {
            ICollector collector = new RegexCollector();
            ImageCasterConfig config = ImageCasterConfig.LoadFromFile();
            
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images")
            {               
                new BuildCommand(collector, config).Configure(),
                new CheckCommand(collector, config.Checks).Configure(),
                new MontageCommand().Configure(),
            };

            command.Name = "imagecaster";
            
            CommandLineBuilder commandLineBuilder = new CommandLineBuilder(command)
            {
                EnableDirectives = false
            };
            
            // Default middlewares we want to add to our console application.
            commandLineBuilder.UseVersionOption().UseHelp().UseTypoCorrections().UseParseErrorReporting().CancelOnProcessTermination();
            
            // These are our own defined middlewares.
            commandLineBuilder.UseLogger().UseLicense().UseImageCaster();

            Parser parser = commandLineBuilder.Build();
            return await parser.InvokeAsync(args);
        }
    }
}
