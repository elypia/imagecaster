using System.IO;
using ImageCasterCore;
using ImageCasterCore.Collectors;
using Xunit;

namespace ImageCasterCoreTest.Collectors
{
    public class DirectoryCollectorTest
    {
        [Fact]
        public void TestDirectoryCollectorResolvesFile()
        {
            FileInfo[] fileInfo = { new FileInfo("src/emoteHappy.png") };

            FileCollector collector = new FileCollector();
            ResolvedData resolvedData = collector.Collect("src/emote(.+).png", fileInfo)[0];

            const string expected = "emoteHappy.png";
            string actual = resolvedData.Name;
            
            Assert.Equal(expected, actual);
        }
    }
}
