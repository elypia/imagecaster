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
        private const string DefaultConfigFileName = "imagecaster.yml";

        /// <summary>The default file location of the configuration.</summary>
        private static readonly string DefaultConfigPath = Path.Combine(".", DefaultConfigFileName);
        
        [YamlMember(Alias = "export")]
        public Export Export { get; set; }

        [YamlMember(Alias = "montages")] 
        public List<PatternConfig> Montages { get; set; }

        [YamlMember(Alias = "checks")]
        public List<CheckConfig> Checks { get; set; }

        /// <summary>Overload of <see cref="Load"/> that loads a file.</summary>
        /// <param name="path">The path to the file which represents the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig LoadFromFile(string path = DefaultConfigFileName)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return Load(reader);
            }
        }
        
        /// <summary>Overload of <see cref="Load"/> that loads a string.</summary>
        /// <param name="config">The literal string to use to load the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig LoadFromString(string config)
        {
            using (StringReader reader = new StringReader(config))
            {
                return Load(reader);
            }
        }

        /// <summary>Load the configuration from the provided <see cref="TextReader"/>.</summary>
        /// <param name="reader">The text reader to read the string from for the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig Load(TextReader reader)
        {
            reader.RequireNonNull();
            IDeserializer deserializer = new DeserializerBuilder()
                .WithTypeConverter(new CheckInfoConverter())
                .WithTypeConverter(new FileInfoConverter())
                .WithTypeConverter(new PercentageConverter())
                .WithTypeConverter(new RegexConverter())
                .WithTypeConverter(new UnitInfoConverter())
                .Build();

            return deserializer.Deserialize<ImageCasterConfig>(reader);
        }
    }
}