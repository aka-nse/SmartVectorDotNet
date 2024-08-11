using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorBenchmarks;

public class ModuloBenchmarkContext
{
    public void Test()
    {
        for(var i = 0; i < 1000; ++i)
        {
            var x = Double.MaxValue / Math.Pow(1.1, i);
            var y = Math.Tau;
            var exp = x % y;
            var act = Modulo(new(x), new(y))[0];
            Console.WriteLine(exp == act);
        }
        for (var i = 0; i < 1000; ++i)
        {
            var x = Double.MaxValue / Math.Pow(1.1, i);
            var y = Double.Epsilon * 1e+10;
            var exp = x % y;
            var act = Modulo(new(x), new(y))[0];
            Console.WriteLine(exp == act);
        }
    }



    public static Vector<double> Modulo(in Vector<double> x, in Vector<double> y)
    {
        Decompose(x, out var s, out var n, out var a);
        Decompose(y, out var t, out var m, out var b);
        var i = n - m;
        var shouldReturnX = Vector.LessThan(i, Vector<long>.Zero);

        var c = Reinterpret<long, ulong>(a | EconomizedBit);
        var d = Reinterpret<long, ulong>(b | EconomizedBit);
        while(true)
        {
            var shouldContinue = Vector.GreaterThan(i, ExponentBitsVector);
            if(Vector.EqualsAll(shouldContinue, Vector<long>.Zero))
            {
                break;
            }
            var cc = Vector.ShiftLeft(c, ExponentBits);
            c = Vector.ConditionalSelect(
                Reinterpret<long, ulong>(shouldContinue),
                cc - d * (cc / d),
                c);
            i = Vector.ConditionalSelect(
                shouldContinue,
                i - ExponentBitsVector,
                i);
        }
        // shift remains
        {
            var cc = ShiftLeftVariable(c, Reinterpret<long, ulong>(i));
            c = cc - d * (cc / d);
            i = Vector<long>.Zero;
        }

        var shouldReturn0 = Vector.Equals(c, Vector<ulong>.Zero);
        c |= shouldReturn0;

        var needToExtraShift = Clz(c) - Reinterpret<long, ulong>(ExponentBitsVector);
        c = ShiftLeftVariable(c, needToExtraShift);
        i -= Reinterpret<ulong, long>(needToExtraShift);

        var ans = Scale(s, m + i, Reinterpret<ulong, long>(c) & FracPartMask);
        return Vector.ConditionalSelect(
            shouldReturnX,
            x,
            Vector.ConditionalSelect(
                Reinterpret<ulong, long>(shouldReturn0),
                Vector<double>.Zero,
                ans));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<TTo> Reinterpret<TFrom, TTo>(in Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(in x));

    internal static void Decompose(in Vector<double> x, out Vector<long> sign, out Vector<long> expo, out Vector<long> frac)
    {
        var bin = Reinterpret<double, long>(x);
        sign = Vector.ShiftRightLogical(
            Vector.BitwiseAnd(bin, SignPartMask),
            DoubleSignBitOffset);
        expo = Vector.ShiftRightLogical(
            Vector.BitwiseAnd(bin, ExpPartMask),
            DoubleExpBitOffset);
        frac = Vector.BitwiseAnd(bin, FracPartMask);
    }

    static Vector<double> Scale(in Vector<long> sign, in Vector<long> expo, in Vector<long> frac)
        => Reinterpret<long, double>(Vector.ShiftLeft(sign, DoubleSignBitOffset) | Vector.ShiftLeft(expo, DoubleExpBitOffset) | frac);

    static Vector<ulong> ShiftLeftVariable(in Vector<ulong> x, in Vector<ulong> y)
    {
        if(Unsafe.SizeOf<Vector<ulong>>() == Unsafe.SizeOf<Vector256<ulong>>())
        {
            var xx = Unsafe.As<Vector<ulong>, Vector256<ulong>>(ref Unsafe.AsRef(in x));
            var yy = Unsafe.As<Vector<ulong>, Vector256<ulong>>(ref Unsafe.AsRef(in y));
            var zz = Avx2.ShiftLeftLogicalVariable(xx, yy);
            return Unsafe.As<Vector256<ulong>, Vector<ulong>>(ref zz);
        }
        if (Unsafe.SizeOf<Vector<ulong>>() == Unsafe.SizeOf<Vector128<ulong>>())
        {
            var xx = Unsafe.As<Vector<ulong>, Vector128<ulong>>(ref Unsafe.AsRef(in x));
            var yy = Unsafe.As<Vector<ulong>, Vector128<ulong>>(ref Unsafe.AsRef(in y));
            var zz = Avx2.ShiftLeftLogicalVariable(xx, yy);
            return Unsafe.As<Vector128<ulong>, Vector<ulong>>(ref zz);
        }
        throw new NotSupportedException();
    }


    static Vector<ulong> Clz(in Vector<ulong> x)
    {
        var buffer = (stackalloc ulong[Vector<ulong>.Count]);
        for(var i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = ulong.LeadingZeroCount(x[i]);
        }
        return MemoryMarshal.Cast<ulong, Vector<ulong>>(buffer)[0];
    }


    internal const int DoubleSignBitOffset = 63;
    internal const int DoubleExpBitOffset = 52;
    internal const long DoubleSignPartMask = 1L << DoubleSignBitOffset;
    internal const long DoubleExpPartMask = 0x7FFL << DoubleExpBitOffset;
    internal const long DoubleFracPartMask = (1L << DoubleExpBitOffset) - 1;
    internal const long DoubleExpPartBias = 1023L;

    internal static readonly Vector<long> SignPartMask = new((DoubleSignPartMask));
    internal static readonly Vector<long> ExpPartMask = new((DoubleExpPartMask));
    internal static readonly Vector<long> FracPartMask = new((DoubleFracPartMask));
    internal static readonly Vector<long> ExpPartBias = new((DoubleExpPartBias));

    internal static readonly Vector<long> EconomizedBit = new(1L << DoubleExpBitOffset);
    internal static readonly Vector<long> ExponentBitsVector = new((long)ExponentBits);
    internal const int ExponentBits = 64 - DoubleExpBitOffset - 1;
}
