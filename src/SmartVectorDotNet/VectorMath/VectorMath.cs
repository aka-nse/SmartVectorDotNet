using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Immutable;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;


/// <summary>
/// Provides vectorized operators of <see cref="System.Math"/>.
/// </summary>
public static partial class VectorMath
{
    [AttributeUsage(AttributeTargets.Method)]
    private class VectorMathAttribute : Attribute { }

    #region utilities

    static partial class DoubleConst
    {
        public static readonly Vector<double> MinusOne = -Vector<double>.One;
        public static readonly Vector<double> E = new(Math.E);
        public static readonly Vector<double> Pi_1p2 = new(0.5 * Math.PI);
        public static readonly Vector<double> Pi_2p2 = new(Math.PI);
        public static readonly Vector<double> Pi_3p2 = new(1.5 * Math.PI);
        public static readonly Vector<double> Pi_4p2 = new(2 * Math.PI);
        public static readonly Vector<double> NaN = new(double.NaN);
        public static readonly Vector<double> PInf = new(double.PositiveInfinity);
        public static readonly Vector<double> NInf = new(double.NegativeInfinity);
        public static readonly Vector<double> ExpMax = new(ScalarMath.Log(double.MaxValue));
        public static readonly Vector<double> ExpMin = new(ScalarMath.Log(1 / double.MaxValue));
        public static readonly Vector<double> Log_2_E = new(ScalarMath.Log(Math.E, 2));
        public static readonly Vector<double> Log_E_2 = new(ScalarMath.Log(2, Math.E));
        public static readonly Vector<long> _1023 = new(1023);

        public static ReadOnlySpan<Vector<double>> ExpCoeffs => _expCoeffs;
        public static ReadOnlySpan<Vector<double>> CosCoeffs => _cosCoeffs;
        private static readonly Vector<double>[] _expCoeffs;
        private static readonly Vector<double>[] _cosCoeffs;

        static DoubleConst()
        {
            _expCoeffs = new Vector<double>[11];
            _cosCoeffs = new Vector<double>[11];
            for(var i = 0; i <= 10; ++i)
            {
                _expCoeffs[i] = new Vector<double>(1.0 / (i + 1));
                _cosCoeffs[i] = new Vector<double>(1.0 / ((2 * i + 1) * (2 * i + 2)));
            }
        }
    }

    static partial class SingleConst
    {
        public static readonly Vector<float> MinusOne = -Vector<float>.One;
        public static readonly Vector<float> E = new((float)Math.E);
        public static readonly Vector<float> Pi_1p2 = new((float)(0.5 * Math.PI));
        public static readonly Vector<float> Pi_2p2 = new((float)Math.PI);
        public static readonly Vector<float> Pi_3p2 = new((float)(1.5 * Math.PI));
        public static readonly Vector<float> Pi_4p2 = new((float)(2 * Math.PI));
        public static readonly Vector<float> NaN = new(float.NaN);
        public static readonly Vector<float> PInf = new(float.PositiveInfinity);
        public static readonly Vector<float> NInf = new(float.NegativeInfinity);
        public static readonly Vector<float> ExpMax = new(ScalarMath.Log(float.MaxValue));
        public static readonly Vector<float> ExpMin = new(ScalarMath.Log(float.Epsilon));
        public static readonly Vector<float> Log_2_E = new(ScalarMath.Log((float)Math.E, 2));
        public static readonly Vector<float> Log_E_2 = new(ScalarMath.Log(2, (float)Math.E));
        public static readonly Vector<int> _127 = new(127);


        public static ReadOnlySpan<Vector<float>> ExpCoeffs => _expCoeffs;
        public static ReadOnlySpan<Vector<float>> CosCoeffs => _cosCoeffs;
        private static readonly Vector<float>[] _expCoeffs;
        private static readonly Vector<float>[] _cosCoeffs;

        static SingleConst()
        {
            _expCoeffs = new Vector<float>[6];
            _cosCoeffs = new Vector<float>[6];
            for (var i = 0; i < 6; ++i)
            {
                _expCoeffs[i] = new Vector<float>(1.0f / (i + 1));
                _cosCoeffs[i] = new Vector<float>(1.0f / ((2 * i + 1) * (2 * i + 2)));
            }
        }
    }

    private static ref readonly Vector<TTo> Reinterpret<TFrom, TTo>(in Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(x));

    #endregion

    #region Abs

    /// <summary> Calculates abs. </summary>
    public static Vector<T> Abs<T>(in Vector<T> d)
        where T : unmanaged
        => VectorOp.Abs(d);

    #endregion

    #region trigonometric functions

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
        x = Modulo(x, DoubleConst.Pi_4p2);
        var lessThan_1_2 = VectorOp.LessThan(x, DoubleConst.Pi_1p2);
        var lessThan_3_2 = VectorOp.LessThan(x, DoubleConst.Pi_3p2);
        x = VectorOp.ConditionalSelect(
            lessThan_1_2,
            x,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                DoubleConst.Pi_2p2 - x,
                x - DoubleConst.Pi_4p2
                )
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_1_2,
            Vector<double>.One,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                DoubleConst.MinusOne,
                Vector<double>.One
                )
            );

        return sign * CosBounded(x);
    }

    private static Vector<float> Cos(Vector<float> x)
    {
        x = Modulo(x, SingleConst.Pi_4p2);
        var lessThan_1_2 = VectorOp.LessThan(x, SingleConst.Pi_1p2);
        var lessThan_3_2 = VectorOp.LessThan(x, SingleConst.Pi_3p2);
        x = VectorOp.ConditionalSelect(
            lessThan_1_2,
            x,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                SingleConst.Pi_2p2 - x,
                x - SingleConst.Pi_4p2
                )
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_1_2,
            Vector<float>.One,
            VectorOp.ConditionalSelect(
                lessThan_3_2,
                SingleConst.MinusOne,
                Vector<float>.One
                )
            );

        return sign * CosBounded(x);
    }

    private static Vector<double> CosBounded(in Vector<double> x)
    {
        Vector<double> y;
        var x2 = x * x;
        y = x2 * DoubleConst.CosCoeffs[10];
        y = x2 * DoubleConst.CosCoeffs[9] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[8] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[7] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[6] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[5] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[4] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[3] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[2] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[1] * (Vector<double>.One - y);
        y = x2 * DoubleConst.CosCoeffs[0] * (Vector<double>.One - y);
        return Vector<double>.One - y;
    }

    private static Vector<float> CosBounded(in Vector<float> x)
    {
        Vector<float> y;
        var x2 = x * x;
        y = x2 * SingleConst.CosCoeffs[5];
        y = x2 * SingleConst.CosCoeffs[4] * (Vector<float>.One - y);
        y = x2 * SingleConst.CosCoeffs[3] * (Vector<float>.One - y);
        y = x2 * SingleConst.CosCoeffs[2] * (Vector<float>.One - y);
        y = x2 * SingleConst.CosCoeffs[1] * (Vector<float>.One - y);
        y = x2 * SingleConst.CosCoeffs[0] * (Vector<float>.One - y);
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
        x = Modulo(x, DoubleConst.Pi_4p2);
        var lessThan_2_2 = VectorOp.LessThan(x, DoubleConst.Pi_2p2);
        x = VectorOp.ConditionalSelect(
            lessThan_2_2,
            x - DoubleConst.Pi_1p2,
            DoubleConst.Pi_3p2 - x
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_2_2,
            Vector<double>.One,
            DoubleConst.MinusOne
            );
        return sign * CosBounded(x);
    }

    private static Vector<float> Sin(Vector<float> x)
    {
        x = Modulo(x, SingleConst.Pi_4p2);
        var lessThan_2_2 = VectorOp.LessThan(x, SingleConst.Pi_2p2);
        x = VectorOp.ConditionalSelect(
            lessThan_2_2,
            x - SingleConst.Pi_1p2,
            SingleConst.Pi_3p2 - x
            );
        var sign = VectorOp.ConditionalSelect(
            lessThan_2_2,
            Vector<float>.One,
            SingleConst.MinusOne
            );
        return sign * CosBounded(x);
    }

    #endregion

#endregion

    #region DivRem

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    public static Vector<T> DivRem<T>(in Vector<T> a, in Vector<T> b, out Vector<T> reminder)
        where T : unmanaged
    {
        if (typeof(T) == typeof(byte)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(nuint)
            || typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            var quotient = a / b;
            reminder = a - quotient * b;
            return quotient;
        }
        if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
        {
            var quotient = Truncate(a / b);
            reminder = FusedMultiplyAdd(quotient, -b, a);
            return quotient;
        }
        throw new NotSupportedException();
    }

    #endregion

    #region Exp

    /// <summary> Calculates exp. </summary>
    [VectorMath]
    public static partial Vector<T> Exp<T>(in Vector<T> d)
        where T : unmanaged;

    // NOTE:
    //  e^x = \sum_{n=0}^{\infty} \cfrac{x^n}{n!}
    //  8 members for double precision
    //  6 members for float precision

    private static Vector<double> Exp(in Vector<double> x)
    {
        var isSaturatedMax = VectorOp.GreaterThan(x, DoubleConst.ExpMax);
        var isSaturatedMin = VectorOp.LessThan(x, DoubleConst.ExpMin);
        var isNormal = VectorOp.OnesComplement(VectorOp.BitwiseOr(isSaturatedMax, isSaturatedMin));

        var y = x * DoubleConst.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * DoubleConst.Log_E_2;
        var z = Vector<double>.Zero;
        z = (b * DoubleConst.ExpCoeffs[9]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[8]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[7]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[6]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[5]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[4]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[3]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[2]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[1]) * (Vector<double>.One + z);
        z = (b * DoubleConst.ExpCoeffs[0]) * (Vector<double>.One + z);
        z = z + Vector<double>.One;

        return VectorOp.ConditionalSelect(
            isNormal,
            Scale(n, z),
            VectorOp.ConditionalSelect(
                isSaturatedMin,
                DoubleConst.NInf,
                DoubleConst.PInf)
            );
    }

    private static Vector<float> Exp(in Vector<float> x)
    {
        var isSaturatedMax = VectorOp.GreaterThan(x, SingleConst.ExpMax);
        var isSaturatedMin = VectorOp.LessThan(x, SingleConst.ExpMin);
        var isNormal = VectorOp.OnesComplement(VectorOp.BitwiseOr(isSaturatedMax, isSaturatedMin));

        var y = x * SingleConst.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * SingleConst.Log_E_2;
        var z = Vector<float>.Zero;
        z = (b * SingleConst.ExpCoeffs[5]) * (Vector<float>.One + z);
        z = (b * SingleConst.ExpCoeffs[4]) * (Vector<float>.One + z);
        z = (b * SingleConst.ExpCoeffs[3]) * (Vector<float>.One + z);
        z = (b * SingleConst.ExpCoeffs[2]) * (Vector<float>.One + z);
        z = (b * SingleConst.ExpCoeffs[1]) * (Vector<float>.One + z);
        z = (b * SingleConst.ExpCoeffs[0]) * (Vector<float>.One + z);
        z = z + Vector<float>.One;

        var p = VectorOp.BitwiseAnd(VectorOp.AsVectorSingle(isNormal), Scale(n, z));
        var q = VectorOp.BitwiseAnd(VectorOp.AsVectorSingle(isSaturatedMax), SingleConst.PInf);
        var r = VectorOp.BitwiseAnd(VectorOp.AsVectorSingle(isSaturatedMin), SingleConst.NInf);
        return VectorOp.BitwiseOr(
            p,
            VectorOp.BitwiseOr(q, r)
            );
    }

    #endregion

    #region Floor

    /// <summary> Calculates floor. </summary>
    [VectorMath]
    public static partial Vector<T> Floor<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Floor(in Vector<double> x)

    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        throw new NotImplementedException();
#endif
    }

    private static Vector<float> Floor(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        throw new NotImplementedException();
#endif
    }

    #endregion

    #region FusedMultiplyAdd

    /// <summary>
    /// Calculates fused-multiply-add <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector<T> FusedMultiplyAdd<T>(in Vector<T> x, in Vector<T> y, in Vector<T> z)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Fma.IsSupported)
        {
            if(Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector256<T>>())
            {
                if(typeof(T) == typeof(double))
                {
                    return Reinterpret<double, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                Reinterpret<T, double>(x).AsVector256(),
                                Reinterpret<T, double>(y).AsVector256(),
                                Reinterpret<T, double>(z).AsVector256())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return Reinterpret<float, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                Reinterpret<T, float>(x).AsVector256(),
                                Reinterpret<T, float>(y).AsVector256(),
                                Reinterpret<T, float>(z).AsVector256())
                            )
                        );
                }
            }
            if (Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector128<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return Reinterpret<double, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                Reinterpret<T, double>(x).AsVector128(),
                                Reinterpret<T, double>(y).AsVector128(),
                                Reinterpret<T, double>(z).AsVector128())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return Reinterpret<float, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                Reinterpret<T, float>(x).AsVector128(),
                                Reinterpret<T, float>(y).AsVector128(),
                                Reinterpret<T, float>(z).AsVector128())
                            )
                        );
                }
            }
        }
#endif
        switch (Vector<T>.Count)
        {
        case 2:
            return new Vector<T>(stackalloc[] {
                ScalarOp.FusedMultiplyAdd(x[0x00], y[0x00], z[0x00]),
                ScalarOp.FusedMultiplyAdd(x[0x01], y[0x01], z[0x01]),
            });
        case 4:
            return new Vector<T>(stackalloc[] {
                ScalarOp.FusedMultiplyAdd(x[0x00], y[0x00], z[0x00]),
                ScalarOp.FusedMultiplyAdd(x[0x01], y[0x01], z[0x01]),
                ScalarOp.FusedMultiplyAdd(x[0x02], y[0x02], z[0x02]),
                ScalarOp.FusedMultiplyAdd(x[0x03], y[0x03], z[0x03]),
            });
        case 8:
            return new Vector<T>(stackalloc[] {
                ScalarOp.FusedMultiplyAdd(x[0x00], y[0x00], z[0x00]),
                ScalarOp.FusedMultiplyAdd(x[0x01], y[0x01], z[0x01]),
                ScalarOp.FusedMultiplyAdd(x[0x02], y[0x02], z[0x02]),
                ScalarOp.FusedMultiplyAdd(x[0x03], y[0x03], z[0x03]),
                ScalarOp.FusedMultiplyAdd(x[0x04], y[0x04], z[0x04]),
                ScalarOp.FusedMultiplyAdd(x[0x05], y[0x05], z[0x05]),
                ScalarOp.FusedMultiplyAdd(x[0x06], y[0x06], z[0x06]),
                ScalarOp.FusedMultiplyAdd(x[0x07], y[0x07], z[0x07]),
            });
        case 16:
            return new Vector<T>(stackalloc[] {
                ScalarOp.FusedMultiplyAdd(x[0x00], y[0x00], z[0x00]),
                ScalarOp.FusedMultiplyAdd(x[0x01], y[0x01], z[0x01]),
                ScalarOp.FusedMultiplyAdd(x[0x02], y[0x02], z[0x02]),
                ScalarOp.FusedMultiplyAdd(x[0x03], y[0x03], z[0x03]),
                ScalarOp.FusedMultiplyAdd(x[0x04], y[0x04], z[0x04]),
                ScalarOp.FusedMultiplyAdd(x[0x05], y[0x05], z[0x05]),
                ScalarOp.FusedMultiplyAdd(x[0x06], y[0x06], z[0x06]),
                ScalarOp.FusedMultiplyAdd(x[0x07], y[0x07], z[0x07]),
                ScalarOp.FusedMultiplyAdd(x[0x08], y[0x08], z[0x08]),
                ScalarOp.FusedMultiplyAdd(x[0x09], y[0x09], z[0x09]),
                ScalarOp.FusedMultiplyAdd(x[0x0A], y[0x0A], z[0x0A]),
                ScalarOp.FusedMultiplyAdd(x[0x0B], y[0x0B], z[0x0B]),
                ScalarOp.FusedMultiplyAdd(x[0x0C], y[0x0C], z[0x0C]),
                ScalarOp.FusedMultiplyAdd(x[0x0D], y[0x0D], z[0x0D]),
                ScalarOp.FusedMultiplyAdd(x[0x0E], y[0x0E], z[0x0E]),
                ScalarOp.FusedMultiplyAdd(x[0x0F], y[0x0F], z[0x0F]),
            });
        default:
            {
                var buffer = (stackalloc T[Vector<T>.Count]);
                for(var i = 0; i < buffer.Length; ++i)
                {
                    buffer[i] = ScalarOp.FusedMultiplyAdd(x[i], y[i], z[i]);
                }
                return new Vector<T>(buffer);
            }
        }
    }

#endregion

    #region Modulo

    /// <summary>
    /// Calculates <c>a % b</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector<T> Modulo<T>(in Vector<T> a, in Vector<T> b)
        where T : unmanaged
    {
        DivRem(a, b, out var reminder);
        return reminder;
    }

    #endregion

    #region Round

    /// <summary> Calculates round. </summary>
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
        var nn = VectorOp.ConvertToInt64(n);
        var pow2n = VectorOp.ShiftLeft(nn + DoubleConst._1023, 52);
        return x * VectorOp.AsVectorDouble(pow2n);
    }


    // pow(2, n) * x
    static Vector<float> Scale(in Vector<float> n, Vector<float> x)
    {
        var nn = VectorOp.ConvertToInt32(n);
        var pow2n = VectorOp.ShiftLeft(nn + SingleConst._127, 23);
        return x * VectorOp.AsVectorSingle(pow2n);
    }

    #endregion

    #region Sqrt

    /// <summary> Calculates sqrt. </summary>
    [VectorMath]
    public static Vector<T> Sqrt<T>(in Vector<T> d)
        where T : unmanaged
        => VectorOp.SquareRoot(d);

    #endregion

    #region Truncate

    /// <summary> Calculates truncate. </summary>
    [VectorMath]
    public static partial Vector<T> Truncate<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate(in Vector<double> d)
        => VectorOp.ConvertToDouble(VectorOp.ConvertToInt64(d));

    private static Vector<float> Truncate(in Vector<float> d)
        => VectorOp.ConvertToSingle(VectorOp.ConvertToInt32(d));

    #endregion


}