using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext) =>
        new Regex(@"[^a-z0-9]").Replace(plaintext.ToLower(), "");

    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        int length = plaintext.Length;
        int columns = (int)Math.Ceiling(Math.Sqrt(length));
        int rows = (int)Math.Ceiling((double)length / columns);
        if (rows <= 1) yield return plaintext;
        else
        {
            string refilledPlainText = string.Concat(plaintext, string.Concat(Enumerable.Range(0, columns * rows - length).Select(_ => " ")));
            var filter = Enumerable.Range(0, rows).SelectMany(_ => Enumerable.Range(0, columns));
            yield return string.Join(" ", 
                refilledPlainText.Zip(filter, (character, filter) => (character, filter))
                                 .GroupBy(pair => pair.filter)
                                 .Select(group => string.Concat(group.Select(pair => pair.character))));
        }
    }

    public static string Ciphertext(string plaintext) =>
        string.Join(" ", PlaintextSegments(NormalizedPlaintext(plaintext)).ToList());
}