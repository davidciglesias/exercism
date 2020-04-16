using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BinarySearchTree : IEnumerable<int>
{
    public List<int> values = new List<int>();

    public BinarySearchTree(int value)
    {
        Value = value;
        values = new List<int> { value };
    }

    public BinarySearchTree(IEnumerable<int> values)
    {
        this.values = new List<int>(values);
        Value = values.First();
        foreach (int value in values.Skip(1))
        {
            Add(value);
        }
    }


    public int Value { get; }

    public BinarySearchTree Left { get; private set; }

    public BinarySearchTree Right { get; private set; }

    public BinarySearchTree Add(int value)
    {
        if (value > Value)
        {
            Right = Right?.Add(value) ?? new BinarySearchTree(value);
        }
        else
        {
            Left = Left?.Add(value) ?? new BinarySearchTree(value);
        }

        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        if (Left != null)
        {
            var leftEnumerator = Left.GetEnumerator();
            while (leftEnumerator.MoveNext())
            {
                yield return leftEnumerator.Current;
            }
        }

        yield return Value;

        if (Right != null)
        {
            var rightEnumerator = Right.GetEnumerator();
            while (rightEnumerator.MoveNext())
            {
                yield return rightEnumerator.Current;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}