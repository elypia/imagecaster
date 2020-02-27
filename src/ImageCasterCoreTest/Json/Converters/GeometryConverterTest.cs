using System;
using System.Text.Json;
using ImageCasterCore.Json.Converters;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest.Json.Converters
{
    public class GeometryConverterTest
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            Converters =
            {
                new GeometryConverter()
            }
        };
        
        [Fact]
        public void TestReading()
        {
            MagickGeometry expected = new MagickGeometry(512);
            MagickGeometry actual = JsonSerializer.Deserialize<MagickGeometry>("\"512x512\"", Options);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestReadingInvalid()
        {
            Assert.ThrowsAny<Exception>(() => JsonSerializer.Deserialize<FilterType>("\"INVALID\"", Options));
        }
    }
}
