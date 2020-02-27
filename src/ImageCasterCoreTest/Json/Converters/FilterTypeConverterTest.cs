using System;
using System.Text.Json;
using ImageCasterCore.Json.Converters;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest.Json.Converters
{
    public class FilterTypeConverterTest
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            Converters =
            {
                new FilterTypeConverter()
            }
        };
        
        [Fact]
        public void TestReadingFilterType()
        {
            const FilterType expected = FilterType.Catrom;
            FilterType actual = JsonSerializer.Deserialize<FilterType>("\"Catrom\"", Options);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestReadingDefaultFilterType()
        {
            const FilterType expected = 0;
            FilterType actual = JsonSerializer.Deserialize<FilterType>("\"Default\"", Options);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestReadingInvalidFilter()
        {
            Assert.ThrowsAny<Exception>(() => JsonSerializer.Deserialize<FilterType>("\"INVALID\"", Options));
        }
    }
}
