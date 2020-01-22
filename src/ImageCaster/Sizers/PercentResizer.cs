using ImageCaster.Api;
using ImageMagick;

namespace ImageCaster.Sizers
{
    public class PercentResizer : IResizer
    {
        public int SizeWidth(IMagickImage magickImage, int desiredHeight)
        {
            return magickImage.Width * (desiredHeight / 100);
        }
        
        public int SizeHeight(IMagickImage magickImage, int desiredWidth)
        {
            return magickImage.Height * (desiredWidth / 100);
        }

        public void Resize(IMagickImage magickImage, int desiredWidth, int desiredHeight)
        {
            int newWidth = (desiredWidth == 0) ? SizeHeight(magickImage, desiredHeight) : magickImage.Width * desiredWidth;
            int newHeight = (desiredHeight == 0) ? SizeHeight(magickImage, desiredWidth) : magickImage.Height * desiredWidth;

            magickImage.Resize(newWidth, newHeight);
        }
    }
}