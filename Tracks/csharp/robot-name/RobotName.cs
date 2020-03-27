using System;
using System.Collections.Generic;
using System.Linq;

public interface INameGenerator
{
    public string GenerateName();
}

public abstract class ANameGenerator : INameGenerator
{
    protected static readonly Dictionary<CharacterType, (char, char)> mapCharacterTypeToBounds = new Dictionary<CharacterType, (char, char)>
    {
        [CharacterType.Character] = ('A', 'Z'),
        [CharacterType.Digit] = ('0', '9'),
    };

    protected readonly List<CharacterType> namePattern;
    public ANameGenerator(List<CharacterType> namePattern) =>
        this.namePattern = new List<CharacterType>(namePattern);

    private char GenerateCharacter((char lower, char upper) pair) => (char)new Random().Next(pair.lower, pair.upper + 1);

    protected char GenerateCharacter(CharacterType type) => GenerateCharacter(mapCharacterTypeToBounds[type]);

    public abstract string GenerateName();
}

public class NameGenerator : ANameGenerator
{
    private readonly int maxNames;

    public NameGenerator(List<CharacterType> namePattern) : base(namePattern)
    {
        Dictionary<CharacterType, int> mapCharacterTypeToCount = mapCharacterTypeToBounds
            .ToDictionary((keyPair) => keyPair.Key, (keyPair) => keyPair.Value.Item2 - keyPair.Value.Item1 + 1);
        maxNames = namePattern.Aggregate(1, (product, charType) => product *= mapCharacterTypeToCount[charType]);
    }

    private static readonly HashSet<string> previousNames = new HashSet<string>();

    public override string GenerateName()
    {
        string newName;
        if (previousNames.Count == maxNames) throw new Exception("Sorry we ran out of names!");
        do
        {
            newName = string.Join("", namePattern.Select(charType => GenerateCharacter(charType)));
        } while (previousNames.Contains(newName));
        previousNames.Add(newName);
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