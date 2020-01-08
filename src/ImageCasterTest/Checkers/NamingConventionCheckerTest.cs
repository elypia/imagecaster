using System.IO;
using ImageCaster;
using ImageCaster.Checks;
using Xunit;

namespace ImageCasterTest.Checkers
{
    public class NamingConventionCheckerTest
    {
        [Fact]
        public void MatchAnyFileWithoutError()
        {
            NamingConventionChecker checker = new NamingConventionChecker(".+");
            FileInfo fileInfo = new FileInfo("emotes/pandaAww.png");
            bool actual = checker.Check(fileInfo);
            Assert.True(actual);
        }
        
        [Fact]
        public void MatchFileWithPrefix()
        {
            NamingConventionChecker checker = new NamingConventionChecker(@"^panda[A-Z][A-Za-z]+.png$");
            FileInfo fileInfo = new FileInfo("emotes/pandaAww.png");
            bool actual = checker.Check(fileInfo);
            Assert.True(actual);
        }
        
        [Fact]
        public void FailToMatchAgainstWrongPrefix()
        {
            NamingConventionChecker checker = new NamingConventionChecker(@"^panda[A-Z][A-Za-z]+.png$");
            FileInfo fileInfo = new FileInfo("emotes/pansaAww.png");
            bool actual = checker.Check(fileInfo);
            Assert.False(actual);
        }
        
        [Fact]
        public void TestFailureMessage()
        {
            NamingConventionChecker checker = new NamingConventionChecker(@"^panda[A-Z][A-Za-z]+.png$");
            FileInfo fileInfo = new FileInfo("emotes/pansaAww.png");
            ResolvedFile resolvedFile = new ResolvedFile(fileInfo, "emotes/panda(.+).png", "Aww");
            
            const string expected = @"NamingConvention check failed for emotes/pansaAww.png: filename does not adhere to the pattern /^panda[A-Z][A-Za-z]+.png$/.";
            string actual = checker.FailureMessage(resolvedFile).ToString();

            Assert.Equal(expected, actual);
        }
    }
}
