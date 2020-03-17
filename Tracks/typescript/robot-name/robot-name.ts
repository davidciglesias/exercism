type CharacterOption = "c" | "d";
type CharacterSets = { [key in CharacterOption]: string[] };

export default class RobotName {
    name: string;
    private readonly characterSets: CharacterSets;

    private readonly generateCharacterSet = (initial: string, last: string): string[] => {
        const lastCharCode = last.charCodeAt(0);
        const initialCharCode = initial.charCodeAt(0);
        return [...Array(lastCharCode - initialCharCode + 1).keys()].reduce<string[]>(
            (current, index) => [...current, String.fromCharCode(initialCharCode + index)],
            []
        );
    };

    private readonly generateNewCharacter = (input: CharacterOption): string =>
        this.characterSets[input][Math.floor(Math.random() * this.characterSets[input].length)].toString();

    private readonly generateNewName = (): string =>
        "ccddd".split("").reduce((prev, current) => prev + this.generateNewCharacter(current as "c" | "d"), "");

    private static readonly previousNames: Set<string> = new Set<string>();

    private readonly generateUniqueName = (forcedName?: string): string => {
        if (forcedName && RobotName.previousNames.has(forcedName)) {
            throw new Error("Unable to force name as it has been used already.");
        }
        let newName = forcedName || this.generateNewName();
        while (RobotName.previousNames.has(newName)) {
            newName = this.generateNewName();
        }
        RobotName.previousNames.add(newName);
        return newName;
    };

    constructor(forcedName?: string) {
        this.characterSets = {
            c: this.generateCharacterSet("A", "Z"),
            d: this.generateCharacterSet("0", "9")
        };
        this.name = this.generateUniqueName(forcedName);
    }

    resetName = () => {
        this.name = this.generateUniqueName();
    };
}
