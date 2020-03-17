type TriangleTypes = "equilateral" | "isosceles" | "scalene";

export default class Triangle {
    sides: number[];

    constructor(...sides: number[]) {
        this.sides = sides;
    }

    kind = (): TriangleTypes => {
        const [a, b, c] = this.sides.sort((sideA, sideB) => sideA - sideB);

        if (a < 0 || a + b <= c) {
            throw new Error("You gotta be more positive!");
        }

        if (a == b && b == c) {
            return "equilateral";
        }

        if (a == b || b == c) {
            return "isosceles";
        }

        return "scalene";
    };
}
