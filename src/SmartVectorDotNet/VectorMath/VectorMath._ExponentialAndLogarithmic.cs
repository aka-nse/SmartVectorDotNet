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
        public static readonly Vector<T> ExpMax
            = IsT<double>()
                ? As(new Vector<double>(ScalarMath.Log(double.MaxValue)))
            : IsT<float>()
                ? As(new Vector<float>(ScalarMath.Log(float.MaxValue)))
            : throw NotSupported();

        public static readonly Vector<T> ExpMin
            = IsT<double>()
                ? As(new Vector<double>(ScalarMath.Log(1 / double.MaxValue)))
            : IsT<float>()
                ? As(new Vector<float>(ScalarMath.Log(1 / float.MaxValue)))
            : throw NotSupported();

        public static readonly Vector<T> Log_2_E
            = IsT<double>()
                ? As(new Vector<double>(ScalarMath.Log(ScalarMath.Const<double>.E, 2)))
            : IsT<float>()
                ? As(new Vector<float>(ScalarMath.Log(ScalarMath.Const<float>.E, 2)))
            : throw NotSupported();


        public static readonly Vector<T> Log_E_2
            = IsT<double>()
                ? As(new Vector<double>(ScalarMath.Log(2, ScalarMath.Const<double>.E)))
            : IsT<float>()
                ? As(new Vector<float>(ScalarMath.Log(2, ScalarMath.Const<float>.E)))
            : throw NotSupported();

        public static ReadOnlySpan<Vector<T>> ExpCoeffs => _expCoeffs;
        private static readonly Vector<T>[] _expCoeffs = GetExpCoeffs();

        static Vector<T>[] GetExpCoeffs()
        {
            if (IsT<double>())
            {
                var expCoeffs = new Vector<double>[11];
                for (var i = 0; i <= 10; ++i)
                {
                    expCoeffs[i] = new Vector<double>(1.0 / (i + 1));
                }
                return Reinterpret<Vector<double>[], Vector<T>[]>(expCoeffs);
            }
            if (IsT<float>())
            {
                var expCoeffs = new Vector<float>[11];
                for (var i = 0; i <= 10; ++i)
                {
                    expCoeffs[i] = new Vector<float>(1.0f / (i + 1));
                }
                return Reinterpret<Vector<float>[], Vector<T>[]>(expCoeffs);
            }
            throw NotSupported();
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

    // NOTE:
    //  e^x = \sum_{n=0}^{\infty} \cfrac{x^n}{n!}
    //  8 members for double precision
    //  6 members for float precision

    private static Vector<double> ExpCore(in Vector<double> x)
    {
        var y = x * Const<double>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Const<double>.Log_E_2;
        var z = Vector<double>.Zero;
        z = (b * Const<double>.ExpCoeffs[9]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[8]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[7]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[6]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[5]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[4]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[3]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[2]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[1]) * (Vector<double>.One + z);
        z = (b * Const<double>.ExpCoeffs[0]) * (Vector<double>.One + z);
        z = z + Vector<double>.One;
        return Scale(n, z);
    }

    private static Vector<float> ExpCore(in Vector<float> x)
    {
        var y = x * Const<float>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Const<float>.Log_E_2;
        var z = Vector<float>.Zero;
        z = (b * Const<float>.ExpCoeffs[5]) * (Vector<float>.One + z);
        z = (b * Const<float>.ExpCoeffs[4]) * (Vector<float>.One + z);
        z = (b * Const<float>.ExpCoeffs[3]) * (Vector<float>.One + z);
        z = (b * Const<float>.ExpCoeffs[2]) * (Vector<float>.One + z);
        z = (b * Const<float>.ExpCoeffs[1]) * (Vector<float>.One + z);
        z = (b * Const<float>.ExpCoeffs[0]) * (Vector<float>.One + z);
        z = z + Vector<float>.One;
        return Scale(n, z);
    }

    #endregion
}