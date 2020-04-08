using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Rectangles
{
    private static List<int> FindCornersInRow(string row) =>
        new Regex(@"(?=[+]{1}[-]*[+]{1})[+]-*").Matches(row).Select(match => match.Index).Append(row.LastIndexOf("+")).ToList();

    private static List<(int min, int max)> GetAllPossibleCombinations(List<int> indexes)
    {
        var temp = new List<int>(indexes);
        var result = new List<(int min, int max)>();
        indexes.ForEach(index =>
        {
            temp.Remove(index);
            result.AddRange(temp.Select(max => (index, max)));
        });
        return result;
    }

    private static List<(int min, int max)> GetNewCandidates(string row) =>
        GetAllPossibleCombinations(FindCornersInRow(row));

    public static int Count(string[] rows)
    {
        var candidates = new List<(int min, int max)>();
        int total = 0;
        void RemoveInvalidCandidates(string row)
        {
            static bool IsCharacterInvalid(char character) => !"+|".Contains(character);
            candidates.RemoveAll(candidate => IsCharacterInvalid(row[candidate.min]) || IsCharacterInvalid(row[candidate.max]));
        }
        rows.ToList().ForEach(row =>
        {
            RemoveInvalidCandidates(row);
            List<(int min, int max)> newCandidates = GetNewCandidates(row);
            total += candidates.Count(previous => newCandidates.Contains(previous));
            candidates.AddRange(newCandidates);
        });
        return total;
    }
}