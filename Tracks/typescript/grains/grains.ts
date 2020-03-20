export default class Grains {
    static square = (tile: number, allowHigh: boolean = false): number => {
        if (tile < 1 || (tile > 64 && allowHigh === false)) {
            throw new Error("Tile out of range");
        }
        const index = tile - 1;
        switch (Math.sign(tile - 32) as 1 | 0 | -1) {
            case 1:
                return 2 ** index;
            case 0:
                return Math.abs(1 << index);
            case -1:
                return 1 << index;
        }
    };
    static total = () => Grains.square(65, true) - 1;
}
