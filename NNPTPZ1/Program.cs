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

            var generator = new FractalGenerator();
            var bmp = generator.Generate(width, height, xmin, xmax, ymin, ymax);

        }
    }
}
