using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ImageCasterCore.Extensions;
using ImageMagick;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// A generic key value pair configuration based on the type parameter.
    /// </summary>
    public class TagConfig<T>
    {
        [JsonPropertyName("tag")]
        public T Tag { get; set; }
        
        /// <summary>The value to assign to this Exif tag.</summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        public TagConfig()
        {
            // Do nothing
        }

        public TagConfig(T tag, string value = null)
        {
            this.Tag = tag.RequireNonNull();
            this.Value = value;
        }
    }
}
