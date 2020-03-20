export default (factors: number[]) => {
    const to = (maxValue: number): number =>
        [
            ...new Set<number>(
                factors.reduce<number[]>(
                    (array, factor) => [
                        ...array,
                        ...[
                            ...Array(maxValue % factor === 0 ? maxValue / factor - 1 : Math.trunc(maxValue / factor))
                        ].map((_, index) => (index + 1) * factor)
                    ],
                    []
                )
            ).values()
        ].reduce((sum, multiple) => sum + multiple, 0);
    return { to };
};
