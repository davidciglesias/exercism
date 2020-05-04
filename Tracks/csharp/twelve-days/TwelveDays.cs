using System;
using System.Collections.Generic;
using System.Linq;

public static class TwelveDays
{
    public static Dictionary<int, (string day, string newItem)> verseItem = new Dictionary<int, (string day, string newItem)>
    {
        { 1, ("first", "a Partridge in a Pear Tree.") },
        { 2, ("second", "two Turtle Doves") },
        { 3, ("third", "three French Hens") },
        { 4, ("fourth", "four Calling Birds") },
        { 5, ("fifth", "five Gold Rings") },
        { 6, ("sixth", "six Geese-a-Laying") },
        { 7, ("seventh", "seven Swans-a-Swimming") },
        { 8, ("eighth", "eight Maids-a-Milking") },
        { 9, ("ninth", "nine Ladies Dancing") },
        { 10, ("tenth", "ten Lords-a-Leaping") },
        { 11, ("eleventh", "eleven Pipers Piping") },
        { 12, ("twelfth", "twelve Drummers Drumming") }
    };


    public static string Recite(int verseNumber)
    {
        if (verseNumber < 1 || verseNumber > 12) throw new ArgumentOutOfRangeException("Verses must be between 1 and 12");

        (string day, string newItem) = verseItem[verseNumber];
        IEnumerable<string> previousItems = Enumerable.Range(1, verseNumber - 1)
                                                      .Select((verseNumber, index) => (index == 0 ? "and " : "") + verseItem[verseNumber].newItem)
                                                      .Append(newItem)
                                                      .Reverse();


        return $"On the {day} day of Christmas my true love gave to me: {string.Join(", ", previousItems)}";
    }

    public static string Recite(int startVerse, int endVerse) => 
        string.Join("\n", Enumerable.Range(startVerse, endVerse - startVerse + 1).Select(verseNumber => Recite(verseNumber)));
}