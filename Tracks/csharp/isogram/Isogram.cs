using System;
using System.Linq;

public static class Isogram
{
    public static bool IsIsogram(string word) =>
        !"abcdefghijklmnopqrstuvwxyz"
            .Any(alphabetCharacter => 
                word.ToLower()
                    .Count((character) => character == alphabetCharacter) > 1);
}
