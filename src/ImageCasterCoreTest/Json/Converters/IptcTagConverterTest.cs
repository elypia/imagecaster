using System;
using System.Text.Json;
using ImageCasterCore.Json.Converters;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest.Json.Converters
{
    public class IptcTagConverterTest
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            Converters =
            {
                new IptcTagConverter()
            }
        };
        
        [Fact]
        public void TestReadingExifTag()
        {
            const IptcTag expected = IptcTag.Source;
            IptcTag actual = JsonSerializer.Deserialize<IptcTag>("\"Source\"", Options);

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestReadingInvalidExifTag()
        {
            Assert.ThrowsAny<Exception>(() => JsonSerializer.Deserialize<IptcTag>("\"INVALID\"", Options));
        }
    }
}
