using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class AffineCipher
{
    private static readonly string alphabet = "abcdefghijklmnopqrstuvwxyz";
    private static readonly string characterPattern = $@"[{alphabet}0-9]+";
    private static readonly int m = alphabet.Length;
    private static readonly IEnumerable<int> mDivisors = Divisors(m);

    private static IEnumerable<int> Divisors(int number)
    {
        int max = number / 2;
        for (int current = 2; current <= max; current++)
        {
            if (number % current == 0)
            {
                yield return current;
            }

            if (current == max)
            {
                yield return number;
            }
        }
    }

    private static bool IsCoprimeToM(this int a) => Divisors(a).All(aDivisor => !mDivisors.Contains(aDivisor));

    public static string Encode(string plainText, int a, int b)
    {
        if (!a.IsCoprimeToM())
        {
            throw new ArgumentException();
        }

        string Encoder(char character, int index) => (index > 0 && index % 5 == 0 ? " " : "") +
            (alphabet.Contains(character) ? (char)(((character - alphabet.First()) * a + b) % m + alphabet.First()) : character);

        var matches = new Regex(characterPattern).Matches(plainText.ToLower());
        return string.Concat(matches.SelectMany(match => match.Value).Select(Encoder));
    }

    private static int CalculateModularMultiplicativeInverse(this int a)
    {
        for (int n = 1; ; n++) { if ((a * n) % m == 1) return n; }
    }

    private static int ToModuleM(this int number) => number > 0 ? number % m :
        number + (int)Math.Ceiling((double)Math.Abs(number) / m) * m;

    public static string Decode(string cipheredText, int a, int b)
    {
        if (!a.IsCoprimeToM())
        {
            throw new ArgumentException();
        }

        int n = a.CalculateModularMultiplicativeInverse();

        char Decoder(char character, int index) => alphabet.Contains(character) ? 
            (char)((n * (character - alphabet.First() - b)).ToModuleM() + alphabet.First()) : character;

        return string.Concat(cipheredText.Where(char.IsLetterOrDigit).Select(Decoder));
    }
}
