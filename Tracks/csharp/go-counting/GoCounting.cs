using System;
using System.Collections.Generic;
using System.Linq;

public enum Owner
{
    None = ' ',
    Black = 'B',
    White = 'W',
    Both
}

public class GoCounting
{
    private readonly Dictionary<Owner, IEnumerable<(int, int)>> territories = new Dictionary<Owner, IEnumerable<(int, int)>>();
    private readonly Dictionary<(int, int), Owner> cells = new Dictionary<(int, int), Owner>();

    public GoCounting(string board)
    {
        string[] rows = board.Split("\n");
        cells = rows
            .SelectMany((row, rowIndex) => row.Select((cell, columnIndex) => new KeyValuePair<(int, int), Owner>((columnIndex, rowIndex), (Owner)cell)))
            .ToDictionary(keyValuePair => keyValuePair.Key, keyValuePair => keyValuePair.Value);
    }

    public IEnumerable<(int, int)> GetNeighbourCoords((int x, int y) coord) =>
        Enumerable.Range(-1, 3).SelectMany(x => Enumerable.Range(-1, 3).Where(y => Math.Abs(x) + Math.Abs(y) == 1).Select(y => (coord.x + x, coord.y + y)));

    public Tuple<Owner, IEnumerable<(int, int)>> Territory((int, int) coord)
    {
        if (!cells.TryGetValue(coord, out Owner cellOwner))
        {
            throw new ArgumentException();
        }
        if (cellOwner != Owner.None) return (Owner.None, new List<(int, int)>().AsEnumerable()).ToTuple();

        Owner territoryOwner = Owner.None;
        IEnumerable<(int, int)> territory = new List<(int, int)> { coord };
        IEnumerable<(int, int)> checkedCells = new List<(int, int)> { coord };

        void GetNeighbours((int, int) coord)
        {
            foreach (var neighbour in GetNeighbourCoords(coord).Where(neighbour => cells.ContainsKey(neighbour) && !checkedCells.Contains(neighbour)))
            {
                Owner neighbourStone = cells[neighbour];
                checkedCells = checkedCells.Append(neighbour);
                switch (territoryOwner)
                {
                    case Owner.None:
                        territoryOwner = neighbourStone;
                        break;
                    case Owner.Black:
                        territoryOwner = neighbourStone == Owner.White ? Owner.Both : Owner.Black;
                        break;
                    case Owner.White:
                        territoryOwner = neighbourStone == Owner.Black ? Owner.Both : Owner.White;
                        break;
                    case Owner.Both:
                        break;
                }

                if (neighbourStone == Owner.None)
                {
                    territory = territory.Append(neighbour);
                    GetNeighbours(neighbour);
                }
            }
        }

        GetNeighbours(coord);

        territory = territory.OrderBy(((int x, int y) coord) => coord.x).ThenBy(((int x, int y) coord) => coord.y);
        territoryOwner = territoryOwner == Owner.Both ? Owner.None : territoryOwner;

        return (territoryOwner, territory).ToTuple();

    }

    public Dictionary<Owner, IEnumerable<(int, int)>> Territories()
    {
        var result = new Dictionary<Owner, IEnumerable<(int, int)>>
        {
            { Owner.Black, new List<(int, int)>() },
            { Owner.White, new List<(int, int)>() },
            { Owner.None, new List<(int, int)>() },
        };
        var checkedCoords = new List<(int, int)>();
        foreach (var coord in cells.Where(keyValuePair => keyValuePair.Value == Owner.None && !checkedCoords.Contains(keyValuePair.Key)).Select(keyValuePair => keyValuePair.Key))
        {
            var (owner, territory) = Territory(coord);
            checkedCoords.AddRange(territory);
            result[owner] = result[owner].Concat(territory);
        }
        return result;
    }
}
