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
        internal static readonly Vector<T> Log_2_E = AsVector(ScalarMath.Log(ScalarMath.Const<double>.E, 2));
        internal static readonly Vector<T> Log_E_2 = AsVector(ScalarMath.Log(2, ScalarMath.Const<double>.E));

        internal static readonly Vector<T> OnePerLog_E_2 = AsVector(1.0 / ScalarMath.Log(2, ScalarMath.Const<double>.E));
        internal static readonly Vector<T> OnePerLog_E_10 = AsVector(1.0 / ScalarMath.Log(10, ScalarMath.Const<double>.E));

    }

    #region Exp

    /// <summary> Calculates exp. </summary>
    public static Vector<T> Exp<T>(in Vector<T> x)
        where T : unmanaged
    {
        var isSaturatedMax = VectorOp.GreaterThan(x, Exp_<T>.Max);
        var isSaturatedMin = VectorOp.LessThan(x, Exp_<T>.Min);
        var isNormal = VectorOp.OnesComplement(VectorOp.BitwiseOr(isSaturatedMax, isSaturatedMin));

        return VectorOp.ConditionalSelect(
            isNormal,
            ExpCore(x),
            VectorOp.ConditionalSelect(
                isSaturatedMin,
                Exp_<T>.NInf,
                Exp_<T>.PInf)
            );
    }

    [VectorMath]
    private static partial Vector<T> ExpCore<T>(in Vector<T> d)
        where T : unmanaged;

#pragma warning disable format
    private static Vector<double> ExpCore(in Vector<double> x)
    {
        var y = x * Exp_<double>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Exp_<double>.Log_E_2;
        var z = Exp_<double>._0;                                        // a_11~
        z = (b * Exp_<double>.Coeffs[10 - 1]) * (Exp_<double>._1 + z);  // a_10
        z = (b * Exp_<double>.Coeffs[ 9 - 1]) * (Exp_<double>._1 + z);  // a_9
        z = (b * Exp_<double>.Coeffs[ 8 - 1]) * (Exp_<double>._1 + z);  // a_8
        z = (b * Exp_<double>.Coeffs[ 7 - 1]) * (Exp_<double>._1 + z);  // a_7
        z = (b * Exp_<double>.Coeffs[ 6 - 1]) * (Exp_<double>._1 + z);  // a_6
        z = (b * Exp_<double>.Coeffs[ 5 - 1]) * (Exp_<double>._1 + z);  // a_5
        z = (b * Exp_<double>.Coeffs[ 4 - 1]) * (Exp_<double>._1 + z);  // a_4
        z = (b * Exp_<double>.Coeffs[ 3 - 1]) * (Exp_<double>._1 + z);  // a_3
        z = (b * Exp_<double>.Coeffs[ 2 - 1]) * (Exp_<double>._1 + z);  // a_2
        z = (b * Exp_<double>.Coeffs[ 1 - 1]) * (Exp_<double>._1 + z);  // a_1
        z = z + Exp_<double>._1;                                        // a_0
        return Scale(n, z);
    }

    private static Vector<float> ExpCore(in Vector<float> x)
    {
        var y = x * Exp_<float>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Exp_<float>.Log_E_2;
        var z = Exp_<float>._0;                                         // a_7~
        z = (b * Exp_<float>.Coeffs[6 - 1]) * (Exp_<float>._1 + z);  // a_6
        z = (b * Exp_<float>.Coeffs[5 - 1]) * (Exp_<float>._1 + z);  // a_5
        z = (b * Exp_<float>.Coeffs[4 - 1]) * (Exp_<float>._1 + z);  // a_4
        z = (b * Exp_<float>.Coeffs[3 - 1]) * (Exp_<float>._1 + z);  // a_3
        z = (b * Exp_<float>.Coeffs[2 - 1]) * (Exp_<float>._1 + z);  // a_2
        z = (b * Exp_<float>.Coeffs[1 - 1]) * (Exp_<float>._1 + z);  // a_1
        z = z + Exp_<float>._1;                                         // a_0
        return Scale(n, z);
    }
#pragma warning restore format

    private class Exp_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> Max = AsVector(ScalarMath.Log(double.MaxValue));
        internal static readonly Vector<T> Min = AsVector(ScalarMath.Log(1 / double.MaxValue));

        internal static ReadOnlySpan<Vector<T>> Coeffs => _expCoeffs;
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
    }

    #endregion

    #region Log

    /// <summary> Calculates log. </summary>
    [VectorMath]
    public static partial Vector<T> Log<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Log(in Vector<double> x)
    {
        var isPositive = VectorOp.GreaterThan(x, Log_<double>._0);
        Decompose<double>(x, out var n, out var a);
        var y = n * Log_<double>.Log_E_2 + LogBounded(a);
        return VectorOp.ConditionalSelect(
            isPositive,
            y,
            Log_<double>.NaN);
    }

    private static Vector<float> Log(in Vector<float> x)
    {
        var isPositive = VectorOp.GreaterThan(x, Log_<float>._0);
        Decompose<float>(x, out var n, out var a);
        var y = n * Log_<float>.Log_E_2 + LogBounded(a);
        return VectorOp.ConditionalSelect(
            isPositive,
            y,
            Log_<float>.NaN);
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

    private class Log_<T> : Const<T> where T : unmanaged
    {
        public static ReadOnlySpan<Vector<T>> Coeffs => _coeffs;
        private static readonly Vector<T>[] _coeffs = GetCoeffs();
        static Vector<T>[] GetCoeffs()
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
        => Log(x) * Const<T>.OnePerLog_E_2;

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
        => Log(x) * Const<T>.OnePerLog_E_10;

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