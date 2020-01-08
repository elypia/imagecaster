using System;
using System.Collections.Generic;
using System.IO;
using ImageCaster.Interfaces;

namespace ImageCaster.Collectors
{
    public class GlobCollector : ICollector
    {
        public List<ResolvedFile> Collect(string pattern)
        {
            throw new NotImplementedException();
        }
        
        public FileInfo Find(ResolvedFile resolvedFile, string target)
        {
            throw new NotImplementedException();
        }
    }
}
