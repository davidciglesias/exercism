export default class House {
  private static whatDidWhat = [
    { what: "horse and the hound and the horn", didWhat: "belonged to" },
    { what: "farmer sowing his corn", didWhat: "kept" },
    { what: "rooster that crowed in the morn", didWhat: "woke" },
    { what: "priest all shaven and shorn", didWhat: "married" },
    { what: "man all tattered and torn", didWhat: "kissed" },
    { what: "maiden all forlorn", didWhat: "milked" },
    { what: "cow with the crumpled horn", didWhat: "tossed" },
    { what: "dog", didWhat: "worried" },
    { what: "cat", didWhat: "killed" },
    { what: "rat", didWhat: "ate" },
    { what: "malt", didWhat: "lay in" },
    { what: "house that Jack built.", didWhat: "" }
  ];

  static verse = (verse: number): string[] =>
    House.whatDidWhat
      .slice(House.whatDidWhat.length - verse)
      .reduce<string[]>((verse, current, index, whatDidWhat) => {
        if (verse.length === 0) {
          verse.push(`This is the ${current.what}`);
        } else {
          verse.push(
            `that ${whatDidWhat[index - 1].didWhat} the ${current.what}`
          );
        }
        return verse;
      }, []);

  static verses = (firstVerse: number, lastVerse: number): string[] =>
    [...Array(lastVerse - firstVerse + 1)]
      .map((_, index) => index + firstVerse)
      .reduce<string[]>(
        (verses, verse) =>
          (verses.length === 0 && [...House.verse(verse)]) ||
          verses.concat(["", ...House.verse(verse)]),
        []
      );
}
