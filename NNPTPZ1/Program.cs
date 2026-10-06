using System;
using System.Collections.Generic;
using System.Drawing;
using NNPTPZ1.Mathematics;

namespace NNPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int MaxIterations = 30;
        private const double RootTolerance = 0.01;
        private const double ConvergenceTolerance = 0.5;

        static void Main(string[] args)
        {
            if (args.Length < 7)
            {
                Console.WriteLine("Insufficient arguments provided. Please provide: width, height, xmin, xmax, ymin, ymax, output_file_path.");
                return;
            }

            int width = int.Parse(args[0]);
            int height = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string outputFile = args[6];

            Bitmap bmp = new Bitmap(width, height);
            double xstep = (xmax - xmin) / width;
            double ystep = (ymax - ymin) / height;

            List<Complex> knownRoots = new List<Complex>();

            Polynomial polynomial = new Polynomial();
            polynomial.Add(new Complex() { Re = 1 });
            polynomial.Add(Complex.Zero);
            polynomial.Add(Complex.Zero);
            polynomial.Add(new Complex() { Re = 1 });

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
                    double y = ymin + i * ystep;
                    double x = xmin + j * xstep;

                    Complex currentValue = new Complex()
                    {
                        Re = x == 0 ? 0.0001 : x,
                        Imaginary = y == 0 ? 0.0001 : y
                    };

                    int iteration = 0;
                    for (int q = 0; q < MaxIterations; q++)
                    {
                        Complex difference = polynomial.Eval(currentValue).Divide(derivedPolynomial.Eval(currentValue));
                        currentValue = currentValue.Subtract(difference);

                        if (Math.Pow(difference.Re, 2) + Math.Pow(difference.Imaginary, 2) >= ConvergenceTolerance)
                        {
                            q--;
                        }
                        iteration++;
                    }

                    bool isKnownRoot = false;
                    int rootId = 0;
                    for (int w = 0; w < knownRoots.Count; w++)
                    {
                        if (Math.Pow(currentValue.Re - knownRoots[w].Re, 2) + Math.Pow(currentValue.Imaginary - knownRoots[w].Imaginary, 2) <= RootTolerance)
                        {
                            isKnownRoot = true;
                            rootId = w;
                            break;
                        }
                    }

                    if (!isKnownRoot)
                    {
                        knownRoots.Add(currentValue);
                        rootId = knownRoots.Count - 1;
                    }

                    Color baseColor = colors[rootId % colors.Length];
                    int r = Math.Min(Math.Max(0, baseColor.R - iteration * 2), 255);
                    int g = Math.Min(Math.Max(0, baseColor.G - iteration * 2), 255);
                    int b = Math.Min(Math.Max(0, baseColor.B - iteration * 2), 255);

                    Color finalColor = Color.FromArgb(r, g, b);
                    bmp.SetPixel(j, i, finalColor);
                }
            }

            bmp.Save(outputFile ?? "../../../out.png");
        }
    }
}
