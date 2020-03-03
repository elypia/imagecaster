using System.Text.Json.Serialization;

namespace ImageCasterCore.Configuration
{
    /// <summary>
    /// A way for ImageCaster to obtain data to be used
    /// during the build.
    /// </summary>
    public class DataSource
    {
        public const char DataSourceDelimeter = ':';
        
        /// <summary>The collector to use to obtain this source.</summary>
        [JsonPropertyName("collector")]
        public string Collector { get; set; }

        /// <summary>The data, or location to obtain the data.</summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }

        public DataSource()
        {
            // Do nothing
        }

        public DataSource(string collector, string source)
        {
            Collector = collector;
            Source = source;
        }

        public override string ToString()
        {
            return Collector + DataSourceDelimeter + Source;
        }
    }
}
