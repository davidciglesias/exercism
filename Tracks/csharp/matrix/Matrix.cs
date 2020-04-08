using System;
using System.Collections.Generic;
using System.Linq;

public class Matrix
{
    private enum Lines { Row, Column }

    private readonly Dictionary<Lines, IEnumerable<IEnumerable<int>>> MatrixLines;

    public Matrix(string input)
    {
        IEnumerable<IEnumerable<int>> rows = input.Split("\n")
                                 .Select(row => row.Split(" ").Select(int.Parse));
        IEnumerable<IEnumerable<int>> columns = Enumerable.Range(0, rows.First().Count())
                                  .Select(index => rows.Select(row => row.ElementAt(index)));
        MatrixLines = new Dictionary<Lines, IEnumerable<IEnumerable<int>>>
        {
            { Lines.Row, rows }, { Lines.Column, columns },
        };
    }

    public int Rows => MatrixLines[Lines.Row].Count();

    public int Cols => MatrixLines[Lines.Column].Count();

    private int[] Line(int line, Lines lines) => MatrixLines[lines].ElementAt(line - 1).ToArray();

    public int[] Row(int row) => Line(row, Lines.Row);

    public int[] Column(int col) => Line(col, Lines.Column);
}