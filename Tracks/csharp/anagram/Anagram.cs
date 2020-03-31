using System;
using System.Linq;

public class Anagram
{
    private readonly string baseWord;

    public Anagram(string baseWord) => this.baseWord = baseWord.ToLower();

    private bool IsAnagram(string word)
    {
        word = word.ToLower();
        return (word == baseWord || word.Length != baseWord.Length) 
            ? false : word.OrderBy(letter => letter).SequenceEqual(baseWord.OrderBy(letter => letter));
    }

    public string[] FindAnagrams(string[] potentialMatches) => potentialMatches.Where(IsAnagram).ToArray();
}