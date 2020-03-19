using System.Linq;
using System;
using System.Collections.Generic;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        return multiples.Aggregate<int, HashSet<int>>(new HashSet<int>(), (set, multiple) =>
        {
            if (max / multiple != 0)
            {
                set.UnionWith(Enumerable.Range(1, (max / multiple)).Select(element => element * multiple));
            }
            return set;
        }).Aggregate(0, (sum, uniqueMultiple) => sum + uniqueMultiple);
    }
}