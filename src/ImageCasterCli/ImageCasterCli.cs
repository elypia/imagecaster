using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using ImageCasterCli.Commands;
using ImageCasterCli.Middleware;
using ImageCasterCore.Api;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using NLog;
using YamlDotNet.Core;
using Parser = System.CommandLine.Parsing.Parser;

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
                Logger.Info("Finished running after: {0}", stopWatch.Elapsed);
            };
            
            ICollector collector = new RegexCollector();
            ImageCasterConfig config;

            try
            {
                config = ImageCasterConfig.LoadFromFile();
            }
            catch (YamlException ex)
            {
                Exception innerEx = ex.InnerException;
                
                if (innerEx.GetType() == typeof(ValidationException))
                {
                    Logger.Fatal("Configuration is malformed: {0}", innerEx.Message);
                    return (int)ExitCode.MalformedConfigFields;
                }

                throw;
            }
            
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images")
            {               
                new ArchiveCommand(config).Configure(),
                new BuildCommand(collector, config).Configure(),
                new CheckCommand(collector, config.Checks).Configure(),
                new MontageCommand(collector, config).Configure(),
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
