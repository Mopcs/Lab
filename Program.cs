namespace ConsoleApp49
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Поиск решений уравнения на интервале [-5, 5]:");

            double intervalStart = -5;
            double intervalEnd = 5;
            double step = 0.001;
            double tolerance = 0.001;
            

            double currentIntervalStart = intervalStart;
            double currentIntervalEnd = intervalStart + step;

            while (currentIntervalStart <= intervalEnd)
            {
                double equationStart = Equation(currentIntervalStart);
                double equationEnd = Equation(currentIntervalEnd);

                if (equationStart * equationEnd <= 0)
                {
                    double root = FindRootInInterval(currentIntervalStart, currentIntervalEnd, tolerance, out int steps);
                    Console.WriteLine($"Интервал [{currentIntervalStart:F3}; {currentIntervalEnd:F3}]");
                    Console.WriteLine($"Решение: x = {root:F3}");
                    Console.WriteLine($"Шаги: {steps}");

                }

                currentIntervalStart = currentIntervalEnd;
                currentIntervalEnd += step;
            }

            Console.ReadLine();
        }

        static double Equation(double x)
        {
            return Math.Pow(x, 3) - 8 * x + 1 + 5 * Math.Sin(x) - 12 * Math.Cos(x);
        }

        static double FindRootInInterval(double start, double end, double tolerance, out int steps)
        {
            steps = 0;

            while (end - start > tolerance)
            {
                double mid = (start + end) / 2;

                if (Equation(start) * Equation(mid) < 0)
                {
                    end = mid;
                }
                else
                {
                    start = mid;
                }

                //steps++;
            }

            steps++;

            return (start + end) / 2;
        }
    }
}