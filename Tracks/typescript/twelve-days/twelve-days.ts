const stuffPerDay = [
  ["first", "a Partridge in a Pear Tree"],
  ["second", "two Turtle Doves"],
  ["third", "three French Hens"],
  ["fourth", "four Calling Birds"],
  ["fifth", "five Gold Rings"],
  ["sixth", "six Geese-a-Laying"],
  ["seventh", "seven Swans-a-Swimming"],
  ["eighth", "eight Maids-a-Milking"],
  ["ninth", "nine Ladies Dancing"],
  ["tenth", "ten Lords-a-Leaping"],
  ["eleventh", "eleven Pipers Piping"],
  ["twelfth", "twelve Drummers Drumming"]
];

class TwelveDays {
  static recite = (firstVerse: number, lastVerse: number) =>
    [...Array(lastVerse - firstVerse + 1)]
      .map((_, index) => index + firstVerse - 1)
      .map(
        verse =>
          `On the ${
            stuffPerDay[verse][0]
          } day of Christmas my true love gave to me:${[...Array(verse + 1)]
            .reduce<string[]>(
              (totalItems, _, index, items) => [
                `${items.length > 1 && index === 0 ? " and " : " "}${
                  stuffPerDay[index][1]
                }${items.length > 1 && index > 0 ? "," : ""}`,
                ...totalItems
              ],
              []
            )
            .join("")}.\n`
      )
      .join("");
}

export default TwelveDays;
