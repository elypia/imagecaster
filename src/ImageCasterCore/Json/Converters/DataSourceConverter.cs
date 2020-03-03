using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterCore.Configuration;
using NLog;

namespace ImageCasterCore.Json.Converters
{
    public class DataSourceConverter : JsonConverter<DataSource>
    {
        /// <summary>Logging with NLog.</summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public override DataSource Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value.Contains(DataSource.DataSourceDelimeter))
            {
                string[] split = value.Split(DataSource.DataSourceDelimeter);
                Logger.Debug("Receiving source where collector is specified: {0}", split[0]);
                return new DataSource(split[0], split[1]);
            }
            
            return new DataSource("file", value);
        }

        public override void Write(Utf8JsonWriter writer, DataSource value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
