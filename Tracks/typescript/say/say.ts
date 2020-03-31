const basicNumbers = Object.freeze({
  0: "zero",
  1: "one",
  2: "two",
  3: "three",
  4: "four",
  5: "five",
  6: "six",
  7: "seven",
  8: "eight",
  9: "nine",
  10: "ten",
  11: "eleven",
  12: "twelve",
  13: "thirteen",
  14: "fourteen",
  15: "fifteen",
  16: "sixteen",
  17: "seventeen",
  18: "eighteen",
  19: "nineteen",
  20: "twenty",
  30: "thirty",
  40: "forty",
  50: "fifty",
  60: "sixty",
  70: "seventy",
  80: "eighty",
  90: "ninety",
  100: "hundred"
});

type BasicNumbers = keyof typeof basicNumbers;

const units = Object.freeze({
  9: "billion",
  6: "million",
  3: "thousand",
  0: ""
});

type Units = keyof typeof units;

const basicNumberToString = (below100: number, previous = false): string => {
  if (below100 > 99 || below100 < 0) {
    throw new Error("Sorry bud, can only work from 0-99");
  }

  const basicNumber = basicNumbers[below100 as BasicNumbers];
  if (basicNumber !== undefined) {
    const isZero = basicNumbers[0] === basicNumber;
    return previous === false || !isZero ? basicNumber : "";
  }
  const possibleNumbers = [...Object.keys(basicNumbers)].map(number =>
    parseInt(number)
  );
  const closestBasicNumber = maxBelowValue(possibleNumbers, below100);
  return (
    basicNumbers[closestBasicNumber as BasicNumbers] +
    "-" +
    basicNumbers[(below100 % closestBasicNumber) as BasicNumbers]
  );
};

const belowThousandToString = (below1000: number, previous = false): string => {
  if (below1000 > 1000 || below1000 < 0) {
    throw new Error("Sorry bud, can only work from 0-999");
  }
  if (below1000 < 100) return basicNumberToString(below1000, previous);
  return (
    basicNumberToString(Math.floor(below1000 / 100), previous) +
    ` ${basicNumbers[100]} ` +
    basicNumberToString(below1000 % 100, true)
  );
};

const maxBelowValue = (
  input: number[],
  maximum?: number,
  fallback: number = -1
): number =>
  (!!maximum ? input.filter(item => item < maximum) : input).reduce(
    (max, current) => (current > max ? current : max),
    fallback
  );

export default class Say {
  inEnglish(input: number): string {
    if (input < 0 || input > 999999999999) {
      throw new Error("Number must be between 0 and 999,999,999,999.");
    }

    const reverseInputString = [...input.toString()].reverse().join("");
    const inputUnitsDescending = [
      ...Array(Math.ceil(reverseInputString.length / 3))
    ]
      .map((_, index) => index * 3)
      .reverse();
    const inputInEnglish = inputUnitsDescending.reduce((previous, unit) => {
      const next = belowThousandToString(
        parseInt(
          [...reverseInputString.slice(unit, unit + 3)].reverse().join("")
        ),
        previous !== ""
      );
      if (next !== "") {
        return `${previous} ${next} ${units[unit as Units]}`;
      } else return previous;
    }, "");

    return inputInEnglish.concat("").trim();
  }
}
