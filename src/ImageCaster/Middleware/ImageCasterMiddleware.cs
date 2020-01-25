using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using NLog;

namespace ImageCaster.Middleware
{
    public static class ImageCasterMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static CommandLineBuilder UseImageCaster(this CommandLineBuilder commandLineBuilder)
        {
            commandLineBuilder.AddOption(new Option(new[] {"--collector", "-c"}, "How to find and collect files")
            {
                Argument = new Argument<string>("collector", () => "regex")
            });
                
            commandLineBuilder.AddOption(new Option(new[] {"--file", "-f"}, "Change where ImageCaster should find the configuration file")
            {
                Argument = new Argument<FileInfo>("file", () => new FileInfo("imagecaster.yml")).ExistingOnly()
            });
               
            commandLineBuilder.UseMiddleware(async (context, next) =>
            {
                CommandResult rootCommandResult = context.ParseResult.RootCommandResult;
                FileInfo file = rootCommandResult.ValueForOption<FileInfo>("-f");
                await next(context);
            });

            return commandLineBuilder;
        }
    }
}