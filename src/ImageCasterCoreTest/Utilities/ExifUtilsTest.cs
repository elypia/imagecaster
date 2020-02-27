using System.Collections.Generic;
using System.Linq;
using ImageCasterCore.Utilities;
using ImageMagick;
using Xunit;

namespace ImageCasterCoreTest.Utilities
{
    public class ExifUtilsTest
    {
        [Fact]
        public void TestFindingExifValue()
        {
            ExifTag expected = ExifTag.Artist;
            ExifTag actual = ExifUtils.FindByName("Artist");

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void TestNotFindingExifValue()
        {
            ExifTag actual = ExifUtils.FindByName("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            Assert.Null(actual);
        }
        
        [Fact]
        public void GetTagNames()
        {
            List<string> tags = ExifUtils.GetNames().ToList();

            const string expected = "FaxProfile";
            string actual = tags[0];
            
            Assert.Equal(expected, actual);
        }
    }
}