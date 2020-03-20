using System.Linq;
using System;
using System.Collections.Generic;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) => multiples
        .Where(multiple => multiple > 0 && max > multiple)
        .SelectMany(multiple => Enumerable.Range(1, (max - 1) / multiple).Select(element => element * multiple))
        .Distinct()
        .Sum();
}