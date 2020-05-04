export default class BinarySearch {
  public array?: number[];

  constructor(input: number[]) {
    if (
      new Array(...input)
        .sort((a, b) => a - b)
        .every((element, index) => input[index] === element)
    ) {
      this.array = input;
    }
  }

  public indexOf = (seeked: number): number => {
    if (this.array === undefined) return -1;
    let start = 0,
      end = this.array.length,
      current;
    do {
      current = Math.floor((start + end) / 2);
      if (this.array[current] === seeked) return current;
      if (this.array[current] > seeked) end = current;
      if (this.array[current] < seeked) start = current;
    } while (current === 0 || current !== this.array.length - 1);
    return -1;
  };
}
