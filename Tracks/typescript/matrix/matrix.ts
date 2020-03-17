class Matrix {
    rows: number[][] = [];
    columns: number[][] = [];
    constructor(matrix: string) {
        this.rows = matrix.split("\n").map(row =>
            row.split(" ").map((numberString, column) => {
                const parsedNumber = parseInt(numberString);
                if (this.columns[column] === undefined) {
                    this.columns[column] = [parsedNumber];
                } else {
                    this.columns[column].push(parsedNumber);
                }
                return parsedNumber;
            })
        );
    }
}

export default Matrix;
