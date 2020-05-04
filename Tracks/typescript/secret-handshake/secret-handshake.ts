type Command = (input: string[]) => string[];

export default class HandShake {
  private readonly commandsById: Map<number, Command> = new Map([
    [1 << 0, (input: string[]) => input.concat("wink")],
    [1 << 1, (input: string[]) => input.concat("double blink")],
    [1 << 2, (input: string[]) => input.concat("close your eyes")],
    [1 << 3, (input: string[]) => input.concat("jump")],
    [1 << 4, (input: string[]) => input.reverse()],
  ]);

  private readonly commandId: number;

  constructor(commandId: number) {
    this.commandId = commandId;
  }

  commands = (): string[] =>
    [...this.commandsById.entries()].reduce<string[]>(
      (prev, idCommand) =>
        (this.commandId & idCommand[0]) === idCommand[0]
          ? idCommand[1](prev)
          : prev,
      []
    );
}
