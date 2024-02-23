using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

#pragma warning disable format
partial class VectorMath
{
    #region ModuloForTrigonometrics

    private static Vector<T> ModuloByTau<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * ModuloByTau_<T>.OnePerTau);
        var reminderHigh = FusedMultiplyAdd(quotient, -ModuloByTau_<T>.TauUpper, x);
        return FusedMultiplyAdd(quotient, -ModuloByTau_<T>.TauLower, reminderHigh);
    }

    private class ModuloByTau_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> OnePerTau
            = AsVector(0.159154943091895);
        internal static readonly Vector<T> TauUpper
            = IsT<double>()
                ? As<double>(new(6.28318530717958))
            : IsT<float>()
                ? As<float>(new(6.283185f))
            : default;
        internal static readonly Vector<T> TauLower
            = IsT<double>()
                ? As<double>(new(0.00000000000000647692528676655901))
            : IsT<float>()
                ? As<float>(new(0.0000003071796f))
            : default;
    }

    private static Vector<T> ModuloByPI<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * ModuloByPI_<T>.OnePerPi);
        var reminderHigh = FusedMultiplyAdd(quotient, -ModuloByPI_<T>.PIUpper, x);
        return FusedMultiplyAdd(quotient, -ModuloByPI_<T>.PILower, reminderHigh);
    }

    private class ModuloByPI_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> OnePerPi
            = AsVector(0.3183098861837907);
        internal static readonly Vector<T> PIUpper
            = IsT<double>()
                ? As<double>(new(3.141592653589793))
            : IsT<float>()
                ? As<float>(new(3.1415925f))
            : default;
        internal static readonly Vector<T> PILower
            = IsT<double>()
                ? As<double>(new(0.00000000000000023846264338327950))
            : IsT<float>()
                ? As<float>(new(0.0000001509958f))
            : default;
    }

    #endregion

    #region Cos

    /// <summary>
    /// Calculates cos(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Cos<T>(in Vector<T> x)
        where T : unmanaged
    {
        var xx = ModuloByTau(x);
        var lessThan_1_2 = OP.LessThan(xx, Cos_<T>.PI_1p2);
        var lessThan_3_2 = OP.LessThan(xx, Cos_<T>.PI_3p2);
        xx =
            OP.ConditionalSelect(lessThan_1_2,
                xx,
            OP.ConditionalSelect(lessThan_3_2,
                Cos_<T>.PI_2p2 - xx,
                xx - Cos_<T>.PI_4p2
                ));
        var sign =
            OP.ConditionalSelect(lessThan_1_2,
                Cos_<T>._1,
            OP.ConditionalSelect(lessThan_3_2,
                Cos_<T>._m1,
                Cos_<T>._1
                ));
        return sign * CosBounded(xx);
    }

    [VectorMath]
    private static partial Vector<T> CosBounded<T>(in Vector<T> x)
        where T : unmanaged;

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<double> CosBounded(in Vector<double> x)
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
    private static Vector<float> CosBounded(in Vector<float> x)
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
    
    private class Cos_<T> : Const<T> where T : unmanaged
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
                    cosCoeffs[n - 1] = new (value);
                }
                return H.ReinterpretVArray<double, T>(cosCoeffs);
            }
            if(IsT<float>())
            {
                var cosCoeffs = new Vector<float>[6];
                for (var n = 1; n <= 6; ++n)
                {
                    float value = 1.0f / ((2 * n - 1) * (2 * n));
                    cosCoeffs[n - 1] = new (value);
                }
                return H.ReinterpretVArray<float, T>(cosCoeffs);
            }
            return default!;
        }

    }

    #endregion

    #region Sin

    /// <summary>
    /// Calculates sin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sin<T>(in Vector<T> x)
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
    private static partial Vector<T> SinBounded<T>(in Vector<T> x)
        where T : unmanaged;
    
    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<double> SinBounded(in Vector<double> x)
    {
        Vector<double> y;
        var x2 = x * x;
        y = x2 * Sin_<double>.Coeffs[10];                          // a_11~
        y = x2 * Sin_<double>.Coeffs[ 9] * (Sin_<double>._1 - y);  // a_10
        y = x2 * Sin_<double>.Coeffs[ 8] * (Sin_<double>._1 - y);  // a_9
        y = x2 * Sin_<double>.Coeffs[ 7] * (Sin_<double>._1 - y);  // a_8
        y = x2 * Sin_<double>.Coeffs[ 6] * (Sin_<double>._1 - y);  // a_7
        y = x2 * Sin_<double>.Coeffs[ 5] * (Sin_<double>._1 - y);  // a_6
        y = x2 * Sin_<double>.Coeffs[ 4] * (Sin_<double>._1 - y);  // a_5
        y = x2 * Sin_<double>.Coeffs[ 3] * (Sin_<double>._1 - y);  // a_4
        y = x2 * Sin_<double>.Coeffs[ 2] * (Sin_<double>._1 - y);  // a_3
        y = x2 * Sin_<double>.Coeffs[ 1] * (Sin_<double>._1 - y);  // a_2
        y = x2 * Sin_<double>.Coeffs[ 0] * (Sin_<double>._1 - y);  // a_1
        return x * (Sin_<double>._1 - y);                          // 1 - a_1
    }

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<float> SinBounded(in Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * Sin_<float>.Coeffs[5];                         // a_6~
        y = x2 * Sin_<float>.Coeffs[4] * (Sin_<float>._1 - y);  // a_5
        y = x2 * Sin_<float>.Coeffs[3] * (Sin_<float>._1 - y);  // a_4
        y = x2 * Sin_<float>.Coeffs[2] * (Sin_<float>._1 - y);  // a_3
        y = x2 * Sin_<float>.Coeffs[1] * (Sin_<float>._1 - y);  // a_2
        y = x2 * Sin_<float>.Coeffs[0] * (Sin_<float>._1 - y);  // a_1
        return x * (Sin_<float>._1 - y);                        // 1 - a_1
    }

    private class Sin_<T> : Const<T> where T : unmanaged
    {
        internal static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
        private static readonly Vector<T>[] _coeffs = GetSinCoeffs();
        static Vector<T>[] GetSinCoeffs()
        {
            if (IsT<double>())
            {
                var cosCoeffs = new Vector<double>[11];
                for (var n = 1; n <= 11; ++n)
                {
                    double value = 1.0 / ((2 * n + 1) * (2 * n));
                    cosCoeffs[n - 1] = new (value);
                }
                return H.ReinterpretVArray<double, T>(cosCoeffs);
            }
            if (IsT<float>())
            {
                var cosCoeffs = new Vector<float>[6];
                for (var n = 1; n <= 6; ++n)
                {
                    float value = 1.0f / ((2 * n + 1) * (2 * n));
                    cosCoeffs[n - 1] = new (value);
                }
                return H.ReinterpretVArray<float, T>(cosCoeffs);
            }
            return default!;
        }
    }

    #endregion

    #region Tan

    /// <summary>
    /// Calculates tan(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Tan<T>(in Vector<T> x)
        where T : unmanaged
    {
        var xx = ModuloByPI(x);
        var shouldReverse = OP.GreaterThan(xx, Tan_<T>.PI_1p2);
        var sign = OP.ConditionalSelect(
            shouldReverse,
            Tan_<T>._m1,
            Tan_<T>._1);
        xx = OP.ConditionalSelect(
            shouldReverse,
            Tan_<T>.PI - xx,
            xx);
        var modifiesByAdditionTheorem = OP.GreaterThan(xx, Tan_<T>.PI_1p4);
        xx = OP.ConditionalSelect(
            modifiesByAdditionTheorem,
            xx - Tan_<T>.PI_1p4,
            xx);
        var tanxx = TanBounded(xx);
        return sign * OP.ConditionalSelect(
            modifiesByAdditionTheorem,
            (Tan_<T>._1 + tanxx) / (Tan_<T>._1 - tanxx),
            tanxx);
    }

    private static Vector<T> TanBounded<T>(in Vector<T> x)
        where T : unmanaged
        => SinBounded(x) / CosBounded(x);

    private class Tan_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> PI_1p4 = AsVector(0.25) * PI;
    }

    #endregion

    #region Atan

    /// <summary>
    /// Calculates atan(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Atan<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Atan(in Vector<double> x)
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

    private class Atan_<T> : Const<T> where T : unmanaged
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

    #endregion

    #region Asin

    /// <summary>
    /// Calculates asin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Asin<T>(in Vector<T> x)
        where T : unmanaged
        => Asin_<T>._2 * Atan(x / (x + Sqrt(Asin_<T>._1 - x * x)));

    private class Asin_<T> : Const<T> where T : unmanaged { }

    #endregion

    #region Acos

    /// <summary>
    /// Calculates acos(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Acos<T>(in Vector<T> x)
        where T : unmanaged
        => Acos_<T>._2 * Atan(Sqrt(Acos_<T>._1 - x * x) / (Acos_<T>._1 + x));

    private class Acos_<T> : Const<T> where T : unmanaged { }

    #endregion

    #region Atan2

    /// <summary>
    /// Calculates atan2(y, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Atan2<T>(in Vector<T> y, in Vector<T> x)
        where T : unmanaged
    {
        var a = Atan(y / x);
        Vector<T> signY = SignFast(y);
        return OP.ConditionalSelect(
            OP.Equals(x, Atan2_<T>._0),
            OP.ConditionalSelect(
                OP.Equals(y, Atan2_<T>._0),
                Atan2_<T>.NaN,
                signY * Atan2_<T>.PI_1p2
                ),
            OP.ConditionalSelect(
                OP.GreaterThan(x, Vector<T>.Zero),
                a,
                a + signY * Atan2_<T>.PI_2p2
                )
            );
    }

    private class Atan2_<T> : Const<T> where T : unmanaged { }

    #endregion
}
#pragma warning restore format