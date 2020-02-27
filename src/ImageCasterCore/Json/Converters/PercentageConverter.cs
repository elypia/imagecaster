using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Json.Converters
{
    public class PercentageConverter : JsonConverter<Percentage>
    {
        public override Percentage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    string stringValue = reader.GetString();
                    double value = Double.Parse(stringValue);
                    return new Percentage(value);
                case JsonTokenType.Number:
                    double doubleValue = reader.GetDouble();
                    return new Percentage(doubleValue);
                default:
                    throw new JsonException();
            }
        }

        public override void Write(Utf8JsonWriter writer, Percentage value, JsonSerializerOptions options)
        {
            double serialized = value.ToDouble();
            writer.WriteNumberValue(serialized);
        }
    }
}
