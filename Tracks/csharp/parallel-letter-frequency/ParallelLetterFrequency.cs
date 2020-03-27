using System.Linq;
using System.Collections.Generic;

public static class ParallelLetterFrequency
{
    private const bool PARALLEL = true;

    public static Dictionary<char, int> Calculate(IEnumerable<string> texts) => PARALLEL ? CalculateParallel(texts) : CalculateWithoutParallelism(texts);

    public static Dictionary<char, int> CalculateParallel(IEnumerable<string> texts) => texts
                .AsParallel()
                .SelectMany(text => text.Trim().ToLower())
                .Where(character => char.IsLetter(character))
                .GroupBy(character => character)
                .ToDictionary((character) => character.Key, (character) => character.Count());

    public static Dictionary<char, int> CalculateWithoutParallelism(IEnumerable<string> texts) => texts
                .SelectMany(text => text.Trim().ToLower())
                .Where(character => char.IsLetter(character))
                .GroupBy(character => character)
                .ToDictionary(character => character.Key, character => character.Count());
}