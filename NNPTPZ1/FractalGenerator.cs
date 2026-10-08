using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    public class FractalGenerator
    {
        private const int MaxIterations = 30;
        private const double RootTolerance = 0.01;
        private const double ConvergenceTolerance = 0.5;

        public Bitmap Generate(int width, int height, double xmin, double xmax, double ymin, double ymax)
        {
            Bitmap bmp = new Bitmap(width, height);
            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Complex> knownRoots = new List<Complex>();
            Polynomial polynomial = CreatePolynomial();
            Polynomial derivedPolynomial = polynomial.Derive();

            Console.WriteLine($"Polynomial: {polynomial}");
            Console.WriteLine($"Derivative: {derivedPolynomial}");

            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = CalculatePixelColor(i, j, xmin, ymin, xstep, ystep, polynomial, derivedPolynomial, knownRoots, colors);
                    bmp.SetPixel(j, i, pixelColor);
                }
            }

            return bmp;
        }

        private Polynomial CreatePolynomial()
        {
            Polynomial polynomial = new Polynomial();
            polynomial.Add(new Complex() { Real = 1 });
            polynomial.Add(Complex.Zero);
            polynomial.Add(Complex.Zero);
            polynomial.Add(new Complex() { Real = 1 });
            return polynomial;
        }

        private Color CalculatePixelColor(int row, int column, double xmin, double ymin, double xstep, double ystep,
            Polynomial polynomial, Polynomial derivedPolynomial, List<Complex> knownRoots, Color[] colors)
        {
            double y = ymin + row * ystep;
            double x = xmin + column * xstep;

            Complex currentValue = new Complex()
            {
                Real = x == 0 ? 0.0001 : x,
                Imaginary = y == 0 ? 0.0001 : y
            };

            int iteration = 0;
            for (int i = 0; i < MaxIterations; i++)
            {
                Complex difference = polynomial.Eval(currentValue).Divide(derivedPolynomial.Eval(currentValue));
                currentValue = currentValue.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= ConvergenceTolerance)
                {
                    i--;
                }
                iteration++;
            }

            int rootId = GetOrAddRoot(knownRoots, currentValue);

            Color baseColor = colors[rootId % colors.Length];
            int r = Math.Min(Math.Max(0, baseColor.R - iteration * 2), 255);
            int g = Math.Min(Math.Max(0, baseColor.G - iteration * 2), 255);
            int b = Math.Min(Math.Max(0, baseColor.B - iteration * 2), 255);

            return Color.FromArgb(r, g, b);
        }

        private int GetOrAddRoot(List<Complex> knownRoots, Complex currentValue)
        {
            for (int w = 0; w < knownRoots.Count; w++)
            {
                if (Math.Pow(currentValue.Real - knownRoots[w].Real, 2) + Math.Pow(currentValue.Imaginary - knownRoots[w].Imaginary, 2) <= RootTolerance)
                {
                    return w;
                }
            }

            knownRoots.Add(currentValue);
            return knownRoots.Count - 1;
        }
    }
}