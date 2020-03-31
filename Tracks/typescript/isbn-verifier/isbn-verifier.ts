export default class ISBN {
  private readonly isbn: string;
  constructor(isbn: string) {
    this.isbn = isbn.replace(/-/g, "");
  }

  isValid = (): boolean => {
    if (this.isbn.length !== 10) return false;
    if (this.isbn.match(/[0-9]{9}[0-9X]{1}/g) === null) return false;
    return (
      [...this.isbn].reduce(
        (total, current, index) =>
          total + (current === "X" ? 10 : parseInt(current)) * (10 - index),
        0
      ) % 11 === 0
    );
  };
}
