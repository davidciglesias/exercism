const transform = (input: { [key: string]: string[] }): { [key: string]: number } =>
    Object.keys(input).reduce(
        (output, key) => ({
            ...output,
            ...input[key].reduce((prev, value) => ({ ...prev, [value.toLowerCase()]: parseInt(key) }), {})
        }),
        {}
    );

export default transform;
