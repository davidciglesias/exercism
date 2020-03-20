using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class LargestSeriesProduct
{
    public static long GetLargestProduct(string digits, int span)
    {
        if (span == 0) return 1;
        int length = digits.Length;
        if (span < 0 || length == 0 || new Regex("\\D").IsMatch(digits) || length < span)
        {
            throw new ArgumentException();
        }
        return Enumerable.Range(0, length - 1)
            .Where(index => (index + span - 1) < length)
            .Aggregate(0, (max, current) =>
                 Math.Max(digits.Substring(current, span)
                                .Select(character => int.Parse($"{character}"))
                                .Aggregate(1, (product, element) => product * element), max));
    }
}