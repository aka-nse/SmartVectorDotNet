using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


#pragma warning disable format
partial class ScalarOp
{
    /// <summary> Counts <c>1</c> bit. </summary>
    /// <exception cref="NotSupportedException" />
    public static T CountPopulation<T>(T x)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(byte  )) return from((byte  )CountPopulation(to<byte  >(x)));
        if (typeof(T) == typeof(ushort)) return from((ushort)CountPopulation(to<ushort>(x)));
        if (typeof(T) == typeof(uint  )) return from((uint  )CountPopulation(to<uint  >(x)));
        if (typeof(T) == typeof(ulong )) return from((ulong )CountPopulation(to<ulong >(x)));
        if (typeof(T) == typeof(nuint )) return from((nuint )CountPopulation(to<nuint >(x)));
        if (typeof(T) == typeof(sbyte )) return from((sbyte )CountPopulation(to<sbyte >(x)));
        if (typeof(T) == typeof(short )) return from((short )CountPopulation(to<short >(x)));
        if (typeof(T) == typeof(int   )) return from((int   )CountPopulation(to<int   >(x)));
        if (typeof(T) == typeof(long  )) return from((long  )CountPopulation(to<long  >(x)));
        if (typeof(T) == typeof(nint  )) return from((nint  )CountPopulation(to<nint  >(x)));
        if (typeof(T) == typeof(float )) return from((float )CountPopulation(to<float >(x)));
        if (typeof(T) == typeof(double)) return from((double)CountPopulation(to<double>(x)));
        throw new NotSupportedException();
    }

    /** <see cref="CountPopulation{T}" /> */ public static partial int CountPopulation(byte   x);
    /** <see cref="CountPopulation{T}" /> */ public static partial int CountPopulation(ushort x);
    /** <see cref="CountPopulation{T}" /> */ public static partial int CountPopulation(uint   x);
    /** <see cref="CountPopulation{T}" /> */ public static partial int CountPopulation(ulong  x);
    /** <see cref="CountPopulation{T}" /> */ public static partial int CountPopulation(nuint  x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(sbyte  x) => CountPopulation((byte  )x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(short  x) => CountPopulation((ushort)x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(int    x) => CountPopulation((uint  )x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(long   x) => CountPopulation((ulong )x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(nint   x) => CountPopulation((nuint )x);
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(float  x) => CountPopulation(H.Reinterpret<float , uint >(x));
    /** <see cref="CountPopulation{T}" /> */ public static int CountPopulation(double x) => CountPopulation(H.Reinterpret<double, ulong>(x));

#if NETCOREAPP3_0_OR_GREATER
    public static partial int CountPopulation(byte   x) => BitOperations.PopCount(x);
    public static partial int CountPopulation(ushort x) => BitOperations.PopCount(x);
    public static partial int CountPopulation(uint   x) => BitOperations.PopCount(x);
    public static partial int CountPopulation(ulong  x) => BitOperations.PopCount(x);
    public static partial int CountPopulation(nuint  x) => BitOperations.PopCount(x);

#else
    public static partial int CountPopulation(byte x)
    {
        uint z = x;
        z = (z & 0x55) + ((z >> 1) & 0x55);
        z = (z & 0x33) + ((z >> 2) & 0x33);
        z = (z & 0x0F) + ((z >> 4) & 0x0F);
        return (int)(z & 15);
    }
    
    public static partial int CountPopulation(ushort x)
    {
        uint z = x;
        z = (z & 0x5555) + ((z >> 1) & 0x5555);
        z = (z & 0x3333) + ((z >> 2) & 0x3333);
        z = (z & 0x0F0F) + ((z >> 4) & 0x0F0F);
        z = (z & 0x00FF) + ((z >> 8) & 0x00FF);
        return (int)(z & 31);
    }
    
    public static partial int CountPopulation(uint x)
    {
        x = (x & 0x55555555) + ((x >> 1) & 0x55555555);
        x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
        x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);
        x = (x & 0x00FF00FF) + ((x >> 8) & 0x00FF00FF);
        x = (x & 0x0000FFFF) + (x >> 16);
        return (int)(x & 63);
    }
    
    public static partial int CountPopulation(ulong x)
    {
        x = (x & 0x5555555555555555) + ((x >> 1) & 0x5555555555555555);
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x & 0x0F0F0F0F0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F0F0F0F0F);
        x = (x & 0x00FF00FF00FF00FF) + ((x >> 8) & 0x00FF00FF00FF00FF);
        x = (x & 0x0000FFFF0000FFFF) + ((x >> 16) & 0x0000FFFF0000FFFF);
        x = (x & 0x00000000FFFFFFFF) + (x >> 32);
        return (int)(x & 127);
    }
    
    public static partial int CountPopulation(nuint x)
        => CountPopulation((ulong)x);
#endif
}
#pragma warning restore format
