using System;
using System.IO.Compression;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Json.Converters
{
    public class CompressionLevelConverter : JsonConverter<CompressionLevel>
    {
        public override CompressionLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            CompressionLevel type = Enum.Parse<CompressionLevel>(value, true);
            return type;
        }

        public override void Write(Utf8JsonWriter writer, CompressionLevel value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}