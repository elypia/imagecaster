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
            FileInfo fileInfo = new FileInfo("emotes/pandaAww.png");
            ResolvedFile resolvedFile = new ResolvedFile(fileInfo, "emotes/panda(.+).png", "Aww");

            const string expected = "emotes/pandaAww.png resolved from emotes/panda(.+).png with tokens: Aww";
            string actual = resolvedFile.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
