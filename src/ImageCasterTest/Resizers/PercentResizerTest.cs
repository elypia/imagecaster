using ImageCaster.Api;
using ImageCaster.Resizers;
using Xunit;

namespace ImageCasterTest.Resizers
{
    public class PercentResizerTest
    {
        /// <summary>
        /// Base Image: 1920x1080
        /// Specified:    20x?
        /// </summary>
        [Fact]
        public void CalculateHeightFromDesiredWidth()
        {
            IResizer resizer = new PercentResizer();

            const int expected = 216;
            int actual = resizer.SizeHeight(1920, 1080, 20);

            Assert.Equal(expected, actual);
        }
    }
}