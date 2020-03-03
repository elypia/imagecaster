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
                int index = value.IndexOf(DataSource.DataSourceDelimeter);
                string collector = value.Substring(0, index);
                string data = value.Substring(index + 1);
                Logger.Debug("Receiving source where collector is specified: {0}", collector);
                return new DataSource(collector, data);
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
