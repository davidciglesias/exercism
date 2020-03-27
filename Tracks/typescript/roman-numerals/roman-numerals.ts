class RomanNumerals {
  private static digitMap: Map<number, string[]> = new Map([
    [0, ["", "", "", ""]],
    [1, ["I", "X", "C", "M"]],
    [2, ["II", "XX", "CC", "MM"]],
    [3, ["III", "XXX", "CCC", "MMM"]],
    [4, ["IV", "XL", "CD", "MMMM"]],
    [5, ["V", "L", "D", "MMMMM"]],
    [6, ["VI", "LX", "DC", "MMMMMM"]],
    [7, ["VII", "LXX", "DCC", "MMMMMMM"]],
    [8, ["VIII", "LXXX", "DCCC", "MMMMMMMM"]],
    [9, ["IX", "XC", "CM", "MMMMMMMMM"]]
  ]);
  static roman = (input: number): string =>
    [...input.toString()]
      .map(element => parseInt(element))
      .reduce(
        (roman, digit, index, array) =>
          roman +
          (RomanNumerals.digitMap.get(digit) || [])[array.length - index - 1],
        ""
      );
}

export default RomanNumerals;
