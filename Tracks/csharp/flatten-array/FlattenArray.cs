using System;
using System.Collections;

public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        foreach (var item in input)
        {
            if (item == null) continue;
            if (item is IEnumerable)
            {
                foreach (var child in Flatten(item as IEnumerable))
                {
                    yield return child;
                }
            }
            else
            {
                yield return item;
            }
        }
    }
}