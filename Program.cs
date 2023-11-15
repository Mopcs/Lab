using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp48
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Поиск решений уравнения на интервале [-5, 5]");

            double start = -5;
            double end = 5;
            double step = 0.1;
            double tolerance = 0.001;

            double currentIntervalStart = start;
            double currentIntervalEnd = start + step;

            while (currentIntervalStart <= end)
            {
                double equationStart = Equation(currentIntervalStart);
                double equationEnd = Equation(currentIntervalEnd);

                if (equationStart * equationEnd <= 0)
                {
                    double root = SolveEquation(currentIntervalStart, currentIntervalEnd, tolerance);
                    Console.WriteLine($"Интервал [{currentIntervalStart:F3}; {currentIntervalEnd:F3}]");
                    Console.WriteLine($"Решение: x = {root:F3}");
                }

                currentIntervalStart = currentIntervalEnd;
                currentIntervalEnd += step;
            }
        }

        static double Equation(double x)
        {
            return Math.Pow(x, 3) - 8 * x + 1 + 5 * Math.Sin(x) - 12 * Math.Cos(x);
        }

        static double SolveEquation(double start, double end, double tolerance)
        {
            double delta = 2 * tolerance;
            while (Equation(start) * Equation(start + delta) > 0) 
            {
                start += delta;
            }

            return start + tolerance;
            
        }
    }


}