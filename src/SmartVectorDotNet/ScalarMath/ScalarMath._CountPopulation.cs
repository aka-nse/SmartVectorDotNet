using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarMath
{
    /// <summary> Counts <c>1</c> bit. </summary>
    public static T CountPopulation<T>(T x)
        where T : unmanaged
    {
#pragma warning disable format
        if (typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>((byte  )CountPopulation(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>((ushort)CountPopulation(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>((uint  )CountPopulation(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>((ulong )CountPopulation(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>((nuint )CountPopulation(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>((sbyte )CountPopulation(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(short )) { return H.Reinterpret<short , T>((short )CountPopulation(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>((int   )CountPopulation(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>((long  )CountPopulation(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>((nint  )CountPopulation(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>((float )CountPopulation(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>((double)CountPopulation(H.Reinterpret<T, ulong >(x))); }
#pragma warning restore format
        throw new NotSupportedException();
    }

#if NETCOREAPP3_0_OR_GREATER
    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(byte x)
        => BitOperations.PopCount(x);

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(ushort x)
        => BitOperations.PopCount(x);

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(uint x)
        => BitOperations.PopCount(x);

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(ulong x)
        => BitOperations.PopCount(x);

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(nuint x)
        => BitOperations.PopCount(x);

#else
    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(byte x)
    {
        uint z = x;
        z = (z & 0x55) + ((z >> 1) & 0x55);
        z = (z & 0x33) + ((z >> 2) & 0x33);
        z = (z & 0x0F) + ((z >> 4) & 0x0F);
        return (int)(z & 15);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(ushort x)
    {
        uint z = x;
        z = (z & 0x5555) + ((z >> 1) & 0x5555);
        z = (z & 0x3333) + ((z >> 2) & 0x3333);
        z = (z & 0x0F0F) + ((z >> 4) & 0x0F0F);
        z = (z & 0x00FF) + ((z >> 8) & 0x00FF);
        return (int)(z & 31);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(uint x)
    {
        x = (x & 0x55555555) + ((x >> 1) & 0x55555555);
        x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
        x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);
        x = (x & 0x00FF00FF) + ((x >> 8) & 0x00FF00FF);
        x = (x & 0x0000FFFF) + (x >> 16);
        return (int)(x & 63);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(ulong x)
    {
        x = (x & 0x5555555555555555) + ((x >> 1) & 0x5555555555555555);
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x & 0x0F0F0F0F0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F0F0F0F0F);
        x = (x & 0x00FF00FF00FF00FF) + ((x >> 8) & 0x00FF00FF00FF00FF);
        x = (x & 0x0000FFFF0000FFFF) + ((x >> 16) & 0x0000FFFF0000FFFF);
        x = (x & 0x00000000FFFFFFFF) + (x >> 32);
        return (int)(x & 127);
    }

    /// <summary> Counts <c>1</c> bit. </summary>
    public static int CountPopulation(nuint x)
        => CountPopulation((ulong)x);
#endif
}
