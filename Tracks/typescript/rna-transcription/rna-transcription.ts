class Transcriptor {
    private transcribe = new Map<string, string>([
        ["G", "C"],
        ["C", "G"],
        ["T", "A"],
        ["A", "U"]
    ]);

    toRna(sequence: string): string {
        if (sequence.match(/[^GCTA]/g) !== null) {
            throw new Error("Invalid input DNA.");
        }

        return (
            sequence
                .match(/[GCTA]/g)
                ?.map(strand => this.transcribe.get(strand))
                .join("") || ""
        );
    }
}

export default Transcriptor;
