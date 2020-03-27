using System.Collections.Generic;
using System.Linq;

public static class Proverb
{
    private static IEnumerable<string> Compose(this IEnumerable<string> subjects)
    {
        string initial = null;
        string previous = null;
        foreach (string subject in subjects)
        {
            if (initial == null)
            {
                initial = subject;
                previous = subject;
            }
            else
            {
                string verse = $"For want of a {previous} the {subject} was lost.";
                previous = subject;
                yield return verse;
            }
        }
        if (initial != null) yield return $"And all for the want of a {initial}.";
    }

    public static string[] Recite(string[] subjects) => subjects.Compose().ToArray();
}