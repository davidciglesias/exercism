using System.Linq;
using System.Collections.Generic;

public static class Etl
{
    public static Dictionary<string, int> Transform(Dictionary<int, string[]> old) => 
        old.SelectMany((pointValues) => pointValues.Value.Select(letter => (pointValues.Key, letter)))
           .ToDictionary((pointValues) => pointValues.letter.ToLower(), (pointValues) => pointValues.Key);
}