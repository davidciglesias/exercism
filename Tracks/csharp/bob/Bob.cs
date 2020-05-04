using System.Linq;
using System.Text.RegularExpressions;

public static class Bob
{
    private static bool IsYelling(this string statement) => new Regex("[A-Z]").IsMatch(statement) && statement == statement.ToUpper();

    private static bool IsSilent(this string statement) => string.IsNullOrWhiteSpace(statement);

    private static bool IsQuestion(this string statement) => statement.TrimEnd().Last() == '?';

    public static string Response(string statement)
    {
        if(statement.IsSilent())
        {
            return "Fine. Be that way!";
        }

        if(statement.IsQuestion())
        {
            return statement.IsYelling() ? "Calm down, I know what I'm doing!" : "Sure.";
        }

        return statement.IsYelling() ? "Whoa, chill out!" : "Whatever.";
    }
}