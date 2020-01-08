using System.Collections.Generic;
using System.IO;
using ImageCaster.Converters;
using ImageCaster.Utilities;
using YamlDotNet.Serialization;

namespace ImageCaster.Configuration
{
    public class ImageCasterConfig
    {
        /// <summary>The default file name for the configuration.</summary>
        private static readonly string DefaultConfigFileName = "imagecaster.yml";

        /// <summary>The default file location of the configuration.</summary>
        private static readonly string DefaultConfigPath = Path.Combine(".", DefaultConfigFileName);
        
        [YamlMember(Alias = "export")]
        public Export Export { get; set; }

        [YamlMember(Alias = "montages")] 
        public List<PatternConfig> Montages { get; set; }

        [YamlMember(Alias = "checks")]
        public List<CheckConfig> Checks { get; set; }

        public static ImageCasterConfig Load(string config)
        {
            config.RequireNonNull();
            IDeserializer deserializer = new DeserializerBuilder()
                .WithTypeConverter(new CheckInfoConverter())
                .WithTypeConverter(new FileInfoConverter())
                .WithTypeConverter(new RegexConverter())
                .WithTypeConverter(new UnitInfoConverter())
                .Build();
            
            return deserializer.Deserialize<ImageCasterConfig>(config);
        }
    }
}