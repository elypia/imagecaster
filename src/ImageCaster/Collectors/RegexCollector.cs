using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using ImageCaster.Interfaces;
using ImageCaster.Utilities;

namespace ImageCaster.Collectors
{
    /// <summary>
    /// An <see cref="ICollector"/> which matches files relative
    /// to the location the application was run.
    /// </summary>
    public class RegexCollector : ICollector
    {
        public List<ResolvedFile> Collect(string pattern)
        {
            pattern.RequireNonNull("A pattern is required to match files.");
            Regex regex = new Regex(pattern);
            
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);

            List<ResolvedFile> resolvedFiles = new List<ResolvedFile>();

            foreach (FileInfo file in files)
            {
                string filename = file.Name;
                Match match = regex.Match(filename);

                if (!match.Success)
                    continue;

                List<string> tokens = new List<string>();

                foreach (Group group in match.Groups)
                    tokens.Add(group.Value);
                
                ResolvedFile resolvedFile = new ResolvedFile(file, pattern, tokens);
                resolvedFiles.Add(resolvedFile);
            }

            return resolvedFiles;
        }

        public FileInfo Find(ResolvedFile resolvedFile, string target)
        {
            throw new NotImplementedException();
        }
    }
}
