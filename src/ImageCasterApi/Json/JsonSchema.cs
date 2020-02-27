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
    public class JsonSchema : JsonSchemaProperty
    {
        [JsonPropertyName("$schema")]
        public string Schema { get; set; }

        [JsonPropertyName("$id")]
        public string Id { get; set; }
    }
}