export default class Acronym {
    public static parse = (phrase: string): string =>
        (phrase.match(/[A-Z]+[a-z]*|[a-z]+/g) || []).reduce((parsed, block) => parsed + block[0].toUpperCase(), "");
}
