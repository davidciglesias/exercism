const howMany = (left: number): string => (left > 1 ? "one" : "it");
const howManyLeft = (left: number, upper = false): string =>
  (left === 0
    ? `${upper ? "N" : "n"}o more bottles`
    : `${left} bottle${left > 1 ? "s" : ""}`) + " of beer";

export default class Beer {
  static verse(left: number): string {
    const firstLine = `${howManyLeft(left, true)} on the wall, ${howManyLeft(
      left
    )}.`;
    const secondPart = `${howManyLeft(left > 0 ? left - 1 : 99)} on the wall`;
    const secondLine =
      left > 0
        ? `Take ${howMany(left)} down and pass it around, ${secondPart}.`
        : `Go to the store and buy some more, ${secondPart}.`;

    return firstLine.concat("\n", secondLine, "\n");
  }
  static sing = (from: number = 99, to: number = 0): string =>
    [...Array(from - to + 1)]
      .map((_, index) => Beer.verse(from - index))
      .join("\n");
}
