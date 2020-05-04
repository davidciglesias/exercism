using System;
using System.Linq;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    private enum Instructions
    {
        Left = 'L',
        Right = 'R',
        Advance = 'A',
    }

    public RobotSimulator(Direction direction, int x, int y) => (Direction, X, Y) = (direction, x, y);

    public Direction Direction { get; private set; }

    public int X { get; private set; }

    public int Y { get; private set; }

    private readonly int directionLength = Enum.GetValues(typeof(Direction)).Length;

    private Direction ToDirection(int direction) => 
        direction < 0 ? (Direction)(direction + directionLength) : (Direction)(direction % directionLength);

    private void Turn(bool left) => Direction = ToDirection((int)Direction + (left ? -1 : +1));

    private void Advance()
    {
        switch (Direction)
        {
            case Direction.North:
                Y++;
                break;
            case Direction.East:
                X++;
                break;
            case Direction.South:
                Y--;
                break;
            case Direction.West:
                X--;
                break;
        }
    }

    public void Move(string instructions) => instructions.ToList().ForEach(instruction =>
        {
            if(!Enum.IsDefined(typeof(Instructions), (Instructions)instruction))
            {
                throw new ArgumentException("Wrong instruction " + instruction);
            }

            switch ((Instructions)instruction)
            {
                case Instructions.Left:
                    Turn(true);
                    break;
                case Instructions.Right:
                    Turn(false);
                    break;
                case Instructions.Advance:
                    Advance();
                    break;
            }
        });
}