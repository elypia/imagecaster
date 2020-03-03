using System.IO;
using ImageCasterCore;
using ImageCasterCore.Collectors;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest
{
    public class ResolvedFileTest
    {
        [Fact]
        public void TestToString()
        {
            FileInfo fileInfo = new FileInfo("src/emoteHappy.png");
            ResolvedData resolvedData = new ResolvedData(fileInfo, "src/emote(.+).png", (o, s) => new MagickImage((FileInfo)o, s), "emoteHappy.png", null, "Happy");

            const string expected = "src/emoteHappy.png resolved from src/emote(.+).png with tokens: Happy";
            string actual = resolvedData.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
