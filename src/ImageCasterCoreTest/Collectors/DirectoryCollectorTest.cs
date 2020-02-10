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
            FileInfo fileInfo = new FileInfo("src/emoteHappy.png");
            ResolvedFile resolvedFile = new ResolvedFile(fileInfo, "src/", "emoteHappy.png");

            DirectoryCollector collector = new DirectoryCollector();

            string expected = new FileInfo("src/masks/emoteHappy.png").Name;
            string actual = collector.Resolve(resolvedFile, "src/masks/").Name;
            
            Assert.Equal(expected, actual);
        }
    }
}
