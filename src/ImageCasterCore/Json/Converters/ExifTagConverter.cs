using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterCore.Utilities;
using ImageMagick;

namespace ImageCasterCore.Json.Converters
{
    public class ExifTagConverter : JsonConverter<ExifTag>
    {
        public override ExifTag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            ExifTag tag = ExifUtils.FindByName(value);
            return (tag != null) ? tag : throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, ExifTag value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
