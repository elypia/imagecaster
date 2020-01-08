using System.Collections.Generic;
using NLog;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    public class CheckConfig
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [YamlMember(Alias = "name")]
        public CheckInfo Name { get; set; }
        
        [YamlMember(Alias = "args")]
        public Dictionary<string, string> Args { get; set; }
    }
}
