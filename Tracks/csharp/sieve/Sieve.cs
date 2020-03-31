using System;
using System.Linq;

public static class Sieve
{
    public static int[] Primes(int limit)
    {
        if (limit < 2) throw new ArgumentOutOfRangeException("Sorry, 2 is the bare minimum!");
        var primes = Enumerable.Range(2, limit - 1).ToList();
        for (int index = 0; index < primes.Count(); index++)
        {
            for(int current = primes.ElementAt(index), elementToRemove = 2 * current; elementToRemove <= primes.Last(); elementToRemove += current)
            {
                primes.Remove(elementToRemove);
            }
        }

        return primes.ToArray();
    }
}