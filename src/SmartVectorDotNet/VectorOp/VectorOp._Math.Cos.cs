namespace SmartVectorDotNet;

using GenericSpecialization;
using H = InternalHelpers;


file class Cos_<T> : VectorOp.Const<T> where T : unmanaged
{
    internal static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
    private static readonly Vector<T>[] _coeffs = GetCoeffs();
    static Vector<T>[] GetCoeffs()
    {
        if (IsT<double>())
        {
            var cosCoeffs = new Vector<double>[12];
            for (var n = 1; n <= 12; ++n)
            {
                double value = 1.0 / ((2 * n - 1) * (2 * n));
                cosCoeffs[n - 1] = new(value);
            }
            return H.ReinterpretVArray<double, T>(cosCoeffs);
        }
        if (IsT<float>())
        {
            var cosCoeffs = new Vector<float>[6];
            for (var n = 1; n <= 6; ++n)
            {
                float value = 1.0f / ((2 * n - 1) * (2 * n));
                cosCoeffs[n - 1] = new(value);
            }
            return H.ReinterpretVArray<float, T>(cosCoeffs);
        }
        return default!;
    }
}


#pragma warning disable format
partial class VectorOp
{
    /// <summary>
    /// Calculates cos(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Cos<T>(Vector<T> x)
        where T : unmanaged
    {
        var xx = ModuloByTau(x);
        var lessThan_1_2 = LessThan(xx, Cos_<T>.PI_1p2);
        var lessThan_3_2 = LessThan(xx, Cos_<T>.PI_3p2);
        xx =
            ConditionalSelect(lessThan_1_2,
                xx,
            ConditionalSelect(lessThan_3_2,
                Cos_<T>.PI_2p2 - xx,
                xx - Cos_<T>.PI_4p2
                ));
        var sign =
            ConditionalSelect(lessThan_1_2,
                Cos_<T>._1,
            ConditionalSelect(lessThan_3_2,
                Cos_<T>._m1,
                Cos_<T>._1
                ));
        return sign * CosBounded(xx);
    }

    [PrimaryGeneric(nameof(CosBounded_default))]
    private static partial Vector<T> CosBounded<T>(Vector<T> x)
        where T : unmanaged;

    private static Vector<T> CosBounded_default<T>(Vector<T> x) where T : unmanaged => throw new NotSupportedException();

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<double> CosBounded(Vector<double> x)
    {
        Vector<double> y;
        var x2 = x * x;
        y = x2 * Cos_<double>.Coeffs[10];                          // a_11~
        y = x2 * Cos_<double>.Coeffs[ 9] * (Cos_<double>._1 - y);  // a_10
        y = x2 * Cos_<double>.Coeffs[ 8] * (Cos_<double>._1 - y);  // a_9
        y = x2 * Cos_<double>.Coeffs[ 7] * (Cos_<double>._1 - y);  // a_8
        y = x2 * Cos_<double>.Coeffs[ 6] * (Cos_<double>._1 - y);  // a_7
        y = x2 * Cos_<double>.Coeffs[ 5] * (Cos_<double>._1 - y);  // a_6
        y = x2 * Cos_<double>.Coeffs[ 4] * (Cos_<double>._1 - y);  // a_5
        y = x2 * Cos_<double>.Coeffs[ 3] * (Cos_<double>._1 - y);  // a_4
        y = x2 * Cos_<double>.Coeffs[ 2] * (Cos_<double>._1 - y);  // a_3
        y = x2 * Cos_<double>.Coeffs[ 1] * (Cos_<double>._1 - y);  // a_2
        y = x2 * Cos_<double>.Coeffs[ 0] * (Cos_<double>._1 - y);  // a_1
        return Cos_<double>._1 - y;                                // 1 - a_1
    }

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<float> CosBounded(Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * Cos_<float>.Coeffs[5];                         // a_6~
        y = x2 * Cos_<float>.Coeffs[4] * (Cos_<float>._1 - y);  // a_5
        y = x2 * Cos_<float>.Coeffs[3] * (Cos_<float>._1 - y);  // a_4
        y = x2 * Cos_<float>.Coeffs[2] * (Cos_<float>._1 - y);  // a_3
        y = x2 * Cos_<float>.Coeffs[1] * (Cos_<float>._1 - y);  // a_2
        y = x2 * Cos_<float>.Coeffs[0] * (Cos_<float>._1 - y);  // a_1
        return Cos_<float>._1 - y;                              // 1 - a_1
    }
    
}
