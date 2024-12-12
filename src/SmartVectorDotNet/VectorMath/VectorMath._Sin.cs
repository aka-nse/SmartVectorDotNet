namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Sin_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
    private static readonly Vector<T>[] _coeffs;

    static Sin_()
    {
        if (IsT<double>())
        {
            var cosCoeffs = new Vector<double>[10];
            for (var n = 1; n <= cosCoeffs.Length; ++n)
            {
                var _2n = 2 * n;
                cosCoeffs[n - 1] = new(-1.0 / (_2n * (_2n + 1)));
            }
            _coeffs = H.ReinterpretVArray<double, T>(cosCoeffs);
            return;
        }
        if (IsT<float>())
        {
            var cosCoeffs = new Vector<float>[6];
            for (var n = 1; n <= cosCoeffs.Length; ++n)
            {
                var _2n = 2 * n;
                cosCoeffs[n - 1] = new((float)(-1.0 / (_2n * (_2n + 1))));
            }
            _coeffs = H.ReinterpretVArray<float, T>(cosCoeffs);
            return;
        }
        _coeffs = default!;
    }
}


#pragma warning disable format
partial class VectorMath
{
    /// <summary>
    /// Calculates sin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sin<T>(Vector<T> x)
        where T : unmanaged
    {
        var xx = ModuloByTau(x);
        var lessThan_1_2 = OP.LessThan(xx, Sin_<T>.PI_1p2);
        var lessThan_3_2 = OP.LessThan(xx, Sin_<T>.PI_3p2);
        xx =
            OP.ConditionalSelect(lessThan_1_2,
                xx,
            OP.ConditionalSelect(lessThan_3_2,
                Sin_<T>.PI_2p2 - xx,
                xx - Sin_<T>.PI_4p2
                ));
        return SinBounded(xx);
    }

    [VectorMath]
    private static partial Vector<T> SinBounded<T>(Vector<T> x)
        where T : unmanaged;
    
    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<double> SinBounded(Vector<double> x)
    {
        var coeffs = Sin_<double>.Coeffs;
        var _1 = Sin_<double>._1;

        var x2 = x * x;
        var y = _1;
        y = FusedMultiplyAdd(coeffs[10 - 1] * x2, y, _1);  // a_10
        y = FusedMultiplyAdd(coeffs[ 9 - 1] * x2, y, _1);  // a_9
        y = FusedMultiplyAdd(coeffs[ 8 - 1] * x2, y, _1);  // a_8
        y = FusedMultiplyAdd(coeffs[ 7 - 1] * x2, y, _1);  // a_7
        y = FusedMultiplyAdd(coeffs[ 6 - 1] * x2, y, _1);  // a_6
        y = FusedMultiplyAdd(coeffs[ 5 - 1] * x2, y, _1);  // a_5
        y = FusedMultiplyAdd(coeffs[ 4 - 1] * x2, y, _1);  // a_4
        y = FusedMultiplyAdd(coeffs[ 3 - 1] * x2, y, _1);  // a_3
        y = FusedMultiplyAdd(coeffs[ 2 - 1] * x2, y, _1);  // a_2
        y = FusedMultiplyAdd(coeffs[ 1 - 1] * x2, y, _1);  // a_1
        return x * y;
    }

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<float> SinBounded(Vector<float> x)
    {
        var coeffs = Sin_<float>.Coeffs;
        var _1 = Sin_<float>._1;

        var x2 = x * x;
        var y = _1;
        y = FusedMultiplyAdd(coeffs[6 - 1] * x2, y, _1);  // a_6
        y = FusedMultiplyAdd(coeffs[5 - 1] * x2, y, _1);  // a_5
        y = FusedMultiplyAdd(coeffs[4 - 1] * x2, y, _1);  // a_4
        y = FusedMultiplyAdd(coeffs[3 - 1] * x2, y, _1);  // a_3
        y = FusedMultiplyAdd(coeffs[2 - 1] * x2, y, _1);  // a_2
        y = FusedMultiplyAdd(coeffs[1 - 1] * x2, y, _1);  // a_1
        return x * y;
    }
}
