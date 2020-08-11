using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ImageCasterApi.Json
{
    /// <inheritdoc cref="JsonSchemaProperty"/>
    public class JsonSchema : JsonSchemaProperty
    {
        [Required]
        [JsonPropertyName("$schema")]
        public string Schema { get; set; }

        [Required]
        [JsonPropertyName("$id")]
        public string Id { get; set; }
    }
}
