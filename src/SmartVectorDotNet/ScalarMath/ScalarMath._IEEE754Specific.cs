using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using static ScalarMath.Const;

partial class ScalarMath
{
    partial class Const
    {
#pragma warning disable IDE1006
        internal const int DoubleSignBitOffset = 63;
        internal const int DoubleExpBitOffset = 52;
        internal const ulong DoubleSignPartMask = 1uL << DoubleSignBitOffset;
        internal const ulong DoubleExpPartMask = 0x7FFuL << DoubleExpBitOffset;
        internal const ulong DoubleFracPartMask = (1uL << DoubleExpBitOffset) - 1;
        internal const ulong DoubleExpPartBias = 1023uL;
        internal const double DoubleFracPartOffsetDenom = 1.0 / (1L << DoubleExpBitOffset);

        internal const int SingleSignBitOffset = 31;
        internal const int SingleExpBitOffset = 23;
        internal const uint SingleSignPartMask = 1u << SingleSignBitOffset;
        internal const uint SingleExpPartMask = 0xFFu << SingleExpBitOffset;
        internal const uint SingleFracPartMask = (1u << SingleExpBitOffset) - 1;
        internal const uint SingleExpPartBias = 127u;
        internal const float SingleFracPartOffsetDenom = 1.0f / (1 << SingleExpBitOffset);
#pragma warning restore IDE1006
    }


    /// <summary>
    /// Calculates the pair of <c>n</c> and <c>a</c>
    /// which satisfies <c>x = pow(2, n) * a</c>
    /// (<c>n</c> is non-negative integer, and <c>1 &lt;= abs(a) &lt; 2 or a == 0</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    public static void Decompose<T>(T x, out T n, out T a)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var bin = Reinterpret<T, ulong>(x);
            var signBit = (bin & DoubleSignPartMask) >> DoubleSignBitOffset;
            var sign = signBit == 0 ? 1.0 : -1.0;
            var e = (bin & DoubleExpPartMask) >> DoubleExpBitOffset;
            var m = bin & DoubleFracPartMask;
            n = Reinterpret<double, T>((long)(e - DoubleExpPartBias));
            a = Reinterpret<double, T>(sign * (1.0 + m * DoubleFracPartOffsetDenom));
            return;
        }
        if (typeof(T) == typeof(float))
        {
            var bin = Reinterpret<T, uint>(x);
            var signBit = (bin & SingleSignPartMask) >> SingleSignBitOffset;
            var sign = signBit == 0 ? 1.0f : -1.0f;
            var e = (bin & SingleExpPartMask) >> SingleExpBitOffset;
            var m = bin & SingleFracPartMask;
            n = Reinterpret<float, T>((int)(e - SingleExpPartBias));
            a = Reinterpret<float, T>(sign * (1.0f + m * SingleFracPartOffsetDenom));
            return;
        }
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates <c>pow(2, n) * x</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T Scale<T>(T n, T x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var nn = Reinterpret<T, double>(n);
            var xx = Reinterpret<T, double>(x);
            return Reinterpret<double, T>(Reinterpret<long, double>(((long)nn + 1023) << 52) * xx);
        }
        if (typeof(T) == typeof(float))
        {
            var nn = Reinterpret<T, float>(n);
            var xx = Reinterpret<T, float>(x);
            return Reinterpret<float, T>(Reinterpret<int, float>(((int)nn + 127) << 23) * xx);
        }
        throw new NotSupportedException();
    }
}