type RaindropMapper = { factor: number; text: string };

export default class Raindrops {
    private readonly defaultMapper: RaindropMapper[] = [
        { factor: 3, text: "Pling" },
        { factor: 5, text: "Plang" },
        { factor: 7, text: "Plong" }
    ];
    private readonly mapper: RaindropMapper[];
    constructor(mapper?: RaindropMapper[]) {
        this.mapper = mapper || this.defaultMapper;
    }
    convert = (input: number): string =>
        this.mapper.reduce((result, { factor, text }) => result + (input % factor === 0 ? text : ""), "") ||
        input.toString();
}
