using System.Linq;
using System.Text.RegularExpressions;

public static class IsbnVerifier
{
    public static bool IsValid(string number)
    {
        var match = new Regex(@"^\d{1}-?\d{3}-?\d{5}-?[\dX]{1}$").Match(number);
        if (!match.Success) return false;

        static (int number, int index) ConvertToDigit(char digitOrX, int index) => (digitOrX == 'X' ? 10 : int.Parse("" + digitOrX), index);
        static int AggregateNumbers(int previous, (int number, int index) current) => previous + current.number * (10 - current.index);

        return match.Value.Where(char.IsLetterOrDigit).Select(ConvertToDigit).Aggregate(0, AggregateNumbers) % 11 == 0;
    }
}