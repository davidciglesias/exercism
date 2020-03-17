export default class Pangram {
    private readonly text: string;
    private readonly alphabet = 
        ["a", "b", "c", "d", "e",
         "f", "g", "h", "i", "j",
         "k", "l", "m", "n", "o", 
         "p", "q", "r", "s", "t", 
         "u", "v", "w", "x", "y", 
         "z"];

    constructor(text: string) {
        this.text = text.toLowerCase();
    }

    isPangram = (): boolean => this.text.length > 0 && 
        new Set(this.text.replace(new RegExp(`[^${this.alphabet.join("")}]`, "g"), "")).size === this.alphabet.length
}