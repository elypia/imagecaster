using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ImageCasterCli.Commands;
using ImageCasterCli.Middleware;
using ImageCasterCore.Api;
using ImageCasterCore.Collectors;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageCasterCore.Json.Converters;
using NLog;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
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
                config = LoadFromFile();
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
        
        /// <summary>The default file name for the configuration.</summary>
        private const string DefaultConfigFileName = "imagecaster.yml";
        
        /// <summary>Overload of <see cref="Load"/> that loads a file.</summary>
        /// <param name="path">The path to the file which represents the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig LoadFromFile(string path = DefaultConfigFileName)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                Logger.Fatal("No configuration file found or specified.");
            }
            
            using (StreamReader reader = new StreamReader(path))
            {
                return Load(reader);
            }
        }
        
        /// <summary>Overload of <see cref="Load"/> that loads a string.</summary>
        /// <param name="config">The literal string to use to load the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig LoadFromString(string config)
        {
            using (StringReader reader = new StringReader(config))
            {
                return Load(reader);
            }
        }

        /// <summary>Load the configuration from the provided <see cref="TextReader"/>.</summary>
        /// <param name="reader">The text reader to read the string from for the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig Load(TextReader reader)
        {
            reader.RequireNonNull();
            
            IDeserializer deserializer = new DeserializerBuilder().Build();
            object yamlObject = deserializer.Deserialize(reader);
            
            ISerializer serializer = new SerializerBuilder().JsonCompatible().Build();
            string json = serializer.Serialize(yamlObject);
            
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Converters =
                {
                    new ExifTagConverter(),
                    new FileInfoConverter(),
                    new FilterTypeConverter(),
                    new GeometryConverter(),
                    new IptcTagConverter(),
                    new PercentageConverter(),
                    new RegexConverter()
                }
            };

            return JsonSerializer.Deserialize<ImageCasterConfig>(json, options);
        }
    }
}
