using System;
using System.Linq;
using System.Collections.Generic;

public enum Schedule
{
    Teenth,
    First,
    Second,
    Third,
    Fourth,
    Last
}

public class Meetup
{
    private readonly int month;
    private readonly int year;
    public Meetup(int month, int year)
    {
        this.month = month;
        this.year = year;
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        IEnumerable<DateTime> daysInMonth = Enumerable
            .Range(1, DateTime.DaysInMonth(year, month))
            .Select(day => new DateTime(year, month, day))
            .Where(date => date.DayOfWeek == dayOfWeek);

        return schedule switch
        {
            Schedule.Teenth => daysInMonth.First(date => date.Day >= 13),
            Schedule.First => daysInMonth.FirstOrDefault(),
            Schedule.Second => daysInMonth.ElementAt(1),
            Schedule.Third => daysInMonth.ElementAt(2),
            Schedule.Fourth => daysInMonth.ElementAt(3),
            Schedule.Last => daysInMonth.LastOrDefault(),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}