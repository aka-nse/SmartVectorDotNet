using System.Runtime.CompilerServices;

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class ScalarOp
{
#pragma warning disable format

    /// <summary>
    /// Adds two values and saturates the result if can; otherwise returns simply add.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T AddSaturate<T>(T lhs, T rhs)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);
        
        if (typeof(T) == typeof(byte  )) return from((byte  )AddSaturate(to<byte  >(lhs), to<byte  >(rhs)));
        if (typeof(T) == typeof(ushort)) return from((ushort)AddSaturate(to<ushort>(lhs), to<ushort>(rhs)));
        if (typeof(T) == typeof(uint  )) return from((uint  )AddSaturate(to<uint  >(lhs), to<uint  >(rhs)));
        if (typeof(T) == typeof(ulong )) return from((ulong )AddSaturate(to<ulong >(lhs), to<ulong >(rhs)));
        if (typeof(T) == typeof(nuint )) return from((nuint )AddSaturate(to<nuint >(lhs), to<nuint >(rhs)));
        if (typeof(T) == typeof(sbyte )) return from((sbyte )AddSaturate(to<sbyte >(lhs), to<sbyte >(rhs)));
        if (typeof(T) == typeof(short )) return from((short )AddSaturate(to<short >(lhs), to<short >(rhs)));
        if (typeof(T) == typeof(int   )) return from((int   )AddSaturate(to<int   >(lhs), to<int   >(rhs)));
        if (typeof(T) == typeof(long  )) return from((long  )AddSaturate(to<long  >(lhs), to<long  >(rhs)));
        if (typeof(T) == typeof(nint  )) return from((nint  )AddSaturate(to<nint  >(lhs), to<nint  >(rhs)));
        if (typeof(T) == typeof(float )) return from((float )AddSaturate(to<float >(lhs), to<float >(rhs)));
        if (typeof(T) == typeof(double)) return from((double)AddSaturate(to<double>(lhs), to<double>(rhs)));
        throw new NotSupportedException();
    }
    
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static byte   AddSaturate(byte   lhs, byte   rhs) => AddSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static ushort AddSaturate(ushort lhs, ushort rhs) => AddSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static uint   AddSaturate(uint   lhs, uint   rhs) => AddSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static ulong  AddSaturate(ulong  lhs, ulong  rhs) => AddSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static nuint  AddSaturate(nuint  lhs, nuint  rhs) => AddSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static sbyte  AddSaturate(sbyte  lhs, sbyte  rhs) => AddSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static short  AddSaturate(short  lhs, short  rhs) => AddSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static int    AddSaturate(int    lhs, int    rhs) => AddSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static long   AddSaturate(long   lhs, long   rhs) => AddSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static nint   AddSaturate(nint   lhs, nint   rhs) => AddSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static float  AddSaturate(float  lhs, float  rhs) => lhs + rhs;
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static double AddSaturate(double lhs, double rhs) => lhs + rhs;

    private static T AddSaturateUnsigned<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var add = Add(lhs, rhs);
        return LessThanOrEqual(rhs, Subtract(Const<T>.MaxValue, lhs))
            ? add
            : Const<T>.MaxValue;
    }

    private static T AddSaturateSigned<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var add = Add(lhs, rhs);
        if (GreaterThanOrEqual(lhs, Const<T>.Zero))
        {
            return LessThanOrEqual(rhs, Subtract(Const<T>.MaxValue, lhs))
                ? add
                : Const<T>.MaxValue;
        }
        else
        {
            return LessThanOrEqual(Subtract(Const<T>.MinValue, lhs), rhs)
                ? add
                : Const<T>.MinValue;
        }
    }


    /// <summary>
    /// Subtracts two values and saturates the result if can; otherwise returns simply subtract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T SubtractSaturate<T>(T lhs, T rhs)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);
        
        if (typeof(T) == typeof(byte  )) return from((byte  )SubtractSaturate(to<byte  >(lhs), to<byte  >(rhs)));
        if (typeof(T) == typeof(ushort)) return from((ushort)SubtractSaturate(to<ushort>(lhs), to<ushort>(rhs)));
        if (typeof(T) == typeof(uint  )) return from((uint  )SubtractSaturate(to<uint  >(lhs), to<uint  >(rhs)));
        if (typeof(T) == typeof(ulong )) return from((ulong )SubtractSaturate(to<ulong >(lhs), to<ulong >(rhs)));
        if (typeof(T) == typeof(nuint )) return from((nuint )SubtractSaturate(to<nuint >(lhs), to<nuint >(rhs)));
        if (typeof(T) == typeof(sbyte )) return from((sbyte )SubtractSaturate(to<sbyte >(lhs), to<sbyte >(rhs)));
        if (typeof(T) == typeof(short )) return from((short )SubtractSaturate(to<short >(lhs), to<short >(rhs)));
        if (typeof(T) == typeof(int   )) return from((int   )SubtractSaturate(to<int   >(lhs), to<int   >(rhs)));
        if (typeof(T) == typeof(long  )) return from((long  )SubtractSaturate(to<long  >(lhs), to<long  >(rhs)));
        if (typeof(T) == typeof(nint  )) return from((nint  )SubtractSaturate(to<nint  >(lhs), to<nint  >(rhs)));
        if (typeof(T) == typeof(float )) return from((float )SubtractSaturate(to<float >(lhs), to<float >(rhs)));
        if (typeof(T) == typeof(double)) return from((double)SubtractSaturate(to<double>(lhs), to<double>(rhs)));
        throw new NotSupportedException();
    }
    
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static byte   SubtractSaturate(byte   lhs, byte   rhs) => SubtractSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static ushort SubtractSaturate(ushort lhs, ushort rhs) => SubtractSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static uint   SubtractSaturate(uint   lhs, uint   rhs) => SubtractSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static ulong  SubtractSaturate(ulong  lhs, ulong  rhs) => SubtractSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static nuint  SubtractSaturate(nuint  lhs, nuint  rhs) => SubtractSaturateUnsigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static sbyte  SubtractSaturate(sbyte  lhs, sbyte  rhs) => SubtractSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static short  SubtractSaturate(short  lhs, short  rhs) => SubtractSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static int    SubtractSaturate(int    lhs, int    rhs) => SubtractSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static long   SubtractSaturate(long   lhs, long   rhs) => SubtractSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static nint   SubtractSaturate(nint   lhs, nint   rhs) => SubtractSaturateSigned(lhs, rhs);
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static float  SubtractSaturate(float  lhs, float  rhs) => lhs - rhs;
    /**<see cref="AddSaturate{T}"/>*/ [MethodImpl(_inlining)] public static double SubtractSaturate(double lhs, double rhs) => lhs - rhs;



    private static T SubtractSaturateUnsigned<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var sub = Subtract(lhs, rhs);
        return GreaterThanOrEqual(lhs, rhs)
            ? sub
            : Const<T>.MinValue;
    }

    private static T SubtractSaturateSigned<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var sub = Subtract(lhs, rhs);
        if (GreaterThanOrEqual(rhs, Const<T>.Zero))
        {
            return LessThanOrEqual(Add(Const<T>.MinValue, rhs), lhs)
                ? sub
                : Const<T>.MinValue;
        }
        else
        {
            return LessThanOrEqual(lhs, Add(Const<T>.MaxValue, rhs))
                ? sub
                : Const<T>.MaxValue;
        }
    }
    
#pragma warning restore format

}
