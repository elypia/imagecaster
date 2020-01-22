using ImageCaster.Api;
using ImageMagick;

namespace ImageCaster.Sizers
{
    public class PixelResizer : IResizer
    {
        public int SizeWidth(IMagickImage magickImage, int desiredHeight)
        {
            int imageHeight = magickImage.Height;
            int imageWidth = magickImage.Width;
            int ratio = desiredHeight / imageHeight;
            return imageWidth * ratio;
        }
        
        public int SizeHeight(IMagickImage magickImage, int desiredWidth)
        {
            int imageHeight = magickImage.Height;
            int imageWidth = magickImage.Width;
            int ratio = desiredWidth / imageWidth;
            return imageHeight * ratio;
        }

        public void Resize(IMagickImage magickImage, int desiredWidth, int desiredHeight)
        {
            int newWidth = (desiredWidth == 0) ? SizeHeight(magickImage, desiredHeight) : desiredWidth;
            int newHeight = (desiredHeight == 0) ? SizeHeight(magickImage, desiredWidth) : desiredHeight;

            magickImage.Resize(newWidth, newHeight);
        }
    }
}