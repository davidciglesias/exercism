using System;
using System.Collections.Generic;
using System.Linq;

public static class House
{
    private static readonly SortedDictionary<int, string[]> nameAction = new SortedDictionary<int, string[]>
    {
        { 1, new string[2] { "house", "Jack built" } },
        { 2, new string[2] { "malt", "lay in" } },
        { 3, new string[2] { "rat", "ate" } },
        { 4, new string[2] { "cat", "killed" } },
        { 5, new string[2] { "dog", "worried" } },
        { 6, new string[2] { "cow with the crumpled horn", "tossed" } },
        { 7, new string[2] { "maiden all forlorn", "milked" } },
        { 8, new string[2] { "man all tattered and torn", "kissed" } },
        { 9, new string[2] { "priest all shaven and shorn", "married" } },
        { 10, new string[2] { "rooster that crowed in the morn", "woke" } },
        { 11, new string[2] { "farmer sowing his corn", "kept" } },
        { 12, new string[2] { "horse and the hound and the horn", "belonged to" } }
    };

    public static string Recite(int verseNumber) => "This is the " + string.Join(" the ", Enumerable.Range(1, verseNumber).Reverse().Select((verse) => string.Join(" that ", nameAction[verse]))) + ".";

    public static string Recite(int startVerse, int endVerse) => string.Join("\n", Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(verse => Recite(verse)));
}