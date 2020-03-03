using System;
using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Api;
using ImageCasterCore.Extensions;
using ImageMagick;
using NLog;

namespace ImageCasterCore.Collectors
{
    /// <summary>
    /// An <see cref="ICollector"/> which collects all files recursively
    /// from a directory.
    /// </summary>
    public class FileCollector : ICollector
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private static readonly Func<object, MagickReadSettings, IMagickImage> ToMagickImage = (o, settings) => new MagickImage((FileInfo)o, settings);

        public List<ResolvedData> Collect(string data)
        {
            data.RequireNonNull("A path is required to collect files from the directory.");
            DirectoryInfo dir = new DirectoryInfo(data);

            if (!dir.Exists)
            {
                throw new ArgumentException("The directory specified doesn't exist: " + data);
            }

            FileInfo[] files = dir.GetFiles();
            return Collect(data, files);
        }

        public List<ResolvedData> Collect(string data, FileInfo[] files)
        {
            List<ResolvedData> resolvedFiles = new List<ResolvedData>();

            foreach (FileInfo file in files)
            {
                string fileName = file.Name;
                
                string[] tokens =
                {
                    fileName,
                    Path.GetFileNameWithoutExtension(fileName),
                    Path.GetExtension(fileName)
                };
                
                ResolvedData resolvedData = new ResolvedData(file, data, ToMagickImage, file.Name, null, tokens);
                resolvedFiles.Add(resolvedData);
            }

            return resolvedFiles;
        }
    }
}
