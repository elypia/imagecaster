using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ImageCasterCore.Json.Converters;

namespace ImageCasterCore.Extensions
{
    public static class JsonSerializerOptionsExtensions
    {
        /// <summary>
        /// Register all ImageCaster defined <see cref="JsonConverter"/>.
        /// </summary>
        /// <param name="o">The <see cref="JsonSerializerOptions"/> to add the converters to.</param>
        public static void AddImageCasterConverters(this JsonSerializerOptions o)
        {
            IList<JsonConverter> converters = o.Converters;
            
            JsonConverter[] newConverters =
            {
                new DataSourceConverter(),
                new ExifTagConverter(),
                new FileInfoConverter(),
                new FilterTypeConverter(),
                new GeometryConverter(),
                new IptcTagConverter(),
                new PercentageConverter(),
                new RegexConverter()
            };
            
            foreach (JsonConverter converter in newConverters)
            {
                converters.Add(converter);
            }
        }
    }
}
