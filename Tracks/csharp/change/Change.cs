using System;
using System.Collections.Generic;
using System.Linq;

public static class Change
{
    public static int[] FindFewestCoins(int[] coins, int target)
    {
        if (target == 0) return new List<int>().ToArray();

        if (target < 0 || target < coins.Min()) throw new ArgumentException();

        var minimalCoins = new int[target + 1][];
        minimalCoins[0] = new int[0];

        for (var amount = 1; amount <= target; amount++)
        {
            minimalCoins[amount] = (coins.Where(coin => coin <= amount)
                                       .Select(coin => minimalCoins[amount - coin].Prepend(coin))
                                       .OrderBy(change => change.Count())
                                       .FirstOrDefault(change => change.Sum() == amount)
                                        ?? new int[0]).ToArray();
        }

        return minimalCoins[target].Count() > 0 ? minimalCoins[target] : throw new ArgumentException();
    }
}