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
        /// <summary>
        /// The steps in this pipeline.
        /// </summary>
        public List<IBuildStep> Pipeline { get; }

        public ResolvedFile ResolvedFile { get; }
        
        /// <summary>
        /// Append directories to the path the images will be exported to.
        /// For example a build step that exports in many colors, may want to create
        /// a folder per color.
        /// <code>build/export/red/@128px/emoteHappy.png</code>
        /// </summary>
        public string[] Path { get; set; }

        /// <summary>
        /// Add a prefix to the filenames.
        /// For example a build step that exports in many colors, may want to
        /// add the color name in front of each image. 
        /// <code>remoteHappy.png</code>
        /// </summary>
        public string[] Prefix { get; set; }

        /// <summary>
        /// Add a suffix to the filenames.
        /// For example a build step that exports in many sizes, may want to
        /// add the filesize at the end of each image.
        /// <code>emoteHappy@128px.png</code>
        /// </summary>
        public string[] Suffix { get; set; }

        /// <summary>
        /// The step of the pipeline this context is currently on.
        /// </summary>
        private int Step { get; set; }
        
        public PipelineContext(List<IBuildStep> pipeline, ResolvedFile resolvedFile, string path = "build")
        {
            path.RequireNonNull();

            this.Pipeline = pipeline.RequireNonNull();
            this.ResolvedFile = resolvedFile.RequireNonNull();
            Path = new[] {path};
            Prefix = new string[0];
            Suffix = new string[0];
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
            this.Path = this.Path.Concat(path).ToArray();
        }

        public void AppendPrefix(params string[] prefix)
        {
            this.Prefix = this.Prefix.Concat(prefix).ToArray();
        }
        
        public void AppendSuffix(params string[] suffx)
        {
            this.Suffix = this.Suffix.Concat(suffx).ToArray();
        }
        
        public PipelineContext Clone()
        {
            return new PipelineContext(Pipeline, ResolvedFile)
            {
                Path = Path,
                Prefix = Prefix,
                Suffix = Suffix,
                Step = Step
            };
        }
    }
}
