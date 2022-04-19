using CalastoneTextParser.Filters;
using NUnit.Framework;

namespace CalastoneTextParserTests.Filters
{
    internal class LengthLessThan3FilterTests
    {
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("bc")]
        [TestCase("xq")]
        public void TestShouldBeFilteredWithStringLessThan3ShouldReturnTrue(string input)
        {
            var x = new LengthLessThan3Filter();

            Assert.IsTrue(x.ShouldBeFiltered(input));
        }

        [TestCase("aasdf")]
        [TestCase("abasdfasdf")]
        [TestCase("asdfasdf")]
        [TestCase("alice")]
        public void TestShouldBeFilteredWithString3OrGreaterShouldReturnFalse(string input)
        {
            var x = new LengthLessThan3Filter();

            Assert.IsFalse(x.ShouldBeFiltered(input));
        }
    }
}
