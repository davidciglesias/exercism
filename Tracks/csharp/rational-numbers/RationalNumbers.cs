using System;
using System.Collections.Generic;
using System.Linq;

public static class RealNumberExtension
{
    public static double Expreal(this int realNumber, RationalNumber r) => r.Expreal(realNumber);
}

public struct RationalNumber
{
    public readonly int numerator, denominator;

    private static IEnumerable<int> GetDivisors(int number)
    {
        if (number < 0) throw new ArgumentException();
        int divisor = 2;
        while (number > 1)
        {
            if (number % divisor == 0)
            {
                number /= divisor;
                yield return divisor;
            }
            else
            {
                divisor++;
            }
        }
        if (number <= 1) yield return 1;
    }

    private static Tuple<int, int> Reduce(int numerator, int denominator)
    {
        if (numerator == 0) return new Tuple<int, int>(0, 1);
        int commonDivisors = GetDivisors(Math.Abs(numerator))
                .Intersect(GetDivisors(Math.Abs(denominator)))
                .Aggregate(1, (product, divisor) => product *= divisor);
        if (Math.Sign(numerator) != Math.Sign(denominator))
        {
            numerator = -1 * Math.Abs(numerator);
            denominator = Math.Abs(denominator);
        }
        return new Tuple<int, int>(numerator / commonDivisors, denominator / commonDivisors);
    }

    public RationalNumber(int numerator, int denominator)
    {
        Tuple<int, int> simplifiedTuple = Reduce(numerator, denominator);
        if (simplifiedTuple.Item2 == 0) throw new ArgumentException();
        this.numerator = simplifiedTuple.Item1;
        this.denominator = simplifiedTuple.Item2;
    }

    public static RationalNumber operator +(RationalNumber r1, RationalNumber r2) => new RationalNumber(r1.numerator * r2.denominator + r2.numerator * r1.denominator, r1.denominator * r2.denominator);

    public static RationalNumber operator -(RationalNumber r1, RationalNumber r2) => r1 + (-1 * r2);

    public static RationalNumber operator *(int number, RationalNumber r) => new RationalNumber(number * r.numerator, r.denominator);

    public static RationalNumber operator *(RationalNumber r1, RationalNumber r2) => new RationalNumber(r1.numerator * r2.numerator, r1.denominator * r2.denominator);

    public static RationalNumber operator /(RationalNumber r1, RationalNumber r2) => r1 * new RationalNumber(r2.denominator, r2.numerator);

    public RationalNumber Abs() => new RationalNumber(Math.Abs(numerator), Math.Abs(denominator));

    public RationalNumber Reduce() => this;

    public RationalNumber Exprational(int power) => new RationalNumber((int)Math.Pow(numerator, power), (int)Math.Pow(denominator, power));
    public double Expreal(int baseNumber) => Math.Pow(Math.Pow(baseNumber, numerator), 1d/ denominator);
}