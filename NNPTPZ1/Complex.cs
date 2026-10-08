using System;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public readonly static Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is Complex x)
            {
                return x.Real == Real && x.Imaginary == Imaginary;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Real.GetHashCode();
                hash = hash * 23 + Imaginary.GetHashCode();
                return hash;
            }
        }

        public Complex Add(Complex b)
        {
            return new Complex()
            {
                Real = Real + b.Real,
                Imaginary = Imaginary + b.Imaginary
            };
        }

        public Complex Subtract(Complex b)
        {
            return new Complex()
            {
                Real = Real - b.Real,
                Imaginary = Imaginary - b.Imaginary
            };
        }

        public Complex Multiply(Complex b)
        {
            return new Complex()
            {
                Real = Real * b.Real - Imaginary * b.Imaginary,
                Imaginary = Real * b.Imaginary + Imaginary * b.Real
            };
        }

        public Complex Divide(Complex b)
        {
            var tmp = this.Multiply(new Complex() { Real = b.Real, Imaginary = -b.Imaginary });
            var denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;

            return new Complex()
            {
                Real = tmp.Real / denominator,
                Imaginary = tmp.Imaginary / denominator
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Real);
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }
    }
}