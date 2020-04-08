using System.Linq;

public static class RotationalCipher
{
    private const char First = 'a', Last = 'z';
    private const int Module = Last - First + 1;

    private enum Mutation { Lower, Upper, None }

    private static Mutation GetMutation(this char character) =>
        character >= First && character <= Last ? Mutation.Lower :
            character >= char.ToUpper(First) && character <= char.ToUpper(Last) ? Mutation.Upper :
                Mutation.None;

    private static char RotateLowerLetter(this char character, int shiftKey) => (char)((character + shiftKey - First) % Module + First);

    private static char RotateUpperLetter(this char character, int shiftKey) => char.ToUpper(char.ToLower(character).RotateLowerLetter(shiftKey));

    private static char RotateCharacter(this char character, int shiftKey) => character.GetMutation() switch
    {
        Mutation.None => character,
        Mutation.Lower => character.RotateLowerLetter(shiftKey),
        Mutation.Upper => character.RotateUpperLetter(shiftKey),
        _ => throw new System.NotImplementedException()
    };

    public static string Rotate(string text, int shiftKey) => string.Concat(text.Select((character) => character.RotateCharacter(shiftKey)));
}
