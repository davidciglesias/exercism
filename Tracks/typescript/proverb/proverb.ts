const phrase = (whatMissing: string, whatLost?: string) =>
  !!whatLost
    ? `For want of a ${whatMissing} the ${whatLost} was lost.\n`
    : `And all for the want of a ${whatMissing}.`;

export default (...items: string[]): string =>
  items
    .slice(0, -1)
    .map((itemMissing, index) => phrase(itemMissing, items[index + 1]))
    .join("")
    .concat(phrase(items[0]));
