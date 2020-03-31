const transpose = (matrix: number[][]): number[][] =>
  [...Array(matrix[0].length)].map((_, index) =>
    [...Array(matrix.length)].map((_, index2) => matrix[index2][index])
  );

const getPointsInMainLine = (
  matrix: number[][],
  condition: (line: number[]) => number[],
  isTransposed: boolean = false
): Point[] =>
  matrix.flatMap((line, index) => [
    ...condition(line).map(mainLine =>
      isTransposed
        ? { row: mainLine, column: index + 1 }
        : { row: index + 1, column: mainLine }
    )
  ]);

const indexesOf = (line: number[], sought: number) =>
  line
    .map((element, index) => (element === sought ? index + 1 : -1))
    .filter(element => element !== -1);

const indexesOfMax = (line: number[]): number[] =>
  indexesOf(line, Math.max(...line));

const indexesOfMin = (line: number[]): number[] =>
  indexesOf(line, Math.min(...line));

type Point = {
  row: number;
  column: number;
};

class SaddlePoints {
  static saddlePoints(matrix: number[][]) {
    const maxsInRows = getPointsInMainLine(matrix, indexesOfMax);
    const minsInColumns = getPointsInMainLine(
      transpose(matrix),
      indexesOfMin,
      true
    );

    return maxsInRows.filter(max =>
      minsInColumns.some(
        min => min.row === max.row && min.column === max.column
      )
    );
  }
}

export default SaddlePoints;
