using System;
using System.Collections.Generic;
using System.Linq;

public class HighScores
{
    private readonly List<int> scores;
    public HighScores(List<int> list) => scores = list;

    public List<int> Scores() => scores;

    public int Latest() => scores.Last();

    public int PersonalBest() => scores.Max();

    public List<int> PersonalTopThree()
    {
        var sortedScores = new List<int>(scores);
        sortedScores.Sort((a, b) => b - a);
        return sortedScores.Take(3).ToList();
    }
}