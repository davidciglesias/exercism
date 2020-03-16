using System;
using System.Linq;


public static class ScrabbleScore
{
    public static int Score(string input) => input.Sum(letter => Score(char.ToUpper(letter)));

    private static int Score(char input)
    {
        switch (input)
        {
            case 'A': case 'E': case 'I': case 'O': case 'U': 
            case 'L': case 'N': case 'R': case 'S': case 'T': 
                return 1;
            case 'D': case 'G':
                return 2;
            case 'B': case 'C': case 'M': case 'P':
                return 3;
            case 'F': case 'H': case 'V': case 'W': case 'Y':
                return 4;
            case 'K':
                return 5;
            case 'J': case 'X':
                return 8;
            case 'Q': case 'Z':
                return 10;
            default: 
                return 0;
        };
    }
}