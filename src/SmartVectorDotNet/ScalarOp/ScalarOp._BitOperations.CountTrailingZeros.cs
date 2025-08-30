using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarOp
{
    /// <inheritdoc cref="CountTrailingZeros_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(CountTrailingZeros_default))]
    public static partial T CountTrailingZeros<T>(T x)
        where T : unmanaged;

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    private static T CountTrailingZeros_default<T>(T x) => throw new NotSupportedException();

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static partial byte CountTrailingZeros(byte x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static partial ushort CountTrailingZeros(ushort x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static partial uint CountTrailingZeros(uint x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static partial ulong CountTrailingZeros(ulong x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static partial nuint CountTrailingZeros(nuint x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static sbyte CountTrailingZeros(sbyte x) => (sbyte)CountTrailingZeros((byte)x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static short CountTrailingZeros(short x) => (short)CountTrailingZeros((ushort)x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static int CountTrailingZeros(int x) => (int)CountTrailingZeros((uint)x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static long CountTrailingZeros(long x) => (long)CountTrailingZeros((ulong)x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static nint CountTrailingZeros(nint x) => (nint)CountTrailingZeros((nuint)x);

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static float CountTrailingZeros(float x) => CountTrailingZeros(H.Reinterpret<float, uint>(x));

    /// <inheritdoc cref="CountTrailingZeros_default" />
    public static double CountTrailingZeros(double x) => CountTrailingZeros(H.Reinterpret<double, ulong>(x));

#if NETCOREAPP3_0_OR_GREATER
    public static partial byte CountTrailingZeros(byte x)
        => (byte)Math.Min(8, BitOperations.TrailingZeroCount(x));

    public static partial ushort CountTrailingZeros(ushort x)
        => (ushort)Math.Min(16, BitOperations.TrailingZeroCount(x));

    public static partial uint CountTrailingZeros(uint x)
        => (uint)BitOperations.TrailingZeroCount(x);

    public static partial ulong CountTrailingZeros(ulong x)
        => (ulong)BitOperations.TrailingZeroCount(x);

    public static partial nuint CountTrailingZeros(nuint x)
        => (nuint)BitOperations.TrailingZeroCount(x);

#else

    private class CTZ : Const
    {
        private static readonly int[] _table32;
        private static readonly int[] _table64;
        public static ReadOnlySpan<int> Table32 => _table32;
        public static ReadOnlySpan<int> Table64 => _table64;

        static CTZ()
        {
            _table32 = new int[32];
            for(var i = 0; i < 32; ++i)
            {
                _table32[BitHash(1u << i)] = i;
            }

            _table64 = new int[64];
            for (var i = 0; i < 64; ++i)
            {
                _table64[BitHash(1uL << i)] = i;
            }
        }
    }

    public static partial byte CountTrailingZeros(byte x)
        => (byte)Math.Min(8, CountTrailingZeros((uint)x));

    public static partial ushort CountTrailingZeros(ushort x)
        => (ushort)Math.Min(16, CountTrailingZeros((uint)x));

    public static partial uint CountTrailingZeros(uint x)
    {
        if(x == 0)
        {
            return 32;
        }

        var y = (uint)((int)x & -(int)x);
        return (uint)CTZ.Table32[BitHash(y)];
    }

    public static partial ulong CountTrailingZeros(ulong x)
    {
        if (x == 0)
        {
            return 64;
        }

        var y = (ulong)((long)x & -(long)x);
        return (ulong)CTZ.Table64[BitHash(y)];
    }

    public static partial nuint CountTrailingZeros(nuint x)
    {
        if (Unsafe.SizeOf<nuint>() == sizeof(uint))
        {
            return CountTrailingZeros((uint)x);
        }
        if(Unsafe.SizeOf<nuint>() == sizeof(ulong))
        {
            return (nuint)CountTrailingZeros((ulong)x);
        }
        throw new NotSupportedException();
    }
#endif
}
