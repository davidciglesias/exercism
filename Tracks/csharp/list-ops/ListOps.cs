using System;
using System.Collections.Generic;
using System.Linq;

public static class ListOps
{
    public static int Length<T>(List<T> input) => Foldl(input, 0, (prev, _) => prev + 1);

    public static List<T> Reverse<T>(List<T> input) => Foldl<T, IEnumerable<T>>(input, new List<T>(), (list, current) => list.Prepend(current)).ToList();

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map) => Foldl<TIn, IEnumerable<TOut>>(input, new List<TOut>(), (list, current) => list.Append(map(current))).ToList();

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate) => Foldl<T, IEnumerable<T>>(input, new List<T>(), (list, current) => predicate(current) ? list.Append(current) : list).ToList();

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        TOut result = start;

        foreach (TIn element in input)
        {
            result = func(result, element);
        }

        return result;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func) => Foldl(Reverse(input), start, (TOut tout, TIn tin) => func(tin, tout));

    public static List<T> Concat<T>(List<List<T>> input) => Foldl<List<T>, IEnumerable<T>>(input, new List<T>(), (list, current) => Append(list.ToList(), current)).ToList();

    public static List<T> Append<T>(List<T> left, List<T> right) => Foldl<T, IEnumerable<T>>(right, left, (list, current) => list.Append(current)).ToList();

}