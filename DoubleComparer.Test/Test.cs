using System;
using Xunit;


namespace DoubleComparer.Test
{
    public class Test
    {
        [Theory]
        [InlineData(1.0 - 0.9, 0.1)]
        [InlineData(0.15 + 0.15, 0.1 + 0.2)]
        public void NotEqualByError(double x, double y)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            Assert.False(x == y);
        }

        [Fact]
        public void NotEqualByCumulativeError()
        {
            var x = 0d;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            Assert.False(x == 1.0);
        }

        [Theory]
        [InlineData(1.0 - 0.9, 0.1)]
        [InlineData(0.15 + 0.15, 0.1 + 0.2)]
        public void EqualWithTolerance(double x, double y)
        {
            Assert.True(Util.Equals(x, y));
        }

        [Fact]
        public void EqualWithToleranceAgainstCumulativeError()
        {
            var x = 0d;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            Assert.True(Util.Equals(x, 1.0));
        }

        [Fact]
        public void NotEqualWithToleranceForAbsError()
        {
            const double tolerance = 1e-10;
            double y;
            var x = y = 1e6;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            y += 1.0;
            Assert.False(Math.Abs(x - y) <= tolerance);
        }

        [Fact]
        public void EqualWithToleranceForRelativeError()
        {
            const double tolerance = 1e-10;
            double y;
            var x = y = 1e6;
            for (var i = 0; i < 10; i++)
                x += 0.1;
            y += 1.0;
            Assert.True(Math.Abs(x - y) <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance);
        }

        [Fact]
        public void NotEqualWithToleranceForRelativeError()
        {
            const double tolerance = 1e-10;
            var x = 1e-11;
            var y = 1e-12;
            Assert.False(Math.Abs(x - y) <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance);
        }

        [Fact]
        public void EqualWithToleranceForRelativeAndAbsError()
        {
            const double tolerance = 1e-10;
            var x = 1e-11;
            var y = 1e-12;
            Assert.True(Math.Abs(x - y) <= tolerance ||
                                 Math.Abs(x - y) <= Math.Max(Math.Abs(x), Math.Abs(y)) * tolerance);
        }

        [Fact]
        public void Compare()
        {
            const double x = 0.1;
            Assert.Equal(1, Util.Compare(x + Util.DefaultTolerance * 2, x));
            Assert.Equal(0, Util.Compare(x, x));
            Assert.Equal(-1, Util.Compare(x, x + Util.DefaultTolerance * 2));
        }
    }
}