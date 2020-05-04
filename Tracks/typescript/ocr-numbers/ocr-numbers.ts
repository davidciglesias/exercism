export default class OcrParser {
  private static numberDictionary: Map<string, string> = new Map<
    string,
    string
  >([
    [" _ \n" + "| |\n" + "|_|\n" + "   ", "0"],
    ["   \n" + "  |\n" + "  |\n" + "   ", "1"],
    [" _ \n" + " _|\n" + "|_ \n" + "   ", "2"],
    [" _ \n" + " _|\n" + " _|\n" + "   ", "3"],
    ["   \n" + "|_|\n" + "  |\n" + "   ", "4"],
    [" _ \n" + "|_ \n" + " _|\n" + "   ", "5"],
    [" _ \n" + "|_ \n" + "|_|\n" + "   ", "6"],
    [" _ \n" + "  |\n" + "  |\n" + "   ", "7"],
    [" _ \n" + "|_|\n" + "|_|\n" + "   ", "8"],
    [" _ \n" + "|_|\n" + " _|\n" + "   ", "9"],
  ]);

  static parseNumber = (candidate: string) =>
    OcrParser.numberDictionary.has(candidate)
      ? OcrParser.numberDictionary.get(candidate)
      : "?";

  static parseRow = (
    row: [string, string, string, string],
    length: number
  ): string =>
    [...Array(length / 3)].reduce(
      (prev, _, index) =>
        prev +
        OcrParser.parseNumber(
          [...Array(4)]
            .map(
              (_, rowIndex) =>
                row[rowIndex][index * 3 + 0] +
                row[rowIndex][index * 3 + 1] +
                row[rowIndex][index * 3 + 2]
            )
            .join("\n")
        ),
      ""
    );

  public static convert = (text: string): string => {
    const lines = text.split("\n");

    if (lines.length % 4 !== 0) throw new Error("Invalid number of lines");

    const length = lines[0].length;

    if (length % 3 !== 0) throw new Error("Invalid line length");
    if (lines.some((line) => line.length !== length)) {
      throw new Error("Lines have different length");
    }

    const rows = [...Array(lines.length / 4)].map(
      (_, index) =>
        [
          lines[0 + index * 4],
          lines[1 + index * 4],
          lines[2 + index * 4],
          lines[3 + index * 4],
        ] as [string, string, string, string]
    );

    return rows.map((row) => OcrParser.parseRow(row, length)).join(",");
  };
}
