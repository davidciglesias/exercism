using System.Collections.Generic;
using System.Linq;

public static class Darts
{
    private static double SquaredRadius(double x, double y) => x * x + y * y;
    private static readonly Dictionary<double, int> pointMap = new Dictionary<double, int>
    {
        { 1, 10 },
        { 25, 5 },
        { 100, 1 },
    };
    public static int Score(double x, double y)
    {
        double squaredRadius = SquaredRadius(x, y);
        return pointMap.FirstOrDefault((keyPair) => squaredRadius <= keyPair.Key).Value;
    }
}
