const isValidExpression = (exp: string) => {
  try {
    return eval(exp);
  } catch (e) {
    return false;
  }
}

export default class Alphametics {
  symbols: string[]
  puzzle: string

  constructor(puzzle: string) {
    this.puzzle = puzzle;
    this.symbols = [...(new Set([...puzzle.replace(/[^A-Z]/g, '')]))]
  }

  _solve(puzzle: string, symbols: string[], values: number[] = []): any {
    if (values.length === symbols.length) {
      const exp = symbols.reduce(
        (exp: string, sym: string, i: number) => exp.replace(RegExp(sym, 'g'), "" + values[i]),
        puzzle
      )
      if (!isValidExpression(exp)) return undefined;
      return symbols.reduce((o: any, sym: string, i: number) => Object.assign(o, { [sym]: values[i] }), {});
    }

    let i = -1;
    while (i < 9) {
      i += 1;
      if (values.includes(i)) continue;

      const solution = this._solve(puzzle, symbols, values.concat(i))
      if (solution) {
        return solution;
      }
    }
  }

  solve() {
    return this._solve(this.puzzle, this.symbols)
  }
}