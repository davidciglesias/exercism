type NumberType = "perfect" | "abundant" | "deficient";
type Sign = 1 | -1 | 0;
const typeMap = new Map<Sign, NumberType>([
    [-1, "abundant"],
    [1, "deficient"],
    [0, "perfect"]
]);

export default class PerfectNumbers {
    static classify = (candidate: number): NumberType => {
        if (candidate <= 0) {
            throw new Error("Classification is only possible for natural numbers.");
        }
        return typeMap.get(Math.sign(PerfectNumbers.substractAllFactors(candidate)) as Sign) as NumberType;
    };

    private static readonly substractAllFactors = (candidate: number) => {
        let sum = candidate;
        for (let index = 1; index <= candidate / 2; index++) {
            sum -= (candidate % index === 0 && index) || 0;
        }
        return sum;
    };
}
