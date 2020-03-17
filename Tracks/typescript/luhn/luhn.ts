export default class Luhn {
    static valid(creditCard: string): boolean {
        const parsedCreditCard = creditCard.replace(/\s/g, "");
        const validLength = parsedCreditCard.length > 1;
        const validCharacters = parsedCreditCard.match(/\D/g) === null;
        const checkSumDivisibleBy10 = this.checksum(parsedCreditCard) % 10 === 0;
        return validLength && validCharacters && checkSumDivisibleBy10;
    }

    private static checksum = (parsedCreditCard: string): number =>
        Array.from(parsedCreditCard)
            .reverse()
            .map(Number)
            .reduce(
                (checkSum, digit, index) => checkSum + (((index % 2 === 0 || digit === 9) && digit) || (digit * 2) % 9),
                0
            );
}
