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


    partial class Const<T>
    {
        /// <summary>
        /// Gets machine epsilon.
        /// </summary>
        public static readonly T MachineEpsilon
            = IsT<double>()
                ? Reinterpret(Scale<double>(-DoubleExpBitOffset, 1))
            : IsT<float>()
                ? Reinterpret(Scale<float>(-SingleExpBitOffset, 1))
            : default;

        /// <summary>
        /// Gets positive minimum normalized number.
        /// </summary>
        public static readonly T PositiveMinimumNormalizedNumber
            = IsT<double>()
                ? Reinterpret(Scale<double>(1.0 - DoubleExpPartBias, 1))
            : IsT<float>()
                ? Reinterpret(Scale<float>(1.0f - SingleExpPartBias, 1))
            : default;
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
            Decompose(Reinterpret<T, double>(x), out long nn, out double aa);
            n = Reinterpret<double, T>(nn);
            a = Reinterpret<double, T>(aa);
            return;
        }
        if (typeof(T) == typeof(float))
        {
            Decompose(Reinterpret<T, float>(x), out int nn, out float aa);
            n = Reinterpret<float, T>(nn);
            a = Reinterpret<float, T>(aa);
            return;
        }
        throw new NotSupportedException();
    }


    internal static void Decompose(double x, out long n, out double a)
    {
        var bin = Reinterpret<double, ulong>(x);
        var signBit = (bin & DoubleSignPartMask) >> DoubleSignBitOffset;
        var sign = signBit == 0 ? 1.0 : -1.0;
        var e = (bin & DoubleExpPartMask) >> DoubleExpBitOffset;
        var m = bin & DoubleFracPartMask;
        n = (long)Max(e, 1uL) - (long)DoubleExpPartBias;
        a = sign * ((e > 0 ? 1.0 : 0.0) + m * DoubleFracPartOffsetDenom);
    }


    internal static void Decompose(float x, out int n, out float a)
    {
        var bin = Reinterpret<float, uint>(x);
        var signBit = (bin & SingleSignPartMask) >> SingleSignBitOffset;
        var sign = signBit == 0 ? 1.0f : -1.0f;
        var e = (bin & SingleExpPartMask) >> SingleExpBitOffset;
        var m = bin & SingleFracPartMask;
        n = (int)Max(e, 1u) - (int)SingleExpPartBias;
        a = sign * ((e > 0 ? 1.0f : 0.0f) + m * SingleFracPartOffsetDenom);
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
            return Reinterpret<double, T>(Reinterpret<long, double>(((long)nn + (long)DoubleExpPartBias) << 52) * xx);
        }
        if (typeof(T) == typeof(float))
        {
            var nn = Reinterpret<T, float>(n);
            var xx = Reinterpret<T, float>(x);
            return Reinterpret<float, T>(Reinterpret<int, float>(((int)nn + (int)SingleExpPartBias) << 23) * xx);
        }
        throw new NotSupportedException();
    }
}