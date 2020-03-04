using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class Acronym
{
    public static string Abbreviate(string phrase)
    {
        string pattern = "[ _-]+";
        var initials = Regex.Split(phrase, pattern, RegexOptions.IgnoreCase)
                         .Select((word) => word.ToUpper().FirstOrDefault());
        var acronym = string.Join("", initials);

        return acronym;
    }
}