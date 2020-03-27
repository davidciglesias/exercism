using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        if (new Regex("[^ACGT]").IsMatch(sequence)) throw new ArgumentException();
        return sequence.Concat("ACGT")
                      .GroupBy(character => character, (character, group) => (Character: character, Count: group.Count() - 1))
                      .ToDictionary((group) => group.Character, (group) => group.Count);
    }
}