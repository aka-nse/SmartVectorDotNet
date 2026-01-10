using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class ScalarOp
{
    /// <inheritdoc cref="CountLeadingZeros_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(CountLeadingZeros_default))]
    public static partial T CountLeadingZeros<T>(T x) where T : unmanaged;

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    private static T CountLeadingZeros_default<T>(T x)
        => throw new NotSupportedException();

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static byte CountLeadingZeros(byte x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        return (byte)CountPopulation((byte)~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static ushort CountLeadingZeros(ushort x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        return (ushort)CountPopulation((ushort)~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static partial uint CountLeadingZeros(uint x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static partial ulong CountLeadingZeros(ulong x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static partial nuint CountLeadingZeros(nuint x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static sbyte CountLeadingZeros(sbyte x)
        => (sbyte)CountLeadingZeros((byte)x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static short CountLeadingZeros(short x)
        => (short)CountLeadingZeros((ushort)x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static int CountLeadingZeros(int x)
        => (int)CountLeadingZeros((uint)x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static long CountLeadingZeros(long x)
        => (long)CountLeadingZeros((ulong)x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static nint CountLeadingZeros(nint x)
        => (nint)CountLeadingZeros((nuint)x);

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static float CountLeadingZeros(float x)
        => CountLeadingZeros(H.BitCast<float, uint>(x));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static double CountLeadingZeros(double x)
        => CountLeadingZeros(H.BitCast<double, ulong>(x));

#if NETCOREAPP3_0_OR_GREATER

    public static partial uint CountLeadingZeros(uint x)
        => (uint)BitOperations.LeadingZeroCount(x);
        
    public static partial ulong CountLeadingZeros(ulong x)
        => (ulong)BitOperations.LeadingZeroCount(x);
        
    public static partial nuint CountLeadingZeros(nuint x)
        => (nuint)BitOperations.LeadingZeroCount(x);

#else

    public static partial uint CountLeadingZeros(uint x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        y |= y >> 16;
        return CountPopulation(~y);
    }

    public static partial ulong CountLeadingZeros(ulong x)
    {
        var y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        y |= y >> 16;
        y |= y >> 32;
        return CountPopulation(~y);
    }

    public static partial nuint CountLeadingZeros(nuint x)
    {
        if (H.SizeOf<nuint>() == sizeof(uint))
        {
            return CountLeadingZeros((uint)x);
        }
        if (H.SizeOf<nuint>() == sizeof(ulong))
        {
            return (nuint)CountLeadingZeros((ulong)x);
        }
        throw new NotSupportedException();
    }
#endif
}
