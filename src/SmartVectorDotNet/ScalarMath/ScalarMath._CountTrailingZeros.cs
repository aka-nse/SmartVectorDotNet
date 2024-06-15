using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarMath
{
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static T CountTrailingZeros<T>(T x)
        where T : unmanaged
    {
#pragma warning disable format
        if (typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>((byte  )CountTrailingZeros(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>((ushort)CountTrailingZeros(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>((uint  )CountTrailingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>((ulong )CountTrailingZeros(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>((nuint )CountTrailingZeros(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>((sbyte )CountTrailingZeros(H.Reinterpret<T, byte  >(x))); }
        if (typeof(T) == typeof(short )) { return H.Reinterpret<short , T>((short )CountTrailingZeros(H.Reinterpret<T, ushort>(x))); }
        if (typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>((int   )CountTrailingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>((long  )CountTrailingZeros(H.Reinterpret<T, ulong >(x))); }
        if (typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>((nint  )CountTrailingZeros(H.Reinterpret<T, nuint >(x))); }
        if (typeof(T) == typeof(float )) { return H.Reinterpret<float , T>((float )CountTrailingZeros(H.Reinterpret<T, uint  >(x))); }
        if (typeof(T) == typeof(double)) { return H.Reinterpret<double, T>((double)CountTrailingZeros(H.Reinterpret<T, ulong >(x))); }
#pragma warning restore format
        throw new NotSupportedException();
    }

#if NETCOREAPP3_0_OR_GREATER
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(byte x)
        => Math.Min(8, BitOperations.TrailingZeroCount(x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(ushort x)
        => Math.Min(16, BitOperations.TrailingZeroCount(x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(uint x)
        => BitOperations.TrailingZeroCount(x);

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(ulong x)
        => BitOperations.TrailingZeroCount(x);

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(nuint x)
        => BitOperations.TrailingZeroCount(x);

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

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(byte x)
        => Math.Min(8, CountTrailingZeros((uint)x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(ushort x)
        => Math.Min(16, CountTrailingZeros((uint)x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(uint x)
    {
        if(x == 0)
        {
            return 0;
        }

        var y = (uint)((int)x & -(int)x);
        return CTZ.Table32[BitHash(y)];
    }

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(ulong x)
    {
        if (x == 0)
        {
            return 0;
        }

        var y = (ulong)((long)x & -(long)x);
        return CTZ.Table64[BitHash(y)];
    }

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static int CountTrailingZeros(nuint x)
        => CountTrailingZeros((ulong)x);
#endif
}
