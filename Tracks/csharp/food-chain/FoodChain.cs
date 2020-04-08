using System;
using System.Collections.Generic;
using System.Linq;

public static class FoodChain
{
    private enum Animal
    {
        fly = 1, spider, bird, cat,
        dog, goat, cow, horse
    }

    private static string AnimalToString(this Animal which) => which == Animal.spider ?
        "spider that wriggled and jiggled and tickled inside her" : Enum.GetName(typeof(Animal), which);

    private static string WhatDidSheSwallow(this Animal what) => $"I know an old lady who swallowed a {what}.";

    private static string HowDidSheSwallowIt(this Animal what) => what switch
    {
        Animal.spider => "It wriggled and jiggled and tickled inside her.",
        Animal.bird => "How absurd to swallow a bird!",
        Animal.cat => "Imagine that, to swallow a cat!",
        Animal.dog => "What a hog, to swallow a dog!",
        Animal.goat => "Just opened her throat and swallowed a goat!",
        Animal.cow => "I don't know how she swallowed a cow!",
        _ => "",
    };

    private static string WhyDidSheSwallowIt(this Animal what) => $"She swallowed the {what} to catch the {(what - 1).AnimalToString()}.";

    private static string WhatHappenedLast(this Animal what) => what == Animal.horse ?
        "She's dead, of course!" : "I don't know why she swallowed the fly. Perhaps she'll die.";

    private static bool IsLastAnimal(this Animal what) => what == Animal.fly || what == Animal.horse;

    public static string Recite(int verseNumber)
    {
        Animal currentAnimal = (Animal)verseNumber;
        var lineList = new List<string>
        {
            currentAnimal.WhatDidSheSwallow(),
            currentAnimal.HowDidSheSwallowIt()
        };
        while (!currentAnimal.IsLastAnimal())
        {
            lineList.Add(currentAnimal.WhyDidSheSwallowIt());
            currentAnimal--;
        }
        lineList.Add(currentAnimal.WhatHappenedLast());
        lineList.RemoveAll(string.IsNullOrEmpty);
        return string.Join("\n", lineList);
    }

    public static string Recite(int startVerse, int endVerse) =>
        string.Join("\n\n", Enumerable.Range(startVerse, endVerse - startVerse + 1).Select((_, index) => Recite(index + startVerse)));
}