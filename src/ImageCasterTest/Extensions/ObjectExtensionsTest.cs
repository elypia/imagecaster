using System;
using ImageCaster.Extensions;
using Xunit;

namespace ImageCasterTest.Extensions
{
    public class ObjectExtensionsTest
    {
        [Fact]
        public void TestThrowsOnNull()
        {
            const string nullString = null;
            Assert.Throws<ArgumentNullException>(() => nullString.RequireNonNull());
        }
        
        [Fact]
        public void TestDontThrowOnNotNull()
        {
            const string notNullString = "not null";

            const string expected = "not null";
            string actual = notNullString.RequireNonNull();

            Assert.Equal(expected, actual);
        }
    }
}
