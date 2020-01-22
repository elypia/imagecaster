using ImageCaster.Configuration;
using Xunit;

namespace ImageCasterTest
{
    public class ImageCasterConfigTest
    {
        [Fact]
        public void ReadExportConfig()
        {
            const string yaml = "{export: {input: src/emote(.+)\\.png$, metadata: {exif: {tags: [{tag: OwnerName, value: 'Elypia CIC'}]}}, sizes: {dimensions: [{height: 512}, {height: 256}]}, colors: {mask: src/emote$1.mask.png, modulate: [{name: blue, hue: 0}]}}}";
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

        /// <summary>
        /// This one is a bit of an integration test to verify it works for the project that motivates this one.
        /// </summary>
        [Fact]
        public void ElypiaEmotesTest()
        {
            const string yaml = "{export: {input: src/static/panda(.+)\\.png, metadata: {exif: {tags: [{tag: OwnerName, value: 'Elypia CIC'}, {tag: Artist, value: 'Elypia CIC and Contributors'}, {tag: 'https://gitlab.com/Elypia/elypia-emotes/blob/master/NOTICE', value: 'Apache 2.0'}, {tag: ImageDescription, value: 'The ${NAME} emote from the Elypia Emotes.'}]}, iptc: {tags: [{tag: Source, value: 'Elypia CIC'}, {tag: Contact, value: 'https://gitlab.com/Elypia/elypia-emotes'}, {tag: CopyrightNotice, value: 'https://gitlab.com/Elypia/elypia-emotes/blob/master/NOTICE'}, {tag: Headline, value: 'The ${NAME} emote from the Elypia Emotes.'}, {tag: Keywords, value: emote}]}}, sizes: {dimensions: [{height: 512}, {height: 256}, {height: 128}, {height: 112}, {height: 72}, {height: 64}, {height: 56}, {height: 36}, {height: 28}]}, colors: {mask: src/masks/panda$1.mask.png, modulate: [{name: blue, hue: 0}, {name: green, hue: 166}, {name: pink, hue: 66.6}, {name: red, hue: 82}, {name: violet, saturation: 70, hue: 50}, {name: yellow, saturation: 115, hue: 115}]}}, montages: [{name: colors, pattern: 'build/128px/.*pandaAww.png'}, {name: emotes, pattern: build/128px/.+}], checks: {power-of-two: [{source: 'src/(?:animated|masks|static)/.+'}], resolution-matches: [{source: src/masks/(.+)\\.mask\\.png, target: src/static/$1.png}], file-exists: [{source: src/projects/(.+)\\.ora, patterns: [src/static/$1.png]}, {source: src/masks/(.+)\\.mask\\.png, patterns: [src/static/$1.png]}], naming-convention: [{source: src/static/.+, pattern: '^panda[A-Z][A-Za-z\\d]+\\.png$'}, {source: src/masks/.+, pattern: '^panda[A-Z][A-Za-z\\d]+\\.mask\\.png$'}, {source: src/projects/.+, pattern: '^panda[A-Z][A-Za-z\\d]+\\.ora$'}, {source: src/animation/.+, pattern: '^apanda[A-Z][A-Za-z\\d]+\\.gif$'}]}}";
            ImageCasterConfig config = ImageCasterConfig.LoadFromString(yaml);

            const string expected = "Elypia CIC";
            string actual = config.Export.Metadata.Exif.Tags[0].Value;
            
            Assert.Equal(expected, actual);
        }
    }
}
