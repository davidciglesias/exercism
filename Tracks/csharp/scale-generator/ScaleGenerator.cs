using System;
using System.Collections.Generic;
using System.Linq;

public static class ScaleGenerator
{
    private static readonly Dictionary<char, bool[]> skipNew = new Dictionary<char, bool[]>
    {
        ['A'] = new[] { true, false, false }, ['M'] = new[] { true, false }, ['m'] = new[] { true }
    };

    private static readonly string[] UseSharps = { "G", "D", "A", "E", "B", "F#", "e", "b", "f#", "c#", "g#", "d#", "a", "C" };
    private static readonly string[] UseFlats = { "F", "Bb", "Eb", "Ab", "Db", "Gb", "d", "g", "c", "f", "bb", "eb" };

    public static readonly List<string> SharpPitches = new List<string> { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
    public static readonly List<string> FlatPitches = new List<string> { "A", "Bb", "B", "C", "Db", "D", "Eb", "E", "F", "Gb", "G", "Ab" };

    public static string[] Chromatic(string tonic)
    {
        string tonicAsUpper = char.ToUpper(tonic.First()) + tonic.Remove(0, 1);

        string[] GetListFromTonic(List<string> list) => 
            Enumerable.Range(0, list.Count).Select(index => list[(index + list.IndexOf(tonicAsUpper)) % list.Count]).ToArray();

        if (UseSharps.Contains(tonic))
        {
            return GetListFromTonic(SharpPitches);
        }

        if (UseFlats.Contains(tonic))
        {
            return GetListFromTonic(FlatPitches);
        }

        throw new ArgumentException($"The note {tonic} is not valid");
    }

    public static string[] Interval(string tonic, string pattern)
    {
        if (pattern.Any(character => !skipNew.ContainsKey(character)))
        {
            throw new ArgumentException("Wrong pattern");
        }
        string[] chromatic = Chromatic(tonic);
        return chromatic.Concat(chromatic)
                        .Zip(pattern.SelectMany(character => skipNew[character]), (tone, shouldTake) => (tone, shouldTake))
                        .Where(pair => pair.shouldTake).Select(pair => pair.tone).ToArray();
    }
}