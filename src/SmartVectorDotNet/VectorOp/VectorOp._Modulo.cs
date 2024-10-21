using System;
using System.Collections.Generic;
using System.Text;
using NVector = System.Numerics.Vector;
using H = SmartVectorDotNet.InternalHelpers;

namespace SmartVectorDotNet;

file static class Const
{
    public static readonly Vector<uint> SingleEBitPosFromLeft = new(31u - (uint)ScalarMath.Const.SingleExpBitOffset);
    public static readonly Vector<int> SingleEconomizedBit = new(1 << ScalarMath.Const.SingleExpBitOffset);
    public static readonly Vector<int> SingleExponentBits = new(32 - ScalarMath.Const.SingleExpBitOffset - 1);

    public static readonly Vector<ulong> DoubleEBitPosFromLeft = new(63u - (ulong)ScalarMath.Const.DoubleExpBitOffset);
    public static readonly Vector<long> DoubleEconomizedBit = new(1L << ScalarMath.Const.DoubleExpBitOffset);
    public static readonly Vector<long> DoubleExponentBits = new(64L - ScalarMath.Const.DoubleExpBitOffset - 1);
}


partial class VectorOp
{
    /// <summary>
    /// Calculates modulos (<c>x % y</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector<T> Modulo<T>(Vector<T> x, Vector<T> y)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
        {
            return H.Reinterpret<float, T>(Modulo(H.Reinterpret<T, float>(x), H.Reinterpret<T, float>(y)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.Reinterpret<double, T>(Modulo(H.Reinterpret<T, double>(x), H.Reinterpret<T, double>(y)));
        }
        return x - y * (x / y);
    }

    /// <summary>
    /// Calculates modulos (<c>x % y</c>).
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector<float> Modulo(Vector<float> x, Vector<float> y)
    {
        VectorMath.Decompose(x, out var s, out var n, out var a);
        VectorMath.Decompose(y, out var _, out var m, out var b);

        var i = n - m;
        var shouldReturnX = LessThan(i, Vector<int>.Zero);

        var c = H.Reinterpret<int, uint>(a | Const.SingleEconomizedBit);
        var d = H.Reinterpret<int, uint>(b | Const.SingleEconomizedBit);
        while(true)
        {
            var condition = H.Reinterpret<int, uint>(GreaterThan(i, Const.SingleExponentBits));
            if(NVector.EqualsAll(condition, VectorMath.Const<uint>.FalseValue))
            {
                break;
            }
            var nextC = ShiftLeft(c, H.Reinterpret<int, uint>(Const.SingleExponentBits));
            nextC -= d * (nextC / d);
            c = ConditionalSelect(condition, nextC, c);
            i = ConditionalSelect(H.Reinterpret<uint, int>(condition), i - Const.SingleExponentBits, i);
        }
        c = ShiftLeft(c, H.Reinterpret<int, uint>(i));
        c -= d * (c / d);
        i = Vector<int>.Zero;

        var shouldReturnZero = Equals(c, Vector<uint>.Zero);
        var eBitShift = VectorMath.CountLeadingZeros(c) - Const.SingleEBitPosFromLeft;
        c = ShiftLeft(c, eBitShift);
        i -= H.Reinterpret<uint, int>(eBitShift);
        
        var retval = VectorMath.Scale(s, m + i, H.Reinterpret<uint, int>(c) & VectorMath.IEEE754Single_.FracPartMask);
        return ConditionalSelect(
            H.Reinterpret<int, float>(shouldReturnX),
            x,
            ConditionalSelect(
                H.Reinterpret<uint, float>(shouldReturnZero),
                Vector<float>.Zero,
                retval));
    }

    /// <summary>
    /// Calculates modulos (<c>x % y</c>).
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector<double> Modulo(Vector<double> x, Vector<double> y)
    {
        VectorMath.Decompose(x, out var s, out var n, out var a);
        VectorMath.Decompose(y, out var _, out var m, out var b);

        var i = n - m;
        var shouldReturnX = LessThan(i, Vector<long>.Zero);

        var c = H.Reinterpret<long, ulong>(a | Const.DoubleEconomizedBit);
        var d = H.Reinterpret<long, ulong>(b | Const.DoubleEconomizedBit);
        while (true)
        {
            var condition = H.Reinterpret<long, ulong>(GreaterThan(i, Const.DoubleExponentBits));
            if (NVector.EqualsAll(condition, VectorMath.Const<ulong>.FalseValue))
            {
                break;
            }
            var nextC = ShiftLeft(c, H.Reinterpret<long, ulong>(Const.DoubleExponentBits));
            nextC -= d * (nextC / d);
            c = ConditionalSelect(condition, nextC, c);
            i = ConditionalSelect(H.Reinterpret<ulong, long>(condition), i - Const.DoubleExponentBits, i);
        }
        c = ShiftLeft(c, H.Reinterpret<long, ulong>(i));
        c -= d * (c / d);
        i = Vector<long>.Zero;

        var shouldReturnZero = Equals(c, Vector<ulong>.Zero);
        var eBitShift = VectorMath.CountLeadingZeros(c) - Const.DoubleEBitPosFromLeft;
        c = ShiftLeft(c, eBitShift);
        i -= H.Reinterpret<ulong, long>(eBitShift);

        var retval = VectorMath.Scale(s, m + i, H.Reinterpret<ulong, long>(c) & VectorMath.IEEE754Double_.FracPartMask);
        return ConditionalSelect(
            H.Reinterpret<long, double>(shouldReturnX),
            x,
            ConditionalSelect(
                H.Reinterpret<ulong, double>(shouldReturnZero),
                Vector<double>.Zero,
                retval));
    }
}
