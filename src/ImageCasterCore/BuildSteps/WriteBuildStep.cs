using System;
using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    public class WriteBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Resize Config { get; set; }

        public bool Configure(ICollector collector, ImageCasterConfig config)
        {
            this.Config = config?.Build?.Resize;
            return Config != null;        
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            string inputFileName = context.ResolvedFile.FileInfo.Name;

            string dir = Path.Combine(context.Path);

            string prefix = String.Join(String.Empty, context.Prefix);
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputFileName);
            string suffix = String.Join(String.Empty, context.Suffix);
            string ext = Path.GetExtension(inputFileName);
            string outputFileName = prefix + filenameWithoutExt + suffix + ext;
                
            string path = Path.Combine(dir, outputFileName);
            
            FileInfo output = new FileInfo(path);

            DirectoryInfo directoryInfo = output.Directory;

            if (directoryInfo == null)
            {
                Logger.Fatal("The file location to write an image didn't have a parent directory.");
                return;
            }

            directoryInfo.Create();

            magickImage.Write(output);
            Logger.Debug("Written file to: {0}", output);

            context.Next(magickImage);
        }
    }
}