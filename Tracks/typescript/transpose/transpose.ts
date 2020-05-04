export default class Transpose {
  static transpose = (input: string[]): string[] => {
    const columns = Math.max(...input.map((element) => element.length));
    return columns <= 0
      ? []
      : [...Array(columns)].map((_, index) => {
          const line = input
            .map((line) => (line.length > index ? line[index] : " "))
            .reduce((prev, current) => prev + current, "");
          return index === columns - 1 ? line.trimEnd() : line;
        });
  };
}
