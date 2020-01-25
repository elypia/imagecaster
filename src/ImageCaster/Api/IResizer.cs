using ImageMagick;

namespace ImageCaster.Api
{
    /// <summary>
    /// Store metadata and the implementation on how
    /// to calculate or resize a particular unit of measurement.
    /// </summary>
    public interface IResizer
    {
        int SizeWidth(IMagickImage magickImage, int desiredHeight);
        int SizeWidth(int width, int height, int desiredHeight);
        int SizeHeight(IMagickImage magickImage, int desiredWidth);
        int SizeHeight(int width, int height, int desiredWidth);
        void Resize(IMagickImage magickImage, int desiredWidth, int desiredHeight);
    }
}