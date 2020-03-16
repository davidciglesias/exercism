using System;

public static class Grains
{
    public static int squares = 64;
    public static ulong Square(int n, bool allowGreaterThan64 = false) {
        if(n <= 0 || (n > 64 && !allowGreaterThan64)) 
        {
            throw new ArgumentOutOfRangeException();
        }
        return (ulong)Math.Pow(2, n - 1);
    }

    public static ulong Total() => Square(squares + 1, allowGreaterThan64: true) - 1;
}