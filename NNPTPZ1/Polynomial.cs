using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<Complex> Coefficient { get; set; }

        public Polynomial() => Coefficient = new List<Complex>();

        public void Add(Complex coe) => Coefficient.Add(coe);

        public Polynomial Derive()
        {
            Polynomial p = new Polynomial();
            for (int q = 1; q < Coefficient.Count; q++)
            {
                p.Coefficient.Add(Coefficient[q].Multiply(new Complex() { Re = q }));
            }
            return p;
        }

        public Complex Eval(double x)
        {
            return Eval(new Complex() { Re = x, Imaginary = 0 });
        }

        public Complex Eval(Complex x)
        {
            Complex s = Complex.Zero;
            for (int i = 0; i < Coefficient.Count; i++)
            {
                Complex coef = Coefficient[i];
                Complex bx = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        bx = bx.Multiply(x);
                    }
                    coef = coef.Multiply(bx);
                }

                s = s.Add(coef);
            }
            return s;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Coefficient.Count; i++)
            {
                s += Coefficient[i];
                if (i > 0)
                {
                    s += new string('x', i);
                }
                if (i + 1 < Coefficient.Count)
                {
                    s += " + ";
                }
            }
            return s;
        }
    }
}