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
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    string stringValue = reader.GetString();
                    return new MagickGeometry(stringValue);
                case JsonTokenType.Number:
                    int intValue = reader.GetInt32();
                    string asString = intValue.ToString();
                    return new MagickGeometry(asString);
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, MagickGeometry value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
