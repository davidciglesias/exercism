class Isogram {
  static isIsogram(input: string) {
    const letters = input.toLowerCase().match(/\w/g) || [];
    return new Set(letters).size === letters.length;
  }
}

export default Isogram;
