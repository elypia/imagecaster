using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ImageCasterCore.Api;
using ImageCasterCore.Extensions;
using ImageMagick;
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
        
        private static readonly Func<object, MagickReadSettings, IMagickImage> ToMagickImage = (o, settings) => new MagickImage((FileInfo)o, settings);
        
        public List<ResolvedData> Collect(string data)
        {
            data.RequireNonNull("A pattern is required to match files.");
            DirectoryInfo dir = new DirectoryInfo(".");
            FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
            return Collect(data, files);
        }

        public List<ResolvedData> Collect(string data, FileInfo[] files)
        {
            Regex regex = new Regex(data);
            Logger.Debug("Collecting from directory with {0} files.", files.Length);

            List<ResolvedData> resolvedFiles = new List<ResolvedData>();
            Logger.Debug("Checking which of the files match the regex: /{0}/.", regex);
            
            foreach (FileInfo file in files)
            {
                string filename = file.FullName;
                Match match = regex.Match(filename);

                if (!match.Success)
                    continue;

                GroupCollection groups = match.Groups;
                List<string> tokens = new List<string>();
                
                for (int i = 0; i < groups .Count; i++)
                    tokens.Add(groups [i].Value);
                
                ResolvedData resolvedData = new ResolvedData(file, data, ToMagickImage, file.Name, null, tokens.ToArray());
                resolvedFiles.Add(resolvedData);
                Logger.Debug("Resolved {0} with tokens: {1}", resolvedData.Data, String.Join(", ", resolvedData.Tokens));
            }

            return resolvedFiles;
        }
    }
}
