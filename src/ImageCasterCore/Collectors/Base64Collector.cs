using System.Collections.Generic;
using System.IO;
using ImageCasterCore.Api;

namespace ImageCasterCore.Collectors
{
    public class Base64Collector : ICollector
    {
        public List<ResolvedFile> Collect(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public FileInfo Resolve(ResolvedFile resolvedFile, string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}