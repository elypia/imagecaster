using System.Collections.Generic;

namespace ImageCaster.Configuration.Checkers
{
    public class FileExistsConfig
    {
        public string Source { get; set; }
        public List<string> Patterns { get; set; }
    }
}