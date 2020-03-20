using System;
using System.Collections.Generic;
using System.Linq;

public static class AllYourBase
{
    public static int[] Rebase(int inputBase, int[] inputDigits, int outputBase)
    {
        if (inputBase <= 1 || outputBase <= 1 || inputDigits.Any(digit => digit < 0 || digit >= inputBase))
        {
            throw new ArgumentException();
        }

        int inputLength = inputDigits.Length;
        if (inputLength == 0 || inputDigits.Sum() == 0)
        {
            return new[] { 0 };
        }

        var result = new List<int>();
        long decimalNumber = (long)Enumerable.Range(0, inputLength).Sum((index) => inputDigits[inputLength - index - 1] * Math.Pow(inputBase, index));
        int lastExponent = (int)Math.Floor(Math.Log10(decimalNumber) / Math.Log10(outputBase));
        Enumerable.Range(0, lastExponent + 1).Aggregate(decimalNumber, (prev, baseExponent) =>
        {
            result.Add((int)(baseExponent == lastExponent ? prev : prev % outputBase));
            return prev / outputBase;
        });
        result.Reverse();
        return result.ToArray();
    }
}