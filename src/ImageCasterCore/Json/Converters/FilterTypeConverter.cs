using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageMagick;

namespace ImageCasterCore.Json.Converters
{
    public class FilterTypeConverter : JsonConverter<FilterType>
    {
        /// <summary>The string that represents the default enum value.</summary>
        private const string DefaultFilter = "Default";
        
        public override FilterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value == DefaultFilter)
            {
                return default;
            }
            
            FilterType type = Enum.Parse<FilterType>(value, true);
            return type;
        }

        public override void Write(Utf8JsonWriter writer, FilterType value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}