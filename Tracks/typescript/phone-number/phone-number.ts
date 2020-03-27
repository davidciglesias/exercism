export default class PhoneNumber {
  parsedNumber: string;
  constructor(input: string) {
    this.parsedNumber = input;
  }

  number = () => {
    if (this.parsedNumber.match(/[a-zA-Z]/g) !== null) return undefined;
    const digits = (this.parsedNumber.match(/\d/g) || []).join("");
    const match = digits.match(/^1?(?<number>([2-9]{1}\d{2}){2}\d{4})$/);
    return (
      (match !== null && !!match.groups && match.groups["number"]) || undefined
    );
  };
}
