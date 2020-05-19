using System;
using System.Collections.Generic;
using System.Linq;

public enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

public static class YachtGame
{
    private static Func<int[], int> NumberTimesCount(int number) => (int[] dice) => number * dice.Count(roll => roll == number);
    private static int FullHouse(int[] dice) => dice.ToLookup(roll => roll).All(group => group.Count() == 2 || group.Count() == 3) ? dice.Sum() : 0;
    private static int FourOfAKind(int[] dice) => dice.ToLookup(roll => roll).Where(group => group.Count() >= 4).Sum(group => 4 * group.Key);
    private static int LittleStraight(int[] dice) => dice.ToLookup(roll => roll).All(group => group.Count() == 1 && group.Key < 6) ? 30 : 0;
    private static int BigStraight(int[] dice) => dice.ToLookup(roll => roll).All(group => group.Count() == 1 && group.Key > 1) ? 30 : 0;
    private static int Choice(int[] dice) => dice.Sum();
    private static int Yacht(int[] dice) => dice.ToLookup(roll => roll).Any(group => group.Count() < 5) ? 0 : 50;

    private static readonly Dictionary<YachtCategory, Func<int[], int>> scoresByCategory = new Dictionary<YachtCategory, Func<int[], int>>
    {
        { YachtCategory.Ones, NumberTimesCount(1) },
        { YachtCategory.Twos, NumberTimesCount(2) },
        { YachtCategory.Threes, NumberTimesCount(3) },
        { YachtCategory.Fours, NumberTimesCount(4) },
        { YachtCategory.Fives, NumberTimesCount(5) },
        { YachtCategory.Sixes, NumberTimesCount(6) },
        { YachtCategory.FullHouse, FullHouse },
        { YachtCategory.FourOfAKind, FourOfAKind },
        { YachtCategory.LittleStraight, LittleStraight },
        { YachtCategory.BigStraight, BigStraight },
        { YachtCategory.Choice, Choice },
        { YachtCategory.Yacht, Yacht },
    };

    public static int Score(int[] dice, YachtCategory category) => scoresByCategory[category](dice);
}

