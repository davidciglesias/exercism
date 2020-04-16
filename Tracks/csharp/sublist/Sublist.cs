using System;
using System.Collections.Generic;
using System.Linq;

public enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

public static class Sublist
{
    public static bool Equals<T>(this List<T> list1, List<T> list2) where T : IComparable =>
        list1.Select((element, index) => (element, index)).All(pair => list2[pair.index].CompareTo(pair.element) == 0);

    public static List<List<T>> ToSublistOfSize<T>(this List<T> list, int size) =>
        Enumerable.Range(0, list.Count - size + 1).Select(index => list.GetRange(index, size)).ToList();

    public static SublistType Classify<T>(List<T> list1, List<T> list2) where T : IComparable =>
        Math.Sign(list1.Count - list2.Count) switch
        {
            1 => list1.ToSublistOfSize(list2.Count).Any(list => list.Equals<T>(list2)) ? SublistType.Superlist : SublistType.Unequal,
            0 => list1.Equals<T>(list2) ? SublistType.Equal : SublistType.Unequal,
            -1 => list2.ToSublistOfSize(list1.Count).Any(list => list.Equals<T>(list1)) ? SublistType.Sublist : SublistType.Unequal,
        };
}