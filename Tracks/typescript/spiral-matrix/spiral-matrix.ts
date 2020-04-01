type LineKeys = "row" | "column";

const otherLine = (lineKey: LineKeys): LineKeys =>
  lineKey === "row" ? "column" : "row";

type LimitKeys = "max" | "min";

type Object<K extends LineKeys | LimitKeys, T> = {
  [key in K]: T;
};

type Limit<T> = Object<LimitKeys, T>;

type Lines<T> = Object<LineKeys, T>;

const initialCurrent = (): Lines<number> => ({ row: 0, column: 0 });

type Limits = Lines<Limit<number>>;

const initialLimits = (input: number): Limits => ({
  row: { max: input - 1, min: 1 },
  column: { max: input - 1, min: 0 }
});

interface Grow extends Lines<boolean> {
  current: LineKeys;
}

const initialGrow = (): Grow => ({
  row: true,
  column: true,
  current: "column"
});

const handleReachedLimits = (
  line: "row" | "column",
  grow: Grow,
  current: Lines<number>,
  limits: Limits
) => {
  const limit: LimitKeys = grow[line] ? "max" : "min";
  if (current[line] === limits[line][limit]) {
    limits[line][limit] += limit === "max" ? -1 : 1;
    grow[line] = !grow[line];
    grow.current = otherLine(line);
  }
};

export default class SpiralMatrix {
  static ofSize = (input: number): number[][] => {
    const matrix = [...new Array(input)].map(_ => new Array(input));
    const elements = [...new Array(input ** 2)].map((_, index) => index + 1);
    const grow = initialGrow();
    const limits = initialLimits(input);
    const current = initialCurrent();
    elements.forEach(element => {
      matrix[current.row][current.column] = element;
      const currentLine = grow.current;
      handleReachedLimits(currentLine, grow, current, limits);
      current[grow.current] += grow[grow.current] ? 1 : -1;
    });
    return matrix;
  };
}
