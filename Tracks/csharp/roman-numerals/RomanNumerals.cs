using System.Collections.Generic;
using System.Linq;

public static class RomanNumeralExtension
{
    public static SortedDictionary<int, (string, string)> digitsInOrder = new SortedDictionary<int, (string, string)>
    {
        [0] = ("I", "V"),
        [1] = ("X", "L"),
        [2] = ("C", "D"),
        [3] = ("M", null),
    };

    public static string ToRoman(this int value)
    {
        string valueString = value.ToString();
        int length = valueString.Length;
        return string.Join("", valueString.Select(digit => int.Parse("" + digit)).Select((digit, index) =>
        {
            var digits = digitsInOrder[length - index - 1];
            return digit < 4
                ? string.Join("", Enumerable.Repeat(digits.Item1, digit))
                : digit == 4
                ? digits.Item1 + digits.Item2
                : digit > 4 && digit < 9
                ? digits.Item2 + string.Join("", Enumerable.Repeat(digits.Item1, digit - 5))
                : digit == 9 ? digits.Item1 + digitsInOrder[length - index].Item1 : "";
        }));
    }
}