using System;
using System.Linq;
using System.Collections.Generic;
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
        return digits.Split("")
                .Select((_, index) => index)
                .Aggregate(0, (max, current) =>
                {
                    int currentSum = digits
                    .Substring(current, span)
                    .Aggregate(1, (product, element) => product * int.Parse($"{element}"));
                    return currentSum > max ? currentSum : max;
                });
    }
}