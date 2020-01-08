using ImageCaster.Configuration;
using Xunit;

namespace ImageCasterTest
{
    public class ImageCasterConfigTest
    {
        [Fact]
        public void ReadExportConfig()
        {
            const string yaml = "{export: {input: 'src/static/panda(.+).png', exif: [{tag: Artist, value: 'Elypia CIC and Contributors'}], sizes: {units: px, dimensions: [{height: 512}, {height: 128}]}, color: {mask: src/static/masks/panda$1.mask.png, colors: [{name: blue, modulate: '100,100,0'}, {name: violet, modulate: '100,70,50'}]}}}";
            ImageCasterConfig config = ImageCasterConfig.Load(yaml);

            const string expected = "src/static/panda(.+).png";
            string actual = config.Export.Input;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ReadCheckConfig()
        {
            const string yaml = "{checks: [{name: mask-resolution-matches}, {name: file-exists, args: [{name: source, value: 'src/static/mask/panda(.+).mask.png'}, {name: target, value: src/static/panda$1.png}]}]}";
            ImageCasterConfig config = ImageCasterConfig.Load(yaml);

            const int expected = 2;
            int actual = config.Checks.Count;
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ElypiaEmotesTest()
        {
            const string yaml = "{export: {input: src/static/panda(.+).png, exif: [{tag: Artist, value: 'Elypia CIC and Contributors'}, {tag: Copyright, value: 'Apache 2.0, See NOTICE'}, {tag: ImageDescription, value: 'The ${FILENAME} emoji from the selection of Elypia emotes.'}], sizes: {units: px, dimensions: [{height: 512}, {height: 258}, {height: 128}, {height: 112}, {height: 72}, {height: 64}, {height: 56}, {height: 36}, {height: 28}]}, color: {mask: src/static/masks/panda$1.mask.png, colors: [{name: blue, modulate: '100,100,0'}, {name: green, modulate: '100,100,166'}, {name: pink, modulate: '100,100,66.6'}, {name: red, modulate: '100,90,79'}, {name: violet, modulate: '100,70,50'}, {name: yellow, modulate: '100,115,115'}]}}, montages: [{name: colors, pattern: 'build/128px/.*pandaAww.png'}, {name: emotes, pattern: build/128px/.+}], checks: [{name: mask-resolution-matches}, {name: file-exists, args: [{name: source, value: src/static/mask/panda(.+).mask.png}, {name: target, value: src/static/panda$1.png}]}, {name: file-exists, args: [{name: source, value: src/static/panda(.+).png}, {name: target, value: src/static/projects/panda$1.psd}]}]}";
            ImageCasterConfig config = ImageCasterConfig.Load(yaml);

            const string expected = "Elypia CIC and Contributors";
            string actual = config.Export.Exif[0].Value;
            
            Assert.Equal(expected, actual);
        }
    }
}
