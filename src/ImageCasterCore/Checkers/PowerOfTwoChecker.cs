using System.Collections.Concurrent;
using System.Collections.Generic;
using ImageCasterCore.Api;
using ImageCasterCore.Configuration.Checkers;
using ImageCasterCore.Extensions;
using ImageMagick;

namespace ImageCasterCore.Checkers
{
    /// <summary>
    /// Verify that all images are sized two powers of two.
    /// Computers work much faster with images that have heights and widths
    /// that are powers of two, so it can be desirable in many cases, especially
    /// game development to ensure images fit this rule, even if it means padding white space.
    /// </summary>
    public class PowerOfTwoChecker : IChecker
    {
        public List<PowerOfTwoConfig> Config { get; }
        
        public PowerOfTwoChecker(List<PowerOfTwoConfig> config)
        {
            this.Config = config.RequireNonNull();
        }
        
        public IEnumerable<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (PowerOfTwoConfig config in Config)
            {
                DataResolver resolver = new DataResolver(config.Source);
                List<ResolvedData> resolvedFiles = resolver.Data;

                foreach (ResolvedData resolvedFile in resolvedFiles)
                {
                    using (IMagickImage magickImage = resolvedFile.ToMagickImage())
                    {
                        if (!IsPowerOfTwo((uint) magickImage.Height, (uint) magickImage.Width))
                        {
                            failures.Add(new Failure(resolvedFile, "dimensions are not powers of 2."));
                        }
                    }
                }
            }

            return failures;
        }

        public bool IsPowerOfTwo(params uint[] values)
        {
            foreach (uint value in values)
            {
                if ((value & value - 1) == 0 && value != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
