export default class FoodChain {
  private static options = Object.freeze({
    fly: () => "",
    spider: (subtitle = false) =>
      (subtitle ? " that " : "\nIt ") +
      "wriggled and jiggled and tickled inside her.",
    bird: () => "\nHow absurd to swallow a bird!",
    cat: () => "\nImagine that, to swallow a cat!",
    dog: () => "\nWhat a hog, to swallow a dog!",
    goat: () => "\nJust opened her throat and swallowed a goat!",
    cow: () => "\nI don't know how she swallowed a cow!",
    horse: () => ""
  });

  private static beginning = (what: keyof typeof FoodChain.options) =>
    `I know an old lady who swallowed a ${what}.${FoodChain.options[what]()}`;

  private static middle = (
    current: keyof typeof FoodChain.options,
    previous: keyof typeof FoodChain.options,
    subtitle: string
  ) => `\nShe swallowed the ${current} to catch the ${previous}${subtitle}`;

  private static end = (what: keyof typeof FoodChain.options) =>
    "\n" +
    (what === "horse"
      ? "She's dead, of course!"
      : `I don't know why she swallowed the ${what}. Perhaps she'll die.`) +
    "\n";

  static verse = (verse: number) =>
    ([...Object.keys(FoodChain.options)] as (keyof typeof FoodChain.options)[])
      .filter((_, index) => index < verse)
      .reverse()
      .reduce(
        (verse, current, index, array) => {
          if (verse.finished === true) {
            return verse;
          }
          if (verse.phrase === "") {
            verse.phrase += FoodChain.beginning(current);
          }
          if (current === "horse" || index === array.length - 1) {
            verse.phrase += FoodChain.end(current);
            verse.finished = true;
            return verse;
          }
          if (index + 1 < array.length) {
            const next = array[index + 1];
            verse.phrase += FoodChain.middle(
              current,
              next,
              (next === "spider" && FoodChain.options[next](true)) || "."
            );
          }
          return verse;
        },
        { phrase: "", finished: false }
      ).phrase;

  static verses = (initial: number, last: number) =>
    [...Array(last - initial + 1)]
      .map((_, index) => index + initial)
      .reduce(
        (result, verseIndex) =>
          result + (result !== "" ? "\n" : "") + FoodChain.verse(verseIndex),
        ""
      );
}
