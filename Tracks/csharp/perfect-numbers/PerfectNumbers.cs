using System;
using System.Collections.Generic;
using System.Linq;

public enum Classification
{
    Perfect = 0,
    Abundant = 1,
    Deficient = -1
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (number == 1)
        {
            return Classification.Deficient;
        }

        return (Classification)Enumerable.Range(1, number / 2)
                                         .Where(current => number % current == 0)
                                         .Sum()
                                         .CompareTo(number);
    }
}
