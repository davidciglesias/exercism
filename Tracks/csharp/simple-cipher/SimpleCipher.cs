using System;
using System.Linq;

public class SimpleCipher
{
    private const int Module = 'z' - 'a' + 1;

    public SimpleCipher() => Key = string.Join("", Enumerable.Range(1, 100).Select(_ => (char)new Random().Next('a', 'z' + 1)));

    public SimpleCipher(string key) => Key = key;

    public string Key { get; private set; }

    private string Xcode(string plaintext, Func<int, int> operation) => string.Join("", plaintext.Select((character, index) => (char)((character + operation(index)) % Module + 'a')));

    public string Encode(string plaintext) => Xcode(plaintext, index => Key[index % Key.Length] - 2 * 'a');

    public string Decode(string ciphertext) => Xcode(ciphertext, index => -Key[index % Key.Length] + Module);
}