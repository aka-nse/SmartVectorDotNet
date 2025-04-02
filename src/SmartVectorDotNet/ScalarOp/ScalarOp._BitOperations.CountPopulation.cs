using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarOp
{
    /// <summary> Counts <c>1</c> bit. </summary>
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(CountPopulation_default))]
    public static partial T CountPopulation<T>(T x)
        where T : unmanaged;

    /// <summary> Counts <c>1</c> bit. </summary>
    private static T CountPopulation_default<T>(T x)
        => throw new NotSupportedException();

    /// <inheritdoc cref="CountPopulation_default" />
    public static partial byte CountPopulation(byte x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static partial ushort CountPopulation(ushort x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static partial uint CountPopulation(uint x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static partial ulong CountPopulation(ulong x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static partial nuint CountPopulation(nuint x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static sbyte CountPopulation(sbyte x)
        => (sbyte)CountPopulation((byte)x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static short CountPopulation(short x)
        => (short)CountPopulation((ushort)x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static int CountPopulation(int x)
        => (int)CountPopulation((uint)x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static long CountPopulation(long x)
        => (long)CountPopulation((ulong)x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static nint CountPopulation(nint x)
        => (nint)CountPopulation((nuint)x);

    /// <inheritdoc cref="CountPopulation_default" />
    public static float CountPopulation(float x)
        => CountPopulation(H.Reinterpret<float, uint>(x));

    /// <inheritdoc cref="CountPopulation_default" />
    public static double CountPopulation(double x)
        => CountPopulation(H.Reinterpret<double, ulong>(x));

#if NETCOREAPP3_0_OR_GREATER
    public static partial byte CountPopulation(byte x)
        => (byte)BitOperations.PopCount(x);

    public static partial ushort CountPopulation(ushort x)
        => (ushort)BitOperations.PopCount(x);

    public static partial uint CountPopulation(uint x)
        => (uint)BitOperations.PopCount(x);

    public static partial ulong CountPopulation(ulong x)
        => (ulong)BitOperations.PopCount(x);

    public static partial nuint CountPopulation(nuint x)
        => (nuint)BitOperations.PopCount(x);

#else
    public static partial byte CountPopulation(byte x)
    {
        uint z = x;
        z = (z & 0x55) + ((z >> 1) & 0x55);
        z = (z & 0x33) + ((z >> 2) & 0x33);
        z = (z & 0x0F) + ((z >> 4) & 0x0F);
        return (byte)(z & 15);
    }
    
    public static partial ushort CountPopulation(ushort x)
    {
        uint z = x;
        z = (z & 0x5555) + ((z >> 1) & 0x5555);
        z = (z & 0x3333) + ((z >> 2) & 0x3333);
        z = (z & 0x0F0F) + ((z >> 4) & 0x0F0F);
        z = (z & 0x00FF) + ((z >> 8) & 0x00FF);
        return (ushort)(z & 31);
    }
    
    public static partial uint CountPopulation(uint x)
    {
        x = (x & 0x55555555) + ((x >> 1) & 0x55555555);
        x = (x & 0x33333333) + ((x >> 2) & 0x33333333);
        x = (x & 0x0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F);
        x = (x & 0x00FF00FF) + ((x >> 8) & 0x00FF00FF);
        x = (x & 0x0000FFFF) + (x >> 16);
        return x & 63;
    }
    
    public static partial ulong CountPopulation(ulong x)
    {
        x = (x & 0x5555555555555555) + ((x >> 1) & 0x5555555555555555);
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x & 0x0F0F0F0F0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F0F0F0F0F);
        x = (x & 0x00FF00FF00FF00FF) + ((x >> 8) & 0x00FF00FF00FF00FF);
        x = (x & 0x0000FFFF0000FFFF) + ((x >> 16) & 0x0000FFFF0000FFFF);
        x = (x & 0x00000000FFFFFFFF) + (x >> 32);
        return x & 127;
    }
    
    public static partial nuint CountPopulation(nuint x)
        => (nuint)CountPopulation((ulong)x);
#endif
}
