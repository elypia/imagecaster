using ImageCasterCore.Configuration;
using Xunit;

namespace ImageCasterCoreTest.Configuration
{
    public class ImageCasterConfigTest
    {
        [Fact]
        public void ReadExportConfig()
        {
            const string yaml = "{export: {input: src/emote(.+)\\.png$, metadata: {exif: {tags: [{tag: OwnerName, value: 'Elypia CIC'}]}}, sizes: {dimensions: [512, 256]}, colors: {mask: src/emote$1.mask.png, modulate: [{name: blue, hue: 0}]}}}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const string expected = "src/emote(.+)\\.png$";    
            string actual = config.Export.Input;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadCheckConfig()
        {
            const string yaml = "{checks: {file-exists: [{source: src/emote(.+)\\.ora$, patterns: [src/emote$1.png]}]}}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const string expected = "src/emote(.+)\\.ora$";
            string actual = config.Checks.FileExists[0].Source;
            
            Assert.Equal(expected, actual);
        }
    }
}
