namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Log_<T> : VectorMath.Const<T> where T : unmanaged
{
    public static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
    private static readonly Vector<T>[] _coeffs = GetCoeffs();
    static Vector<T>[] GetCoeffs()
    {
        if (IsT<double>())
        {
            var logCoeffs = new Vector<double>[20];
            for (var i = 0; i < logCoeffs.Length; ++i)
            {
                logCoeffs[i] = new(ScalarMath.Pow(-1.0, i) / (i + 1.0));
            }
            return H.Reinterpret<Vector<double>[], Vector<T>[]>(logCoeffs);
        }
        if (IsT<float>())
        {
            var logCoeffs = new Vector<float>[10];
            for (var i = 0; i < logCoeffs.Length; ++i)
            {
                logCoeffs[i] = new(ScalarMath.Pow(-1.0f, i) / (i + 1.0f));
            }
            return H.Reinterpret<Vector<float>[], Vector<T>[]>(logCoeffs);
        }
        return default!;
    }
}


partial class VectorMath
{
    /// <summary> Calculates log. </summary>
    [VectorMath]
    public static partial Vector<T> Log<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Log(in Vector<double> x)
    {
        var isNaN = IsNaN(x);
        var isNega = OP.LessThan(x, Log_<double>._0);
        var isZero = OP.Equals(x, Log_<double>._0);
        var isPInf = IsPositiveInfinity(x);
        Decompose<double>(x, out var n, out var a);
        Decompose<double>(a, out var m, out a);
        var y = (n + m) * Log_<double>.Log_E_2 + LogBounded(a);
        return
            OP.ConditionalSelect(
                isNaN, Log_<double>.NaN,
            OP.ConditionalSelect(
                isNega, Log_<double>.NaN,
            OP.ConditionalSelect(
                isZero, Log_<double>.NInf,
            OP.ConditionalSelect(
                isPInf, Log_<double>.PInf,
            y))));
    }

    private static Vector<float> Log(in Vector<float> x)
    {
        var isNaN = IsNaN(x);
        var isNega = OP.LessThan(x, Log_<float>._0);
        var isZero = OP.Equals(x, Log_<float>._0);
        var isPInf = IsPositiveInfinity(x);
        Decompose<float>(x, out var n, out var a);
        Decompose<float>(a, out var m, out a);
        var y = (n + m) * Log_<float>.Log_E_2 + LogBounded(a);
        return
            OP.ConditionalSelect(
                isNaN, Log_<float>.NaN,
            OP.ConditionalSelect(
                isNega, Log_<float>.NaN,
            OP.ConditionalSelect(
                isZero, Log_<float>.NInf,
            OP.ConditionalSelect(
                isPInf, Log_<float>.PInf,
            y))));
    }

    /// <summary> Calculates <c>log(x)</c>. </summary>
    /// <param name="x"> $1 \le x \lt 2$ </param>
    private static Vector<double> LogBounded(in Vector<double> x)
    {
        var preScale = Vector.ConditionalSelect(
            Vector.LessThan(x, Log_<double>._Sqrt2),
            Log_<double>._1,
            Log_<double>._1p2);
        var postOffset = Vector.ConditionalSelect(
            Vector.LessThan(x, Log_<double>._Sqrt2),
            Log_<double>._0,
            Log_<double>.Log_E_2);
        var xx = x * preScale - Log_<double>._1;
        return postOffset
            + xx * (Log_<double>.Coeffs[1 - 1]
            + xx * (Log_<double>.Coeffs[2 - 1]
            + xx * (Log_<double>.Coeffs[3 - 1]
            + xx * (Log_<double>.Coeffs[4 - 1]
            + xx * (Log_<double>.Coeffs[5 - 1]
            + xx * (Log_<double>.Coeffs[6 - 1]
            + xx * (Log_<double>.Coeffs[7 - 1]
            + xx * (Log_<double>.Coeffs[8 - 1]
            + xx * (Log_<double>.Coeffs[9 - 1]
            + xx * (Log_<double>.Coeffs[10 - 1]
            + xx * (Log_<double>.Coeffs[11 - 1]
            + xx * (Log_<double>.Coeffs[12 - 1]
            + xx * (Log_<double>.Coeffs[13 - 1]
            + xx * (Log_<double>.Coeffs[14 - 1]
            + xx * (Log_<double>.Coeffs[15 - 1]
            + xx * (Log_<double>.Coeffs[16 - 1]
            + xx * (Log_<double>.Coeffs[17 - 1]
            + xx * (Log_<double>.Coeffs[18 - 1]
            + xx * (Log_<double>.Coeffs[19 - 1]
            + xx * (Log_<double>.Coeffs[20 - 1]
            ))))))))))))))))))));
    }

    /// <summary> Calculates <c>log(x + 1)</c>. </summary>
    /// <param name="x"> $1 \le x \lt 2$ </param>
    //  NOTE:
    //      $$
    //      \begin{cases}
    //      |\ln x| < \left|\ln\frac{x}{2}\right | &(x < \sqrt{2}) \\[1ex]
    //      |\ln x| = \left|\ln\frac{x}{2}\right | &(x = \sqrt{2}) \\[1ex]
    //      |\ln x| > \left|\ln\frac{x}{2}\right | &(x > \sqrt{2}) \\
    //      \end{cases}
    //      $$
    private static Vector<float> LogBounded(in Vector<float> x)
    {
        var preScale = Vector.ConditionalSelect(
            Vector.LessThan(x, Log_<float>._Sqrt2),
            Log_<float>._1,
            Log_<float>._1p2);
        var postOffset = Vector.ConditionalSelect(
            Vector.LessThan(x, Log_<float>._Sqrt2),
            Log_<float>._0,
            Log_<float>.Log_E_2);
        var xx = x * preScale - Log_<float>._1;
        return postOffset
            + xx * (Log_<float>.Coeffs[1 - 1]
            + xx * (Log_<float>.Coeffs[2 - 1]
            + xx * (Log_<float>.Coeffs[3 - 1]
            + xx * (Log_<float>.Coeffs[4 - 1]
            + xx * (Log_<float>.Coeffs[5 - 1]
            + xx * (Log_<float>.Coeffs[6 - 1]
            + xx * (Log_<float>.Coeffs[7 - 1]
            + xx * (Log_<float>.Coeffs[8 - 1]
            ))))))));
    }
}
