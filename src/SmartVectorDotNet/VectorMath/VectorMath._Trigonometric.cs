using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorMath
{
    partial class Const<T>
    {
        public static ReadOnlySpan<Vector<T>> CosCoeffs => _cosCoeffs;
        private static readonly Vector<T>[] _cosCoeffs = GetCosCoeffs();
        static Vector<T>[] GetCosCoeffs()
        {
            if (IsT<double>())
            {
                var cosCoeffs = new Vector<double>[11];
                for (var i = 0; i < 11; ++i)
                {
                    cosCoeffs[i] = new Vector<double>(1.0 / ((2 * i + 1) * (2 * i + 2)));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(cosCoeffs);
            }
            if(IsT<float>())
            {
                var cosCoeffs = new Vector<float>[6];
                for (var i = 0; i < 6; ++i)
                {
                    cosCoeffs[i] = new Vector<float>(1.0f / ((2 * i + 1) * (2 * i + 2)));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(cosCoeffs);
            }
            throw NotSupported();
        }
    }

    #region Cos

    /// <summary>
    /// Calculates sin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Cos<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Cos(Vector<double> x)
    {
        x = ModuloByFloor(x, Const<double>.PI_4p2);
        var lessThan_1_2 = VectorOp.LessThan(x, Const<double>.PI_1p2);
        var lessThan_3_2 = VectorOp.LessThan(x, Const<double>.PI_3p2);
        x = VectorOp.ConditionalSelect(
            lessThan_1_2,
            x,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                Const<double>.PI_2p2 - x,
                x - Const<double>.PI_4p2
                )
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_1_2,
            Vector<double>.One,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                Const<double>.MinusOne,
                Vector<double>.One
                )
            );
        return sign * CosBounded(x);
    }

    private static Vector<float> Cos(Vector<float> x)
    {
        x = ModuloByFloor(x, Const<float>.PI_4p2);
        var lessThan_1_2 = VectorOp.LessThan(x, Const<float>.PI_1p2);
        var lessThan_3_2 = VectorOp.LessThan(x, Const<float>.PI_3p2);
        x = VectorOp.ConditionalSelect(
            lessThan_1_2,
            x,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                Const<float>.PI_2p2 - x,
                x - Const<float>.PI_4p2
                )
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_1_2,
            Vector<float>.One,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                Const<float>.MinusOne,
                Vector<float>.One
                )
            );

        return sign * CosBounded(x);
    }

    private static Vector<double> CosBounded(in Vector<double> x)
    {
        Vector<double> y;
        var x2 = x * x;
        y = x2 * Const<double>.CosCoeffs[10];
        y = x2 * Const<double>.CosCoeffs[9] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[8] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[7] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[6] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[5] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[4] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[3] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[2] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[1] * (Vector<double>.One - y);
        y = x2 * Const<double>.CosCoeffs[0] * (Vector<double>.One - y);
        return Vector<double>.One - y;
    }

    private static Vector<float> CosBounded(in Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * Const<float>.CosCoeffs[5];
        y = x2 * Const<float>.CosCoeffs[4] * (Vector<float>.One - y);
        y = x2 * Const<float>.CosCoeffs[3] * (Vector<float>.One - y);
        y = x2 * Const<float>.CosCoeffs[2] * (Vector<float>.One - y);
        y = x2 * Const<float>.CosCoeffs[1] * (Vector<float>.One - y);
        y = x2 * Const<float>.CosCoeffs[0] * (Vector<float>.One - y);
        return Vector<float>.One - y;
    }

    #endregion

    #region Sin

    /// <summary>
    /// Calculates sin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Sin<T>(in Vector<T> x)
        where T : unmanaged;

    private static Vector<double> Sin(Vector<double> x)
    {
        x = ModuloByFloor(x, Const<double>.PI_4p2);
        var lessThan_2_2 = VectorOp.LessThan(x, Const<double>.PI_2p2);
        x = VectorOp.ConditionalSelect(
            lessThan_2_2,
            x - Const<double>.PI_1p2,
            Const<double>.PI_3p2 - x
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_2_2,
            Vector<double>.One,
            Const<double>.MinusOne
            );
        return sign * CosBounded(x);
    }

    private static Vector<float> Sin(Vector<float> x)
    {
        x = ModuloByFloor(x, Const<float>.PI_4p2);
        var lessThan_2_2 = VectorOp.LessThan(x, Const<float>.PI_2p2);
        x = VectorOp.ConditionalSelect(
            lessThan_2_2,
            x - Const<float>.PI_1p2,
            Const<float>.PI_3p2 - x
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_2_2,
            Vector<float>.One,
            Const<float>.MinusOne
            );
        return sign * CosBounded(x);
    }

    #endregion
}