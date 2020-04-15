using System;
using System.Diagnostics.CodeAnalysis;

public class Clock : IEquatable<Clock>
{
    public bool Equals([AllowNull] Clock other) => (hours, minutes) == (other.hours, other.minutes);

    private readonly int hours;
    private readonly int minutes;
    private const int MINUTES_IN_ONE_HOUR = 60;
    private const int HOURS_IN_ONE_DAY = 24;

    private (int extra, int modulated) ToPositiveModule(int number, int module)
    {
        double timesOverModule = (double)Math.Abs(number) / module;
        (int upper, int lower) = ((int)Math.Ceiling(timesOverModule), (int)Math.Floor(timesOverModule));
        return number < 0
            ? (-upper, number + upper * module)
            : (lower, number - lower * module);
    }

    public Clock(int hours, int minutes)
    {
        (int extraHours, int newMinutes) = ToPositiveModule(minutes, MINUTES_IN_ONE_HOUR);
        (this.hours, this.minutes) = (ToPositiveModule(hours + extraHours, HOURS_IN_ONE_DAY).modulated, newMinutes);
    }

    public Clock Add(int minutesToAdd) => new Clock(hours, minutes + minutesToAdd);


    public Clock Subtract(int minutesToSubtract) => Add(-minutesToSubtract);

    public override string ToString() => $"{hours:D2}:{minutes:D2}";
}
