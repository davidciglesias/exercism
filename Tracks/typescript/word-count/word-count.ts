class Words {
    count(text: string): Map<string, number> {
        const map = new Map<string, number>();
        const regex = new RegExp(/[ \t\n]+/g)
        text.trim().split(regex).forEach(word => {
            const loweredWord = word.toLowerCase();
            map.set(loweredWord, (map.get(loweredWord) || 0) + 1);
        });

        return map;
    }
}

export default Words;
