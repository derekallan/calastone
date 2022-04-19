using CalastoneTextParser.Filters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParserTests.Filters
{
    internal class MiddleVowelFilterTests
    {
        [TestCase("clean")]
        public void TestShouldBeFilteredWithOddMiddleVowelShouldReturnTrue(string input)
        {
            var x = new MiddleVowelFilter();

            Assert.IsTrue(x.ShouldBeFiltered(input));
        }

        [TestCase("ha")]
        [TestCase("what")]
        public void TestShouldBeFilteredWithEvenMiddleVowelShouldReturnTrue(string input)
        {
            var x = new MiddleVowelFilter();

            Assert.IsTrue(x.ShouldBeFiltered(input));
        }

        [TestCase("the")]
        public void TestShouldBeFilteredWithOddMiddleConsonantShouldReturnFalse(string input)
        {
            var x = new MiddleVowelFilter();

            Assert.IsFalse(x.ShouldBeFiltered(input));
        }

        [TestCase("rather")]
        public void TestShouldBeFilteredWithEvenMiddleConsonantShouldReturnfalse(string input)
        {
            var x = new MiddleVowelFilter();

            Assert.IsFalse(x.ShouldBeFiltered(input));
        }
    }
}
