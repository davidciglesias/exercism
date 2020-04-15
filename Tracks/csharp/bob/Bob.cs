using System.Linq;
using System.Text.RegularExpressions;

public static class Bob
{
    private static bool IsYelling(string statement) => new Regex("[A-Z]").IsMatch(statement) && statement == statement.ToUpper();

    public static string Response(string statement) =>
        string.IsNullOrWhiteSpace(statement)
            ? "Fine. Be that way!"
            : statement.TrimEnd().Last() == '?'
            ? IsYelling(statement) ? "Calm down, I know what I'm doing!" : "Sure."
            : IsYelling(statement) ? "Whoa, chill out!" : "Whatever.";
}