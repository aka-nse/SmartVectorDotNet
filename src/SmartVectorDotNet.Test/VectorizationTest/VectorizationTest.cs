#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
namespace SmartVectorDotNet;


public partial class VectorizationTest
{
    private const AccuracyMode TestAccuracyMode
        = AccuracyMode.AbsoluteOrRelative | AccuracyMode.RelaxNaNCheck;

    private static Vectorization Emulated => Vectorization.Emulated;
    private static Vectorization SIMD => Vectorization.SIMD;

    public abstract class UnaryOperatorTestSuite
    {
        protected abstract void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) where T : unmanaged;

        public void DoubleAccuracy(double[] x)
        { // double accuracy
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Operate<double>(Emulated, x, exp);
            Operate<double>(SIMD, x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }

        public void FloatAccuracy(double[] x)
        { // float accuracy
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Operate<float>(Emulated, xx, exp);
            Operate<float>(SIMD, xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }

        public void Overlap(Vectorization vectorization, double[] x)
        {
            { // fully overlap
                var exp = new double[x.Length];
                var act = new double[x.Length];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act, exp);
                Operate<double>(vectorization, act, act);
                Assert.Equal(exp, act);
            }
            if (x.Length >= 6)
            { // partial overlap left
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act[..x.Length], exp);
                Operate<double>(vectorization, act[..x.Length], act[6..]);
                Assert.Equal(exp, act[6..]);
            }
            if (x.Length >= 6)
            { // partial overlap right
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act[6..]);
                Operate<double>(vectorization, act[6..], exp);
                Operate<double>(vectorization, act[6..], act[..x.Length]);
                Assert.Equal(exp, act[..x.Length]);
            }
        }
    }

    private static partial UnaryOperatorTestSuite[] UnaryOperatorTestSuites();

    public static TheoryData<UnaryOperatorTestSuite, double[]> UnaryOperatorTestCases()
    {
        double[][] arguments = [
            [],
            [0],
            [0, 1, 2, 3],
            [0, 1, 2, 3, 4],
            [0, 1, 2, 3, 4, 5, 6, 7],
            [0, 1, 2, 3, 4, 5, 6, 7, 8],
            [1e-3, 2e-3, 3e-3, 5e-3, 1e-2, 2e-2, 3e-2, 5e-2, 1e-1, 2e-1, 3e-1, 5e-1, 1e+0, 2e+0, 3e+0, 5e+0, 1e+1, 2e+1, 3e+1, 5e+1, 1e+2, 2e+2, 3e+2, 5e+2, 1e+3, 2e+3, 3e+3, 5e+3,],
        ];
        var data = new TheoryData<UnaryOperatorTestSuite, double[]>();
        foreach(var suite in UnaryOperatorTestSuites())
        {
            foreach (var x in arguments)
            {
                data.Add(suite, x);
            }
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void UnaryOperator(UnaryOperatorTestSuite suite, double[] x)
    {
        suite.DoubleAccuracy(x);
        suite.FloatAccuracy(x);
        suite.Overlap(Vectorization.Emulated, x);
        suite.Overlap(Vectorization.SIMD, x);
    }


    public abstract class BinaryOperatorTestSuite
    {
        protected abstract void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) where T : unmanaged;
        protected abstract void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) where T : unmanaged;
        protected abstract void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) where T : unmanaged;

        public void DoubleAccuracy(double[] x, double[] y)
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Operate<double>(Emulated, x.FirstOrDefault(), y, exp);
            Operate<double>(SIMD, x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Operate<double>(Emulated, x, y.FirstOrDefault(), exp);
            Operate<double>(SIMD, x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Operate<double>(Emulated, x, y, exp);
            Operate<double>(SIMD, x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }

        public void FloatAccuracy(double[] x, double[] y)
        { // float accuracy
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Operate<float>(Emulated, xx.FirstOrDefault(), yy, exp);
            Operate<float>(SIMD, xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Operate<float>(Emulated, xx, yy.FirstOrDefault(), exp);
            Operate<float>(SIMD, xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Operate<float>(Emulated, xx, yy, exp);
            Operate<float>(SIMD, xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }

        public void RhsOverlap(Vectorization vectorization, double x, double[] y)
        {
            { // fully overlap y
                var exp = new double[y.Length];
                var act = new double[y.Length];
                y.AsSpan().CopyTo(act);
                Operate<double>(vectorization, x, act, exp);
                Operate<double>(vectorization, x, act, act);
                Assert.Equal(exp, act);
            }
            if (y.Length < 6)
            {
                return;
            }
            { // partial overlap left y
                var exp = new double[y.Length];
                var act = new double[y.Length + 6];
                y.AsSpan().CopyTo(act);
                Operate<double>(vectorization, x, act[..y.Length], exp);
                Operate<double>(vectorization, x, act[..y.Length], act[6..]);
                Assert.Equal(exp, act[6..]);
            }
            { // partial overlap right y
                var exp = new double[y.Length];
                var act = new double[y.Length + 6];
                y.AsSpan().CopyTo(act[6..]);
                Operate<double>(vectorization, x, act[6..], exp);
                Operate<double>(vectorization, x, act[6..], act[..y.Length]);
                Assert.Equal(exp, act[..y.Length]);
            }
        }

        public void LhsOverlap(Vectorization vectorization, double[] x, double y)
        {
            { // fully overlap x
                var exp = new double[x.Length];
                var act = new double[x.Length];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act, y, exp);
                Operate<double>(vectorization, act, y, act);
                Assert.Equal(exp, act);
            }
            if (x.Length < 6)
            {
                return;
            }
            { // partial overlap left x
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act[..x.Length], y, exp);
                Operate<double>(vectorization, act[..x.Length], y, act[6..]);
                Assert.Equal(exp, act[6..]);
            }
            { // partial overlap right x
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act[6..]);
                Operate<double>(vectorization, act[6..], y, exp);
                Operate<double>(vectorization, act[6..], y, act[..x.Length]);
                Assert.Equal(exp, act[..x.Length]);
            }
        }

        public void BothOverlap(Vectorization vectorization, double[] x, double[] y)
        {
            { // fully overlap x
                var exp = new double[x.Length];
                var act = new double[x.Length];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act, y, exp);
                Operate<double>(vectorization, act, y, act);
                Assert.Equal(exp, act);
            }
            { // fully overlap y
                var exp = new double[y.Length];
                var act = new double[y.Length];
                y.AsSpan().CopyTo(act);
                Operate<double>(vectorization, x, act, exp);
                Operate<double>(vectorization, x, act, act);
                Assert.Equal(exp, act);
            }
            if (x.Length < 6 || y.Length < 6)
            {
                return;
            }
            { // partial overlap left x
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act);
                Operate<double>(vectorization, act[..x.Length], y, exp);
                Operate<double>(vectorization, act[..x.Length], y, act[6..]);
                Assert.Equal(exp, act[6..]);
            }
            { // partial overlap right x
                var exp = new double[x.Length];
                var act = new double[x.Length + 6];
                x.AsSpan().CopyTo(act[6..]);
                Operate<double>(vectorization, act[6..], y, exp);
                Operate<double>(vectorization, act[6..], y, act[..x.Length]);
                Assert.Equal(exp, act[..x.Length]);
            }
            { // partial overlap left y
                var exp = new double[y.Length];
                var act = new double[y.Length + 6];
                y.AsSpan().CopyTo(act);
                Operate<double>(vectorization, x, act[..y.Length], exp);
                Operate<double>(vectorization, x, act[..y.Length], act[6..]);
                Assert.Equal(exp, act[6..]);
            }
            { // partial overlap right y
                var exp = new double[y.Length];
                var act = new double[y.Length + 6];
                y.AsSpan().CopyTo(act[6..]);
                Operate<double>(vectorization, x, act[6..], exp);
                Operate<double>(vectorization, x, act[6..], act[..y.Length]);
                Assert.Equal(exp, act[..y.Length]);
            }
        }
    }

    private static partial BinaryOperatorTestSuite[] BinaryOperatorTestSuites();

    public static TheoryData<BinaryOperatorTestSuite, double[], double[]> BinaryOperatorTestCases()
    {
        (double[], double[])[] arguments = [
            ([], []),
            ([-1.0], [-1.0]),
            ([-1.0], [+0.0]),
            ([-1.0], [+1.0]),
            ([+0.0], [-1.0]),
            ([+0.0], [+0.0]),
            ([+0.0], [+1.0]),
            ([+1.0], [-1.0]),
            ([+1.0], [+0.0]),
            ([+1.0], [+1.0]),
            ([0, 1, 2, 3, 4, 5, 6, 7, 8], [0, 1, 2, 3, 4, 5, 6, 7, 8]),
            (
            [1e-3, 2e-3, 3e-3, 5e-3, 1e-2, 2e-2, 3e-2, 5e-2, 1e-1, 2e-1, 3e-1, 5e-1, 1e+0, 2e+0, 3e+0, 5e+0, 1e+1, 2e+1, 3e+1, 5e+1, 1e+2, 2e+2, 3e+2, 5e+2, 1e+3, 2e+3, 3e+3, 5e+3,],
                [1e-3, 2e-3, 3e-3, 5e-3, 1e-2, 2e-2, 3e-2, 5e-2, 1e-1, 2e-1, 3e-1, 5e-1, 1e+0, 2e+0, 3e+0, 5e+0, 1e+1, 2e+1, 3e+1, 5e+1, 1e+2, 2e+2, 3e+2, 5e+2, 1e+3, 2e+3, 3e+3, 5e+3,]
            ),
        ];
        var data = new TheoryData<BinaryOperatorTestSuite, double[], double[]>();
        foreach (var suite in BinaryOperatorTestSuites())
        {
            foreach (var (x, y) in arguments)
            {
                data.Add(suite, x, y);
            }
        }
        return data;
    }

    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void BinaryOperator(BinaryOperatorTestSuite suite, double[] x, double[] y)
    {
        suite.DoubleAccuracy(x, y);
        suite.FloatAccuracy(x, y);
        suite.RhsOverlap(Vectorization.Emulated, x.FirstOrDefault(), y);
        suite.RhsOverlap(Vectorization.SIMD, x.FirstOrDefault(), y);
        suite.LhsOverlap(Vectorization.Emulated, x, y.FirstOrDefault());
        suite.LhsOverlap(Vectorization.SIMD, x, y.FirstOrDefault());
        suite.BothOverlap(Vectorization.Emulated, x, y);
        suite.BothOverlap(Vectorization.SIMD, x, y);
    }
}