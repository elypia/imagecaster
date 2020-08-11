using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        
        private static readonly Func<object, MagickReadSettings, MagickImage> ToMagickImage = (o, settings) => new MagickImage((FileInfo)o, settings);

        public List<ResolvedData> Collect(string data)
        {
            data.RequireNonNull("A path is required to collect files from the directory.");

            if (Directory.Exists(data))
            {
                DirectoryInfo dir = new DirectoryInfo(data);
                FileInfo[] files = dir.GetFiles();

                if (files.Length == 0)
                {
                    throw new FileNotFoundException("No files found in the specified directory.");
                }
                
                return Collect(data, files);
            }
            else if (File.Exists(data))
            {
                FileInfo[] files = { new FileInfo(data) };
                return Collect(data, files);
            }
            else
            {
                string[] paths = Directory.GetDirectories(".", data, SearchOption.AllDirectories);

                if (paths.Length == 0)
                {
                    throw new FileNotFoundException("No files match the specified search pattern.");
                }
                
                FileInfo[] files = paths.Select((path) => new FileInfo(path)).ToArray();
                return Collect(data, files);
            }
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
