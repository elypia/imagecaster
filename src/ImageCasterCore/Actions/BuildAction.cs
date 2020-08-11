using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImageCasterCore.Api;
using ImageCasterCore.BuildSteps;
using ImageCasterCore.Configuration;
using ImageCasterCore.Exceptions;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Actions
{
    /// <summary>
    /// Using the <see cref="Build"/> configuration to export the input
    /// in all desired ouput images.
    /// </summary>
    public class BuildAction : IAction
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The user defined configuration to export images.</summary>
        public ImageCasterConfig Config { get; }
        
        /// <param name="config"><see cref="Config"/></param>
        public BuildAction(ImageCasterConfig config)
        {
            this.Config = config.RequireNonNull();
        }

        /// <summary>Start building all images.</summary>
        /// <exception cref="InvalidOperationException">If the configuration is malformed.</exception>
        public void Execute()
        {
            Logger.Trace("Executed build command, started working.");

            Build build = Config.Build;

            if (build == null)
            {
                throw new ConfigurationException("Build command was called, but no build configuration was defined, doing nothing.");
            }

            List<DataSource> input = build.Input;

            if (input == null)
            {
                throw new ConfigurationException("Build command was called but build.input configuration was not specified, this is required.");
            }

            DataResolver resolver = new DataResolver(input);
            List<ResolvedData> resolvedDatas = new DataResolver(input).Data;
            Logger.Debug("Found {0} files matching collection pattern.", resolvedDatas.Count);

            List<IBuildStep> pipeline = new List<IBuildStep>();

            if (Config.Build.Metadata != null)
            {
                pipeline.Add(new ExifBuildStep(Config));
                pipeline.Add(new IptcBuildStep(Config));
            }

            if (Config.Build.Recolor != null)
            {
                if (Config.Build.Recolor.Mask != null)
                {
                    resolver.ResolveAdditional("masks", Config.Build.Recolor.Mask.Sources);
                }
                
                pipeline.Add(new RecolorBuildStep(Config));
            }

            if (Config.Build.Resize != null)
            {
                pipeline.Add(new ResizeBuildStep(Config));
            }
            
            pipeline.Add(new WriteBuildStep());
        
            Parallel.ForEach(resolvedDatas, (resolvedData) =>
            {
                PipelineContext context = new PipelineContext(resolver, pipeline, resolvedData);
                context.AppendPath("export");
        
                using (MagickImage magickImage = resolvedData.ToMagickImage())
                {
                    context.Next(magickImage);
                }

                Logger.Info("Finished all exports for {0}.", resolvedData);
            });
        }
    }
}
