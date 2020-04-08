using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public enum ConnectWinner
{
    White = 'O',
    Black = 'X',
    None = '.',
}

public class Connect
{

    private readonly ConnectWinner[][] matrix;
    private readonly int lastRow, lastColumn;

    public Connect(string[] input)
    {
        matrix = input.Select(line => new Regex(@"\s+").Replace(line.Trim(), string.Empty)
            .Select(character => (ConnectWinner)character)
            .ToArray()
        ).ToArray();

        lastRow = matrix.Length - 1;
        lastColumn = matrix.FirstOrDefault().Length - 1;
    }

    public ConnectWinner Result() =>
        Enumerable.Range(0, lastColumn + 1).Any(column => IsNodeConnectedToBeginning((lastRow, column), ConnectWinner.White, new List<(int row, int col)>()))
                ? ConnectWinner.White
                : Enumerable.Range(0, lastRow + 1).Any(row => IsNodeConnectedToBeginning((row, lastColumn), ConnectWinner.Black, new List<(int row, int col)>()))
                ? ConnectWinner.Black
                : ConnectWinner.None;

    private bool IsNodeConnectedToBeginning((int row, int col) current, ConnectWinner candidate, List<(int row, int col)> previous)
    {
        if (!IsWithinBoundaries(current) || previous.Contains(current) || matrix[current.row][current.col] != candidate) return false;

        if (IsFirstNode(candidate, current)) return true;

        previous.Add(current);

        return GetSurroundingCandidates(current).Any(node => IsNodeConnectedToBeginning(node, candidate, previous));
    }

    private bool IsFirstNode(ConnectWinner candidate, (int row, int col) nodePath) =>
        candidate == ConnectWinner.Black && nodePath.col == 0 || candidate == ConnectWinner.White && nodePath.row == 0;

    private bool IsWithinBoundaries((int row, int col) pair) => pair.row >= 0 && pair.row <= lastRow && pair.col >= 0 && pair.col <= lastColumn;

    private readonly (int, int)[] surrounds = new[] { (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0) };

    private IEnumerable<(int row, int col)> GetSurroundingCandidates((int row, int col) p) => surrounds
        .Select<(int row, int col), (int row, int col)>(pair => (row: p.row + pair.row, col: p.col + pair.col));
}