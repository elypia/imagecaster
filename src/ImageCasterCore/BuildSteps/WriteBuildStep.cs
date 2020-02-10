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
            this.Config = config?.Export?.Sizes;
            return Config != null;        
        }

        public void Execute(PipelineContext context, IMagickImage magickImage)
        {
            string inputFileName = context.ResolvedFile.FileInfo.Name;

            string dir = Path.Combine(context.Path);
            string outputFileName = String.Join("", context.Prefix) + Path.GetFileNameWithoutExtension(inputFileName) + String.Join("", context.Suffix) + Path.GetExtension(inputFileName);
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