using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Json.Converters
{
    public class GeometryConverter : JsonConverter<MagickGeometry>
    {
        public override MagickGeometry Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return new MagickGeometry(value);
        }

        public override void Write(Utf8JsonWriter writer, MagickGeometry value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
