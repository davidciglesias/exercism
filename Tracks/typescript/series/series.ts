export default class Series {
  readonly digits: number[];
  constructor(input: string) {
    this.digits = [...input].map(digit => parseInt(digit));
  }
  slices = (length: number): number[][] => {
    if (length > this.digits.length) throw new Error();
    return this.digits
      .map((_, index) => index)
      .filter(index => index + length <= this.digits.length)
      .map(index => this.digits.slice(index, index + length));
  };
}
