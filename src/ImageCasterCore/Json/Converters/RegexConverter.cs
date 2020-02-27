using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace ImageCasterCore.Json.Converters
{
    public class RegexConverter : JsonConverter<Regex>
    {
        public override Regex Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            return new Regex(value);
        }

        public override void Write(Utf8JsonWriter writer, Regex value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
