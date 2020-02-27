using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageCasterApi.Json
{
    /// <summary>
    /// This is not intended to fulfil the JSON Schema spec nor be
    /// backward compatible with any version of the spec.
    ///
    /// This is due to be removed once .NET Core has official
    /// support for JSON Schemas.
    /// </summary>
    /// <see href="https://github.com/dotnet/runtime/issues/29887"/>
    public class JsonSchemaProperty
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; }
        
        [JsonPropertyName("default")]
        public object Default { get; set; }

        [JsonPropertyName("minimum")]
        public int? Minimum { get; set; }

        [JsonPropertyName("maximum")]
        public int? Maximum { get; set; }

        [JsonPropertyName("enum")]
        public IEnumerable<string> Enum { get; set; }

        [JsonPropertyName("items")]
        public JsonSchemaProperty Items { get; set; }

        [JsonPropertyName("properties")]
        public Dictionary<string, JsonSchemaProperty> Properties { get; set; }
    }
}
