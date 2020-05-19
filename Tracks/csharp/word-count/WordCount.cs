using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class WordCount
{
    public static IDictionary<string, int> CountWords(string phrase) => new Regex(@"\w+('\w)*")
        .Matches(phrase.ToLower()).ToLookup(match => match.Value)
        .ToDictionary(lookUp => lookUp.Key, lookUp => lookUp.Count());
}