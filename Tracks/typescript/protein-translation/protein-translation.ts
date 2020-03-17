class ProteinTranslation {
    static codonMap = new Map<string, string>([
        ["AUG", "Methionine"],
        ["UUU", "Phenylalanine"],
        ["UUC", "Phenylalanine"],
        ["UUA", "Leucine"],
        ["UUG", "Leucine"],
        ["UCU", "Serine"],
        ["UCC", "Serine"],
        ["UCA", "Serine"],
        ["UCG", "Serine"],
        ["UAU", "Tyrosine"],
        ["UAC", "Tyrosine"],
        ["UGU", "Cysteine"],
        ["UGC", "Cysteine"],
        ["UGG", "Tryptophan"],
        ["UAA", "STOP"],
        ["UAG", "STOP"],
        ["UGA", "STOP"]
    ]);

    static proteins(sequence: string) {
        let proteins: string[] = [];
        for (const codon of sequence.match(/(\w{3})/g) || []) {
            const protein = this.codonMap.get(codon) || "STOP";
            if (protein === "STOP") {
                return proteins;
            } else {
                proteins.push(protein);
            }
        }
        return proteins;
    }
}

export default ProteinTranslation;
