class ColumnEdges {
  first: number;
  last: number;

  constructor(first: number, last: number) {
    this.first = first;
    this.last = last;
  }

  equals = (other: ColumnEdges) =>
    other.first === this.first && other.last === this.last;
}

const getAllPossibleCombinations = (input: number[]): ColumnEdges[] =>
  input
    .slice(0, -1)
    .flatMap((firstIndex, index) =>
      input
        .slice(index + 1)
        .map(lastIndex => new ColumnEdges(firstIndex, lastIndex))
    );

const array = (from: number, to: number) =>
  [...Array(to - from - 1)].map((_, index) => index + from);

const getAllEdgesOnLine = (line: string): number[] =>
  [...line.matchAll(/(?=[+]{1}[-]*[+]{1})/g)]
    .map(match => match.index as number)
    .concat(line.lastIndexOf("+"));

export default class Rectangles {
  static count(input: string[]): number {
    if (input.every(line => !line.includes("+"))) return 0;
    const candidatesPerLine = input
      .map((line, index) => ({
        line: index + 1,
        candidates: getAllPossibleCombinations(getAllEdgesOnLine(line))
      }))
      .filter(line => line.candidates.length > 0);
    return candidatesPerLine
      .slice(0, -1)
      .reduce((rectangles, candidatesOnCurrentLine, index) => {
        const firstLine = candidatesOnCurrentLine.line;
        const candidatesOnNextLines = candidatesPerLine.slice(index + 1);
        return (
          rectangles +
          candidatesOnCurrentLine.candidates.reduce(
            (rectanglesOnCurrentLine, candidate) => {
              const nextLinesThatCouldFormRectangle = candidatesOnNextLines
                .filter(nextLineCandidates =>
                  nextLineCandidates.candidates.some(nextLineCandidate =>
                    nextLineCandidate.equals(candidate)
                  )
                )
                .map(candidate => candidate.line);

              return (
                rectanglesOnCurrentLine +
                nextLinesThatCouldFormRectangle.reduce(
                  (rectanglesOnThisLine, lastLine) => {
                    if (
                      lastLine - 1 === lastLine ||
                      array(firstLine, lastLine).every(
                        line =>
                          "|+".includes(input[line][candidate.first]) &&
                          "|+".includes(input[line][candidate.last])
                      )
                    )
                      return rectanglesOnThisLine + 1;
                    return rectanglesOnThisLine;
                  },
                  0
                )
              );
            },
            0
          )
        );
      }, 0);
  }
}
