using System.Collections.Generic;
using System.Linq;

public static class PascalsTriangle
{
    public static IEnumerable<IEnumerable<int>> Calculate(int rows)
    {
        var previousRow = new List<int>();
        for (int currentRow = 1; currentRow <= rows; currentRow++)
        {
            var newRow = new List<int> { 1 };
            if (currentRow == 1)
            {
                yield return newRow;
            }
            else
            {
                newRow.AddRange(Enumerable.Range(1, currentRow / 2 - 1).Select((column) => previousRow[column - 1] + previousRow[column]));
                var reverseRow = new List<int>(newRow);
                reverseRow.Reverse();
                if (currentRow % 2 != 0)
                {
                    newRow.Add(previousRow[currentRow / 2 - 1] + previousRow[currentRow / 2]);
                }
                newRow.AddRange(reverseRow);
                previousRow = newRow;
                yield return newRow;
            }
        }
    }
}