using ImageCaster.Api;
using ImageCaster.Resizers;
using Xunit;

namespace ImageCasterTest.Resizers
{
    public class PixelResizerTest
    {
        /// <summary>
        /// Base Image: 1920x1080
        /// Specified:     ?x480
        /// </summary>
        [Fact]
        public void CalculateWidthFromDesiredHeight()
        {
            IResizer resizer = new PixelResizer();

            const int expected = 853;
            int actual = resizer.SizeWidth(1920, 1080, 480);

            Assert.Equal(expected, actual);
        }
    }
}