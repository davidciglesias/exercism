export default class Anagram {
  private readonly baseWord: string;

  constructor(baseWord: string) {
    this.baseWord = baseWord.toLowerCase();
  }

  matches = (...words: string[]): string[] =>
    words.filter(
      word =>
        word.length === this.baseWord.length &&
        word.toLowerCase() !== this.baseWord &&
        [...word.toLowerCase()].sort().join() ===
          [...this.baseWord].sort().join()
    );
}
