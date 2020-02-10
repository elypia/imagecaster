using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ImageCasterCore.Api;
using ImageCasterCore.Extensions;
using NLog;

namespace ImageCasterCore.Collectors
{
    /// <summary>
    /// An <see cref="ICollector"/> which matches files relative
    /// to the location the application was run.
    /// </summary>
    public class RegexCollector : ICollector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public List<ResolvedFile> Collect(string pattern)
        {
            pattern.RequireNonNull("A pattern is required to match files.");
            Regex regex = new Regex(pattern);
            
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            Logger.Debug("Collecting from directory with {0} files.", files.Length);

            List<ResolvedFile> resolvedFiles = new List<ResolvedFile>();
            Logger.Debug("Checking which of the files match the regex: /{0}/.", regex);
            
            foreach (FileInfo file in files)
            {
                string filename = file.FullName;
                Match match = regex.Match(filename);

                if (!match.Success)
                    continue;

                GroupCollection groups = match.Groups;
                List<string> tokens = new List<string>();
                
                for (int i = 1; i < groups .Count; i++)
                    tokens.Add(groups [i].Value);
                
                ResolvedFile resolvedFile = new ResolvedFile(file, pattern, tokens);
                resolvedFiles.Add(resolvedFile);
                Logger.Debug("Resolved {0} with tokens: {1}", resolvedFile.FileInfo, String.Join(", ", resolvedFile.Tokens));
            }

            return resolvedFiles;
        }

        public FileInfo Resolve(ResolvedFile resolvedFile, string pattern)
        {
            resolvedFile.RequireNonNull();
            string resolution = pattern.RequireNonNull();
            string[] tokens = resolvedFile.Tokens;
            
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                string token = tokens[i];
                resolution = resolution.Replace("$" + (i + 1), token);
            }

            Logger.Trace("Resolved file: {0}", resolution);
            return new FileInfo(resolution);
        }
    }
}
