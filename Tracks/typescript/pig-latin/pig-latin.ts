export default class PigLatin {
  private static translateWord = (word: string): string => {
    const firstLetterIsVowel = word[0].match(/^[aeiou]/) !== null;
    if (firstLetterIsVowel) return word + "ay";
    const firstVowelAfterConsonantGroup = word.match(
      /((?<!q)[aeiouy]|(?<=q)[aeioy])/
    );
    if (firstVowelAfterConsonantGroup === null) {
      throw new Error("Invalid word, it has no vowels!");
    }
    return (
      word.slice(firstVowelAfterConsonantGroup.index || 1) +
      word.slice(0, firstVowelAfterConsonantGroup.index || 1) +
      "ay"
    );
  };

  static translate = (english: string): string =>
    english
      .split(" ")
      .map(PigLatin.translateWord)
      .join(" ");
}
