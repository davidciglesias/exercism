using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

public class HangmanState
{
    public string MaskedWord { get; }
    public ImmutableHashSet<char> GuessedChars { get; }
    public int RemainingGuesses { get; }

    public HangmanState(string maskedWord, ImmutableHashSet<char> guessedChars, int remainingGuesses)
    {
        MaskedWord = maskedWord;
        GuessedChars = guessedChars;
        RemainingGuesses = remainingGuesses;
    }
}

public class TooManyGuessesException : Exception { }

public class Hangman
{
    private readonly string word;
    private readonly BehaviorSubject<HangmanState> state;

    private string GetMaskedWord(ImmutableHashSet<char> guessedChars) => 
        string.Concat(word.Select(character => (!guessedChars.Contains(character)) ? '_' : character));

    public IObservable<HangmanState> StateObservable => state.AsObservable();

    public IObserver<char> GuessObserver => Observer.Create((char character) =>
        {
            if (state.Value.RemainingGuesses == 0)
            {
                state.OnError(new TooManyGuessesException());
            }
            else
            {
                ImmutableHashSet<char> newGuessedChars = state.Value.GuessedChars.Append(character).ToImmutableHashSet();
                string newMaskedWord = GetMaskedWord(newGuessedChars);
                if (newMaskedWord == word) state.OnCompleted();
                else
                {
                    int newRemainingGuesses = state.Value.GuessedChars.Contains(character) || !word.Contains(character) ? state.Value.RemainingGuesses - 1 : state.Value.RemainingGuesses;
                    var newHangmanState = new HangmanState(newMaskedWord, newGuessedChars, newRemainingGuesses);
                    state.OnNext(newHangmanState);
                }
            }
        });

    public Hangman(string word)
    {
        this.word = word;
        var guessedChars = new HashSet<char>().ToImmutableHashSet();
        state = new BehaviorSubject<HangmanState>(new HangmanState(GetMaskedWord(guessedChars), guessedChars, 9));
    }
}
