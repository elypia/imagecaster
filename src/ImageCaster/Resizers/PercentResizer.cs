using ImageCaster.Api;
using ImageMagick;

namespace ImageCaster.Resizers
{
    public class PercentResizer : IResizer
    {
        public int SizeWidth(IMagickImage magickImage, int desiredHeight)
        {
            return SizeWidth(magickImage.Width, magickImage.Height, desiredHeight);
        }

        public int SizeWidth(int width, int height, int desiredHeight)
        {
            return (int)(width * ((float)desiredHeight / 100));
        }

        public int SizeHeight(IMagickImage magickImage, int desiredWidth)
        {
            return SizeWidth(magickImage.Width, magickImage.Height, desiredWidth);
        }

        public int SizeHeight(int width, int height, int desiredWidth)
        {
            return (int)(height * ((float)desiredWidth / 100));
        }

        public void Resize(IMagickImage magickImage, int desiredWidth, int desiredHeight)
        {
            int newWidth = (desiredWidth == 0) ? SizeHeight(magickImage, desiredHeight) : magickImage.Width * desiredWidth;
            int newHeight = (desiredHeight == 0) ? SizeHeight(magickImage, desiredWidth) : magickImage.Height * desiredWidth;

            magickImage.Resize(newWidth, newHeight);
        }
    }
}