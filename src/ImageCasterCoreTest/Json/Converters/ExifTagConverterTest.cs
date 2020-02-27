using System.Text.Json;
using ImageCasterCore.Json.Converters;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest.Json.Converters
{
    public class ExifTagConverterTest
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            Converters =
            {
                new ExifTagConverter()
            }
        };
        
        [Fact]
        public void TestReadingExifTag()
        {
            ExifTag expected = ExifTag.Artist;
            ExifTag actual = JsonSerializer.Deserialize<ExifTag>("\"Artist\"", Options);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestReadingInvalidExifTag()
        {
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<ExifTag>("\"INVALID\"", Options));
        }
    }
}
