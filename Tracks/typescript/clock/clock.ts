export default class Clock {
  private static module = Object.freeze({
    hour: 24,
    minute: 60
  });

  private readonly hours: number;
  private readonly minutes: number;

  private static toModulated = (
    input: number,
    type: keyof typeof Clock.module
  ): number => {
    if (input > 0) return input;
    const module = Clock.module[type];
    return input + (Math.abs(Math.floor(input / module)) + 1) * module;
  };

  constructor(hours: number, minutes: number = 0) {
    this.minutes =
      Clock.toModulated(minutes, "minute") % Clock.module["minute"];
    this.hours =
      Clock.toModulated(
        hours + Math.floor(minutes / Clock.module["minute"]),
        "hour"
      ) % Clock.module["hour"];
  }

  plus = (minutes: number): Clock =>
    new Clock(this.hours, this.minutes + minutes);

  minus = (minutes: number): Clock => this.plus(-1 * minutes);

  equals = (other: Clock): boolean =>
    this.hours === other.hours && this.minutes === other.minutes;

  private static toTwoDigits = (input: number): string =>
    input.toFixed(0).padStart(2, "0");

  toString = (): string =>
    `${Clock.toTwoDigits(this.hours)}:${Clock.toTwoDigits(this.minutes)}`;
}
