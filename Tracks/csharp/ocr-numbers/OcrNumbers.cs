using System;
using System.Collections.Generic;
using System.Linq;

public static class OcrNumbers
{
    public static Dictionary<int, Dictionary<string, List<int>>> candidatesByRowAndPattern = new Dictionary<int, Dictionary<string, List<int>>>
    {
        { 0, new Dictionary<string, List<int>> {
            { " _ ", new List<int> { 0, 2, 3, 5, 6, 7, 8, 9 } },
            { "   ", new List<int> { 1, 4 } },
        }},
        { 1, new Dictionary<string, List<int>> {
            { "| |", new List<int> { 0 } },
            { "  |", new List<int> { 1, 7 } },
            { " _|", new List<int> { 2, 3 } },
            { "|_|", new List<int> { 4, 8, 9 } },
            { "|_ ", new List<int> { 5, 6 } },
        }},
        { 2, new Dictionary<string, List<int>> {
            { "|_|", new List<int> { 0, 6, 8 } },
            { "  |", new List<int> { 1, 4, 7 } },
            { "|_ ", new List<int> { 2 } },
            { " _|", new List<int> { 3, 5, 9 } },
        }}
    };

    public static List<int> initialCandidates = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public static string Convert(string input)
    {
        var rows = input.Split('\n');

        if (rows.Length % 4 != 0) throw new ArgumentException();
        if (rows.Any(row => row.Length % 3 != 0)) throw new ArgumentException();

        var columnGroups = Enumerable.Range(0, rows.First().Length / 3);

        var initialCandidatesOnRowGroup = columnGroups.Select(_ => new List<int>(initialCandidates));

        static string GetCharacterOnColumnGroup(IEnumerable<int> list) => list.Count() == 1 ? list.First().ToString() : "?";

        IEnumerable<IEnumerable<int>> GetNumberOnRowGroup(IEnumerable<IEnumerable<int>> currentCandidates, (string row, int index) rowIndex) =>
            currentCandidates.Select((candidateList, index) =>
            {
                var currentColumnKey = string.Concat(rowIndex.row.Skip(index * 3).Take(3));
                var candidatesInRowAndColumn = candidatesByRowAndPattern[rowIndex.index].TryGetValue(currentColumnKey, out List<int> candidates) ? candidates : new List<int>();
                return candidateList.Where(candidate => candidatesInRowAndColumn.Contains(candidate));
            });

        IEnumerable<(string row, int index)> GetRowGroup(int rowGroup) => rows.Skip(4 * rowGroup).Take(3).Select((row, index) => (row, index));

        IEnumerable<string> GetCharactersOnRowGroup(int rowIndex) => GetRowGroup(rowIndex)
            .Aggregate<(string row, int index), IEnumerable<IEnumerable<int>>>(initialCandidatesOnRowGroup, GetNumberOnRowGroup)
            .Select(GetCharacterOnColumnGroup);

        var rowGroups = Enumerable.Range(0, rows.Length / 4);
        string GetRowGroupText(int rowIndex) => string.Concat(GetCharactersOnRowGroup(rowIndex));

        return string.Join(",", rowGroups.Select(GetRowGroupText));
    }
}