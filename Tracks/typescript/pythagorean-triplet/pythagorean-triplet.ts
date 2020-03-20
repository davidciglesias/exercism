export default class Triplet {
    triplet: number[];
    constructor(a: number, b: number, c: number) {
        this.triplet = [a, b, c].sort();
    }
    isPythagorean = () => this.triplet[0] ** 2 + this.triplet[1] ** 2 === this.triplet[2] ** 2;
    sum = () => this.triplet.reduce((sum, current) => sum + current, 0);
    product = () => this.triplet.reduce((sum, current) => sum * current, 1);
    static where = (maxValue: number, initialValue?: number, n?: number): Triplet[] => [
        ...Triplet.calculateTriplets(initialValue, maxValue, n)
    ];

    private static calculateTriplets = (initialValue: number = 1, maxValue: number, n?: number): Triplet[] => {
        const triplets: Triplet[] = [];
        for (let a = initialValue; a <= maxValue - 2 && ((n !== undefined && a < n) || n === undefined); a++) {
            for (let b = a + 1; b <= maxValue - 1 && ((n !== undefined && a + b < n) || n === undefined); b++) {
                if (n !== undefined) {
                    const c = n - b - a;
                    if (c > b && a ** 2 + b ** 2 === c ** 2) {
                        triplets.push(new Triplet(a, b, c));
                    }
                } else {
                    for (let c = b + 1; c <= maxValue; c++) {
                        if (a ** 2 + b ** 2 === c ** 2) {
                            triplets.push(new Triplet(a, b, c));
                        }
                    }
                }
            }
        }
        return triplets;
    };
}
