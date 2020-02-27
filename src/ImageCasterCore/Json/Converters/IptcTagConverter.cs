using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterCore.Utilities;
using ImageMagick;

namespace ImageCasterCore.Json.Converters
{
    public class IptcTagConverter : JsonConverter<IptcTag>
    {
        public override IptcTag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            IptcTag type = Enum.Parse<IptcTag>(value, true);
            return type;
        }

        public override void Write(Utf8JsonWriter writer, IptcTag value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}
