using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleLinkedList<T> : IEnumerable<T>
{
    public SimpleLinkedList(T value)
    {
        Value = value;
        Next = null;
    }

    public SimpleLinkedList(IEnumerable<T> values)
    {
        Value = values.First();
        foreach(T value in values.Skip(1))
        {
            Add(value);
        }
    }

    public T Value { get; private set; }

    public SimpleLinkedList<T> Next { get; private set; }

    public SimpleLinkedList<T> Add(T value)
    {
        var next = this;
        while (next.Next != null)
        {
            next = next.Next;
        }
        next.Next = new SimpleLinkedList<T>(value);
        return this;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;
        if (Next != null)
        {
            var nextEnumerator = Next.GetEnumerator();
            while (nextEnumerator.MoveNext())
            {
                yield return nextEnumerator.Current;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}