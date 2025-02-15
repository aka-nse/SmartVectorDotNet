using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class ScalarOp
{
    /// <summary>
    /// Calculates <c>Floor(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T DivideFloor<T>(T a, T b)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
        {
            return H.Reinterpret<float, T>(Floor(H.Reinterpret<T, float>(a) / H.Reinterpret<T, float>(b)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.Reinterpret<double, T>(Floor(H.Reinterpret<T, double>(a) / H.Reinterpret<T, double>(b)));
        }
        if (typeof(T) == typeof(byte)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(nuint))
        {
            return Divide(a, b);
        }
        if (typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            a = Multiply(Sign(b), a);
            b = Multiply(Sign(b), b);
            return GreaterThanOrEqual(a, Const<T>.Zero)
                ? Divide(a, b)
                : Subtract(Divide(Add(a, Const<T>.One), b), Const<T>.One);
        }
        throw new NotSupportedException();
    }


    /// <summary>
    /// Calculates <c>Ceiling(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T DivideCeiling<T>(T a, T b)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
        {
            return H.Reinterpret<float, T>(Ceiling(H.Reinterpret<T, float>(a) / H.Reinterpret<T, float>(b)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.Reinterpret<double, T>(Ceiling(H.Reinterpret<T, double>(a) / H.Reinterpret<T, double>(b)));
        }
        if (typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            a = Multiply(Sign(b), a);
            b = Multiply(Sign(b), b);
        }
        if (typeof(T) == typeof(byte)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(nuint)
            || typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            return GreaterThan(a, Const<T>.Zero)
                ? Add(Divide(Subtract(a, Const<T>.One), b), Const<T>.One)
                : Divide(a, b);
        }
        throw new NotSupportedException();
    }


    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="reminder"/> will be same with <c>a</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T DivRem<T>(T a, T b, out T reminder)
        where T : unmanaged
    {
        Unsafe.SkipInit(out reminder);
#pragma warning disable format
        if(typeof(T) == typeof(int   )) return H.Reinterpret<int   , T>(Math.DivRem(H.Reinterpret<T, int   >(a), H.Reinterpret<T, int   >(b), out H.ReinterpretMutable<T, int   >(ref reminder)));
        if(typeof(T) == typeof(long  )) return H.Reinterpret<long  , T>(Math.DivRem(H.Reinterpret<T, long  >(a), H.Reinterpret<T, long  >(b), out H.ReinterpretMutable<T, long  >(ref reminder)));
#if NET6_0_OR_GREATER
        if(typeof(T) == typeof(byte  )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, byte  >(a), H.Reinterpret<T, byte  >(b)); reminder = H.Reinterpret<byte  , T>(rem); return H.Reinterpret<byte  , T>(div); }
        if(typeof(T) == typeof(ushort)) { var (div, rem) = Math.DivRem(H.Reinterpret<T, ushort>(a), H.Reinterpret<T, ushort>(b)); reminder = H.Reinterpret<ushort, T>(rem); return H.Reinterpret<ushort, T>(div); }
        if(typeof(T) == typeof(uint  )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, uint  >(a), H.Reinterpret<T, uint  >(b)); reminder = H.Reinterpret<uint  , T>(rem); return H.Reinterpret<uint  , T>(div); }
        if(typeof(T) == typeof(ulong )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, ulong >(a), H.Reinterpret<T, ulong >(b)); reminder = H.Reinterpret<ulong , T>(rem); return H.Reinterpret<ulong , T>(div); }
        if(typeof(T) == typeof(sbyte )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, sbyte >(a), H.Reinterpret<T, sbyte >(b)); reminder = H.Reinterpret<sbyte , T>(rem); return H.Reinterpret<sbyte , T>(div); }
        if(typeof(T) == typeof(short )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, short >(a), H.Reinterpret<T, short >(b)); reminder = H.Reinterpret<short , T>(rem); return H.Reinterpret<short , T>(div); }
        if(typeof(T) == typeof(nint  )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, nint  >(a), H.Reinterpret<T, nint  >(b)); reminder = H.Reinterpret<nint  , T>(rem); return H.Reinterpret<nint  , T>(div); }
        if(typeof(T) == typeof(nuint )) { var (div, rem) = Math.DivRem(H.Reinterpret<T, nuint >(a), H.Reinterpret<T, nuint >(b)); reminder = H.Reinterpret<nuint , T>(rem); return H.Reinterpret<nuint , T>(div); }
#else
        if(typeof(T) == typeof(byte  )
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint  )
            || typeof(T) == typeof(ulong )
            || typeof(T) == typeof(sbyte )
            || typeof(T) == typeof(short ))
        {
            var div = Divide(a, b);
            reminder = Subtract(a, Multiply(div, b));
            return div;
        }
#endif
#pragma warning restore format
        {
            var div = Truncate(Divide(a, b));
            reminder = Subtract(a, Multiply(div, b));
            return div;
        }
    }

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="reminder"/> will be same with <c>b</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T DivRemByFloor<T>(T a, T b, out T reminder)
        where T : unmanaged
    {
        var div = DivideFloor(a, b);
        reminder = Subtract(a, Multiply(div, b));
        return div;
    }

    /// <summary>
    /// Calculates <c>a % b</c>so that its sign will be same with <c>b</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T ModuloByFloor<T>(T a, T b)
        where T : unmanaged
    {
        DivRemByFloor(a, b, out var reminder);
        return reminder;
    }
}
