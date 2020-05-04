export default class Prime {
  private static primeList: number[] = [2];

  nth = (index: number) => {
    if (index <= 0) throw new Error("Prime is not possible");

    let candidate: number = Prime.primeList[Prime.primeList.length - 1];
    while (Prime.primeList.length < index) {
      candidate++;
      const isNotFactor = (prime: number) => candidate % prime !== 0;

      if (Prime.primeList.every(isNotFactor)) {
        Prime.primeList.push(candidate);
      }
    }
    return Prime.primeList[index - 1];
  };
}
