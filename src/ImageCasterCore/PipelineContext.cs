using System.Collections.Generic;
using System.Linq;
using ImageCasterCore.Api;
using ImageCasterCore.Extensions;
using ImageMagick;

namespace ImageCasterCore
{
    /// <summary>
    /// <see cref="BuildSteps"/> can set or append data here so that
    /// the end writer can have more context on where to write files.
    /// </summary>
    public class PipelineContext
    {
        /// <summary>Resolve input or additional data needed for the build.</summary>
        public DataResolver DataResolver { get; }
        
        /// <summary>
        /// The steps in this pipeline.
        /// </summary>
        public List<IBuildStep> Pipeline { get; }

        public ResolvedData ResolvedData { get; }
        
        /// <summary>
        /// Append directories to the path the images will be exported to.
        /// For example a build step that exports in many colors, may want to create
        /// a folder per color.
        /// <code>build/export/red/@128px/emoteHappy.png</code>
        /// </summary>
        public IEnumerable<string> Path { get; set; }

        /// <summary>
        /// Add a prefix to the filenames.
        /// For example a build step that exports in many colors, may want to
        /// add the color name in front of each image. 
        /// <code>remoteHappy.png</code>
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Add a suffix to the filenames.
        /// For example a build step that exports in many sizes, may want to
        /// add the filesize at the end of each image.
        /// <code>emoteHappy@128px.png</code>
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// The step of the pipeline this context is currently on.
        /// </summary>
        private int Step { get; set; }
        
        public PipelineContext(DataResolver resolver, List<IBuildStep> pipeline, ResolvedData resolvedData, string path = "build")
        {
            path.RequireNonNull();
            this.DataResolver = resolver.RequireNonNull();
            this.Pipeline = pipeline.RequireNonNull();
            this.ResolvedData = resolvedData.RequireNonNull();
            Path = new List<string>(){path};
            Step = 0;
        }

        public void Next(IMagickImage magickImage)
        {
            if (Step >= Pipeline.Count)
            {
                return;
            }

            IBuildStep step = Pipeline[Step++];
            step.Execute(this, magickImage);
        }
        
        public void AppendPath(params string[] path)
        {
            Path = this.Path.Concat(path);
        }

        public void AppendPrefix(string prefix)
        {
            this.Prefix = (this.Prefix == null) ? prefix : this.Prefix + prefix;
        }
        
        public void AppendSuffix(string suffix)
        {
            this.Suffix = (this.Suffix == null) ? suffix : this.Suffix + suffix;
        }
        
        public PipelineContext Clone()
        {
            return new PipelineContext(DataResolver, Pipeline, ResolvedData)
            {
                Path = new List<string>(this.Path),
                Prefix = this.Prefix,
                Suffix = this.Suffix,
                Step = this.Step
            };
        }
    }
}
