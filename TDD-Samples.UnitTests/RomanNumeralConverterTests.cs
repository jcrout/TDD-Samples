using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using TDD_Samples;

namespace TDD_Samples.UnitTests
{
    [TestFixture]
    public class RomanNumeralConverterTests
    {
        [TestCase(1, "I")]
        [TestCase(6, "VI")]
        [TestCase(2489, "MMCDLXXXIX")]
        [TestCase(3000, "MMM")]
        public void GetRomanNumeral_NumberBetween1And3000_ReturnsCorrectRomanNumeral(int numberToConvert, string expectedRomanNumeralString)
        {
            var converter = GetRomanNumeralConverter();

            var result = converter.GetRomanNumeral(numberToConvert);

            Assert.AreEqual(expectedRomanNumeralString, result);
        }

        [Test]
        public void GetRomanNumeral_NumberBelow1_ThrowsIndexOutOfRange()
        {
            var converter = GetRomanNumeralConverter();
            var invalidIndexBelow1 = 0;

            Assert.Throws<IndexOutOfRangeException>(() => converter.GetRomanNumeral(invalidIndexBelow1));
        }

        [Test]
        public void GetRomanNumeral_NumberAbove3000_ThrowsIndexOutOfRange()
        {
            var converter = GetRomanNumeralConverter();
            var invalidIndexAbove3000 = 3001;

            Assert.Throws<IndexOutOfRangeException>(() => converter.GetRomanNumeral(invalidIndexAbove3000));
        }

        public RomanNumeralConverter GetRomanNumeralConverter()
        {
            return new RomanNumeralConverter();
        }
    }
}
