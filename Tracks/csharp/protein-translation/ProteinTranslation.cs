using System;
using System.Linq;
using System.Collections.Generic;

public static class ProteinTranslation
{
    private const string STOP = "STOP";
    private static string MapCodonToProtein(string codon)
    {
        switch (codon)
        {
            case "AUG": return "Methionine";
            case "UUU": case "UUC": return "Phenylalanine";
            case "UUA": case "UUG": return "Leucine";
            case "UCU": case "UCC": case "UCA": case "UCG": return "Serine";
            case "UAU": case "UAC": return "Tyrosine";
            case "UGU": case "UGC": return "Cysteine";
            case "UGG": return "Tryptophan";
            case "UAA": case "UAG": case "UGA": default: return STOP;
        }
    }

    private static IEnumerable<string> GetNextCodon(string strand)
    {
        int skip = 0;
        string newProtein;
        do
        {
            newProtein = MapCodonToProtein(string.Join("", strand.Skip(skip).Take(3)));
            if (newProtein != STOP)
            {
                skip += 3;
                yield return newProtein;
            }
        } while (newProtein != STOP && skip < strand.Length);
    }

    public static string[] Proteins(string strand)
    {
        return GetNextCodon(strand).ToArray();
    }
}