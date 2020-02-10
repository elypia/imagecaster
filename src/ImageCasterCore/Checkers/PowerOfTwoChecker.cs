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
        public ICollector Collector { get; }
        public List<PowerOfTwoConfig> Config { get; }
        
        public PowerOfTwoChecker(ICollector collector, List<PowerOfTwoConfig> config)
        {
            this.Collector = collector.RequireNonNull();
            this.Config = config.RequireNonNull();
        }
        
        public List<Failure> Check()
        {
            List<Failure> failures = new List<Failure>();
            
            foreach (PowerOfTwoConfig config in Config)
            {
                List<ResolvedFile> resolvedFiles = Collector.Collect(config.Source);

                foreach (ResolvedFile resolvedFile in resolvedFiles)
                {
                    using (MagickImage magickImage = new MagickImage(resolvedFile.FileInfo))
                    {
                        if (!IsPowerOfTwo((uint) magickImage.Height, (uint) magickImage.Width))
                        {
                            failures.Add(new Failure(resolvedFile, "dimensions are not powers of 2"));
                        }
                    }
                }
            }

            return failures;
        }

        private bool IsPowerOfTwo(params uint[] values)
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
