using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{

#pragma warning disable format
    
    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<T> CountLeadingZeros<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(byte  ) || typeof(T) == typeof(sbyte )) { return H.Reinterpret<byte  , T>(CountLeadingZeros(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort) || typeof(T) == typeof(short )) { return H.Reinterpret<ushort, T>(CountLeadingZeros(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  ) || typeof(T) == typeof(int   )) { return H.Reinterpret<uint  , T>(CountLeadingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong ) || typeof(T) == typeof(long  )) { return H.Reinterpret<ulong , T>(CountLeadingZeros(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint ) || typeof(T) == typeof(nint  )) { return H.Reinterpret<nuint , T>(CountLeadingZeros(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>(Vector.ConvertToSingle(CountLeadingZeros(H.Reinterpret<T, uint  >(x)))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>(Vector.ConvertToDouble(CountLeadingZeros(H.Reinterpret<T, ulong >(x)))); }
        throw new NotSupportedException();
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<byte> CountLeadingZeros(in Vector<byte> x)
    {
        Vector<byte> y = x;
        y |= OP.ShiftRightLogical(y, 1);
        y |= OP.ShiftRightLogical(y, 2);
        y |= OP.ShiftRightLogical(y, 4);
        return CountPopulation(~y);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<ushort> CountLeadingZeros(in Vector<ushort> x)
    {
        Vector<ushort> y = x;
        y |= OP.ShiftRightLogical(y, 1);
        y |= OP.ShiftRightLogical(y, 2);
        y |= OP.ShiftRightLogical(y, 4);
        y |= OP.ShiftRightLogical(y, 8);
        return CountPopulation(~y);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<uint> CountLeadingZeros(in Vector<uint> x)
    {
        Vector<uint> y = x;
        y |= OP.ShiftRightLogical(y, 1);
        y |= OP.ShiftRightLogical(y, 2);
        y |= OP.ShiftRightLogical(y, 4);
        y |= OP.ShiftRightLogical(y, 8);
        y |= OP.ShiftRightLogical(y, 16);
        return CountPopulation(~y);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static Vector<ulong> CountLeadingZeros(in Vector<ulong> x)
    {
        Vector<ulong> y = x;
        y |= OP.ShiftRightLogical(y, 1);
        y |= OP.ShiftRightLogical(y, 2);
        y |= OP.ShiftRightLogical(y, 4);
        y |= OP.ShiftRightLogical(y, 8);
        y |= OP.ShiftRightLogical(y, 16);
        y |= OP.ShiftRightLogical(y, 32);
        return CountPopulation(~y);
    }

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<nuint> CountLeadingZeros(in Vector<nuint> x)
    {
        if (Unsafe.SizeOf<nuint>() == sizeof(ulong)) { return H.Reinterpret<ulong, nuint>(CountLeadingZeros(H.Reinterpret<nuint, ulong>(x))); }
        if (Unsafe.SizeOf<nuint>() == sizeof(uint)) { return H.Reinterpret<uint, nuint>(CountLeadingZeros(H.Reinterpret<nuint, uint>(x))); }
        throw new NotSupportedException();
    }

#pragma warning restore format
}
