using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorMath
{
    /*
    NOTE:
        Maclaurin expansion of expornential function:
        $$
        \begin{split}
        e^x &= \sum_{n=0}^{\infty} \cfrac{x^n}{n!}  \\
            &= a_0
        \end{split}\\
        \left(a_n := \frac{x}{n}(1 + a_{n + 1})\right)
        $$
     */

    partial class Const<T>
    {
        public static readonly Vector<T> ExpMax = AsVector(ScalarMath.Log(double.MaxValue));
        public static readonly Vector<T> ExpMin = AsVector(ScalarMath.Log(1 / double.MaxValue));
        public static readonly Vector<T> Log_2_E = AsVector(ScalarMath.Log(ScalarMath.Const<double>.E, 2));
        public static readonly Vector<T> Log_E_2 = AsVector(ScalarMath.Log(2, ScalarMath.Const<double>.E));

        public static readonly Vector<T> DivideBy_Log_E_2 = Vector<T>.One / Log_E_2;
        public static readonly Vector<T> DivideBy_Log_E_10 = AsVector(1.0 / ScalarMath.Log(10, ScalarMath.Const<double>.E));

        public static ReadOnlySpan<Vector<T>> ExpCoeffs => _expCoeffs;
        private static readonly Vector<T>[] _expCoeffs = GetExpCoeffs();
        static Vector<T>[] GetExpCoeffs()
        {
            if (IsT<double>())
            {
                var expCoeffs = new Vector<double>[11];
                for (var i = 1; i <= 11; ++i)
                {
                    expCoeffs[i - 1] = new Vector<double>(1 / (double)i);
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(expCoeffs);
            }
            if (IsT<float>())
            {
                var expCoeffs = new Vector<float>[6];
                for (var i = 1; i <= 6; ++i)
                {
                    expCoeffs[i - 1] = new Vector<float>(1 / (float)i);
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(expCoeffs);
            }
            return default!;
        }


        public static ReadOnlySpan<Vector<T>> LogCoeffs => _logCoeffs;
        private static readonly Vector<T>[] _logCoeffs = GetLogCoeffs();
        static Vector<T>[] GetLogCoeffs()
        {
            if (IsT<double>())
            {
                var logCoeffs = new Vector<double>[10];
                for (var i = 0; i < logCoeffs.Length; ++i)
                {
                    logCoeffs[i] = new(ScalarMath.Pow(-1.0, i) / (i + 1.0));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(logCoeffs);
            }
            if (IsT<float>())
            {
                var logCoeffs = new Vector<float>[10];
                for (var i = 0; i < logCoeffs.Length; ++i)
                {
                    logCoeffs[i] = new(ScalarMath.Pow(-1.0f, i) / (i + 1.0f));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(logCoeffs);
            }
            return default!;
        }

    }

    #region Exp

    /// <summary> Calculates exp. </summary>
    public static Vector<T> Exp<T>(in Vector<T> x)
        where T : unmanaged
    {
        var isSaturatedMax = VectorOp.GreaterThan(x, Const<T>.ExpMax);
        var isSaturatedMin = VectorOp.LessThan(x, Const<T>.ExpMin);
        var isNormal = VectorOp.OnesComplement(VectorOp.BitwiseOr(isSaturatedMax, isSaturatedMin));

        return VectorOp.ConditionalSelect(
            isNormal,
            ExpCore(x),
            VectorOp.ConditionalSelect(
                isSaturatedMin,
                Const<T>.NInf,
                Const<T>.PInf)
            );
    }

    [VectorMath]
    private static partial Vector<T> ExpCore<T>(in Vector<T> d)
        where T : unmanaged;

#pragma warning disable format
    private static Vector<double> ExpCore(in Vector<double> x)
    {
        var y = x * Const<double>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Const<double>.Log_E_2;
        var z = Vector<double>.Zero;                                           // a_11~
        z = (b * Const<double>.ExpCoeffs[10 - 1]) * (Vector<double>.One + z);  // a_10
        z = (b * Const<double>.ExpCoeffs[ 9 - 1]) * (Vector<double>.One + z);  // a_9
        z = (b * Const<double>.ExpCoeffs[ 8 - 1]) * (Vector<double>.One + z);  // a_8
        z = (b * Const<double>.ExpCoeffs[ 7 - 1]) * (Vector<double>.One + z);  // a_7
        z = (b * Const<double>.ExpCoeffs[ 6 - 1]) * (Vector<double>.One + z);  // a_6
        z = (b * Const<double>.ExpCoeffs[ 5 - 1]) * (Vector<double>.One + z);  // a_5
        z = (b * Const<double>.ExpCoeffs[ 4 - 1]) * (Vector<double>.One + z);  // a_4
        z = (b * Const<double>.ExpCoeffs[ 3 - 1]) * (Vector<double>.One + z);  // a_3
        z = (b * Const<double>.ExpCoeffs[ 2 - 1]) * (Vector<double>.One + z);  // a_2
        z = (b * Const<double>.ExpCoeffs[ 1 - 1]) * (Vector<double>.One + z);  // a_1
        z = z + Vector<double>.One;                                            // a_0
        return Scale(n, z);
    }

    private static Vector<float> ExpCore(in Vector<float> x)
    {
        var y = x * Const<float>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Const<float>.Log_E_2;
        var z = Vector<float>.Zero;                                         // a_7~
        z = (b * Const<float>.ExpCoeffs[6 - 1]) * (Vector<float>.One + z);  // a_6
        z = (b * Const<float>.ExpCoeffs[5 - 1]) * (Vector<float>.One + z);  // a_5
        z = (b * Const<float>.ExpCoeffs[4 - 1]) * (Vector<float>.One + z);  // a_4
        z = (b * Const<float>.ExpCoeffs[3 - 1]) * (Vector<float>.One + z);  // a_3
        z = (b * Const<float>.ExpCoeffs[2 - 1]) * (Vector<float>.One + z);  // a_2
        z = (b * Const<float>.ExpCoeffs[1 - 1]) * (Vector<float>.One + z);  // a_1
        z = z + Vector<float>.One;                                          // a_0
        return Scale(n, z);
    }
#pragma warning restore format

    #endregion

    #region Log

    /// <summary> Calculates log. </summary>
    [VectorMath]
    public static partial Vector<T> Log<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Log(in Vector<double> x)
    {
        var isPositive = VectorOp.GreaterThan(x, Vector<double>.Zero);
        Decompose<double>(x, out var n, out var a);
        var y = n * Const<double>.Log_E_2 + LogBounded(a);
        return VectorOp.ConditionalSelect(
            isPositive,
            y,
            Const<double>.NaN);
    }

    private static Vector<float> Log(in Vector<float> x)
    {
        var isPositive = VectorOp.GreaterThan(x, Vector<float>.Zero);
        Decompose<float>(x, out var n, out var a);
        var y = n * Const<float>.Log_E_2 + LogBounded(a);
        return VectorOp.ConditionalSelect(
            isPositive,
            y,
            Const<float>.NaN);
    }

    /// <summary> Calculates <c>log(x)</c>. </summary>
    /// <param name="x"> $1 \le x \lt 2$ </param>
    private static Vector<double> LogBounded(in Vector<double> x)
    {
        var preScale = Vector.ConditionalSelect(
            Vector.LessThan(x, Const<double>.Sqrt2),
            Vector<double>.One,
            Const<double>.Half);
        var postOffset = Vector.ConditionalSelect(
            Vector.LessThan(x, Const<double>.Sqrt2),
            Vector<double>.Zero,
            Const<double>.Log_E_2);
        var xx = x * preScale - Vector<double>.One;
        return postOffset
            + xx * (Const<double>.LogCoeffs[1 - 1]
            + xx * (Const<double>.LogCoeffs[2 - 1]
            + xx * (Const<double>.LogCoeffs[3 - 1]
            + xx * (Const<double>.LogCoeffs[4 - 1]
            + xx * (Const<double>.LogCoeffs[5 - 1]
            + xx * (Const<double>.LogCoeffs[6 - 1]
            + xx * (Const<double>.LogCoeffs[7 - 1]
            + xx * (Const<double>.LogCoeffs[8 - 1]
            + xx * (Const<double>.LogCoeffs[9 - 1]
            + xx * (Const<double>.LogCoeffs[10 - 1]
            ))))))))));
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
            Vector.LessThan(x, Const<float>.Sqrt2),
            Vector<float>.One,
            Const<float>.Half);
        var postOffset = Vector.ConditionalSelect(
            Vector.LessThan(x, Const<float>.Sqrt2),
            Vector<float>.Zero,
            Const<float>.Log_E_2);
        var xx = x * preScale - Vector<float>.One;
        return postOffset
            + xx * (Const<float>.LogCoeffs[1 - 1]
            + xx * (Const<float>.LogCoeffs[2 - 1]
            + xx * (Const<float>.LogCoeffs[3 - 1]
            + xx * (Const<float>.LogCoeffs[4 - 1]
            + xx * (Const<float>.LogCoeffs[5 - 1]
            + xx * (Const<float>.LogCoeffs[6 - 1]
            + xx * (Const<float>.LogCoeffs[7 - 1]
            + xx * (Const<float>.LogCoeffs[8 - 1]
            ))))))));
    }

    #endregion

    #region Pow

    /// <summary>
    /// Calculates pow(a, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Pow<T>(in Vector<T> a, in Vector<T> x)
        where T : unmanaged
        => Exp(x * Log(a));

    #endregion

    #region Log2

    /// <summary>
    /// Calculates log2(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Log2<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x) * Const<T>.DivideBy_Log_E_2;

    #endregion

    #region Log10

    /// <summary>
    /// Calculates log10(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Log10<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x) * Const<T>.DivideBy_Log_E_10;

    #endregion

    #region Log_anyBase

    /// <summary>
    /// Calculates log(x, newBase).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="newBase"></param>
    /// <returns></returns>
    public static Vector<T> Log<T>(in Vector<T> x, in Vector<T> newBase)
        where T : unmanaged
        => Log(x) / Log(newBase);

    #endregion
}