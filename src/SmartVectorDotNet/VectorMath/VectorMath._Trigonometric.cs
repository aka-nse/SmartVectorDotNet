using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

#pragma warning disable format
partial class VectorMath
{
    /*
    NOTE:
        Maclaurin expansion of trigonometric function:
        $$
        \begin{split}
        \cos x &= \sum_{n=0}^{\infty} (-1)^n\cfrac{x^{2n}}{(2n)!}  \\
               &= 1 - a_1
        \end{split}\\
        \left(a_n := \cfrac{x^2}{(2n - 1)\cdot 2n}(1 - a_{n+1}) \right)
        $$

        Remap `x` into $-\frac{\pi}{2} \le x' \lt +\frac{\pi}{2}$:
        $$
        \begin{split}
        \sin x &= \begin{cases}
             \cos(x - \frac{1}{2}\pi) & (0   \le x \lt  \pi) \\
            -\cos(\frac{3}{2}\pi - x) & (\pi \le x \lt 2\pi)
        \end{cases}\\
        \cos x &= \begin{cases}
             \cos x          & (0              \le x \lt \frac{1}{2}\pi)  \\
            -\cos (\pi - x)  & (\frac{1}{2}\pi \le x \lt \frac{3}{2}\pi)  \\
             \cos (x - 2\pi) & (\frac{3}{2}\pi \le x \lt 2\pi)
        \end{cases}
        \end{split}
        $$
     */

    partial class Const<T>
    {
        public static ReadOnlySpan<Vector<T>> CosCoeffs => _cosCoeffs;
        private static readonly Vector<T>[] _cosCoeffs = GetCosCoeffs();
        static Vector<T>[] GetCosCoeffs()
        {
            if (IsT<double>())
            {
                var cosCoeffs = new Vector<double>[12];
                for (var n = 1; n <= 12; ++n)
                {
                    cosCoeffs[n  - 1] = new Vector<double>(1.0 / ((2 * n - 1) * (2 * n)));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(cosCoeffs);
            }
            if(IsT<float>())
            {
                var cosCoeffs = new Vector<float>[6];
                for (var n = 1; n <= 6; ++n)
                {
                    cosCoeffs[n - 1] = new Vector<float>(1.0f / ((2 * n - 1) * (2 * n)));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(cosCoeffs);
            }
            return default!;
        }

        public static ReadOnlySpan<Vector<T>> SinCoeffs => _sinCoeffs;
        private static readonly Vector<T>[] _sinCoeffs = GetSinCoeffs();
        static Vector<T>[] GetSinCoeffs()
        {
            if (IsT<double>())
            {
                var cosCoeffs = new Vector<double>[11];
                for (var n = 1; n <= 11; ++n)
                {
                    cosCoeffs[n - 1] = new Vector<double>(1.0 / ((2 * n + 1) * (2 * n)));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(cosCoeffs);
            }
            if (IsT<float>())
            {
                var cosCoeffs = new Vector<float>[6];
                for (var n = 1; n <= 6; ++n)
                {
                    cosCoeffs[n - 1] = new Vector<float>(1.0f / ((2 * n + 1) * (2 * n)));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(cosCoeffs);
            }
            return default!;
        }

        public static ReadOnlySpan<Vector<T>> AtanCoeffs => _atanCoeffs;
        private static readonly Vector<T>[] _atanCoeffs = GetAtanCoeffs();
        static Vector<T>[] GetAtanCoeffs()
        {
            if (IsT<double>())
            {
                var atanCoeffs = new Vector<double>[11];
                for (var n = 1; n <= 11; ++n)
                {
                    atanCoeffs[n - 1] = new(ScalarMath.Pow(-1.0, n) / (2 * n + 1));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(atanCoeffs);
            }
            if (IsT<float>())
            {
                var atanCoeffs = new Vector<float>[6];
                for (var n = 1; n <= 6; ++n)
                {
                    atanCoeffs[n - 1] = new(ScalarMath.Pow(-1f, n) / (2 * n + 1));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(atanCoeffs);
            }
            return default!;
        }

        public static readonly Vector<T> AtanCoreReflectThreshold
            = AsVector(ScalarMath.Sqrt(2.0) - 1);
        public static readonly Vector<T> AtanCoreReflectedOffset
            = AsVector(ScalarMath.Const<double>.PI / 4);
        public static readonly Vector<T> AtanReflectOffset
            = AsVector(ScalarMath.Const<double>.PI / 2);

        public static readonly Vector<T> OnePerTau_Modulo
            = AsVector(0.159154943091895);
        public static readonly Vector<T> Tau_ModuloUpper
            = IsT<double>()
                ? As<double>(new(6.28318530717958))
            : IsT<float>()
                ? As<float>(new(6.283185f))
            : default;
        public static readonly Vector<T> Tau_ModuloLower
            = IsT<double>()
                ? As<double>(new(0.00000000000000647692528676655901))
            : IsT<float>()
                ? As<float>(new(0.0000003071796f))
            : default;


        public static readonly Vector<T> OnePerPi_Modulo
            = AsVector(0.3183098861837907);
        public static readonly Vector<T> Pi_ModuloUpper
            = IsT<double>()
                ? As<double>(new(3.141592653589793))
            : IsT<float>()
                ? As<float>(new(3.1415925f))
            : default;
        public static readonly Vector<T> Pi_ModuloLower
            = IsT<double>()
                ? As<double>(new(0.00000000000000023846264338327950))
            : IsT<float>()
                ? As<float>(new(0.0000001509958f))
            : default;
    }


    private static Vector<T> ModuloByTau<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * Const<T>.OnePerTau_Modulo);
        var reminderHigh = FusedMultiplyAdd(quotient, -Const<T>.Tau_ModuloUpper, x);
        return FusedMultiplyAdd(quotient, -Const<T>.Tau_ModuloLower, reminderHigh);
    }
    

    private static Vector<T> ModuloByPi<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * Const<T>.OnePerPi_Modulo);
        var reminderHigh = FusedMultiplyAdd(quotient, -Const<T>.Pi_ModuloUpper, x);
        return FusedMultiplyAdd(quotient, -Const<T>.Pi_ModuloLower, reminderHigh);
    }


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
        var lessThan_1_2 = VectorOp.LessThan(xx, Const<T>.PI_1p2);
        var lessThan_3_2 = VectorOp.LessThan(xx, Const<T>.PI_3p2);
        xx =
            VectorOp.ConditionalSelect(lessThan_1_2,
                xx,
            VectorOp.ConditionalSelect(lessThan_3_2,
                Const<T>.PI_2p2 - xx,
                xx - Const<T>.PI_4p2
                ));
        var sign =
            VectorOp.ConditionalSelect(lessThan_1_2,
                Vector<T>.One,
            VectorOp.ConditionalSelect(lessThan_3_2,
                Const<T>.MinusOne,
                Vector<T>.One
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
        y = x2 * Const<double>.CosCoeffs[10];                             // a_11~
        y = x2 * Const<double>.CosCoeffs[ 9] * (Vector<double>.One - y);  // a_10
        y = x2 * Const<double>.CosCoeffs[ 8] * (Vector<double>.One - y);  // a_9
        y = x2 * Const<double>.CosCoeffs[ 7] * (Vector<double>.One - y);  // a_8
        y = x2 * Const<double>.CosCoeffs[ 6] * (Vector<double>.One - y);  // a_7
        y = x2 * Const<double>.CosCoeffs[ 5] * (Vector<double>.One - y);  // a_6
        y = x2 * Const<double>.CosCoeffs[ 4] * (Vector<double>.One - y);  // a_5
        y = x2 * Const<double>.CosCoeffs[ 3] * (Vector<double>.One - y);  // a_4
        y = x2 * Const<double>.CosCoeffs[ 2] * (Vector<double>.One - y);  // a_3
        y = x2 * Const<double>.CosCoeffs[ 1] * (Vector<double>.One - y);  // a_2
        y = x2 * Const<double>.CosCoeffs[ 0] * (Vector<double>.One - y);  // a_1
        return Vector<double>.One - y;                                    // 1 - a_1
    }

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<float> CosBounded(in Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * Const<float>.CosCoeffs[5];                            // a_6~
        y = x2 * Const<float>.CosCoeffs[4] * (Vector<float>.One - y);  // a_5
        y = x2 * Const<float>.CosCoeffs[3] * (Vector<float>.One - y);  // a_4
        y = x2 * Const<float>.CosCoeffs[2] * (Vector<float>.One - y);  // a_3
        y = x2 * Const<float>.CosCoeffs[1] * (Vector<float>.One - y);  // a_2
        y = x2 * Const<float>.CosCoeffs[0] * (Vector<float>.One - y);  // a_1
        return Vector<float>.One - y;                                  // 1 - a_1
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
        var lessThan_1_2 = VectorOp.LessThan(xx, Const<T>.PI_1p2);
        var lessThan_3_2 = VectorOp.LessThan(xx, Const<T>.PI_3p2);
        xx =
            VectorOp.ConditionalSelect(lessThan_1_2,
                xx,
            VectorOp.ConditionalSelect(lessThan_3_2,
                Const<T>.PI_2p2 - xx,
                xx - Const<T>.PI_4p2
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
        y = x2 * Const<double>.SinCoeffs[10];                             // a_11~
        y = x2 * Const<double>.SinCoeffs[ 9] * (Vector<double>.One - y);  // a_10
        y = x2 * Const<double>.SinCoeffs[ 8] * (Vector<double>.One - y);  // a_9
        y = x2 * Const<double>.SinCoeffs[ 7] * (Vector<double>.One - y);  // a_8
        y = x2 * Const<double>.SinCoeffs[ 6] * (Vector<double>.One - y);  // a_7
        y = x2 * Const<double>.SinCoeffs[ 5] * (Vector<double>.One - y);  // a_6
        y = x2 * Const<double>.SinCoeffs[ 4] * (Vector<double>.One - y);  // a_5
        y = x2 * Const<double>.SinCoeffs[ 3] * (Vector<double>.One - y);  // a_4
        y = x2 * Const<double>.SinCoeffs[ 2] * (Vector<double>.One - y);  // a_3
        y = x2 * Const<double>.SinCoeffs[ 1] * (Vector<double>.One - y);  // a_2
        y = x2 * Const<double>.SinCoeffs[ 0] * (Vector<double>.One - y);  // a_1
        return x * (Vector<double>.One - y);                              // 1 - a_1
    }

    /// <param name="x"> $-\frac{\pi}{2} \le x \lt \frac{\pi}{2}$ </param>
    private static Vector<float> SinBounded(in Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * Const<float>.SinCoeffs[5];                            // a_6~
        y = x2 * Const<float>.SinCoeffs[4] * (Vector<float>.One - y);  // a_5
        y = x2 * Const<float>.SinCoeffs[3] * (Vector<float>.One - y);  // a_4
        y = x2 * Const<float>.SinCoeffs[2] * (Vector<float>.One - y);  // a_3
        y = x2 * Const<float>.SinCoeffs[1] * (Vector<float>.One - y);  // a_2
        y = x2 * Const<float>.SinCoeffs[0] * (Vector<float>.One - y);  // a_1
        return x * (Vector<float>.One - y);                            // 1 - a_1
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
        var xx = ModuloByPi(x);
        var shouldReverse = Vector.GreaterThan(xx, Const<T>.PI_1p2);
        var sign = Vector.ConditionalSelect(
            shouldReverse,
            -Vector<T>.One,
            Vector<T>.One);
        xx = Vector.ConditionalSelect(
            shouldReverse,
            Const<T>.PI - xx,
            xx);
        var modifiesByAdditionTheorem = Vector.GreaterThan(xx, Const<T>.PI_1p4);
        xx = Vector.ConditionalSelect(
            modifiesByAdditionTheorem,
            xx - Const<T>.PI_1p4,
            xx);
        var tanxx = TanBounded(xx);
        return sign * Vector.ConditionalSelect(
            modifiesByAdditionTheorem,
            (Vector<T>.One + tanxx) / (Vector<T>.One - tanxx),
            tanxx);
    }

    private static Vector<T> TanBounded<T>(in Vector<T> x)
        where T : unmanaged
        => SinBounded(x) / CosBounded(x);

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


    private static Vector<double> Atan(Vector<double> x)
    {
        static Vector<double> core(Vector<double> x)
        {
            var condition = Vector.LessThan(x, Const<double>.AtanCoreReflectThreshold);
            x = Vector.ConditionalSelect(
                condition,
                x,
                (x - Vector<double>.One) / (x + Vector<double>.One)
            );
            var offset = Vector.ConditionalSelect(
                condition,
                Vector<double>.Zero,
                Const<double>.AtanCoreReflectedOffset
            );
            var x2 = x * x;
            return x * (Vector<double>.One
                + x2 * (Const<double>.AtanCoeffs[0]
                + x2 * (Const<double>.AtanCoeffs[1]
                + x2 * (Const<double>.AtanCoeffs[2]
                + x2 * (Const<double>.AtanCoeffs[3]
                + x2 * (Const<double>.AtanCoeffs[4]
                + x2 * (Const<double>.AtanCoeffs[5]
                + x2 * (Const<double>.AtanCoeffs[6]
                + x2 * (Const<double>.AtanCoeffs[7]
                + x2 * (Const<double>.AtanCoeffs[8]
                + x2 * (Const<double>.AtanCoeffs[9]
                + x2 * (Const<double>.AtanCoeffs[10]
                )))))))))))) + offset;
        }

        var sign = SignFast(x);
        x = sign * x;
        var xx = Vector.ConditionalSelect(
            Vector.LessThan(x, Vector<double>.One),
            x,
            Vector<double>.One / x
        );
        var y = core(xx);
        return sign * Vector.ConditionalSelect(
            Vector.LessThan(x, Vector<double>.One),
            y,
            Const<double>.AtanReflectOffset - y
        );
    }

    private static Vector<float> Atan(Vector<float> x)
    {
        static Vector<float> core(Vector<float> x)
        {
            var condition = Vector.LessThan(x, Const<float>.AtanCoreReflectThreshold);
            x = Vector.ConditionalSelect(
                condition,
                x,
                (x - Vector<float>.One) / (x + Vector<float>.One)
            );
            var offset = Vector.ConditionalSelect(
                condition,
                Vector<float>.Zero,
                Const<float>.AtanCoreReflectedOffset
            );
            var x2 = x * x;
            return x * (Vector<float>.One
                + x2 * (Const<float>.AtanCoeffs[0]
                + x2 * (Const<float>.AtanCoeffs[1]
                + x2 * (Const<float>.AtanCoeffs[2]
                + x2 * (Const<float>.AtanCoeffs[3]
                + x2 * (Const<float>.AtanCoeffs[4]
                + x2 * (Const<float>.AtanCoeffs[5]
                ))))))) + offset;
        }

        var sign = SignFast(x);
        x = sign * x;
        var xx = Vector.ConditionalSelect(
            Vector.LessThan(x, Vector<float>.One),
            x,
            Vector<float>.One / x
        );
        var y = core(xx);
        return sign * Vector.ConditionalSelect(
            Vector.LessThan(x, Vector<float>.One),
            y,
            Const<float>.AtanReflectOffset - y
        );
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
        => Const<T>.Two * Atan(x / (x + Sqrt(Vector<T>.One - x * x)));

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
        => Const<T>.Two * Atan(Sqrt(Vector<T>.One - x * x) / (Vector<T>.One + x));

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
        return Vector.ConditionalSelect(
            Vector.Equals(x, Vector<T>.Zero),
            Vector.ConditionalSelect(
                Vector.Equals(y, Vector<T>.Zero),
                Const<T>.NaN,
                signY * Const<T>.PI_1p2
                ),
            Vector.ConditionalSelect(
                Vector.GreaterThan(x, Vector<T>.Zero),
                a,
                a + signY * Const<T>.PI_2p2
                )
            );
    }

    #endregion
}
#pragma warning restore format