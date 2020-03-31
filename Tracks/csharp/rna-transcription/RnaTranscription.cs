using System.Collections.Generic;
using System.Linq;

public static class RnaTranscription
{
    private static readonly Dictionary<char, char> getComplement = new Dictionary<char, char>
    {
        ['G'] = 'C', ['C'] = 'G', ['T'] = 'A', ['A'] = 'U',
    };

    public static string ToRna(string nucleotide) => 
        new string(nucleotide.Select(character => getComplement[character]).ToArray());
}