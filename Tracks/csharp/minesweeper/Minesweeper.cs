using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class Point
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Point(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public bool Equals([AllowNull] Point other) => other.Row == Row && other.Column == Column;

    public Point[] SurroundingPoints(int maxRow, int maxColumn)
    {
        bool IsWithinBoundaries(Point point) => point.Row <= maxRow && point.Row >= 0 && point.Column <= maxColumn && point.Column >= 0;

        int[] options = new[] { -1, 1, 0 };
        return options
            .SelectMany
            (
                optionRow => options
                    .Select(optionColumn => new Point(Row + optionRow, Column + optionColumn))
                    .Where(IsWithinBoundaries)
            )
            .SkipLast(1)
            .ToArray();
    }
}



public class PointComparer : IEqualityComparer<Point>
{
    public bool Equals([AllowNull] Point x, [AllowNull] Point y) => x.Equals(y);

    public int GetHashCode([DisallowNull] Point obj) => 31 * obj.Column + obj.Row;
}

public static class Minesweeper
{
    public static string[] Annotate(string[] input)
    {
        string[] result = input.ToArray();
        static bool IsAsterisk(char candidate) => candidate == '*';
        IEnumerable<Point> asteriskPoints = input.SelectMany
        (
            (columns, row) => columns
                .Select((character, column) => IsAsterisk(character) ? new Point(row, column) : null)
                .Where(point => point != null)
        );

        if (asteriskPoints.Count() == 0) return input;

        int maxRow = input.Length - 1;
        int maxColumn = input.FirstOrDefault().Length - 1;

        void IncreaseValue(Point point)
        {
            char[] line = result[point.Row].ToCharArray();
            var characterAtPoint = line[point.Column];
            int.TryParse("" + characterAtPoint, out var digit);
            line[point.Column] = char.Parse("" + (digit + 1));
            result[point.Row] = new string(line);
        }

        asteriskPoints.ToList().ForEach
        (
            asteriskPoint => asteriskPoint
                .SurroundingPoints(maxRow, maxColumn)
                .Except(asteriskPoints, new PointComparer())
                .ToList()
                .ForEach(IncreaseValue)
        );

        return result;
    }
}
