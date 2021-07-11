using System.Collections.Generic;

namespace DoubleComparer
{
    public class Comparer : EqualityComparer<double>, IComparer<double>
    {
        private readonly double _tolerance;

        public Comparer(double tolerance = Util.DefaultTolerance)
        {
            _tolerance = tolerance;
        }

        public int Compare(double x, double y)
        {
            return Util.Compare(x, y, _tolerance);
        }

        public override bool Equals(double x, double y)
        {
            return Util.Equals(x, y, _tolerance);
        }

        public override int GetHashCode(double obj)
        {
            return obj.GetHashCode();
        }
    }
}