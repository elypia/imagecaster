using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Threading.Tasks;
using ImageCasterCli.Middleware;
using ImageCasterCore.Actions;
using ImageCasterCore.Configuration;
using NLog;

namespace ImageCasterCli
{
    public class ImageCasterCli
    {
        /// <summary>Instance of the NLog logger for this class.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        /// <summary>
        /// Intialize ImageCaster commands, parse arguments,
        /// and process the command the user input.
        ///
        /// This method also times the time taken to perform
        /// the command to help optimizations in development
        /// or user configurations.
        ///
        /// We postpone logging until after calling InvokeAsync so that
        /// any user defined logging configurations (ie from CLI arguments)
        /// have already been set.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> Main(string[] args)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            AppDomain.CurrentDomain.ProcessExit += (s, e) =>
            {
                stopWatch.Stop();
                Logger.Debug("Finished running after: {0}", stopWatch.Elapsed);
            };
            
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images")
            {               
                new Command("archive", "Archive collections of images or files into compressed archives")
                {
                    Handler = CommandHandler.Create(() =>
                    {
                        ImageCasterConfig config = ConfigurationFile.LoadFromFile();
                        return new ArchiveAction(config).Execute();
                    })
                },
                new Command("build", "Export the output images from the source")
                {
                    Handler = CommandHandler.Create(() =>
                    {
                        ImageCasterConfig config = ConfigurationFile.LoadFromFile();
                        return new BuildAction(config).Execute();
                    })
                },
                new Command("check", "Validate that the project structure and standards are maintained")
                {
                    Handler = CommandHandler.Create(() =>
                    {
                        ImageCasterConfig config = ConfigurationFile.LoadFromFile();
                        return new CheckAction(config.Checks).Execute();
                    })
                },
                new Command("montage", "Export a single image comprised of all matching output images")
                {
                    Handler = CommandHandler.Create(() =>
                    {
                        ImageCasterConfig config = ConfigurationFile.LoadFromFile();
                        return new MontageAction(config).Execute();
                    })
                }
            };

            command.Name = "imagecaster";
            
            CommandLineBuilder commandLineBuilder = new CommandLineBuilder(command)
            {
                EnableDirectives = false
            };
            
            // Default middlewares we want to add to our console application.
            commandLineBuilder.UseVersionOption().UseHelp().UseParseErrorReporting().CancelOnProcessTermination();
            
            // These are our own defined middlewares.
            commandLineBuilder.UseLogger().UseLicense().UseImageCaster();

            Parser parser = commandLineBuilder.Build();
            return await parser.InvokeAsync(args);
        }
    }
}
