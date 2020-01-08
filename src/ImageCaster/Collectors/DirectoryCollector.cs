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

        /// <summary>Find another file using tokens from the
        /// <param name="resolvedFile">The file that was initially resolved.</param>
        /// <param name="pattern">The pattern for the new file, using tokens from the previously resolved file.</param>
        /// <returns></returns>
        public FileInfo Resolve(ResolvedFile resolvedFile, string pattern)
        {
            string name = resolvedFile.Tokens[0];
            string path = Path.Combine(pattern, name);
            return new FileInfo(path);
        }
    }
}