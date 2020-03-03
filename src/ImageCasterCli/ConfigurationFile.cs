using System;
using System.IO;
using System.Text.Json;
using ImageCasterCore.Configuration;
using ImageCasterCore.Extensions;
using YamlDotNet.Serialization;

namespace ImageCasterCli
{
    public class ConfigurationFile
    {
        /// <summary>The default file name for the configuration.</summary>
        private const string DefaultConfigFileName = "imagecaster.yml";
        
        /// <summary>Overload of <see cref="Load"/> that loads a file.</summary>
        /// <param name="path">The path to the file which represents the configuration.</param>
        /// <returns>A data object that represents the configuration passed.</returns>
        public static ImageCasterConfig LoadFromFile(string path = DefaultConfigFileName)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (!fileInfo.Exists)
            {
                throw new Exception();
            }
            
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
            
            IDeserializer deserializer = new DeserializerBuilder().Build();
            object yamlObject = deserializer.Deserialize(reader);
            
            ISerializer serializer = new SerializerBuilder().JsonCompatible().Build();
            string json = serializer.Serialize(yamlObject);
            
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.AddImageCasterConverters();

            ImageCasterConfig config = JsonSerializer.Deserialize<ImageCasterConfig>(json, options);
            return config;
        }
    }
}
