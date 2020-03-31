export default class Series {
  input: string;

  constructor(input: string) {
    this.input = input;
  }

  largestProduct(slice: number) {
    if (slice < 0 || this.input.match(/\D/g) !== null)
      throw new Error("Invalid input.");
    if (slice > this.input.length) throw new Error("Slice size is too big.");
    if (slice === 0) return 1;
    return [...this.input].reduce(
      (largestProduct, _, index, array) =>
        index + slice > array.length
          ? largestProduct
          : Math.max(
              largestProduct,
              array
                .slice(index, index + slice)
                .reduce((product, digit) => (product *= parseInt(digit)), 1)
            ),
      0
    );
  }
}
