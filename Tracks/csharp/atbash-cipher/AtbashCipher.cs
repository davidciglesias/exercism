using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class AtbashCipher
{
    private static string Clean(string input) => new Regex(@"[^(\w\d)]").Replace(input.ToLower(), "");

    private static readonly IEnumerable<char> dictionary = Enumerable.Range('a', 'z' - 'a' + 1).Select(letter => (char)letter);

    private static readonly Dictionary<char, char> mapper = dictionary
        .Select((letter, index) => (letter, index))
        .ToDictionary(pair => pair.letter, pair => dictionary.ElementAt(dictionary.Count() - pair.index - 1));

    public static string Encode(string plainValue) => Clean(plainValue).Aggregate
        (
            (text: "", count: 0), ((string text, int count) prev, char letter) =>
                ($"{prev.text}{(prev.count == 5 ? " " : "")}{(mapper.TryGetValue(letter, out char value) ? value : letter)}", prev.count == 5 ? 1 : prev.count + 1)
        ).text;

    public static string Decode(string encodedValue) =>
        string.Concat(Clean(encodedValue).Select(letter => mapper.TryGetValue(letter, out char value) ? value : letter));
}
