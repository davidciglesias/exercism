using System.Collections.Generic;
using System.Linq;

public static class MatchingBrackets
{
    private static readonly Dictionary<char, char> openCloseMap = new Dictionary<char, char>()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
    };

    public static bool IsPaired(string input)
    {
        var bracketStack = new Stack<char>();

        bool isMismatch = false;

        input.ToList().ForEach(character =>
        {
            if (!isMismatch)
            {
                if (openCloseMap.ContainsKey(character))
                {
                    bracketStack.Push(openCloseMap[character]);
                }
                else if (openCloseMap.ContainsValue(character) && (!bracketStack.TryPop(out char last2) || last2 != character))
                {
                    isMismatch = true;
                }
            }
        });

        return !isMismatch && bracketStack.Count == 0; 
    }
}
