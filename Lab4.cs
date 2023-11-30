namespace ConsoleApp50
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double startInterval = 0;
            double endInterval = 10;

            Func<double, double> func = x => x * x;

            Func<double, double> derivative = x => 2 * x;

            Func<double, double> integrand = x => Math.Sqrt(1 + Math.Pow(derivative(x), 2));

            double length = TrapezoidalRule(integrand, startInterval, endInterval, 1000);

            Console.WriteLine($"Длина параболы y = x^2 на интервале [0, 10]: {length}");
        }

        static double TrapezoidalRule(Func<double, double> func, double startInterval, double endInterval, int trapCounts)
        {
            double step = (endInterval - startInterval) / trapCounts;
            double result = (func(startInterval) + func(endInterval)) / 2.0;

            for (int i = 1; i < trapCounts; i++)
            {
                double x = startInterval + i * step;
                result += func(x);
            }

            result *= step;
            return result;
        }
    }
}
