type Position = [number, number];
interface QueenPositions {
  black: Position;
  white: Position;
}

const Equals = (vector1: Position, vector2: Position): boolean =>
  vector1[0] === vector2[0] && vector1[1] === vector2[1];

export default class QueenAttack {
  public white: Position;
  public black: Position;

  constructor(positions: QueenPositions) {
    if (Equals(positions.white, positions.black)) {
      throw new Error("Queens cannot share the same space");
    }
    this.white = positions.white;
    this.black = positions.black;
  }

  toString = (): string =>
    [...Array(8)]
      .map((_, row) =>
        [...Array(8)]
          .map((_, column) =>
            Equals(this.white, [row, column])
              ? "W"
              : Equals(this.black, [row, column])
              ? "B"
              : "_"
          )
          .join(" ")
      )
      .join("\n") + "\n";

  canAttack(): boolean {
    const distance = [
      this.white[0] - this.black[0],
      this.white[1] - this.black[1],
    ];
    const absDistance = distance.map((coord) => Math.abs(coord));
    const maxDistance = Math.max(...absDistance);
    const normalizedAbs = absDistance.map((coord) => coord / maxDistance);
    return [
      [0, 1],
      [1, 0],
      [1, 1],
    ].some((vector) => Equals(vector as Position, normalizedAbs as Position));
  }
}
