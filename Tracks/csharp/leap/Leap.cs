using System;

public static class Leap
{
    public static bool IsLeapYear(int year)
    {
        return DivisibleBy4(year) && (NotDivisibleBy100(year) || DivisibleBy400(year));

        static bool DivisibleBy4(int year)
        {
            return year % 4 == 0;
        }

        static bool NotDivisibleBy100(int year)
        {
            return year % 100 != 0;
        }

        static bool DivisibleBy400(int year)
        {
            return year % 400 == 0;
        }
    }
}