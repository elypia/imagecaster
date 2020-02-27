using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ImageCasterCore.Json.Converters
{
    public class FileInfoConverter : JsonConverter<FileInfo>
    {
        public override FileInfo Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return new FileInfo(value);
        }

        public override void Write(Utf8JsonWriter writer, FileInfo value, JsonSerializerOptions options)
        {
            string serialized = value.FullName;
            writer.WriteStringValue(serialized);
        }
    }
}
