using System.IO;
using ImageCaster.Interfaces;

namespace ImageCaster.Checks
{
    /// <summary>
    /// Verify that all images are sized two powers of two.
    /// Computers work much faster with images that have heights and widths
    /// that are powers of two, so it can be desirable in many cases, especially
    /// game development to ensure images fit this rule, even if it means padding white space.
    /// </summary>
    public class PowerOfTwoChecker : IChecker
    {
        public bool Check(FileInfo fileInfo)
        {
            throw new System.NotImplementedException();
        }

        public Failure FailureMessage(ResolvedFile resolvedFile)
        {
            throw new System.NotImplementedException();
        }
    }
}