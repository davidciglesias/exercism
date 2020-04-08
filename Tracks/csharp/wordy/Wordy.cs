using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public static class Wordy
{
    private static readonly Dictionary<string, Func<int, int, int>> operationByName = new Dictionary<string, Func<int, int, int>>
    {
        ["plus"] = (int a, int b) => a + b,
        ["minus"] = (int a, int b) => a - b,
        ["multiplied by"] = (int a, int b) => a * b,
        ["divided by"] = (int a, int b) => a / b,
    };
    public static int Answer(string question)
    {
        var match = new Regex(@"(?<=What is )(?<number>[-]?\d+)([\s]{1}(?<operation>(\w+( by)?))?[?\s]?(?<number>[-]?\d+)?(?<operation>(\w+( by)?))?)*").Match(question);
        var operations = match.Groups["operation"];
        var number = match.Groups["number"];
        if (!match.Success || (operations.Captures.Count != number.Captures.Count - 1)) throw new ArgumentException();
        if (!operations.Success && number.Success) return int.Parse(number.Captures.First().Value);
        if (operations.Captures.Any(operation => !operationByName.ContainsKey(operation.Value))) throw new ArgumentException();
        return operations.Captures
            .Select((operation, index) => (operation, index))
            .Aggregate(int.Parse(number.Captures[0].Value), (result, operationIndex) => 
                operationByName[operationIndex.operation.Value](result, int.Parse(number.Captures[operationIndex.index + 1].Value))
        );
    }
}