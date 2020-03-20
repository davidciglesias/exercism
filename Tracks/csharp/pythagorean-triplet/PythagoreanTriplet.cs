using System;
using System.Collections.Generic;
using System.Linq;

public static class PythagoreanTriplet
{
    public static IEnumerable<(int a, int b, int c)> TripletsWithSum(int sum)
    {
        foreach (int a in Enumerable.Range(1, sum - 2))
        {
            for (int b = a + 1; b <= (sum - a - 1 / 2); b++)
            {
                int c = sum - b - a;
                if (a * a + b * b - c * c == 0)
                {
                    yield return (a, b, c);
                }
            }
        }
    }
}