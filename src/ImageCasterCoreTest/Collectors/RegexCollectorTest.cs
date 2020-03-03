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
            FileInfo[] fileInfo = { new FileInfo("src/emoteHappy.png") };
            
            RegexCollector collector = new RegexCollector();
            ResolvedData resolvedData = collector.Collect("src/emote(.+).png", fileInfo)[0];
            
            const string expected = "emoteHappy.png";
            string actual = resolvedData.Name;
            
            Assert.Equal(expected, actual);
        }
    }
}
