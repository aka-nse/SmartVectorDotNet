using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using SC = ScalarMath.Const;

partial class VectorMath
{
    private class IEEE754Double_ : Const<double>
    {
        internal static readonly Vector<long> IntZero = Const<long>._0;
        internal static readonly Vector<long> SignPartMask = new(unchecked((long)SC.DoubleSignPartMask));
        internal static readonly Vector<long> ExpPartMask = new(unchecked((long)SC.DoubleExpPartMask));
        internal static readonly Vector<long> FracPartMask = new(unchecked((long)SC.DoubleFracPartMask));
        internal static readonly Vector<long> ExpPartBias = new(unchecked((long)SC.DoubleExpPartBias));
        internal static readonly Vector<long> NMin = new(-(long)SC.DoubleExpPartBias);
        internal static readonly Vector<long> NMax = new(unchecked((long)((SC.DoubleExpPartMask >> SC.DoubleExpBitOffset) - SC.DoubleExpPartBias)));

        internal static readonly Vector<double> FracPartOffsetDenom = new (SC.DoubleFracPartOffsetDenom);
    }

    private class IEEE754Single_ : Const<float>
    {
        internal static readonly Vector<int> IntZero = Const<int>._0;
        internal static readonly Vector<int> SignPartMask = new(unchecked((int)SC.SingleSignPartMask));
        internal static readonly Vector<int> ExpPartMask = new(unchecked((int)SC.SingleExpPartMask));
        internal static readonly Vector<int> FracPartMask = new(unchecked((int)SC.SingleFracPartMask));
        internal static readonly Vector<int> ExpPartBias = new(unchecked((int)SC.SingleExpPartBias));
        internal static readonly Vector<int> NMin = new(-(int)SC.SingleExpPartBias);
        internal static readonly Vector<int> NMax = new(unchecked((int)((SC.SingleExpPartMask >> SC.SingleExpBitOffset) - SC.SingleExpPartBias)));

        internal static readonly Vector<float> FracPartOffsetDenom = new (SC.SingleFracPartOffsetDenom);
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
            OP.BitwiseAnd(bin, IEEE754Double_.SignPartMask),
            SC.DoubleSignBitOffset);
        var e = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Double_.ExpPartMask),
            SC.DoubleExpBitOffset);
        var m = OP.BitwiseAnd(bin, IEEE754Double_.FracPartMask);
        var sign = OP.ConditionalSelect(
            OP.Equals(s, IEEE754Double_.IntZero),
            IEEE754Double_._1,
            IEEE754Double_._m1);
        n = e - IEEE754Double_.ExpPartBias;
        a = sign * (IEEE754Double_._1 + OP.ConvertToDouble(m) * IEEE754Double_.FracPartOffsetDenom);
    }

    internal static void Decompose(in Vector<float> x, out Vector<int> n, out Vector<float> a)
    {
        var bin = Reinterpret<float, int>(x);
        var s = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Single_.SignPartMask),
            SC.SingleExpBitOffset);
        var e = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Single_.ExpPartMask),
            SC.SingleExpBitOffset);
        var m = OP.BitwiseAnd(bin, IEEE754Single_.FracPartMask);
        var sign = OP.ConditionalSelect(
            OP.Equals(s, IEEE754Single_.IntZero),
            IEEE754Single_._1,
            IEEE754Single_._m1);
        n = e - IEEE754Single_.ExpPartBias;
        a = sign * (IEEE754Single_._1 + OP.ConvertToSingle(m) * IEEE754Single_.FracPartOffsetDenom);
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
        var pow2n = OP.ShiftLeft(nn + IEEE754Double_.ExpPartBias, 52);
        return x * OP.AsVectorDouble(pow2n);
    }


    // pow(2, n) * x
    static Vector<float> Scale(in Vector<float> n, Vector<float> x)
    {
        var nn = OP.ConvertToInt32(n);
        var pow2n = OP.ShiftLeft(nn + IEEE754Single_.ExpPartBias, 23);
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
                Vector.GreaterThan(n, IEEE754Double_.NMin),
                Vector.LessThan(n, IEEE754Double_.NMax));
            return Reinterpret<long, T>(retval);
        }
        if (typeof(T) == typeof(float))
        {
            var xx = Reinterpret<T, float>(x);
            Decompose(xx, out Vector<int> n, out _);
            var retval = Vector.BitwiseAnd(
                Vector.GreaterThan(n, IEEE754Single_.NMin),
                Vector.LessThan(n, IEEE754Single_.NMax));
            return Reinterpret<int, T>(retval);
        }
        return default;
    }

    #endregion

    #region IsDenormalized

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 denormalized number or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsDenormalized<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = Reinterpret<T, double>(x);
            Decompose(xx, out Vector<long> n, out var a);
            var retval = Vector.BitwiseAnd(
                Vector.Equals(n, IEEE754Double_.NMin),
                Vector.OnesComplement(Vector.Equals(a, IEEE754Double_._0)));
            return Reinterpret<long, T>(retval);
        }
        if (typeof(T) == typeof(float))
        {
            var xx = Reinterpret<T, float>(x);
            Decompose(xx, out Vector<int> n, out var a);
            var retval = Vector.BitwiseAnd(
                Vector.Equals(n, IEEE754Single_.NMin),
                Vector.OnesComplement(Vector.Equals(a, IEEE754Single_._0)));
            return Reinterpret<int, T>(retval);
        }
        return default;
    }

    #endregion

    #region IsInfinity

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 infinity or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsInfinity<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = Reinterpret<T, double>(x);
            return Reinterpret<long, T>(IsInfinity(xx));
        }
        if (typeof(T) == typeof(float))
        {
            var xx = Reinterpret<T, float>(x);
            return Reinterpret<int, T>(IsInfinity(xx));
        }
        return default;
    }

    private static Vector<long> IsInfinity(in Vector<double> x)
    {
        Decompose(x, out Vector<long> n, out var a);
        return Vector.BitwiseAnd(
            Vector.Equals(n, IEEE754Double_.NMax),
            Vector.Equals(a, IEEE754Double_._0));
    }

    private static Vector<int> IsInfinity(in Vector<float> x)
    {
        Decompose(x, out Vector<int> n, out var a);
        return Vector.BitwiseAnd(
            Vector.Equals(n, IEEE754Single_.NMax),
            Vector.Equals(a, IEEE754Single_._0));
    }

    #endregion

    #region IsInfinity

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 positive infinity or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsPositiveInfinity<T>(in Vector<T> x)
        where T : unmanaged
    {
#error not implemented
        return default;
    }

    #endregion
}