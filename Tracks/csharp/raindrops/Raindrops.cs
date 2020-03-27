using System.Collections.Generic;
using System.Linq;

public static class Raindrops
{
    private static readonly SortedDictionary<int, string> plinger = new SortedDictionary<int, string> { { 3, "Pling" }, { 5, "Plang" }, { 7, "Plong" } };
    public static string Convert(int number) => string.Join("", plinger.Where((keyPair) => number % keyPair.Key == 0)
                                                                       .Select((keyPair) => keyPair.Value)
                                                                       .DefaultIfEmpty(number.ToString()));
}