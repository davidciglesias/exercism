using System;
using System.Linq;
using System.Collections.Generic;

public static class Pangram
{
    public static bool IsPangram(string input)
    {
        return input
            .Trim()
            .ToLower()
            .Where(char.IsLetter)
            .Distinct()
            .Count() == 26;
    }
}
