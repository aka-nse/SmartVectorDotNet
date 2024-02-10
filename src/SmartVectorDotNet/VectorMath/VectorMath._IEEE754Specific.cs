using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using SC = ScalarMath.Const;

partial class VectorMath
{
    partial class Const
    {
        public static readonly Vector<long> DoubleSignPartMask = new(unchecked((long)SC.DoubleSignPartMask));
        public static readonly Vector<long> DoubleExpPartMask  = new(unchecked((long)SC.DoubleExpPartMask ));
        public static readonly Vector<long> DoubleFracPartMask = new(unchecked((long)SC.DoubleFracPartMask));
        public static readonly Vector<long> DoubleExpPartBias  = new(unchecked((long)SC.DoubleExpPartBias));
        public static readonly Vector<int> SingleSignPartMask = new(unchecked((int) SC.SingleSignPartMask));
        public static readonly Vector<int> SingleExpPartMask  = new(unchecked((int) SC.SingleExpPartMask ));
        public static readonly Vector<int> SingleFracPartMask = new(unchecked((int) SC.SingleFracPartMask));
        public static readonly Vector<int> SingleExpPartBias  = new(unchecked((int)SC.SingleExpPartBias));

        public static readonly Vector<long> DoubleNMax = new(unchecked((long)((SC.DoubleExpPartMask >> SC.DoubleExpBitOffset) - SC.DoubleExpPartBias)));
        public static readonly Vector<int> SingleNMax  = new(unchecked((int)((SC.SingleExpPartMask >> SC.SingleExpBitOffset) - SC.SingleExpPartBias)));
    }
    partial class Const<T>
    {
        public static readonly Vector<T> FracPartOffsetDenom
            = IsT<double>()
                ? As(new Vector<double>(SC.DoubleFracPartOffsetDenom))
            : IsT<float>()
                ? As(new Vector<float>(SC.SingleFracPartOffsetDenom))
            : default;
    }

    #region Decompose

    /// <summary>
    /// Calculates the pair of <c>n</c> and <c>a</c>
    /// which satisfies <c>x = pow(2, n) * a</c>
    /// (<c>n</c> is integer, and <c>1 &lt;= abs(a) &lt; 2 or a == 0</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    public static void Decompose<T>(in Vector<T> x, out Vector<T> n, out Vector<T> a)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            Decompose(Reinterpret<T, double>(x), out Vector<long> nn, out Vector<double> aa);
            n = Reinterpret<double, T>(Vector.ConvertToDouble(nn));
            a = Reinterpret<double, T>(aa);
            return;
        }
        if (typeof(T) == typeof(float))
        {
            Decompose(Reinterpret<T, float>(x), out Vector<int> nn, out Vector<float> aa);
            n = Reinterpret<float, T>(Vector.ConvertToSingle(nn));
            a = Reinterpret<float, T>(aa);
            return;
        }
        throw new NotSupportedException();
    }

    internal static void Decompose(in Vector<double> x, out Vector<long> n, out Vector<double> a)
    {
        var bin = Reinterpret<double, long>(x);
        var s = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, Const.DoubleSignPartMask),
            SC.DoubleSignBitOffset);
        var e = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, Const.DoubleExpPartMask),
            SC.DoubleExpBitOffset);
        var m = OP.BitwiseAnd(bin, Const.DoubleFracPartMask);
        var sign = OP.ConditionalSelect(
            OP.Equals(s, Vector<long>.Zero),
            Vector<double>.One,
            -Vector<double>.One);
        n = e - Const.DoubleExpPartBias;
        a = sign * (Vector<double>.One + OP.ConvertToDouble(m) * Const<double>.FracPartOffsetDenom);
    }

    internal static void Decompose(in Vector<float> x, out Vector<int> n, out Vector<float> a)
    {
        var bin = Reinterpret<float, int>(x);
        var s = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, Const.SingleSignPartMask),
            SC.SingleExpBitOffset);
        var e = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, Const.SingleExpPartMask),
            SC.SingleExpBitOffset);
        var m = OP.BitwiseAnd(bin, Const.SingleFracPartMask);
        var sign = OP.ConditionalSelect(
            OP.Equals(s, Vector<int>.Zero),
            Vector<float>.One,
            -Vector<float>.One);
        n = e - Const.SingleExpPartBias;
        a = sign * (Vector<float>.One + OP.ConvertToSingle(m) * Const<float>.FracPartOffsetDenom);
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
        var nn = OP.ConvertToInt64(n);
        var pow2n = OP.ShiftLeft(nn + Const.DoubleExpPartBias, 52);
        return x * OP.AsVectorDouble(pow2n);
    }


    // pow(2, n) * x
    static Vector<float> Scale(in Vector<float> n, Vector<float> x)
    {
        var nn = OP.ConvertToInt32(n);
        var pow2n = OP.ShiftLeft(nn + Const.SingleExpPartBias, 23);
        return x * OP.AsVectorSingle(pow2n);
    }

    #endregion

    #region IsNormalized

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 normalized number or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsNormalized<T>(in Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            var xx = Reinterpret<T, double>(x);
            Decompose(xx, out Vector<long> n, out _);
            var retval = Vector.BitwiseAnd(
                Vector.GreaterThan(n, Const.DoubleExpPartBias),
                Vector.LessThan(n, Const.DoubleNMax));
            return Reinterpret<long, T>(retval);
        }
        if (typeof(T) == typeof(float))
        {
            var xx = Reinterpret<T, float>(x);
            Decompose(xx, out Vector<int> n, out _);
            var retval = Vector.BitwiseAnd(
                Vector.GreaterThan(n, Const.SingleExpPartBias),
                Vector.LessThan(n, Const.SingleNMax));
            return Reinterpret<int, T>(retval);
        }
        return default;
    }

    #endregion
}