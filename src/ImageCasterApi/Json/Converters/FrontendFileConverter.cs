using System;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterApi.Models;

namespace ImageCasterApi.Json.Converters
{
    public class FrontendFileConverter : JsonConverter<FrontendFile>
    {
        private const string DataPrefix = "data:";
        private const string Base64Seperator = "base64,";
        
        public override FrontendFile Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (value.StartsWith(DataPrefix))
            {
                string data = value.Substring(DataPrefix.Length);
                string[] split = data.Split(Base64Seperator);
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
            StringBuilder builder = new StringBuilder();
            
            ContentType contentType = value.ContentType;

            if (contentType != null)
            {
                builder.Append(DataPrefix).Append(contentType).Append(Base64Seperator);
            }

            builder.Append(Convert.ToBase64String(value.Data));

            writer.WriteStringValue(builder.ToString());
        }
    }
}