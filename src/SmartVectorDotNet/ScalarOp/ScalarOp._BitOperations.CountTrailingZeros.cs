using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarOp
{
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    /// <exception cref="NotSupportedException" />
    public static T CountTrailingZeros<T>(T x)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

#pragma warning disable format
        if (typeof(T) == typeof(byte  )) return from((byte  )CountTrailingZeros(to<byte  >(x)));
        if (typeof(T) == typeof(ushort)) return from((ushort)CountTrailingZeros(to<ushort>(x)));
        if (typeof(T) == typeof(uint  )) return from((uint  )CountTrailingZeros(to<uint  >(x)));
        if (typeof(T) == typeof(ulong )) return from((ulong )CountTrailingZeros(to<ulong >(x)));
        if (typeof(T) == typeof(nuint )) return from((nuint )CountTrailingZeros(to<nuint >(x)));
        if (typeof(T) == typeof(sbyte )) return from((sbyte )CountTrailingZeros(to<byte  >(x)));
        if (typeof(T) == typeof(short )) return from((short )CountTrailingZeros(to<ushort>(x)));
        if (typeof(T) == typeof(int   )) return from((int   )CountTrailingZeros(to<uint  >(x)));
        if (typeof(T) == typeof(long  )) return from((long  )CountTrailingZeros(to<ulong >(x)));
        if (typeof(T) == typeof(nint  )) return from((nint  )CountTrailingZeros(to<nuint >(x)));
        if (typeof(T) == typeof(float )) return from((float )CountTrailingZeros(to<uint  >(x)));
        if (typeof(T) == typeof(double)) return from((double)CountTrailingZeros(to<ulong >(x)));
#pragma warning restore format
        throw new NotSupportedException();
    }
    
    /** <see cref="CountTrailingZeros{T}" /> */ public static partial int CountTrailingZeros(byte   x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static partial int CountTrailingZeros(ushort x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static partial int CountTrailingZeros(uint   x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static partial int CountTrailingZeros(ulong  x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static partial int CountTrailingZeros(nuint  x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(sbyte  x) => CountTrailingZeros((byte  )x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(short  x) => CountTrailingZeros((ushort)x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(int    x) => CountTrailingZeros((uint  )x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(long   x) => CountTrailingZeros((ulong )x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(nint   x) => CountTrailingZeros((nuint )x);
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(float  x) => CountTrailingZeros(H.Reinterpret<float , uint >(x));
    /** <see cref="CountTrailingZeros{T}" /> */ public static int CountTrailingZeros(double x) => CountTrailingZeros(H.Reinterpret<double, ulong>(x));


#if NETCOREAPP3_0_OR_GREATER
    public static partial int CountTrailingZeros(byte   x) => Math.Min(8, BitOperations.TrailingZeroCount(x));
    public static partial int CountTrailingZeros(ushort x) => Math.Min(16, BitOperations.TrailingZeroCount(x));
    public static partial int CountTrailingZeros(uint   x) => BitOperations.TrailingZeroCount(x);
    public static partial int CountTrailingZeros(ulong  x) => BitOperations.TrailingZeroCount(x);
    public static partial int CountTrailingZeros(nuint  x) => BitOperations.TrailingZeroCount(x);
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
    public static partial int CountTrailingZeros(byte x)
        => Math.Min(8, CountTrailingZeros((uint)x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static partial int CountTrailingZeros(ushort x)
        => Math.Min(16, CountTrailingZeros((uint)x));

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static partial int CountTrailingZeros(uint x)
    {
        if(x == 0)
        {
            return 32;
        }

        var y = (uint)((int)x & -(int)x);
        return CTZ.Table32[BitHash(y)];
    }

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static partial int CountTrailingZeros(ulong x)
    {
        if (x == 0)
        {
            return 64;
        }

        var y = (ulong)((long)x & -(long)x);
        return CTZ.Table64[BitHash(y)];
    }

    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    public static partial int CountTrailingZeros(nuint x)
    {
        if (Unsafe.SizeOf<nuint>() == sizeof(uint))
        {
            return CountTrailingZeros((uint)x);
        }
        if(Unsafe.SizeOf<nuint>() == sizeof(ulong))
        {
            return CountTrailingZeros((ulong)x);
        }
        throw new NotSupportedException();
    }
#endif
}
