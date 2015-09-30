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
        [TestCase(101, "CI")]
        [TestCase(2489, "MMCDLXXXIX")]
        [TestCase(2644, "MMDCXLIV")]
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


        [Test]
        public void GetNumber_NullString_ThrowsArgumentNullException()
        {
            var converter = GetRomanNumeralConverter();

            Assert.Throws<ArgumentNullException>(() => converter.GetNumber(null));
        }

        [Test]
        public void GetNumber_EmptyString_ThrowsArgumentException()
        {
            var converter = GetRomanNumeralConverter();

            Assert.Throws<ArgumentException>(() => converter.GetNumber(String.Empty));
        }
        
        [TestCase("abc")]
        [TestCase("IVR")]
        [TestCase("MM ")]
        [TestCase("O_O")]
        public void GetNumber_StringContainingInvalidCharacters_ThrowsArgumentException(string invalidInput)
        {
            var converter = GetRomanNumeralConverter();

            Assert.Throws<ArgumentException>(() => converter.GetNumber(invalidInput));
        }

        public void GetNumber_StringContainingLowerCaseSymbols_ThrowsArgumentException()
        {
            var converter = GetRomanNumeralConverter();
            var invalidStringWithLowerCaseSymbols = "mmm";

            Assert.Throws<ArgumentException>(() => converter.GetNumber(invalidStringWithLowerCaseSymbols));
        }

        [TestCase("IIII")]
        [TestCase("XXXXX")]
        [TestCase("MCCCC")]
        public void GetNumber_InvalidSubtractiveNotation_ThrowsArgumentException(string invalidSubtractiveNotation)
        {
            var converter = GetRomanNumeralConverter();

            Assert.Throws<ArgumentException>(() => converter.GetNumber(invalidSubtractiveNotation));
        }

        [Test]
        public void GetNumber_NumeralAbove3000_ThrowsIndexOutOfRange()
        {
            var converter = GetRomanNumeralConverter();
            var number3002 = "MMMII";

            Assert.Throws<IndexOutOfRangeException>(() => converter.GetNumber(number3002));
        }

        [TestCase("I", 1)]
        [TestCase("VI", 6)]
        [TestCase("CI", 101)]
        [TestCase("MMCDLXXXIX", 2489)]
        [TestCase("MMDCXLIV", 2644)]
        [TestCase("MMM", 3000)]
        public void GetNumber_NumeralBetween1And3000_ReturnsCorrectNumber(string validInput, int expectedResult)
        {
            var converter = GetRomanNumeralConverter();

            var result = converter.GetNumber(validInput);

            Assert.AreEqual(expectedResult, result);
        }

        public RomanNumeralConverter GetRomanNumeralConverter()
        {
            return new RomanNumeralConverter();
        }
    }
}
