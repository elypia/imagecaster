using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using NLog;

namespace ImageCasterCli.Middleware
{
    /// <summary>Parse global ImageCaster arguments.</summary>
    public static class ImageCasterMiddleware
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static CommandLineBuilder UseImageCaster(this CommandLineBuilder commandLineBuilder)
        {
            commandLineBuilder.AddOption(new Option(new[] {"--config", "-c"}, "Define the configration in part or in whole through arguments")
            {
                Argument = new Argument<string>("config")
            });
            
            commandLineBuilder.AddOption(new Option(new[] {"--file", "-f"}, "Change where ImageCaster should find the configuration file")
            {
                Argument = new Argument<FileInfo>("file", () => new FileInfo("imagecaster.yml")).ExistingOnly()
            });
            
            commandLineBuilder.AddOption(new Option(new[] {"--output", "-o"}, "Change where ImageCaster should output created files")
            {
                Argument = new Argument<DirectoryInfo>("output", () => new DirectoryInfo(Directory.GetCurrentDirectory())).ExistingOnly()
            });
    
            commandLineBuilder.UseMiddleware(async (context, next) =>
            {
                CommandResult rootCommandResult = context.ParseResult.RootCommandResult;

                try
                {
                    FileInfo file = rootCommandResult.ValueForOption<FileInfo>("-f");
                    Logger.Debug("Executed ImageCaster using configuration file at: {0}", file.FullName);
                    await next(context);
                }
                catch (InvalidOperationException ex)
                {
                    Logger.Fatal(ex.Message);
                    Environment.Exit((int)ExitCode.NoConfig);
                }
            });

            return commandLineBuilder;
        }
    }
}