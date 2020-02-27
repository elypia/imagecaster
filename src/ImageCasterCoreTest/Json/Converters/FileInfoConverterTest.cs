using System.IO;
using System.Text.Json;
using ImageCasterCore.Json.Converters;
using Xunit;

namespace ImageCasterCoreTest.Json.Converters
{
    public class FileInfoConverterTest
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            Converters =
            {
                new FileInfoConverter()
            }
        };
        
        [Fact]
        public void TestReadingFileInfo()
        {
            FileInfo file = JsonSerializer.Deserialize<FileInfo>("\"hello/world.png\"", Options);

            const string expected = "hello/world.png";
            string actual = file.ToString();
            
            Assert.Equal(expected, actual);
        }
    }
}
