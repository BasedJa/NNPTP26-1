using System;

namespace NNPTPZ1.Mathematics
{
    public class Complex
    {
        public double Re { get; set; }
        public double Imaginary { get; set; }

        public readonly static Complex Zero = new Complex()
        {
            Re = 0,
            Imaginary = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is Complex x)
            {
                return x.Re == Re && x.Imaginary == Imaginary;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Re.GetHashCode();
                hash = hash * 23 + Imaginary.GetHashCode();
                return hash;
            }
        }

        public Complex Add(Complex b)
        {
            return new Complex()
            {
                Re = Re + b.Re,
                Imaginary = Imaginary + b.Imaginary
            };
        }

        public Complex Subtract(Complex b)
        {
            return new Complex()
            {
                Re = Re - b.Re,
                Imaginary = Imaginary - b.Imaginary
            };
        }

        public Complex Multiply(Complex b)
        {
            return new Complex()
            {
                Re = Re * b.Re - Imaginary * b.Imaginary,
                Imaginary = Re * b.Imaginary + Imaginary * b.Re
            };
        }

        public Complex Divide(Complex b)
        {
            var tmp = this.Multiply(new Complex() { Re = b.Re, Imaginary = -b.Imaginary });
            var denominator = b.Re * b.Re + b.Imaginary * b.Imaginary;

            return new Complex()
            {
                Re = tmp.Re / denominator,
                Imaginary = tmp.Imaginary / denominator
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(Re * Re + Imaginary * Imaginary);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan2(Imaginary, Re);
        }

        public override string ToString()
        {
            return $"({Re} + {Imaginary}i)";
        }
    }
}