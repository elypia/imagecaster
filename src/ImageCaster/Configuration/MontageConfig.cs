using DotNet.Globbing;
using NLog;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    public class MontageConfig
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "glob")]
        public Glob Glob { get; set; }
    }
}