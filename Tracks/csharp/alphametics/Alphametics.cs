using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

internal class EquationTerms
{
    public List<int> options = Enumerable.Range(0, 10).ToList();
    public Dictionary<char, List<int>> optionsAllowedPerLetter = new Dictionary<char, List<int>>();
    public List<Dictionary<char, int>> solutions = new List<Dictionary<char, int>>();
    public List<Dictionary<char, int>> letterCountPerIndex = new List<Dictionary<char, int>>();
    public char[] letters;
    public string[] leftTerms;
    public string rightTerm;
    public string[] allTerms;

    public EquationTerms(string[] leftTerms, string rightTerm)
    {
        this.leftTerms = leftTerms.OrderByDescending(term => term.Length).ToArray();
        this.rightTerm = rightTerm;
        allTerms = this.leftTerms.Append(this.rightTerm).ToArray();

        letters = string.Join("", allTerms).Distinct().OrderByDescending(letter => rightTerm.Reverse().ToList().LastIndexOf(letter)).ToArray();

        optionsAllowedPerLetter = letters.ToDictionary(letter => letter, _ => new List<int>(options));

        letterCountPerIndex = leftTerms
            .SelectMany(term => term.Reverse().Select((character, index) => new Tuple<int, char>(index, character)))
            .GroupBy(tuple => tuple.Item1, tuple => tuple.Item2)
            .Select(group => group.Distinct().ToDictionary(character => character, character => group.Count(current => current == character)))
            .ToList();
    }

    private void RemoveOtherOptionsForExclusiveChars() => options.ForEach(currentOption =>
        {
            IEnumerable<char> lettersThatContainCurrentOption =
                optionsAllowedPerLetter.Where(pair => pair.Value.Contains(currentOption)).Select(pair => pair.Key);
            IEnumerable<char> lettersThatHaveMoreThanOneOption =
                optionsAllowedPerLetter.Where(pair => pair.Value.Count() > 1).Select(pair => pair.Key);

            if (lettersThatContainCurrentOption.Count() == 1 && lettersThatHaveMoreThanOneOption.Count() > 1)
            {
                optionsAllowedPerLetter[lettersThatContainCurrentOption.First()].RemoveAll(option => option != currentOption);
            }
        });

    private void SimplifyWhenOverTenRepetitions() => letterCountPerIndex.Select((element, index) => new Tuple<int, Dictionary<char, int>>(index, element)).ToList()
        .ForEach(tuple =>
        {
            tuple.Item2.Where(pair => pair.Value >= 10).ToList().ForEach(pair =>
            {
                letterCountPerIndex[tuple.Item1][pair.Key] %= 10;
                if (letterCountPerIndex.Count < tuple.Item1 + 2)
                {
                    letterCountPerIndex.Add(new Dictionary<char, int> { { pair.Key, pair.Value / 10 } });
                }
                else
                {
                    letterCountPerIndex[tuple.Item1 + 1][pair.Key] += pair.Value / 10;
                }
            });
        });

    private void RemoveZeroToLeadingChars() => allTerms
        .Select(term => term.First()).Distinct().ToList()
        .ForEach(character => optionsAllowedPerLetter.Where(characterOptions => characterOptions.Key == character)
                                                     .Select(characterOptions => characterOptions.Value)
                                                     .ToList()
                                                     .ForEach(options => options.Remove(0)));

    private void RemoveLeadingCharacterInvalidOptions()
    {
        int maxLength = rightTerm.Length;
        int maxLeftTermLength = letterCountPerIndex.Select((_, index) => index).Max() + 1;
        if (maxLeftTermLength == maxLength)
        {
            Dictionary<char, int> lastIndexLetterCount = letterCountPerIndex[maxLength - 1];
            foreach (KeyValuePair<char, int> letterCount in lastIndexLetterCount)
            {
                optionsAllowedPerLetter[letterCount.Key].RemoveAll(option => option > Math.Ceiling((double)(9 / letterCount.Value)) - 1);
            }
        }
        else
        {
            optionsAllowedPerLetter[rightTerm.First()]
                .RemoveAll(option => option > CalculateMaxCarry(letterCountPerIndex[maxLength - 2]));
        }
    }

    private int CalculateMaxCarry(Dictionary<char, int> letterCount)
    {
        var remainingOptions = new List<int>(options);
        return (4 + letterCount.OrderBy(pair => pair.Value).Sum(pair =>
        {
            int max = remainingOptions.Max();
            remainingOptions.Remove(max);
            return pair.Value * max;
        })) / 10;
    }

    private void BruteForce() => Recursive(letters.ToDictionary((character) => character, (_) => -1));

    private void Recursive(Dictionary<char, int> candidate)
    {
        var candidatesLeft = candidate.Where(keypair => keypair.Value == -1);
        if (candidatesLeft.Count() == 0)
        {
            CheckSolution(candidate);
        }
        else
        {
            var currentChar = candidatesLeft.First().Key;
            foreach (int option in optionsAllowedPerLetter[currentChar].Except(candidate.Values))
            {
                Recursive(new Dictionary<char, int>(candidate)
                {
                    [currentChar] = option
                });
            }
        }
    }

    private void CheckSolution(Dictionary<char, int> candidate)
    {
        long rightTermSum = SumString(rightTerm, candidate);
        long leftTermSum = 0;
        for (int index = 0; index < letterCountPerIndex.Count; index++)
        {
            long currentPowerOfTen = (long)Math.Pow(10, index);
            leftTermSum += letterCountPerIndex[index].Sum(charCount => candidate[charCount.Key] * charCount.Value) * currentPowerOfTen;
        }
        if (rightTermSum == leftTermSum)
        {
            solutions.Add(new Dictionary<char, int>(candidate));
        }
    }

    private long SumString(string input, Dictionary<char, int> candidate) =>
        input.Reverse().Select((letter, index) => (long)(candidate[letter] * Math.Pow(10, index))).Sum();

    public Dictionary<char, int> Solve()
    {
        if (leftTerms.Any(term => term.Length > rightTerm.Length)) throw new ArgumentException("At least one addend is longer than the result");
        RemoveZeroToLeadingChars();
        SimplifyWhenOverTenRepetitions();
        RemoveLeadingCharacterInvalidOptions();
        RemoveOtherOptionsForExclusiveChars();
        BruteForce();
        if (solutions.Count == 0) throw new ArgumentException("No solutions found, check your input");
        return solutions.First();
    }
}

internal static class Grammar
{
    private static readonly Parser<char> EqualChar = Parse.Char('=');
    private static readonly Parser<IEnumerable<char>> EqualDelimiter = EqualChar.Repeat(2);
    private static readonly Parser<char> AddChar = Parse.Char('+');
    private static readonly Parser<char> Letter = Parse.AnyChar.Except(EqualChar).Except(AddChar);
    private static readonly Parser<string> Word = Letter.AtLeastOnce().Text();

    public static readonly Parser<EquationTerms> Terms = from leftTerms in Word.DelimitedBy(AddChar)
                                                         from _ in EqualDelimiter
                                                         from rightTerm in Word
                                                         select new EquationTerms(leftTerms.ToArray(), rightTerm);
}

public static class Alphametics
{
    public static IDictionary<char, int> Solve(string equation) => Grammar.Terms.Parse(equation.Replace(" ", "")).Solve();
}