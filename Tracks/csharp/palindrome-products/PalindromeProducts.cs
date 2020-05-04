using System;
using System.Collections.Generic;
using System.Linq;

public static class PalindromeProducts
{
    public static bool IsPalindrome(int number) => int.Parse(string.Concat(number.ToString().Reverse())) == number;

    public static (int, IEnumerable<(int, int)>) Palindrome(Func<int, int, (int, IEnumerable<(int, int)>)> func, int minFactor, int maxFactor)
    {
        if (minFactor > maxFactor) throw new ArgumentException();
        var result = func(minFactor, maxFactor);

        return result == default ? throw new ArgumentException() : result;
    }
    private static (int product, IEnumerable<(int, int)> factors) GetLargestPalindrome(int minFactor, int maxFactor)
    {
        (int product, IEnumerable<(int, int)> factors) result = default;
        for (int n = maxFactor; n >= minFactor; n--)
        {
            for (int m = n; m >= minFactor; m--)
            {
                var product = n * m;
                if (result == default || product > result.product)
                {
                    if (IsPalindrome(product))
                    {
                        result = (product, Enumerable.Empty<(int, int)>().Append((m, n)));
                    }
                }
                else if (result.product == product)
                {
                    result.factors = result.factors.Append((m, n));
                }
            }
        }

        return result;
    }

    public static (int, IEnumerable<(int, int)>) Largest(int minFactor, int maxFactor) => Palindrome(GetLargestPalindrome, minFactor, maxFactor);

    private static (int product, IEnumerable<(int, int)> factors) GetSmallestPalindrome(int minFactor, int maxFactor)
    {
        (int product, IEnumerable<(int, int)> factors) result = default;
        for (int n = minFactor; n <= maxFactor; n++)
        {
            for (int m = minFactor; m <= n; m++)
            {
                var product = n * m;
                if (result == default || product < result.product)
                {
                    if (IsPalindrome(product))
                    {
                        result = (product, Enumerable.Empty<(int, int)>().Append((m, n)));
                    }
                }
                else if (result.product == product)
                {
                    result.factors = result.factors.Append((m, n));
                }
            }
        }

        return result;
    }

    public static (int, IEnumerable<(int, int)>) Smallest(int minFactor, int maxFactor) => Palindrome(GetSmallestPalindrome, minFactor, maxFactor);
}
