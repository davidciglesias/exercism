using System;
using System.Linq;

public enum Allergen
{
    Eggs = 1,
    Peanuts = 2,
    Shellfish = 4,
    Strawberries = 8,
    Tomatoes = 16,
    Chocolate = 32,
    Pollen = 64,
    Cats = 128
}

public class Allergies
{
    private readonly int mask;

    public Allergies(int mask) => this.mask = mask;

    public bool IsAllergicTo(Allergen allergen) => (mask & (int)allergen) > 0;

    public Allergen[] List() => (Enum.GetValues(typeof(Allergen)) as Allergen[]).Where(IsAllergicTo).ToArray();
}