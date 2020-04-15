using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

public class SgfTree
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
    {
        if (data.Keys.Any(key => key.ToUpper() != key)) throw new ArgumentException();
        Data = data;
        Children = children;
    }

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }

    public override bool Equals(object other)
    {
        var otherSgfTree = other as SgfTree;

        return Data.Keys.SequenceEqual(otherSgfTree.Data.Keys) && Data.Keys.All(key => Data[key].SequenceEqual(otherSgfTree.Data[key]))
             && otherSgfTree.Children.SequenceEqual(Children);
    }

    public override int GetHashCode() => HashCode.Combine(Data, Children);
}

internal static class Grammar
{
    private static readonly Parser<char> BackslashEscape = Parse.Char('\\');
    private static readonly Parser<char> PropertyDelimiterBegin = Parse.Char('[');
    private static readonly Parser<char> PropertyDelimiterEnd = Parse.Char(']');
    private static readonly Parser<char> ChildrenDelimiterBegin = Parse.Char('(');
    private static readonly Parser<char> ChildrenDelimiterEnd = Parse.Char(')');

    private static readonly Parser<char> ChildrenDelimiters = ChildrenDelimiterBegin.Or(ChildrenDelimiterEnd);

    private static Parser<T> Escaped<T>(Parser<T> following) =>
        from escape in BackslashEscape.Once()
        from f in following
        select f;

    private static readonly Parser<char> PropertyContent = Escaped(PropertyDelimiterBegin).Or(Escaped(PropertyDelimiterEnd))
        .Or(Escaped(BackslashEscape)).Or(Parse.AnyChar.Except(PropertyDelimiterBegin).Except(PropertyDelimiterEnd));

    public static readonly Parser<string> Property =
        from open in PropertyDelimiterBegin
        from content in PropertyContent.Many().Text()
        from end in PropertyDelimiterEnd
        select content.Replace("\\t", " ").Replace("\\n", "\n");

    public static readonly Parser<IEnumerable<string>> Properties = from properties in Property.Many() select properties;

    public static readonly Parser<string> Key = Parse.AnyChar.Except(PropertyDelimiterBegin).Many().Text();

    public static readonly Parser<KeyValuePair<string, IEnumerable<string>>> KeyProperties =
        from key in Key
        from properties in Properties
        from _ in ChildrenDelimiters.Many()
        select new KeyValuePair<string, IEnumerable<string>>(key, properties);

    public static readonly Parser<Dictionary<string, string[]>> ValueProperties =
        from keyProperties in KeyProperties.Many()
        select keyProperties.ToDictionary(keyProperties => keyProperties.Key, 
            keyProperties => keyProperties.Value.ToArray());

    private static readonly Parser<char> InstructionStart = Parse.Char(';');

    public static readonly Parser<string> Internal = from _ in InstructionStart
                                                     from key in Parse.AnyChar.Except(InstructionStart).Many().Text()
                                                     select key;

    public static string GetItemsWithinOuterParenthesis(string item) =>
        item.StartsWith('(') && item.EndsWith(')') ? item[1..^1] : item;

}

public class SgfParser
{
    public static SgfTree CombineAllTrees(Dictionary<int, (int parent, SgfTree tree)> treeList)
    {
        Dictionary<int, SgfTree> finalTreeList = new Dictionary<int, SgfTree>();
        foreach (int id in treeList.Keys.OrderByDescending(id => id))
        {
            var (parent, tree) = treeList[id];
            var parentTree = parent == -1 ? null : treeList[parent].tree;
            var currentTree = finalTreeList.ContainsKey(id) ? finalTreeList[id] : tree;

            if (parent == -1)
            {
                if (!finalTreeList.ContainsKey(0)) finalTreeList.Add(0, tree);
            }
            else
            {
                if (finalTreeList.ContainsKey(parent))
                {
                    finalTreeList[parent] = new SgfTree(finalTreeList[parent].Data, finalTreeList[parent].Children.Prepend(currentTree).ToArray());
                }
                else
                {
                    finalTreeList.Add(parent, new SgfTree(parentTree.Data, parentTree.Children.Prepend(currentTree).ToArray()));
                }
            }

        }
        return finalTreeList[0];
    }

    public static SgfTree ParseTree(string input)
    {
        var getItemsInParenthesis = Grammar.GetItemsWithinOuterParenthesis(input);

        if (getItemsInParenthesis == input)
        {
            throw new ArgumentException();
        }

        var getInstructions = Grammar.Internal.Many().Parse(getItemsInParenthesis);

        var parent = -1;
        var id = 0;
        var children = 0;
        Dictionary<int, (int parent, SgfTree tree)> treeList = new Dictionary<int, (int parent, SgfTree tree)>();

        if (getInstructions.Count() == 0)
        {
            throw new ArgumentException();
        }

        if (getInstructions.First().Length == 0)
        {
            return new SgfTree(new Dictionary<string, string[]>());
        }

        void CalculateBranch(string instruction)
        {
            Dictionary<string, string[]> branch = Grammar.ValueProperties.Parse(instruction);

            if (branch.Keys.Any(key => key.ToUpper() != key) || branch.Values.Any(values => values.Count() == 0))
            {
                throw new ArgumentException();
            }

            bool closingChild = instruction.Last() == ')' || instruction[^2] == ')';
            if (closingChild)
            {
                parent -= children;
            }

            if (instruction.Last() == '(')
            {
                children = 0;
            }

            treeList.Add(id, (parent, new SgfTree(branch.ToDictionary(keyPair => keyPair.Key, keyPair => keyPair.Value))));

            id++;
            if(!closingChild) parent++;
        }

        getInstructions.ToList().ForEach(CalculateBranch);

        return CombineAllTrees(treeList);
    }
}