using System;
using System.Collections.Generic;

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

    public static string[] Colors()
    {
        string[] colors = new string[colorMapper.Count];
        colorMapper.Keys.CopyTo(colors, 0);
        return colors;
    }
}