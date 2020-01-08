using System;
using System.Collections.Generic;
using System.IO;
using ImageCaster.Interfaces;
using ImageCaster.Utilities;
using NLog;

namespace ImageCaster.Collectors
{
    /// <summary>
    /// An <see cref="ICollector"/> which collects all files recursively
    /// from a directory.
    /// </summary>
    public class DirectoryCollector : ICollector
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public List<ResolvedFile> Collect(string pattern)
        {
            pattern.RequireNonNull("A path is required to collect files from the directory.");
            DirectoryInfo dir = new DirectoryInfo(pattern);

            if (!dir.Exists)
                throw new ArgumentException("The directory specified doesn't exist.");

            FileInfo[] files = dir.GetFiles();

            List<ResolvedFile> resolvedFiles = new List<ResolvedFile>();

            foreach (FileInfo file in files)
            {
                ResolvedFile resolvedFile = new ResolvedFile(file, pattern, file.Name);
                resolvedFiles.Add(resolvedFile);
            }

            return resolvedFiles;
        }

        public FileInfo Find(ResolvedFile file, string target)
        {
            string name = file.FileInfo.Name;
            string path = Path.Combine(target, name);
            return new FileInfo(path);
        }
    }
}