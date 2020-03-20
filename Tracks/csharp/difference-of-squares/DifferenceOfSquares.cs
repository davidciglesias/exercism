using System;

public static class DifferenceOfSquares
{
    private static long CalculateSum(int max) => max * (max + 1) / 2;
   
    public static long CalculateSquareOfSum(int max) => CalculateSum(max) * CalculateSum(max);

    public static long CalculateSumOfSquares(int max) => CalculateSum(max) * (2 * max + 1) / 3;

    public static long CalculateDifferenceOfSquares(int max) => CalculateSum(max) * (3 * (max * max) - max - 2) / 6;
}