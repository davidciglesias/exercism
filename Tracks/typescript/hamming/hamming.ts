export default class Hamming {
    compute = (strand1: string, strand2: string): number => {
        if (strand1.length !== strand2.length) throw new Error("DNA strands must be of equal length.");
        return [...strand1].filter((char1, index) => char1 !== strand2.charAt(index)).length;
    };
}
