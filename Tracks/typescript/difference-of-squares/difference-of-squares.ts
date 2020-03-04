class Squares {
    squareOfSum: number;
    sumOfSquares: number
    difference: number
    constructor(n: number) {
        const halfNNplus1 = n * (n + 1) / 2;
        this.squareOfSum = halfNNplus1 ** 2;
        this.sumOfSquares = (halfNNplus1 * (2 * n + 1)) / 3;
        this.difference = this.squareOfSum - this.sumOfSquares;
    }
}

export default Squares;
