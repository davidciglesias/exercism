using System;

public struct ComplexNumber
{
    private readonly double real;
    private readonly double imaginary;

    public ComplexNumber(double real, double imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public double Real() => real;

    public double Imaginary() => imaginary;

    public ComplexNumber Mul(ComplexNumber other) => new ComplexNumber(real * other.real - imaginary * other.imaginary, imaginary * other.real + real * other.imaginary);

    public ComplexNumber Add(ComplexNumber other) => new ComplexNumber(real + other.real, imaginary + other.imaginary);

    public ComplexNumber Sub(ComplexNumber other) => new ComplexNumber(real - other.real, imaginary - other.imaginary);

    public ComplexNumber Div(ComplexNumber other)
    {
        double denominator = other.imaginary * other.imaginary + other.real * other.real;
        return Mul(other.Conjugate()) / denominator;
    }

    public static ComplexNumber operator /(ComplexNumber complex, double number) => new ComplexNumber(complex.real / number, complex.imaginary / number);
    public static ComplexNumber operator *(ComplexNumber complex, double number) => new ComplexNumber(complex.real * number, complex.imaginary * number);

    public double Abs() => Math.Sqrt(real * real + imaginary * imaginary);

    public ComplexNumber Conjugate() => new ComplexNumber(real, -1 * imaginary);

    public ComplexNumber Exp()
    {
        double commonFactor = Math.Exp(real);
        return new ComplexNumber(Math.Cos(imaginary), Math.Sin(imaginary)) * commonFactor;
    }
}