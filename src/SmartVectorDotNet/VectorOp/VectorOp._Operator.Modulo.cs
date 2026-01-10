using NVector = System.Numerics.Vector;
using H = SmartVectorDotNet.InternalHelpers;

namespace SmartVectorDotNet;

file static class Const
{
    public static readonly Vector<uint> SingleEBitPosFromLeft = new(31u - (uint)ScalarOp.Const.SingleExpBitOffset);
    public static readonly Vector<int> SingleEconomizedBit = new(1 << ScalarOp.Const.SingleExpBitOffset);
    public static readonly Vector<int> SingleExponentBits = new(32 - ScalarOp.Const.SingleExpBitOffset - 1);

    public static readonly Vector<ulong> DoubleEBitPosFromLeft = new(63u - (ulong)ScalarOp.Const.DoubleExpBitOffset);
    public static readonly Vector<long> DoubleEconomizedBit = new(1L << ScalarOp.Const.DoubleExpBitOffset);
    public static readonly Vector<long> DoubleExponentBits = new(64L - ScalarOp.Const.DoubleExpBitOffset - 1);
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
            return H.BitCastV<float, T>(Modulo(H.BitCastV<T, float>(x), H.BitCastV<T, float>(y)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.BitCastV<double, T>(Modulo(H.BitCastV<T, double>(x), H.BitCastV<T, double>(y)));
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
        VectorOp.Decompose(x, out var s, out var n, out var a);
        VectorOp.Decompose(y, out var _, out var m, out var b);

        var i = n - m;
        var shouldReturnX = LessThan(i, Vector<int>.Zero);

        var c = H.BitCastV<int, uint>(a | Const.SingleEconomizedBit);
        var d = H.BitCastV<int, uint>(b | Const.SingleEconomizedBit);
        while(true)
        {
            var condition = H.BitCastV<int, uint>(GreaterThan(i, Const.SingleExponentBits));
            if(NVector.EqualsAll(condition, VectorOp.Const<uint>.FalseValue))
            {
                break;
            }
            var nextC = ShiftLeft(c, H.BitCastV<int, uint>(Const.SingleExponentBits));
            nextC -= d * (nextC / d);
            c = ConditionalSelect(condition, nextC, c);
            i = ConditionalSelect(H.BitCastV<uint, int>(condition), i - Const.SingleExponentBits, i);
        }
        c = ShiftLeft(c, H.BitCastV<int, uint>(i));
        c -= d * (c / d);
        i = Vector<int>.Zero;

        var shouldReturnZero = Equals(c, Vector<uint>.Zero);
        var eBitShift = VectorOp.CountLeadingZeros(c) - Const.SingleEBitPosFromLeft;
        c = ShiftLeft(c, eBitShift);
        i -= H.BitCastV<uint, int>(eBitShift);
        
        var retval = VectorOp.Scale(s, m + i, H.BitCastV<uint, int>(c) & VectorOp.IEEE754Single_.FracPartMask);
        return ConditionalSelect(
            H.BitCastV<int, float>(shouldReturnX),
            x,
            ConditionalSelect(
                H.BitCastV<uint, float>(shouldReturnZero),
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
        VectorOp.Decompose(x, out var s, out var n, out var a);
        VectorOp.Decompose(y, out var _, out var m, out var b);

        var i = n - m;
        var shouldReturnX = LessThan(i, Vector<long>.Zero);

        var c = H.BitCastV<long, ulong>(a | Const.DoubleEconomizedBit);
        var d = H.BitCastV<long, ulong>(b | Const.DoubleEconomizedBit);
        while (true)
        {
            var condition = H.BitCastV<long, ulong>(GreaterThan(i, Const.DoubleExponentBits));
            if (NVector.EqualsAll(condition, VectorOp.Const<ulong>.FalseValue))
            {
                break;
            }
            var nextC = ShiftLeft(c, H.BitCastV<long, ulong>(Const.DoubleExponentBits));
            nextC -= d * (nextC / d);
            c = ConditionalSelect(condition, nextC, c);
            i = ConditionalSelect(H.BitCastV<ulong, long>(condition), i - Const.DoubleExponentBits, i);
        }
        c = ShiftLeft(c, H.BitCastV<long, ulong>(i));
        c -= d * (c / d);
        i = Vector<long>.Zero;

        var shouldReturnZero = Equals(c, Vector<ulong>.Zero);
        var eBitShift = VectorOp.CountLeadingZeros(c) - Const.DoubleEBitPosFromLeft;
        c = ShiftLeft(c, eBitShift);
        i -= H.BitCastV<ulong, long>(eBitShift);

        var retval = VectorOp.Scale(s, m + i, H.BitCastV<ulong, long>(c) & VectorOp.IEEE754Double_.FracPartMask);
        return ConditionalSelect(
            H.BitCastV<long, double>(shouldReturnX),
            x,
            ConditionalSelect(
                H.BitCastV<ulong, double>(shouldReturnZero),
                Vector<double>.Zero,
                retval));
    }
}
