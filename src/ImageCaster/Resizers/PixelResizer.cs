using ImageCaster.Api;
using ImageMagick;

namespace ImageCaster.Resizers
{
    public class PixelResizer : IResizer
    {
        public int SizeWidth(IMagickImage magickImage, int desiredHeight)
        {
            return SizeWidth(magickImage.Width, magickImage.Height, desiredHeight);
        }

        public int SizeWidth(int width, int height, int desiredHeight)
        {
            float ratio = (float)desiredHeight / height;
            return (int)(width * ratio);
        }

        public int SizeHeight(IMagickImage magickImage, int desiredWidth)
        {
            return SizeWidth(magickImage.Width, magickImage.Height, desiredWidth);
        }

        public int SizeHeight(int width, int height, int desiredWidth)
        {
            float ratio = (float)desiredWidth / width;
            return (int)(height * ratio);
        }

        public void Resize(IMagickImage magickImage, int desiredWidth, int desiredHeight)
        {
            int newWidth = (desiredWidth == 0) ? SizeHeight(magickImage, desiredHeight) : desiredWidth;
            int newHeight = (desiredHeight == 0) ? SizeHeight(magickImage, desiredWidth) : desiredHeight;

            magickImage.Resize(newWidth, newHeight);
        }
    }
}