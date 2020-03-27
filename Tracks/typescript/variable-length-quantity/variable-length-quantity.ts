export default class VLQ {
  static encodeSingle(input: number): number[] {
    const result = [];
    let next = input;
    do {
      let currentBits = next & 127;
      if (next !== input) {
        currentBits += 128;
      }
      result.unshift(currentBits);
      next = next >>> 7;
    } while (next !== 0);

    return result;
  }

  static encode = (input: number[]): number[] =>
    input.reduce<number[]>(
      (prev, current) => [...prev, ...VLQ.encodeSingle(current)],
      []
    );

  static decode = (input: number[]): number[] => {
    if ((input[input.length - 1] & 128) !== 0) {
      throw new Error("Incomplete sequence");
    }

    const result: number[] = [];
    let temp: number = 0;

    input.forEach(term => {
      temp = (((temp === 0 && temp) || temp << 7) + (term & 127)) >>> 0;
      if ((term & 128) === 0) {
        result.push(temp);
        temp = 0;
      }
    });

    return result;
  };
}
