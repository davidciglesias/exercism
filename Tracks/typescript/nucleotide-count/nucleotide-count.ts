enum NucleotideType {
    A = "A",
    C = "C",
    G = "G",
    T = "T"
}

type NucleotideCountResult = {
    [key in NucleotideType]?: number;
};

class NucleotideCount {
    static nucleotideCounts(strand: string): NucleotideCountResult {
        const types = Object.keys(NucleotideType);
        if (strand.match(new RegExp(`[^${types.join("")}]`, "g")) !== null) {
            throw new Error("Invalid nucleotide in strand");
        }
        return types.reduce(
            (prev, type) => ({ ...prev, [type]: strand.match(new RegExp(type, "g"))?.length || 0 }),
            {}
        );
    }
}

export default NucleotideCount;
