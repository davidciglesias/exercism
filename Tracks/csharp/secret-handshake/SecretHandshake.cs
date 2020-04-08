using System;
using System.Collections.Generic;
using System.Linq;

public static class SecretHandshake
{
    private const int ReverseCommand = 1 << 4;
    private static readonly Dictionary<int, string> actions = new Dictionary<int, string>
    {
        {  1 << 0, "wink" },
        {  1 << 1, "double blink" },
        {  1 << 2, "close your eyes" },
        {  1 << 3, "jump" },
    };

    public static string[] Commands(int commandValue)
    {
        bool IncludesCommand(int command) => (commandValue & command) == command;
        var result = actions.Where(commandAction => IncludesCommand(commandAction.Key))
                            .Select(commandAction => commandAction.Value);

        return (IncludesCommand(ReverseCommand) ? result.Reverse() : result).ToArray();
    }
}
