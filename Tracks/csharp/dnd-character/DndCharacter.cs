using System;
using System.Collections.Generic;
using System.Linq;

public class DndCharacter
{
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Constitution { get; private set; }
    public int Intelligence { get; private set; }
    public int Wisdom { get; private set; }
    public int Charisma { get; private set; }
    public int Hitpoints { get; private set; }

    public static int Modifier(int score) => (int)Math.Floor((double)(score - 10) / 2);

    private static int Get1d6(int _) => new Random().Next(1, 7);

    public static int Ability() => Enumerable.Range(0, 4).Select(Get1d6).OrderByDescending(x => x).Take(3).Sum();

    public static DndCharacter Generate()
    {
        int Constitution = Ability();
        var dndCharacter = new DndCharacter
        {
            Strength = Ability(),
            Dexterity = Ability(),
            Constitution = Constitution,
            Intelligence = Ability(),
            Wisdom = Ability(),
            Charisma = Ability(),
            Hitpoints = 10 + Modifier(Constitution),
        };
        return dndCharacter;
    }
}
