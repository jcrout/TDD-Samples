namespace TDD_Samples
{
    /// <summary>
    ///     Provides functionality to convert integers to roman numerals and vice versa. 
    /// </summary>
    public interface IRomanNumeralConverter
    {
        /// <summary>
        ///     Parses a string value to return the numeric equivalent.
        /// </summary>
        /// <param name="romanNumeralString"></param>
        /// <returns>The integer value of the input string.</returns>
        int GetNumber(string romanNumeralString);

        /// <summary>
        ///     Converts an integer to the string representation of the equivalent roman numerals.
        /// </summary>
        /// <param name="numberToConvert"></param>
        /// <returns>The roman numerals string.</returns>
        string GetRomanNumeral(int numberToConvert);
    }
}