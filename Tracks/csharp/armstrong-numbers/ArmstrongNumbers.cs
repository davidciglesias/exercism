using System;
using System.Linq;
using System.Collections.Generic;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        string parsedNumber = number.ToString();
        int length = parsedNumber.Length;
        return parsedNumber.Aggregate(0, (sum, digit) =>
            sum += (int)Math.Pow(char.GetNumericValue(digit), length)
        ) == number;
    }
}