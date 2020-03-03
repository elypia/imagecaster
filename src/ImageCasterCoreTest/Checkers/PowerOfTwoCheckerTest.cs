using System.Collections.Generic;
using ImageCasterCore.Checkers;
using ImageCasterCore.Configuration.Checkers;
using Xunit;

namespace ImageCasterCoreTest.Checkers
{
    public class PowerOfTwoCheckerTest
    {
        [Fact]
        public void TestPowerOfTwo()
        {
            List<PowerOfTwoConfig> config = new List<PowerOfTwoConfig>();
            PowerOfTwoChecker checker = new PowerOfTwoChecker(config);

            bool actual = checker.IsPowerOfTwo(0, 1, 4, 9, 16, 25, 36, 49, 64, 81, 100, 121, 144);

            Assert.True(actual);
        }
        
        [Fact]
        public void TestNotPowerOfTwo()
        {
            List<PowerOfTwoConfig> config = new List<PowerOfTwoConfig>();
            PowerOfTwoChecker checker = new PowerOfTwoChecker(config);

            bool actual = checker.IsPowerOfTwo(2);

            Assert.True(actual);
        }
    }
}