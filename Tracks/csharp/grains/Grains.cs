using System;

public static class Grains
{
    public static int squares = 64;
    public static ulong Square(int n)
    {
        if (n <= 0 || n > 64)
        {
            throw new ArgumentOutOfRangeException();
        }
        return (ulong)1 << (n - 1);
    }

    public static ulong Total() => Square(squares) * 2 - 1;
}