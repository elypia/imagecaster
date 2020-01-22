using System.IO;
using ImageCaster;
using Xunit;

namespace ImageCasterTest
{
    public class ResolvedFileTest
    {
        [Fact]
        public void TestToString()
        {
            FileInfo fileInfo = new FileInfo("src/emoteHappy.png");
            ResolvedFile resolvedFile = new ResolvedFile(fileInfo, "src/emote(.+).png", "Happy");

            const string expected = "src/emoteHappy.png resolved from src/emote(.+).png with tokens: Happy";
            string actual = resolvedFile.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
