using System.Collections.Generic;
using System.Linq;

public static class ResistorColor
{
    private static Dictionary<string, int> colorMapper = new Dictionary<string, int>
    {
        {"black", 0},
        {"brown", 1},
        {"red", 2},
        {"orange", 3},
        {"yellow", 4},
        {"green", 5},
        {"blue", 6},
        {"violet", 7},
        {"grey", 8},
        {"white", 9}
    };

    public static int ColorCode(string color) => colorMapper[color];

    public static string[] Colors() => colorMapper.Keys.ToArray();
}