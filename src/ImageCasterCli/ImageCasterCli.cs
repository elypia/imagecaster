using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ImageCasterCli.Middleware;
using ImageCasterCore.Actions;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageCasterCore.Exceptions;
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
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>The exit code.</returns>
        public static async Task<int> Main(string[] args)
        {           
            RootCommand command = new RootCommand("Perform aggregate tasks against a collection of images")
            {               
                new Command("archive", "Archive collections of images or files into compressed archives")
                {
                    Handler = CommandHandler.Create<FileInfo>((file) =>
                    {
                        try
                        {
                            ImageCasterConfig config = ConfigurationFile.LoadFromFile(file.FullName);
                            IAction action = new ArchiveAction(config);

                            try
                            {
                                action.Execute();
                            }
                            catch (ConfigurationException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.MalformedConfigFields;
                            }
                            catch (FileNotFoundException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.MalformedConfigFields;
                            }
                            catch (Exception ex)
                            {
                                return OnInternalException(ex);
                            }
                        }
                        catch (JsonException ex)
                        {
                            return OnJsonException(ex);
                        }

                        return (int)ExitCode.Normal;
                    })
                },
                new Command("build", "Export the output images from the source")
                {
                    Handler = CommandHandler.Create<FileInfo>((file) =>
                    {
                        try
                        {
                            ImageCasterConfig config = ConfigurationFile.LoadFromFile(file.FullName);
                            IAction action = new BuildAction(config);

                            try
                            {
                                action.Execute();
                            }
                            catch (ConfigurationException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.MalformedConfigFields;
                            }
                            catch (Exception ex)
                            {
                                return OnInternalException(ex);
                            }
                        
                            return (int)ExitCode.Normal;
                        }
                        catch (JsonException ex)
                        {
                            return OnJsonException(ex);
                        }
                    })
                },
                new Command("check", "Validate that the project structure and standards are maintained")
                {
                    Handler = CommandHandler.Create<FileInfo>((file) =>
                    {
                        try
                        {
                            ImageCasterConfig config = ConfigurationFile.LoadFromFile(file.FullName);
                            IAction action = new CheckAction(config.Checks);

                            try
                            {
                                action.Execute();
                            }
                            catch (ConfigurationException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.MalformedConfigFields;
                            }
                            catch (ValidationException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.CheckFailures;
                            }
                            catch (Exception ex)
                            {
                                return OnInternalException(ex);
                            }
                        }
                        catch (JsonException ex)
                        {
                            return OnJsonException(ex);
                        }
                        
                        return (int)ExitCode.Normal;
                    })
                },
                new Command("montage", "Export a single image comprised of all matching output images")
                {
                    Handler = CommandHandler.Create<FileInfo>((file) =>
                    {
                        try
                        {
                            ImageCasterConfig config = ConfigurationFile.LoadFromFile(file.FullName);
                            IAction action = new MontageAction(config);
                        
                            try
                            {
                                action.Execute();
                            }
                            catch (ConfigurationException ex)
                            {
                                Logger.Error(ex.Message);
                                return (int)ExitCode.MalformedConfigFields;
                            }
                            catch (Exception ex)
                            {
                                return OnInternalException(ex);
                            }
                        }
                        catch (JsonException ex)
                        {
                            return OnJsonException(ex);
                        }
                        
                        return (int)ExitCode.Normal;
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
            commandLineBuilder.UseLogger().UseTimer().UseLicense().UseImageCaster();

            Parser parser = commandLineBuilder.Build();
            return await parser.InvokeAsync(args);
        }

        /// <summary>
        /// Generic method to call when an internal exception occurs.
        /// </summary>
        /// <param name="ex">The internal exception that occured.</param>
        /// <returns>The exit code for an internal exception.</returns>
        private static int OnInternalException(Exception ex)
        {
            Logger.Error(ex);
            return (int)ExitCode.InternalError;
        }
        
        /// <summary>
        /// Generic method to call when the JSON configuration
        /// provided to ImageCaster is invalid.
        /// </summary>
        /// <param name="ex">The exception that occured.</param>
        /// <returns>The exit code for an invalid configuration.</returns>
        private static int OnJsonException(JsonException ex)
        {
            Logger.Error("The configuration file is malformed. Please check the documentation and verify your configuration is valid.");
            Logger.Error("The JSON value for {0} could not be converted propertly.", ex.Path);
            return (int)ExitCode.MalformedConfigFields;
        }
    }
}
