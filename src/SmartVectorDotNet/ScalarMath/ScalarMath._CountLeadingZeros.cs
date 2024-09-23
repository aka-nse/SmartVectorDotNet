using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarMath
{
    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static T CountLeadingZeros<T>(T x)
        where T : unmanaged
    {
#pragma warning disable format
        if (typeof(T) == typeof(byte  ) || typeof(T) == typeof(sbyte )) { return H.Reinterpret<byte  , T>((byte  )CountLeadingZeros(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort) || typeof(T) == typeof(short )) { return H.Reinterpret<ushort, T>((ushort)CountLeadingZeros(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  ) || typeof(T) == typeof(int   )) { return H.Reinterpret<uint  , T>((uint  )CountLeadingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong ) || typeof(T) == typeof(long  )) { return H.Reinterpret<ulong , T>((ulong )CountLeadingZeros(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint ) || typeof(T) == typeof(nint  )) { return H.Reinterpret<nuint , T>((nuint )CountLeadingZeros(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>((float )CountLeadingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>((double)CountLeadingZeros(H.Reinterpret<T, ulong >(x))); }
#pragma warning restore format
        throw new NotSupportedException();
    }

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(byte x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        return CountPopulation((byte)~y);
    }

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(ushort x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        return CountPopulation((ushort)~y);
    }

#if NETCOREAPP3_0_OR_GREATER

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(uint x)
        => BitOperations.LeadingZeroCount(x);

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(ulong x)
        => BitOperations.LeadingZeroCount(x);

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(nuint x)
        => BitOperations.LeadingZeroCount(x);

#else

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(uint x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        y |= y >> 16;
        return CountPopulation(~y);
    }

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(ulong x)
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

    /// <summary> Count the number of leading zero bits in a mask. </summary>
    public static int CountLeadingZeros(nuint x)
    {
        if (Unsafe.SizeOf<nuint>() == sizeof(uint))
        {
            return CountLeadingZeros((uint)x);
        }
        if (Unsafe.SizeOf<nuint>() == sizeof(ulong))
        {
            return CountLeadingZeros((ulong)x);
        }
        throw new NotSupportedException();
    }
#endif
}
