using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_Samples
{
    public class RomanNumeralConverter
    {

        private int maxValue = 3000;
        private int minValue = 1;

        private static LinkedList<RomanNumeral> numerals;
        private static LinkedListNode<RomanNumeral> lowestNumeral;
        private static LinkedListNode<RomanNumeral> highestNumeral;

        static RomanNumeralConverter()
        {
            var romanNumeralI = new RomanNumeral(1, 'I');
            var romanNumeralV = new RomanNumeral(5, 'V');
            var romanNumeralX = new RomanNumeral(10, 'X');
            var romanNumeralL = new RomanNumeral(50, 'L');
            var romanNumeralC = new RomanNumeral(100, 'C');
            var romanNumeralD = new RomanNumeral(500, 'D');
            var romanNumeralM = new RomanNumeral(1000, 'M');

            romanNumeralV.SubtractiveNumeral = romanNumeralI;
            romanNumeralX.SubtractiveNumeral = romanNumeralI;
            romanNumeralL.SubtractiveNumeral = romanNumeralX;
            romanNumeralC.SubtractiveNumeral = romanNumeralX;
            romanNumeralD.SubtractiveNumeral = romanNumeralC;
            romanNumeralM.SubtractiveNumeral = romanNumeralC;

            numerals = new LinkedList<RomanNumeral>(new RomanNumeral[]
            {
                    romanNumeralI,
                    romanNumeralV,
                    romanNumeralX,
                    romanNumeralL,
                    romanNumeralC,
                    romanNumeralD,
                    romanNumeralM
            });

            lowestNumeral = numerals.First;
            highestNumeral = numerals.Last;
        }

        /// <summary>
        ///     Simple data-container class for storing information about individual roman numerals.
        /// </summary>
        private class RomanNumeral
        {
            public int Value { get; }
            public char Symbol { get; }

            /// <summary>
            ///     Gets the RomanNumeral that, when preceeding this RomanNumeral, represents the subtractive notation. 
            /// </summary>
            /// <example>
            ///     Examples include IV to represent 4 instead of IIII, and IX to represent 9 instead of VIIII.
            /// </example>
            public RomanNumeral SubtractiveNumeral;

            public RomanNumeral(int value, char symbol)
            {
                this.Value = value;
                this.Symbol = symbol;
            }

            public override string ToString()
            {
                return this.Symbol.ToString();
            }
        }

        public string GetRomanNumeral(int numberToConvert)
        {
            if (numberToConvert < minValue || numberToConvert > maxValue)
            {
                throw new IndexOutOfRangeException();
            }

            var builder = new StringBuilder();
            var value = (double)numberToConvert;
            var currentNumeral = highestNumeral;

            do
            {
                value = this.UpdateAndReturnValueByNumeral(builder, value, currentNumeral.Value);
                currentNumeral = currentNumeral.Previous;
            }
            while (currentNumeral != null && value >= 1);

            return builder.ToString();
        }

        private double UpdateAndReturnValueByNumeral(StringBuilder builder, double value, RomanNumeral numeral)
        {
            var numeralValue = (double)numeral.Value;

            if (value >= numeralValue)
            {
                var count = (int)Math.Floor(value / numeralValue);
                builder.Append(numeral.Symbol, count);

                value -= (numeralValue * count);
            }

            if (numeral.SubtractiveNumeral == null)
            {
                return value;
            }

            var subNumeral = numeral.SubtractiveNumeral;
            var subValue = (double)(numeral.Value - subNumeral.Value);

            if (value >= subValue)
            {
                builder.Append(subNumeral.Symbol);
                builder.Append(numeral.Symbol);

                value -= subValue;
            }

            return value;
        }

        public int GetNumber(string romanNumeralString)
        {
            if (romanNumeralString == null)
            {
                throw new ArgumentNullException();
            }

            if (romanNumeralString.Length == 0)
            {
                throw new ArgumentException();
            }

            int computedValue = 0;
            int endIndex = romanNumeralString.Length - 1;

            for (int i = 0; i < romanNumeralString.Length; i++)
            {
                var numeral = GetRomanNumeralFromChar(romanNumeralString[i]);
                if (numeral == null)
                {
                    throw new ArgumentException();
                }
                
                if (i == endIndex)
                {
                    computedValue += numeral.Value;
                }
                else
                {
                    var nextNumeral = GetRomanNumeralFromChar(romanNumeralString[i + 1]);
                    if (nextNumeral == null)
                    {
                        throw new ArgumentException();
                    }

                    if (numeral == nextNumeral)
                    {
                        this.ThrowIfInvalidSubtractiveNotation(romanNumeralString, i);

                        computedValue += numeral.Value;
                        computedValue += numeral.Value;
                        i++;
                    }
                    else
                    {
                        var isSubtractiveNotation = nextNumeral.SubtractiveNumeral == numeral;
                        if (isSubtractiveNotation)
                        {
                            var subValue = nextNumeral.Value - numeral.Value;
                            computedValue += subValue;
                            i++;
                        }
                        else
                        {
                            computedValue += numeral.Value;
                        }
                    }
                }

                // prevent overflow by checking the value against the max allowed value each time
                if (computedValue > maxValue)
                {
                    throw new IndexOutOfRangeException();
                }
            }

            return computedValue;
        }

        private void ThrowIfInvalidSubtractiveNotation(string romanNumeralString, int startIndex)
        {
            var remainingLength = romanNumeralString.Length - startIndex;
            if (remainingLength < 4)
            {
                return;
            }

            var symbol = romanNumeralString[startIndex];
            if (symbol != 'I' && symbol != 'X' && symbol != 'C')
            {
                return;
            }

            for (int i = startIndex + 1; i < startIndex + 4; i++)
            {
                if (romanNumeralString[i] != symbol)
                {
                    return;
                }
            }

            throw new ArgumentException();
        }

        private RomanNumeral GetRomanNumeralFromChar(char symbolChar)
        {
            var currentNumeral = highestNumeral;

            do
            {
                if (currentNumeral.Value.Symbol == symbolChar)
                {
                    return currentNumeral.Value;
                }

                currentNumeral = currentNumeral.Previous;
            }
            while (currentNumeral != null);

            return null;
        }
    }
}
