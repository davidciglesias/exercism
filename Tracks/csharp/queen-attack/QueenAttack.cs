using System;
using System.Collections.Generic;
using System.Numerics;

public class Queen
{
    public Queen(int row, int column)
    {
        if (row < 0 || column < 0 || row > 7 || column > 7) throw new ArgumentOutOfRangeException("Invalid positions");
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black)
    {
        var normalizedAbsolute = Vector2.Abs(Vector2.Normalize(new Vector2(Math.Abs(white.Row - black.Row), Math.Abs(white.Column - black.Column))));
        return new List<Vector2> { Vector2.UnitX, Vector2.UnitY, Vector2.Normalize(Vector2.One) }.Contains(normalizedAbsolute);
    }

    public static Queen Create(int row, int column) => new Queen(row, column);
}