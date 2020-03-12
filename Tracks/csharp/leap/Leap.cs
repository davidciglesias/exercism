public static class Leap
{
    public static bool IsLeapYear(int year) => DivisibleBy4(year) && (NotDivisibleBy100(year) || DivisibleBy400(year));

    private static bool DivisibleBy4(int year) => year % 4 == 0;

    private static bool NotDivisibleBy100(int year) => year % 100 != 0;

    private static bool DivisibleBy400(int year) => year % 400 == 0;
}