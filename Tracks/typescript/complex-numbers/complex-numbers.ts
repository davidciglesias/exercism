export default class ComplexNumber {
    real: number = 0;
    imag: number = 0;

    constructor(real: number, imag: number) {
        this.real = real;
        this.imag = imag;
    }

    add(other: ComplexNumber): ComplexNumber {
        return new ComplexNumber(this.real + other.real, this.imag + other.imag);
    }
    sub(other: ComplexNumber): ComplexNumber {
        return new ComplexNumber(this.real - other.real, this.imag - other.imag);
    }
    mul(other: ComplexNumber): ComplexNumber {
        return new ComplexNumber(
            this.real * other.real - this.imag * other.imag,
            this.imag * other.real + this.real * other.imag
        );
    }
    div(other: ComplexNumber): ComplexNumber {
        const denominator = other.real ** 2 + other.imag ** 2;
        return new ComplexNumber(
            (this.real * other.real + this.imag * other.imag) / denominator,
            (this.imag * other.real - this.real * other.imag) / denominator
        );
    }
    abs(): number {
        return Math.sqrt(this.real ** 2 + this.imag ** 2);
    }
    conj(): ComplexNumber {
        return new ComplexNumber(this.real, this.imag === 0 ? 0 : -1 * this.imag);
    }
    exp(): ComplexNumber {
        const expReal = Math.exp(this.real);
        return new ComplexNumber(expReal * Math.cos(this.imag), expReal * Math.sin(this.imag));
    }
}
