using CalastoneTextParser.Filters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalastoneTextParserTests.Filters
{
    internal class LetterTFilterTests
    {
        [TestCase("at")]
        [TestCase("atb")]
        [TestCase("tbc")]
        [TestCase("xqt")]
        [TestCase("aT")]
        [TestCase("aTb")]
        [TestCase("Tbc")]
        [TestCase("xqT")]
        public void TestShouldBeFilteredWithTInStringShouldReturnTrue(string input)
        {
            var x = new LetterTFilter();

            Assert.IsTrue(x.ShouldBeFiltered(input));
        }

        [TestCase("sadfasdimj")]
        [TestCase("amkmalosd")]
        [TestCase("annndfguiuui")]
        [TestCase("aoinofiasdnpsaw")]
        public void TestShouldBeFilteredWithNoTShouldReturnFalse(string input)
        {
            var x = new LetterTFilter();

            Assert.IsFalse(x.ShouldBeFiltered(input));
        }
    }
}
