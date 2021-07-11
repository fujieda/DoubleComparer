using System;

namespace DoubleComparer
{
    public static class Util
    {
        public const double DefaultTolerance = 1e-10;

        public static bool Equals(double x, double y, double tolerance = DefaultTolerance)
        {
            var diff = Math.Abs(x - y);
            return diff <= tolerance ||
                   diff <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance;
        }

        public static int Compare(double x, double y, double tolerance = DefaultTolerance)
        {
            return Equals(x, y, tolerance) ? 0 : x < y ? -1 : 1;
        }
    }
}