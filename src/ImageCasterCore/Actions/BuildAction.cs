using System;
using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.BuildSteps;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Actions
{
    /// <summary>
    /// Using the <see cref="Export"/> configuration to export the input
    /// in all desired ouput images.
    /// </summary>
    public class BuildAction : IAction
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The collector implementation in use, determines how files are found.</summary>
        public ICollector Collector { get; }
        
        /// <summary>The user defined configuration to export images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="collector"><see cref="Collector"/></param>
        /// <param name="config"><see cref="Config"/></param>
        public BuildAction(ICollector collector, ImageCasterConfig config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }

        /// <summary>Start building all images.</summary>
        /// <exception cref="InvalidOperationException">If the configuration is malformed.</exception>
        public int Execute()
        {
            Logger.Trace("Executed build command, started working.");

            Export export = Config.Export;

            if (export == null)
            {
                Logger.Warn("Build command was called, but no export configuration was defined, doing nothing.");
            }
            
            string input = export.Input;

            if (input == null)
            {
                Logger.Fatal("Build command was called but export.input configuration was not specified, this is required.");
            }
            
            List<ResolvedFile> resolvedFiles = Collector.Collect(input);
            Logger.Debug("Found {0} files matching collection pattern.", resolvedFiles.Count);

            List<IBuildStep> pipeline = new List<IBuildStep>()
            {
                new ExifBuildStep(),
                new ModulateBuildStep(),
                new ResizeBuildStep(),
                new WriteBuildStep()
            };
            
            foreach (IBuildStep step in pipeline)
            {
                step.Configure(Collector, Config);
            }

            foreach (ResolvedFile resolvedFile in resolvedFiles)
            {
                FileInfo fileInfo = resolvedFile.FileInfo;
                PipelineContext context = new PipelineContext(pipeline, resolvedFile);
                context.AppendPath("export");
            
                using (MagickImage magickImage = new MagickImage(fileInfo))
                {
                    context.Next(magickImage);
                }

                Logger.Info("Finished all exports for {0}.", resolvedFile);
            }

            return 0;
        }
    }
}
