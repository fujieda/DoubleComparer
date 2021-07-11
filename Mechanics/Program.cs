using System;

namespace Mechanics
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var one = 0d;
            for (var i = 0; i < 10; i++)
                one += 0.1;
            var pairs = new[]
            {
                (1.0 - 0.9, 0.1),
                (0.15 + 0.15, 0.1 + 0.2),
                (one, 1.0)
            };
            StrictEqual(pairs);
            AbsError(pairs);
            AbsErrorProblem();
            RelativeError();
            RelativeErrorProblem();
            TolerateBothAbsAndRelativeError();
            UseMachineEpsilon();
        }

        private static void StrictEqual((double, double)[] pairs)
        {
            foreach (var p in pairs)
            {
                var (x, y) = p;
                Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                                  $"x == y: {x == y}, Abs(x-y): {Math.Abs(x - y):f18}");
            }
        }

        private static void AbsError((double, double)[] pairs)
        {
            const double tolerance = 1e-10;
            foreach (var p in pairs)
            {
                var (x, y) = p;
                var r = Math.Abs(x - y) <= tolerance;
                Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                                  $"TolerateAbsError: {r}, Abs(x-y): {Math.Abs(x - y):f18}");
            }
        }

        private static void AbsErrorProblem()
        {
            const double tolerance = 1e-10;
            double x, y;
            x = y = 1e6;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            y += 1.0;
            var r = Math.Abs(x - y) <= tolerance;
            Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                              $"TolerateAbsError: {r}, Abs(x-y): {Math.Abs(x - y):f18}");
        }

        private static void RelativeError()
        {
            const double tolerance = 1e-10;
            double x, y;
            x = y = 1e6;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            y += 1.0;
            var r = Math.Abs(x - y) <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance;
            var error = Math.Abs(x - y) / Math.Max(Math.Abs(x), Math.Abs(y));
            Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                              $"TolerateRelativeError: {r}, RelativeError: {error:f18}");
        }

        private static void RelativeErrorProblem()
        {
            const double tolerance = 1e-10;
            var x = 1e-11;
            var y = 1e-12;
            var r = Math.Abs(x - y) <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance;
            var error = Math.Abs(x - y) / Math.Max(Math.Abs(x), Math.Abs(y));
            Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                              $"TolerateRelativeError: {r}, RelativeError: {error:f18}");
        }

        private static bool Equals(double x, double y, double tolerance)
        {
            var diff = Math.Abs(x - y);
            return diff <= tolerance ||
                   diff <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance;
        }

        private static void TolerateBothAbsAndRelativeError()
        {
            const double tolerance = 1e-10;
            double x, y;
            bool r;
            x = y = 1e6;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            y += 1.0;
            r = Equals(x, y, tolerance);
            Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                              $"TolerateRelativeAndAbsError: {r}");
            x = 1e-11;
            y = 1e-12;
            r = Equals(x, y, tolerance);
            Console.WriteLine($"x: {x:f18}, y: {y:f18},\r\n    " +
                              $"TolerateRelativeAndAbsError: {r}");
        }

        private static void UseMachineEpsilon()
        {
            var one = 0d;
            for (var i = 0; i < 10; i++)
                one += 0.1;
            var r = Equals(one, 1.0, double.Epsilon);
            Console.WriteLine($"x: {one:f18}, y: {1.0:f18},\r\n    " +
                              $"UseMachineEpsilonAsTolerance: {r}");
        }
    }
}