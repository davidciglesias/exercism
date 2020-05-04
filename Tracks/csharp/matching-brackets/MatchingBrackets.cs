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
        var bracketsPendingClosure = new List<char>();

        bool isMismatch = false;

        input.ToList().ForEach(character =>
        {
            if (!isMismatch)
            {
                if (openCloseMap.ContainsKey(character))
                {
                    bracketsPendingClosure.Add(openCloseMap[character]);
                }
                else if (openCloseMap.ContainsValue(character))
                {
                    var last = bracketsPendingClosure.LastOrDefault();
                    if (last == default || last != character)
                    {
                        isMismatch = true;
                    }
                    else
                    {
                        bracketsPendingClosure.RemoveAt(bracketsPendingClosure.Count - 1);
                    }
                }
            }
        });

        return !isMismatch && bracketsPendingClosure.Count == 0;
    }
}
