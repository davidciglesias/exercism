using System;
using System.Linq;

public static class Series
{
    public static string[] Slices(string numbers, int sliceLength)
    {
        int length = numbers.Length;
        if (sliceLength <= 0 || sliceLength > length || string.IsNullOrEmpty(numbers))
        {
            throw new ArgumentException();
        }
        return Enumerable.Range(0, length - sliceLength + 1)
                         .Select(index => numbers.Substring(index, sliceLength)).ToArray();
    }
}