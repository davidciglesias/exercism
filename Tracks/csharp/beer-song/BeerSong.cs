using System.Linq;

public static class BeerSong
{
    private const string ITEM = "bottle";
    private const string CONTENT = "beer";
    private const string LOCATION = "wall";
    private static string ItemsOfContent(int left, bool onTheWall = false) => $"{Items(left)} of {CONTENT}{(onTheWall ? $" on the {LOCATION}" : "")}";

    private static string Items(int left) => left switch
    {
        1 => $"{left} {ITEM}",
        0 => $"No more {ITEM}s",
        _ => $"{left} {ITEM}s",
    };

    private static string Single(int left) => left switch
    {
        1 => "it",
        _ => "one"
    };

    private static string WhatDoWeDoWithThese(int left) => left switch
    {
        0 => $"Go to the store and buy some more, {ItemsOfContent(99, onTheWall: true).ToLower()}.",
        _ => $"Take {Single(left)} down and pass it around, {ItemsOfContent(left - 1, onTheWall: true).ToLower()}."
    };

    public static string Recite(int startBottles, int takeDown) => string.Join("\n\n", Enumerable.Range(0, takeDown).Select((index) =>
        {
            int left = startBottles - index;
            return $"{ItemsOfContent(left, onTheWall: true)}, {ItemsOfContent(left).ToLower()}.\n{WhatDoWeDoWithThese(left)}";
        }));
}