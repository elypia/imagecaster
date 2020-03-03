using System;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterApi.Models.Data;
using ImageCasterCore.Collectors;

namespace ImageCasterApi.Json.Converters
{
    public class FrontendFileConverter : JsonConverter<FrontendFile>
    {       
        public override FrontendFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value.StartsWith(Base64Collector.DataPrefix))
            {
                string data = value.Substring(Base64Collector.DataPrefix.Length);
                string[] split = data.Split(Base64Collector.Base64Seperator);
                ContentType contentType = new ContentType(split[0]);
                byte[] bytes = Convert.FromBase64String(split[1]);
                return new FrontendFile(contentType, bytes);
            }
            else
            {
                byte[] bytes = Convert.FromBase64String(value);
                return new FrontendFile(bytes);
            }
        }

        public override void Write(Utf8JsonWriter writer, FrontendFile value, JsonSerializerOptions options)
        {
            string serialized = value.ToString();
            writer.WriteStringValue(serialized);
        }
    }
}