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
    
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<T> CountTrailingZeros<T>(Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(byte  ) || typeof(T) == typeof(sbyte )) { return H.Reinterpret<byte  , T>(CountTrailingZeros(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort) || typeof(T) == typeof(short )) { return H.Reinterpret<ushort, T>(CountTrailingZeros(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  ) || typeof(T) == typeof(int   )) { return H.Reinterpret<uint  , T>(CountTrailingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong ) || typeof(T) == typeof(long  )) { return H.Reinterpret<ulong , T>(CountTrailingZeros(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint ) || typeof(T) == typeof(nint  )) { return H.Reinterpret<nuint , T>(CountTrailingZeros(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>(Vector.ConvertToSingle(CountTrailingZeros(H.Reinterpret<T, uint  >(x)))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>(Vector.ConvertToDouble(CountTrailingZeros(H.Reinterpret<T, ulong >(x)))); }
        throw new NotSupportedException();
    }
    
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<byte> CountTrailingZeros(Vector<byte> x)
        => CountPopulation(OP.Subtract(OP.BitwiseAnd(x, -x), Const<byte>._1));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<ushort> CountTrailingZeros(Vector<ushort> x)
        => CountPopulation(OP.Subtract(OP.BitwiseAnd(x, -x), Const<ushort>._1));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<uint> CountTrailingZeros(Vector<uint> x)
        => CountPopulation(OP.Subtract(OP.BitwiseAnd(x, -x), Const<uint>._1));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<ulong> CountTrailingZeros(Vector<ulong> x)
        => CountPopulation(OP.Subtract(OP.BitwiseAnd(x, -x), Const<ulong>._1));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static Vector<nuint> CountTrailingZeros(Vector<nuint> x)
        => CountPopulation(OP.Subtract(OP.BitwiseAnd(x, -x), Const<nuint>._1));

#pragma warning restore format
}
