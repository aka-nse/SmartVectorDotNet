using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;


public static partial class VectorMath
{
    [AttributeUsage(AttributeTargets.Method)]
    private class VectorMathAttribute : Attribute { }

    #region utilities

    static partial class DoubleConst
    {
        public static readonly Vector<double> NaN = new Vector<double>(double.NaN);
        public static readonly Vector<double> PInf = new Vector<double>(double.PositiveInfinity);
        public static readonly Vector<double> NInf = new Vector<double>(double.NegativeInfinity);
        public static readonly Vector<double> ExpMax = new(ValueOperation.Log(double.MaxValue));
        public static readonly Vector<double> ExpMin = new(ValueOperation.Log(double.Epsilon));
        public static readonly Vector<double> Log_2_E = new(ValueOperation.Log(Math.E, 2));
        public static readonly Vector<double> Log_E_2 = new(ValueOperation.Log(2, Math.E));
        public static readonly Vector<long> _1023 = new(1023);
    }
    static partial class SingleConst
    {
        public static readonly Vector<float> NaN = new Vector<float>(float.NaN);
        public static readonly Vector<float> PInf = new Vector<float>(float.PositiveInfinity);
        public static readonly Vector<float> NInf = new Vector<float>(float.NegativeInfinity);
        public static readonly Vector<float> ExpMax = new(ValueOperation.Log(float.MaxValue));
        public static readonly Vector<float> ExpMin = new(ValueOperation.Log(float.Epsilon));
        public static readonly Vector<float> Log_2_E = new(ValueOperation.Log((float)Math.E, 2));
        public static readonly Vector<float> Log_E_2 = new(ValueOperation.Log(2, (float)Math.E));
        public static readonly Vector<int> _127 = new(127);
    }

    private static ref readonly Vector<TTo> Reinterpret<TFrom, TTo>(in Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(x));

    #endregion

    #region Sqrt

    [VectorMath]
    public static Vector<T> Sqrt<T>(in Vector<T> d)
        where T : unmanaged
        => VectorCompatible.SquareRoot(d);

    #endregion

    #region Round

    private static Vector<double> Round(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        if(Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector256<double>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector128<double>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Vector<double>.Count switch
        {
            2 => new Vector<double>(stackalloc[] {
                Math.Round(x[0]),
                Math.Round(x[1]),
            }),
            4 => new Vector<double>(stackalloc[] {
                Math.Round(x[0]),
                Math.Round(x[1]),
                Math.Round(x[2]),
                Math.Round(x[3]),
            }),
            8 => new Vector<double>(stackalloc[] {
                Math.Round(x[0]),
                Math.Round(x[1]),
                Math.Round(x[2]),
                Math.Round(x[3]),
                Math.Round(x[4]),
                Math.Round(x[5]),
                Math.Round(x[6]),
                Math.Round(x[7]),
            }),
            _ => throw new NotSupportedException(),
        };
    }


    private static Vector<float> Round(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector256<float>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector128<float>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Vector<float>.Count switch
        {
            4 => new Vector<float>(stackalloc[] {
                MathF.Round(x[0]),
                MathF.Round(x[1]),
                MathF.Round(x[2]),
                MathF.Round(x[3]),
            }),
            8 => new Vector<float>(stackalloc[] {
                MathF.Round(x[0]),
                MathF.Round(x[1]),
                MathF.Round(x[2]),
                MathF.Round(x[3]),
                MathF.Round(x[4]),
                MathF.Round(x[5]),
                MathF.Round(x[6]),
                MathF.Round(x[7]),
            }),
            16 => new Vector<float>(stackalloc[] {
                MathF.Round(x[0]),
                MathF.Round(x[1]),
                MathF.Round(x[2]),
                MathF.Round(x[3]),
                MathF.Round(x[4]),
                MathF.Round(x[5]),
                MathF.Round(x[6]),
                MathF.Round(x[7]),
                MathF.Round(x[8]),
                MathF.Round(x[9]),
                MathF.Round(x[10]),
                MathF.Round(x[11]),
                MathF.Round(x[12]),
                MathF.Round(x[13]),
                MathF.Round(x[14]),
                MathF.Round(x[15]),
            }),
            _ => throw new NotSupportedException(),
        };
    }


    #endregion

    #region Exp

    [VectorMath]
    public static partial Vector<T> Exp<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Exp(in Vector<double> x)
    {
        var isSaturatedMax = VectorCompatible.GreaterThan(x, DoubleConst.ExpMax);
        var isSaturatedMin = VectorCompatible.LessThan(x, DoubleConst.ExpMin);
        var isNormal = VectorCompatible.OnesComplement(VectorCompatible.BitwiseOr(isSaturatedMax, isSaturatedMin));

        var y = x * DoubleConst.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * DoubleConst.Log_E_2;
        var z = Vector<double>.Zero;
        z = (b / new Vector<double>(8)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(7)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(6)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(5)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(4)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(3)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(2)) * (Vector<double>.One + z);
        z = (b / new Vector<double>(1)) * (Vector<double>.One + z);
        z = z + Vector<double>.One;

        var p = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorDouble(isNormal), Scale(n, z));
        var q = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorDouble(isSaturatedMax), DoubleConst.PInf);
        var r = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorDouble(isSaturatedMin), DoubleConst.NInf);
        return VectorCompatible.BitwiseOr(
            p,
            VectorCompatible.BitwiseOr(q, r)
            );
    }

    private static Vector<float> Exp(in Vector<float> x)
    {
        var isSaturatedMax = VectorCompatible.GreaterThan(x, SingleConst.ExpMax);
        var isSaturatedMin = VectorCompatible.LessThan(x, SingleConst.ExpMin);
        var isNormal = VectorCompatible.OnesComplement(VectorCompatible.BitwiseOr(isSaturatedMax, isSaturatedMin));

        var y = x * SingleConst.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * SingleConst.Log_E_2;
        var z = Vector<float>.Zero;
        z = (b / new Vector<float>(8)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(7)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(6)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(5)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(4)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(3)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(2)) * (Vector<float>.One + z);
        z = (b / new Vector<float>(1)) * (Vector<float>.One + z);
        z = z + Vector<float>.One;

        var p = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorSingle(isNormal), Scale(n, z));
        var q = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorSingle(isSaturatedMax), SingleConst.PInf);
        var r = VectorCompatible.BitwiseAnd(VectorCompatible.AsVectorSingle(isSaturatedMin), SingleConst.NInf);
        return VectorCompatible.BitwiseOr(
            p,
            VectorCompatible.BitwiseOr(q, r)
            );
    }

    #endregion

    #region Floor

    [VectorMath]
    public static partial Vector<T> Floor<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Floor(in Vector<double> x)

    {
#if NET6_0_OR_GREATER
        return VectorCompatible.Floor(x);
#else
        throw new NotImplementedException();
#endif
    }

    private static Vector<float> Floor(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorCompatible.Floor(x);
#else
        throw new NotImplementedException();
#endif
    }

    #endregion

    #region Scale

    /// <summary>
    /// Calculates <c>pow(2, n) * x</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n">Must be integer.</param>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Scale<T>(in Vector<T> n, in Vector<T> x)
        where T : unmanaged;

    // pow(2, n) * x
    static Vector<double> Scale(in Vector<double> n, Vector<double> x)
    {
        var nn = VectorCompatible.ConvertToInt64(n);
        var pow2n = VectorCompatible.ShiftLeft(nn + DoubleConst._1023, 52);
        return x * VectorCompatible.AsVectorDouble(pow2n);
    }


    // pow(2, n) * x
    static Vector<float> Scale(in Vector<float> n, Vector<float> x)
    {
        var nn = VectorCompatible.ConvertToInt32(n);
        var pow2n = VectorCompatible.ShiftLeft(nn + SingleConst._127, 23);
        return x * VectorCompatible.AsVectorSingle(pow2n);
    }

    #endregion

    #region Truncate

    [VectorMath]
    public static partial Vector<T> Truncate<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate(in Vector<double> d)
        => VectorCompatible.ConvertToDouble(VectorCompatible.ConvertToInt64(d));

    private static Vector<float> Truncate(in Vector<float> d)
        => VectorCompatible.ConvertToSingle(VectorCompatible.ConvertToInt32(d));

    #endregion


}