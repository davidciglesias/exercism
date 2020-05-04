using System;
using System.Collections.Generic;
using System.Linq;

public static class SaddlePoints
{
    public static IEnumerable<(int, int)> Calculate(int[,] matrix)
    {
        (var rowRange, var columnRange) = (Enumerable.Range(0, matrix.GetLength(0)), Enumerable.Range(0, matrix.GetLength(1)));

        IEnumerable<int> GetAggregateFromLine(bool useRows, Func<IEnumerable<int>, int> func) => 
            (useRows ? rowRange : columnRange).Select(index1 => func((useRows ? columnRange : rowRange).Select(index2 => useRows ? matrix[index1, index2] : matrix[index2, index1])));

        (IEnumerable<int> maxInRow, IEnumerable<int> minInColumn) = (GetAggregateFromLine(true, (IEnumerable<int> input) => input.Max()), GetAggregateFromLine(false, (IEnumerable<int> input) => input.Min()));

        bool IsSaddlePoint(int rowIndex, int columnIndex) => matrix[rowIndex, columnIndex] == maxInRow.ElementAt(rowIndex) && matrix[rowIndex, columnIndex] == minInColumn.ElementAt(columnIndex);

        return rowRange.SelectMany(rowIndex => columnRange.Where(columnIndex => IsSaddlePoint(rowIndex, columnIndex)).Select(columnIndex => (rowIndex + 1, columnIndex + 1)));
    }
}
