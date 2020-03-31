using System;
using System.Linq;
using System.Collections.Generic;

public enum Students
{
    Alice = 0, Bob = 2, Charlie = 4, David = 6,
    Eve = 8, Fred = 10, Ginny = 12, Harriet = 14,
    Ileana = 16, Joseph = 18, Kincaid = 20, Larry = 22
}

public enum Plant
{
    Violets = 'V',
    Radishes = 'R',
    Clover = 'C',
    Grass = 'G'
}

public class KindergartenGarden
{
    private readonly string[] diagramByLevel;

    public KindergartenGarden(string diagram) => diagramByLevel = diagram.Split("\n");

    public IEnumerable<Plant> Plants(string student)
    {
        if (!Enum.TryParse(typeof(Students), student, out object column)) throw new ArgumentException("That is not one of my students!");
        return diagramByLevel.Select(row => string.Concat(row[(int)column], row[(int)column + 1]))
                             .SelectMany(plantsForStudentByRow => plantsForStudentByRow)
                             .Select(current => (Plant)current);
    }
}