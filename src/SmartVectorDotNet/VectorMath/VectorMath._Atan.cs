using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Atan_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
    private static readonly Vector<T>[] _coeffs = GetCoeffs();
    static Vector<T>[] GetCoeffs()
    {
        if (IsT<double>())
        {
            var atanCoeffs = new Vector<double>[11];
            for (var n = 1; n <= 11; ++n)
            {
                atanCoeffs[n - 1] = new(ScalarMath.Pow(-1.0, n) / (2 * n + 1));
            }
            return H.ReinterpretVArray<double, T>(atanCoeffs);
        }
        if (IsT<float>())
        {
            var atanCoeffs = new Vector<float>[6];
            for (var n = 1; n <= 6; ++n)
            {
                atanCoeffs[n - 1] = new(ScalarMath.Pow(-1f, n) / (2 * n + 1));
            }
            return H.ReinterpretVArray<float, T>(atanCoeffs);
        }
        return default!;
    }

    internal static readonly Vector<T> CoreReflectThreshold
        = AsVector(ScalarMath.Sqrt(2.0) - 1);
    internal static readonly Vector<T> CoreReflectedOffset
        = AsVector(ScalarMath.Const<double>.PI / 4);
    internal static readonly Vector<T> ReflectOffset
        = AsVector(ScalarMath.Const<double>.PI / 2);
}


partial class VectorMath
{
    /// <summary>
    /// Calculates atan(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Atan<T>(Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Atan(Vector<double> x)
    {
        static Vector<double> core(Vector<double> x)
        {
            var condition = OP.LessThan(x, Atan_<double>.CoreReflectThreshold);
            x = OP.ConditionalSelect(
                condition,
                x,
                (x - Atan_<double>._1) / (x + Atan_<double>._1)
            );
            var offset = OP.ConditionalSelect(
                condition,
                Atan_<double>._0,
                Atan_<double>.CoreReflectedOffset
            );
            var x2 = x * x;
            return x * (Atan_<double>._1
                + x2 * (Atan_<double>.Coeffs[0]
                + x2 * (Atan_<double>.Coeffs[1]
                + x2 * (Atan_<double>.Coeffs[2]
                + x2 * (Atan_<double>.Coeffs[3]
                + x2 * (Atan_<double>.Coeffs[4]
                + x2 * (Atan_<double>.Coeffs[5]
                + x2 * (Atan_<double>.Coeffs[6]
                + x2 * (Atan_<double>.Coeffs[7]
                + x2 * (Atan_<double>.Coeffs[8]
                + x2 * (Atan_<double>.Coeffs[9]
                + x2 * (Atan_<double>.Coeffs[10]
                )))))))))))) + offset;
        }

        var sign = SignFast(x);
        var xx = sign * x;
        var xIsLessThan1 = OP.LessThan(xx, Atan_<double>._1);
        xx = OP.ConditionalSelect(
            xIsLessThan1,
            xx,
            Atan_<double>._1 / xx
        );
        var y = core(xx);
        return sign * OP.ConditionalSelect(
            xIsLessThan1,
            y,
            Atan_<double>.ReflectOffset - y
        );
    }

    private static Vector<float> Atan(Vector<float> x)
    {
        static Vector<float> core(Vector<float> x)
        {
            var condition = OP.LessThan(x, Atan_<float>.CoreReflectThreshold);
            x = OP.ConditionalSelect(
                condition,
                x,
                (x - Atan_<float>._1) / (x + Atan_<float>._1)
            );
            var offset = OP.ConditionalSelect(
                condition,
                Vector<float>.Zero,
                Atan_<float>.CoreReflectedOffset
            );
            var x2 = x * x;
            return x * (Atan_<float>._1
                + x2 * (Atan_<float>.Coeffs[0]
                + x2 * (Atan_<float>.Coeffs[1]
                + x2 * (Atan_<float>.Coeffs[2]
                + x2 * (Atan_<float>.Coeffs[3]
                + x2 * (Atan_<float>.Coeffs[4]
                + x2 * (Atan_<float>.Coeffs[5]
                ))))))) + offset;
        }

        var sign = SignFast(x);
        x = sign * x;
        var xx = OP.ConditionalSelect(
            OP.LessThan(x, Atan_<float>._1),
            x,
            Atan_<float>._1 / x
        );
        var y = core(xx);
        return sign * OP.ConditionalSelect(
            OP.LessThan(x, Atan_<float>._1),
            y,
            Atan_<float>.ReflectOffset - y
        );
    }

}
