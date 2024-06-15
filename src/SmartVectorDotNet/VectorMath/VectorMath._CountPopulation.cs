using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

file class CountPopulation_
{
    public static readonly Vector<byte> UInt8LastMask = new(15);
    public static ReadOnlySpan<Vector<byte>> UInt8Masks => _uInt8Masks;
    private static readonly Vector<byte>[] _uInt8Masks
        = new Vector<byte>[]
        {
            new(0x55),
            new(0x33),
            new(0x0F),
        };

    public static readonly Vector<ushort> UInt16LastMask = new(31);
    public static ReadOnlySpan<Vector<ushort>> UInt16Masks => _uInt16Masks;
    private static readonly Vector<ushort>[] _uInt16Masks
        = new Vector<ushort>[]
        {
            new(0x5555),
            new(0x3333),
            new(0x0F0F),
            new(0x00FF),
        };

    public static readonly Vector<uint> UInt32LastMask = new(63);
    public static ReadOnlySpan<Vector<uint>> UInt32Masks => _uInt32Masks;
    private static readonly Vector<uint>[] _uInt32Masks
        = new Vector<uint>[]
        {
            new(0x55555555),
            new(0x33333333),
            new(0x0F0F0F0F),
            new(0x00FF00FF),
            new(0x0000FFFF),
        };

    public static readonly Vector<ulong> UInt64LastMask = new(127);
    public static ReadOnlySpan<Vector<ulong>> UInt64Masks => _uInt64Masks;
    private static readonly Vector<ulong>[] _uInt64Masks
        = new Vector<ulong>[]
        {
            new(0x5555555555555555),
            new(0x3333333333333333),
            new(0x0F0F0F0F0F0F0F0F),
            new(0x00FF00FF00FF00FF),
            new(0x0000FFFF0000FFFF),
            new(0x00000000FFFFFFFF),
        };
}

partial class VectorMath
{

#pragma warning disable format
    
    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<T> CountPopulation<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(byte  ) || typeof(T) == typeof(sbyte )) { return H.Reinterpret<byte  , T>(CountPopulation(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort) || typeof(T) == typeof(short )) { return H.Reinterpret<ushort, T>(CountPopulation(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  ) || typeof(T) == typeof(int   )) { return H.Reinterpret<uint  , T>(CountPopulation(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong ) || typeof(T) == typeof(long  )) { return H.Reinterpret<ulong , T>(CountPopulation(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint ) || typeof(T) == typeof(nint  )) { return H.Reinterpret<nuint , T>(CountPopulation(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>(Vector.ConvertToSingle(CountPopulation(H.Reinterpret<T, uint  >(x)))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>(Vector.ConvertToDouble(CountPopulation(H.Reinterpret<T, ulong >(x)))); }
        throw new NotSupportedException();
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<byte> CountPopulation(in Vector<byte> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt8Masks[0]) + (OP.ShiftRightLogical(y, 1) & CountPopulation_.UInt8Masks[0]);
        y = (y & CountPopulation_.UInt8Masks[1]) + (OP.ShiftRightLogical(y, 2) & CountPopulation_.UInt8Masks[1]);
        y = (y & CountPopulation_.UInt8Masks[2]) +  OP.ShiftRightLogical(y, 4);
        return y & CountPopulation_.UInt8LastMask;
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<ushort> CountPopulation(in Vector<ushort> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt16Masks[0]) + (OP.ShiftRightLogical(y, 1) & CountPopulation_.UInt16Masks[0]);
        y = (y & CountPopulation_.UInt16Masks[1]) + (OP.ShiftRightLogical(y, 2) & CountPopulation_.UInt16Masks[1]);
        y = (y & CountPopulation_.UInt16Masks[2]) + (OP.ShiftRightLogical(y, 4) & CountPopulation_.UInt16Masks[2]);
        y = (y & CountPopulation_.UInt16Masks[3]) +  OP.ShiftRightLogical(y, 8);
        return y & CountPopulation_.UInt16LastMask;
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<uint> CountPopulation(in Vector<uint> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt32Masks[0]) + (OP.ShiftRightLogical(y,  1) & CountPopulation_.UInt32Masks[0]);
        y = (y & CountPopulation_.UInt32Masks[1]) + (OP.ShiftRightLogical(y,  2) & CountPopulation_.UInt32Masks[1]);
        y = (y & CountPopulation_.UInt32Masks[2]) + (OP.ShiftRightLogical(y,  4) & CountPopulation_.UInt32Masks[2]);
        y = (y & CountPopulation_.UInt32Masks[3]) + (OP.ShiftRightLogical(y,  8) & CountPopulation_.UInt32Masks[3]);
        y = (y & CountPopulation_.UInt32Masks[4]) +  OP.ShiftRightLogical(y, 16);
        return y & CountPopulation_.UInt32LastMask;
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<ulong> CountPopulation(in Vector<ulong> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt64Masks[0]) + (OP.ShiftRightLogical(y,  1) & CountPopulation_.UInt64Masks[0]);
        y = (y & CountPopulation_.UInt64Masks[1]) + (OP.ShiftRightLogical(y,  2) & CountPopulation_.UInt64Masks[1]);
        y = (y & CountPopulation_.UInt64Masks[2]) + (OP.ShiftRightLogical(y,  4) & CountPopulation_.UInt64Masks[2]);
        y = (y & CountPopulation_.UInt64Masks[3]) + (OP.ShiftRightLogical(y,  8) & CountPopulation_.UInt64Masks[3]);
        y = (y & CountPopulation_.UInt64Masks[4]) + (OP.ShiftRightLogical(y, 16) & CountPopulation_.UInt64Masks[4]);
        y = (y & CountPopulation_.UInt64Masks[5]) +  OP.ShiftRightLogical(y, 32);
        return y & CountPopulation_.UInt64LastMask;
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<nuint> CountPopulation(in Vector<nuint> x)
    {
        if (Unsafe.SizeOf<nuint>() == sizeof(ulong)) { return H.Reinterpret<ulong, nuint>(CountPopulation(H.Reinterpret<nuint, ulong>(x))); }
        if (Unsafe.SizeOf<nuint>() == sizeof(uint )) { return H.Reinterpret<uint , nuint>(CountPopulation(H.Reinterpret<nuint, uint >(x))); }
        throw new NotSupportedException();
    }

#pragma warning restore format
}
