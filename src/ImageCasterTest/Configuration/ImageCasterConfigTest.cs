using ImageCaster.Configuration;
using Xunit;

namespace ImageCasterTest
{
    public class ImageCasterConfigTest
    {
        [Fact]
        public void ReadExportConfig()
        {
            const string yaml = "{export: {input: 'src/static/panda(.+).png', exif: [{tag: Artist, value: 'Elypia CIC and Contributors'}], sizes: {units: px, dimensions: [{height: 512}, {height: 128}]}, colors: {mask: src/static/masks/panda$1.mask.png, modulate: [{name: blue, hue: 0}, {name: violet, saturation: 70, hue: 50}]}}}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const string expected = "src/static/panda(.+).png";
            string actual = config.Export.Input;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadCheckConfig()
        {
            const string yaml = "{checks: [{name: naming-convention, args: {pattern: '^panda[A-Z][A-Za-z]+.png$'}}, {name: file-exists, args: {source: src/static/mask/panda(.+).mask.png, target: src/static/panda$1.png}}]}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const int expected = 2;
            int actual = config.Checks.Count;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ElypiaEmotesTest()
        {
            const string yaml = "{export: {input: src/static/panda(.+).png, exif: [{tag: Artist, value: 'Elypia CIC and Contributors'}, {tag: Copyright, value: 'Apache 2.0, See NOTICE'}, {tag: ImageDescription, value: 'The ${FILENAME} emoji from the selection of Elypia emotes.'}], sizes: {units: px, dimensions: [{height: 512}, {height: 258}, {height: 128}, {height: 112}, {height: 72}, {height: 64}, {height: 56}, {height: 36}, {height: 28}]}, colors: {mask: src/static/masks/panda$1.mask.png, modulate: [{name: blue, hue: 0}, {name: green, hue: 166}, {name: pink, hue: 66.6}, {name: red, saturation: 90, hue: 79}, {name: violet, saturation: 70, hue: 50}, {name: yellow, saturation: 115, hue: 115}]}}, montages: [{name: colors, pattern: 'build/128px/.*pandaAww.png'}, {name: emotes, pattern: build/128px/.+}], checks: [{name: mask-resolution-matches}, {name: naming-convention, args: {pattern: '^panda[A-Z][A-Za-z]+.png$'}}, {name: file-exists, args: {source: src/static/mask/panda(.+).mask.png, target: src/static/panda$1.png}}, {name: file-exists, args: {source: src/static/panda(.+).png, target: src/static/projects/panda$1.psd}}]}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const string expected = "Elypia CIC and Contributors";
            string actual = config.Export.Exif[0].Value;
            
            Assert.Equal(expected, actual);
        }
    }
}
