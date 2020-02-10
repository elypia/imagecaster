using System.IO;
using ImageCasterCore;
using ImageCasterCore.Collectors;
using Xunit;

namespace ImageCasterCoreTest.Collectors
{
    public class RegexCollectorTest
    {
        [Fact]
        public void TestRegexCollectorResolvesFile()
        {
            FileInfo fileInfo = new FileInfo("src/emoteHappy.png");
            ResolvedFile resolvedFile = new ResolvedFile(fileInfo, "src/emote(.+).png", "Happy");

            RegexCollector collector = new RegexCollector();

            string expected = new FileInfo("src/emoteHappy.mask.png").Name;
            string actual = collector.Resolve(resolvedFile, "src/emote$1.mask.png").Name;
            
            Assert.Equal(expected, actual);
        }
    }
}
