using System;
using System.Collections.Generic;
using System.Linq;

public interface INameGenerator
{
    public string GenerateName();
}

public abstract class ANameGenerator : INameGenerator
{
    protected readonly Func<CharacterType, (char, char)> charMapping;

    protected readonly List<CharacterType> namePattern;
    public ANameGenerator(List<CharacterType> namePattern, Func<CharacterType, (char, char)> charMapping) => 
        (this.namePattern, this.charMapping) = (namePattern.ToList(), charMapping);

    private char GenerateCharacter((char lower, char upper) pair) => (char)new Random().Next(pair.lower, pair.upper + 1);

    protected char GenerateCharacter(CharacterType type) => GenerateCharacter(charMapping(type));

    public abstract string GenerateName();
}

public class NameGenerator : ANameGenerator
{
    private readonly int maxNames;

    private static (char lower, char upper) UpperCaseMapping(CharacterType type) => type switch
    {
        CharacterType.Character => ('A', 'Z'),
        CharacterType.Digit => ('0', '9'),
        _ => throw new InvalidOperationException(),
    };


    public NameGenerator(List<CharacterType> namePattern) : base(namePattern, UpperCaseMapping)
    {
        static int CharMapSize((char lower, char upper) map) => 1 + map.upper - map.lower;
        maxNames = namePattern.Aggregate(1, (product, charType) => product *= CharMapSize(UpperCaseMapping(charType)));
    }

    private static readonly HashSet<string> previousNames = new HashSet<string>();

    public override string GenerateName()
    {
        if (previousNames.Count == maxNames) throw new Exception("Sorry we ran out of names!");
        string newName;
        do
        {
            newName = string.Concat(namePattern.Select(GenerateCharacter));
        } while (!previousNames.Add(newName));
        return newName;
    }
}

public enum CharacterType
{
    Digit, Character
}

public class Robot
{
    private static readonly List<CharacterType> namePattern = new List<CharacterType>()
    {
        CharacterType.Character,
        CharacterType.Character,
        CharacterType.Digit,
        CharacterType.Digit,
        CharacterType.Digit
    };

    private readonly INameGenerator nameGenerator = new NameGenerator(namePattern);

    public Robot() => Name = nameGenerator.GenerateName();

    public string Name { get; private set; }

    public void Reset() => Name = nameGenerator.GenerateName();
}