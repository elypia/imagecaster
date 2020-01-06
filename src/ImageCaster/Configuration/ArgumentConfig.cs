using NLog;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    public class ArgumentConfig
    {
        /// <summary>NLog logger.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>The name of this argument.</summary>
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        /// <summary>The value to assign to this argument.</summary>
        [YamlMember(Alias = "value")]
        public string Value { get; set; }
    }
}
