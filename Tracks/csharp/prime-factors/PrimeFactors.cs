using System;
using System.Collections.Generic;
using System.Linq;

public static class PrimeFactors
{
    private static IEnumerable<long> Divisors(long number)
    {
        long divisor = 2;
        do
        {
            if (number % divisor == 0)
            {
                number /= divisor;
                yield return divisor;
            }
            else
            {
                divisor++;
            }
        } while (divisor <= number);
    }

    public static long[] Factors(long number)
    {
        return Divisors(number).ToArray();
    }
}