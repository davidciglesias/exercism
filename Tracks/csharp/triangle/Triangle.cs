using System.Collections.Generic;
using System.Linq;

public static class Triangle
{
    private static bool IsTriangle(double side1, double side2, double side3)
    {
        var sides = new List<double> { side1, side2, side3 };
        return sides.All((side) => side > 0) && sides.Sum() > 2 * sides.Max();
    }
    public static bool IsScalene(double side1, double side2, double side3) => IsTriangle(side1, side2, side3) && !IsIsosceles(side1, side2, side3);

    public static bool IsIsosceles(double side1, double side2, double side3) => IsTriangle(side1, side2, side3) && (side1 == side2 || side1 == side3 || side2 == side3);

    public static bool IsEquilateral(double side1, double side2, double side3) => IsTriangle(side1, side2, side3) && side1 == side2 && side1 == side3;
}