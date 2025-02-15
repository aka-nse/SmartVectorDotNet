using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


#pragma warning disable format
partial class ScalarOp
{
    /// <summary> Count the number of leading zero bits in a mask. </summary>
    /// <exception cref="NotSupportedException" />
    public static T CountLeadingZeros<T>(T x)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(byte  )) return from((byte  )CountLeadingZeros(to<byte  >(x)));
        if (typeof(T) == typeof(ushort)) return from((ushort)CountLeadingZeros(to<ushort>(x)));
        if (typeof(T) == typeof(uint  )) return from((uint  )CountLeadingZeros(to<uint  >(x)));
        if (typeof(T) == typeof(ulong )) return from((ulong )CountLeadingZeros(to<ulong >(x)));
        if (typeof(T) == typeof(nuint )) return from((nuint )CountLeadingZeros(to<nuint >(x)));
        if (typeof(T) == typeof(sbyte )) return from((sbyte )CountLeadingZeros(to<sbyte >(x)));
        if (typeof(T) == typeof(short )) return from((short )CountLeadingZeros(to<short >(x)));
        if (typeof(T) == typeof(int   )) return from((int   )CountLeadingZeros(to<int   >(x)));
        if (typeof(T) == typeof(long  )) return from((long  )CountLeadingZeros(to<long  >(x)));
        if (typeof(T) == typeof(nint  )) return from((nint  )CountLeadingZeros(to<nint  >(x)));
        if (typeof(T) == typeof(float )) return from((float )CountLeadingZeros(to<float >(x)));
        if (typeof(T) == typeof(double)) return from((double)CountLeadingZeros(to<double>(x)));
        throw new NotSupportedException();
    }

    /** <see cref="CountLeadingZeros{T}" /> */
    public static int CountLeadingZeros(byte x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        return CountPopulation((byte)~y);
    }

    /** <see cref="CountLeadingZeros{T}" /> */
    public static int CountLeadingZeros(ushort x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        return CountPopulation((ushort)~y);
    }

    /** <see cref="CountLeadingZeros{T}" /> */ public static partial int CountLeadingZeros(uint  x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static partial int CountLeadingZeros(ulong x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static partial int CountLeadingZeros(nuint x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(sbyte  x) => CountLeadingZeros((byte  )x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(short  x) => CountLeadingZeros((ushort)x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(int    x) => CountLeadingZeros((uint  )x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(long   x) => CountLeadingZeros((ulong )x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(nint   x) => CountLeadingZeros((nuint )x);
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(float  x) => CountLeadingZeros(H.Reinterpret<float, uint>(x));
    /** <see cref="CountLeadingZeros{T}" /> */ public static int CountLeadingZeros(double x) => CountLeadingZeros(H.Reinterpret<double, ulong>(x));


#if NETCOREAPP3_0_OR_GREATER

    public static partial int CountLeadingZeros(uint  x) => BitOperations.LeadingZeroCount(x);
    public static partial int CountLeadingZeros(ulong x) => BitOperations.LeadingZeroCount(x);
    public static partial int CountLeadingZeros(nuint x) => BitOperations.LeadingZeroCount(x);

#else

    public static partial int CountLeadingZeros(uint x)
    {
        uint y = x;
        y |= y >> 1;
        y |= y >> 2;
        y |= y >> 4;
        y |= y >> 8;
        y |= y >> 16;
        return CountPopulation(~y);
    }

    public static partial int CountLeadingZeros(ulong x)
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

    public static partial int CountLeadingZeros(nuint x)
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
#pragma warning restore format
