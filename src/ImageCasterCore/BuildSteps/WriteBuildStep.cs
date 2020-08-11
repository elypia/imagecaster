using System;
using System.IO;
using System.Linq;
using ImageCasterCore.Api;
using ImageMagick;
using NLog;

namespace ImageCasterCore.BuildSteps
{
    public class WriteBuildStep : IBuildStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Execute(PipelineContext context, MagickImage magickImage)
        {
            string inputFileName = context.ResolvedData.Name ?? context.ResolvedData.Data.GetHashCode().ToString();

            string dir = Path.Combine(context.Path.ToArray());

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