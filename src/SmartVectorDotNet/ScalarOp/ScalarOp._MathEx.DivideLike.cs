using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;
using H = InternalHelpers;

#pragma warning disable format
partial class ScalarOp
{
    #region DivideFloor

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    /// <typeparam name="T"></typeparam>
    /// <exception cref="NotSupportedException" />
    public static T DivideFloor<T>(T a, T b)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(byte  )) return from(DivideFloor(to<byte  >(a), to<byte  >(b)));
        if (typeof(T) == typeof(ushort)) return from(DivideFloor(to<ushort>(a), to<ushort>(b)));
        if (typeof(T) == typeof(uint  )) return from(DivideFloor(to<uint  >(a), to<uint  >(b)));
        if (typeof(T) == typeof(ulong )) return from(DivideFloor(to<ulong >(a), to<ulong >(b)));
        if (typeof(T) == typeof(nuint )) return from(DivideFloor(to<nuint >(a), to<nuint >(b)));
        if (typeof(T) == typeof(sbyte )) return from(DivideFloor(to<sbyte >(a), to<sbyte >(b)));
        if (typeof(T) == typeof(short )) return from(DivideFloor(to<short >(a), to<short >(b)));
        if (typeof(T) == typeof(int   )) return from(DivideFloor(to<int   >(a), to<int   >(b)));
        if (typeof(T) == typeof(long  )) return from(DivideFloor(to<long  >(a), to<long  >(b)));
        if (typeof(T) == typeof(nint  )) return from(DivideFloor(to<nint  >(a), to<nint  >(b)));
        if (typeof(T) == typeof(float )) return from(DivideFloor(to<float >(a), to<float >(b)));
        if (typeof(T) == typeof(double)) return from(DivideFloor(to<double>(a), to<double>(b)));
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates <c>Floor(a / b)</c>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [MethodImpl(_inlining)]
    public static byte DivideFloor(byte a, byte b) => (byte)(a / b);

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static ushort DivideFloor(ushort a, ushort b) => (ushort)(a / b);

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static uint DivideFloor(uint a, uint b) => a / b;

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static ulong DivideFloor(ulong a, ulong b) => a / b;

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static nuint DivideFloor(nuint a, nuint b) => a / b;

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static sbyte DivideFloor(sbyte a, sbyte b)
    {
        a = (sbyte)(Sign(b) * a);
        b = (sbyte)(Sign(b) * b);
        return a >= 0
            ? (sbyte)(a / b)
            : (sbyte)((a + 1) / b - 1);
    }

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static short DivideFloor(short a, short b)
    {
        a = (short)(Sign(b) * a);
        b = (short)(Sign(b) * b);
        return a >= 0
            ? (short)(a / b)
            : (short)((a + 1) / b - 1);
    }

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static int DivideFloor(int a, int b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a >= 0
            ? (a / b)
            : ((a + 1) / b - 1);
    }

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static long DivideFloor(long a, long b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a >= 0
            ? (a / b)
            : ((a + 1) / b - 1);
    }

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static nint DivideFloor(nint a, nint b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a >= 0
            ? (a / b)
            : ((a + 1) / b - 1);
    }

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static float DivideFloor(float a, float b) => Floor(a / b);

    /// <inheritdoc cref="DivideFloor(byte, byte)" />
    [MethodImpl(_inlining)]
    public static double DivideFloor(double a, double b) => Floor(a / b);

    #endregion DivideFloor

    #region DivideCeiling
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    /// <typeparam name="T"></typeparam>
    /// <exception cref="NotSupportedException" />
    public static T DivideCeiling<T>(T a, T b)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(byte  )) return from(DivideCeiling(to<byte  >(a), to<byte  >(b)));
        if (typeof(T) == typeof(ushort)) return from(DivideCeiling(to<ushort>(a), to<ushort>(b)));
        if (typeof(T) == typeof(uint  )) return from(DivideCeiling(to<uint  >(a), to<uint  >(b)));
        if (typeof(T) == typeof(ulong )) return from(DivideCeiling(to<ulong >(a), to<ulong >(b)));
        if (typeof(T) == typeof(nuint )) return from(DivideCeiling(to<nuint >(a), to<nuint >(b)));
        if (typeof(T) == typeof(sbyte )) return from(DivideCeiling(to<sbyte >(a), to<sbyte >(b)));
        if (typeof(T) == typeof(short )) return from(DivideCeiling(to<short >(a), to<short >(b)));
        if (typeof(T) == typeof(int   )) return from(DivideCeiling(to<int   >(a), to<int   >(b)));
        if (typeof(T) == typeof(long  )) return from(DivideCeiling(to<long  >(a), to<long  >(b)));
        if (typeof(T) == typeof(nint  )) return from(DivideCeiling(to<nint  >(a), to<nint  >(b)));
        if (typeof(T) == typeof(float )) return from(DivideCeiling(to<float >(a), to<float >(b)));
        if (typeof(T) == typeof(double)) return from(DivideCeiling(to<double>(a), to<double>(b)));
        throw new NotSupportedException();
    }
    
    /// <summary>
    /// Calculates <c>Ceiling(a / b)</c>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [MethodImpl(_inlining)]
    public static byte DivideCeiling(byte a, byte b) => a > 0 ? (byte)((a - 1) / b + 1) : (byte)0;
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static ushort DivideCeiling(ushort a, ushort b) => a > 0 ? (ushort)((a - 1) / b + 1) : (ushort)0;
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static uint DivideCeiling(uint a, uint b) => a > 0 ? ((a - 1) / b + 1) : 0;
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static ulong DivideCeiling(ulong a, ulong b) => a > 0 ? ((a - 1) / b + 1) : 0;
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static nuint DivideCeiling(nuint a, nuint b) => a > 0 ? ((a - 1) / b + 1) : 0;
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static sbyte DivideCeiling(sbyte a, sbyte b)
    {
        a = (sbyte)(Sign(b) * a);
        b = (sbyte)(Sign(b) * b);
        return a > 0
            ? (sbyte)((a - 1) / b + 1)
            : (sbyte)(a / b);
    }
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static short DivideCeiling(short a, short b)
    {
        a = (short)(Sign(b) * a);
        b = (short)(Sign(b) * b);
        return a > 0
            ? (short)((a - 1) / b + 1)
            : (short)(a / b);
    }
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static int DivideCeiling(int a, int b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a > 0
            ? ((a - 1) / b + 1)
            : (a / b);
    }
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static long DivideCeiling(long a, long b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a > 0
            ? ((a - 1) / b + 1)
            : (a / b);
    }
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static nint DivideCeiling(nint a, nint b)
    {
        a = Sign(b) * a;
        b = Sign(b) * b;
        return a > 0
            ? ((a - 1) / b + 1)
            : (a / b);
    }
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static float DivideCeiling(float a, float b) => Ceiling(a / b);
    
    /// <inheritdoc cref="DivideCeiling(byte, byte)" />
    [MethodImpl(_inlining)]
    public static double DivideCeiling(double a, double b) => Ceiling(a / b);


    #endregion DivideCeiling

    #region DivRem

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    /// <typeparam name="T"></typeparam>
    /// <exception cref="NotSupportedException" />
    public static T DivRem<T>(T a, T b, out T remainder)
        where T : unmanaged
    {
        static ref TTo toOut<TTo>(ref T x) => ref H.ReinterpretMutable<T, TTo>(ref x);
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        Unsafe.SkipInit(out remainder);
        if (typeof(T) == typeof(byte  )) return from(DivRem(to<byte  >(a), to<byte  >(b), out toOut<byte  >(ref remainder)));
        if (typeof(T) == typeof(ushort)) return from(DivRem(to<ushort>(a), to<ushort>(b), out toOut<ushort>(ref remainder)));
        if (typeof(T) == typeof(uint  )) return from(DivRem(to<uint  >(a), to<uint  >(b), out toOut<uint  >(ref remainder)));
        if (typeof(T) == typeof(ulong )) return from(DivRem(to<ulong >(a), to<ulong >(b), out toOut<ulong >(ref remainder)));
        if (typeof(T) == typeof(nuint )) return from(DivRem(to<nuint >(a), to<nuint >(b), out toOut<nuint >(ref remainder)));
        if (typeof(T) == typeof(sbyte )) return from(DivRem(to<sbyte >(a), to<sbyte >(b), out toOut<sbyte >(ref remainder)));
        if (typeof(T) == typeof(short )) return from(DivRem(to<short >(a), to<short >(b), out toOut<short >(ref remainder)));
        if (typeof(T) == typeof(int   )) return from(DivRem(to<int   >(a), to<int   >(b), out toOut<int   >(ref remainder)));
        if (typeof(T) == typeof(long  )) return from(DivRem(to<long  >(a), to<long  >(b), out toOut<long  >(ref remainder)));
        if (typeof(T) == typeof(nint  )) return from(DivRem(to<nint  >(a), to<nint  >(b), out toOut<nint  >(ref remainder)));
        if (typeof(T) == typeof(float )) return from(DivRem(to<float >(a), to<float >(b), out toOut<float >(ref remainder)));
        if (typeof(T) == typeof(double)) return from(DivRem(to<double>(a), to<double>(b), out toOut<double>(ref remainder)));
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="remainder"/> will be same with <c>a</c>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    [MethodImpl(_inlining)]
    public static partial byte DivRem(byte a, byte b, out byte remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial ushort DivRem(ushort a, ushort b, out ushort remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial uint DivRem(uint a, uint b, out uint remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial ulong DivRem(ulong a, ulong b, out ulong remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial nuint DivRem(nuint a, nuint b, out nuint remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial sbyte DivRem(sbyte a, sbyte b, out sbyte remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial short DivRem(short a, short b, out short remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static int DivRem(int a, int b, out int remainder) => Math.DivRem(a, b, out remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static long DivRem(long a, long b, out long remainder) => Math.DivRem(a, b, out remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static partial nint DivRem(nint a, nint b, out nint remainder);

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static float DivRem(float a, float b, out float remainder)
    {
        var div = Truncate(Divide(a, b));
        remainder = Subtract(a, Multiply(div, b));
        return div;
    }

    /// <inheritdoc cref="DivRem(byte, byte, out byte)" />
    [MethodImpl(_inlining)]
    public static double DivRem(double a, double b, out double remainder)
    {
        var div = Truncate(Divide(a, b));
        remainder = Subtract(a, Multiply(div, b));
        return div;
    }

#if NET6_0_OR_GREATER
    public static partial byte   DivRem(byte   a, byte   b, out byte   remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial ushort DivRem(ushort a, ushort b, out ushort remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial uint   DivRem(uint   a, uint   b, out uint   remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial ulong  DivRem(ulong  a, ulong  b, out ulong  remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial nuint  DivRem(nuint  a, nuint  b, out nuint  remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial sbyte  DivRem(sbyte  a, sbyte  b, out sbyte  remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial short  DivRem(short  a, short  b, out short  remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
    public static partial nint   DivRem(nint   a, nint   b, out nint   remainder) => ((_, remainder) = Math.DivRem(a, b)).Item1;
#else
    public static partial byte   DivRem(byte   a, byte   b, out byte   remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial ushort DivRem(ushort a, ushort b, out ushort remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial uint   DivRem(uint   a, uint   b, out uint   remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial ulong  DivRem(ulong  a, ulong  b, out ulong  remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial nuint  DivRem(nuint  a, nuint  b, out nuint  remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial sbyte  DivRem(sbyte  a, sbyte  b, out sbyte  remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial short  DivRem(short  a, short  b, out short  remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    public static partial nint   DivRem(nint   a, nint   b, out nint   remainder) { var div = Divide(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
#endif

    #endregion DivRem

    #region DivRemByFloor

    /// <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" />
    /// <typeparam name="T"></typeparam>
    /// <exception cref="NotSupportedException" />
    public static T DivRemByFloor<T>(T a, T b, out T remainder)
        where T : unmanaged
    {
        static ref TTo toOut<TTo>(ref T x) => ref H.ReinterpretMutable<T, TTo>(ref x);
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        Unsafe.SkipInit(out remainder);
        if (typeof(T) == typeof(byte  )) return from(DivRemByFloor(to<byte  >(a), to<byte  >(b), out toOut<byte  >(ref remainder)));
        if (typeof(T) == typeof(ushort)) return from(DivRemByFloor(to<ushort>(a), to<ushort>(b), out toOut<ushort>(ref remainder)));
        if (typeof(T) == typeof(uint  )) return from(DivRemByFloor(to<uint  >(a), to<uint  >(b), out toOut<uint  >(ref remainder)));
        if (typeof(T) == typeof(ulong )) return from(DivRemByFloor(to<ulong >(a), to<ulong >(b), out toOut<ulong >(ref remainder)));
        if (typeof(T) == typeof(nuint )) return from(DivRemByFloor(to<nuint >(a), to<nuint >(b), out toOut<nuint >(ref remainder)));
        if (typeof(T) == typeof(sbyte )) return from(DivRemByFloor(to<sbyte >(a), to<sbyte >(b), out toOut<sbyte >(ref remainder)));
        if (typeof(T) == typeof(short )) return from(DivRemByFloor(to<short >(a), to<short >(b), out toOut<short >(ref remainder)));
        if (typeof(T) == typeof(int   )) return from(DivRemByFloor(to<int   >(a), to<int   >(b), out toOut<int   >(ref remainder)));
        if (typeof(T) == typeof(long  )) return from(DivRemByFloor(to<long  >(a), to<long  >(b), out toOut<long  >(ref remainder)));
        if (typeof(T) == typeof(nint  )) return from(DivRemByFloor(to<nint  >(a), to<nint  >(b), out toOut<nint  >(ref remainder)));
        if (typeof(T) == typeof(float )) return from(DivRemByFloor(to<float >(a), to<float >(b), out toOut<float >(ref remainder)));
        if (typeof(T) == typeof(double)) return from(DivRemByFloor(to<double>(a), to<double>(b), out toOut<double>(ref remainder)));
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="remainder"/> will be same with <c>b</c>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="remainder"></param>
    /// <returns></returns>
    [MethodImpl(_inlining)]
    public static byte DivRemByFloor(byte a, byte b, out byte remainder)
    {
        var div = DivideFloor(a, b);
        remainder = Subtract(a, Multiply(div, b));
        return div;
    }
    
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static ushort DivRemByFloor(ushort a, ushort b, out ushort remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static uint   DivRemByFloor(uint   a, uint   b, out uint   remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static ulong  DivRemByFloor(ulong  a, ulong  b, out ulong  remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static nuint  DivRemByFloor(nuint  a, nuint  b, out nuint  remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static sbyte  DivRemByFloor(sbyte  a, sbyte  b, out sbyte  remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static short  DivRemByFloor(short  a, short  b, out short  remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static int    DivRemByFloor(int    a, int    b, out int    remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static long   DivRemByFloor(long   a, long   b, out long   remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static nint   DivRemByFloor(nint   a, nint   b, out nint   remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static float  DivRemByFloor(float  a, float  b, out float  remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }
    /** <inheritdoc cref="DivRemByFloor(byte, byte, out byte)" /> */ [MethodImpl(_inlining)] public static double DivRemByFloor(double a, double b, out double remainder) { var div = DivideFloor(a, b); remainder = Subtract(a, Multiply(div, b)); return div; }

    #endregion DivRemByFloor

    #region ModuloByFloor

    /// <inheritdoc cref="ModuloByFloor(byte, byte)" />
    /// <typeparam name="T"></typeparam>
    /// <exception cref="NotSupportedException" />
    public static T ModuloByFloor<T>(T a, T b)
        where T : unmanaged
    {
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(byte  )) return from(ModuloByFloor(to<byte  >(a), to<byte  >(b)));
        if (typeof(T) == typeof(ushort)) return from(ModuloByFloor(to<ushort>(a), to<ushort>(b)));
        if (typeof(T) == typeof(uint  )) return from(ModuloByFloor(to<uint  >(a), to<uint  >(b)));
        if (typeof(T) == typeof(ulong )) return from(ModuloByFloor(to<ulong >(a), to<ulong >(b)));
        if (typeof(T) == typeof(nuint )) return from(ModuloByFloor(to<nuint >(a), to<nuint >(b)));
        if (typeof(T) == typeof(sbyte )) return from(ModuloByFloor(to<sbyte >(a), to<sbyte >(b)));
        if (typeof(T) == typeof(short )) return from(ModuloByFloor(to<short >(a), to<short >(b)));
        if (typeof(T) == typeof(int   )) return from(ModuloByFloor(to<int   >(a), to<int   >(b)));
        if (typeof(T) == typeof(long  )) return from(ModuloByFloor(to<long  >(a), to<long  >(b)));
        if (typeof(T) == typeof(nint  )) return from(ModuloByFloor(to<nint  >(a), to<nint  >(b)));
        if (typeof(T) == typeof(float )) return from(ModuloByFloor(to<float >(a), to<float >(b)));
        if (typeof(T) == typeof(double)) return from(ModuloByFloor(to<double>(a), to<double>(b)));
        throw new NotSupportedException();
    }
    
    /// <summary>
    /// Calculates <c>a % b</c>so that its sign will be same with <c>b</c>.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [MethodImpl(_inlining)]
    public static byte ModuloByFloor(byte a, byte b)
    {
        DivRemByFloor(a, b, out var remainder);
        return remainder;
    }
    
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static ushort ModuloByFloor(ushort a, ushort b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static uint   ModuloByFloor(uint   a, uint   b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static ulong  ModuloByFloor(ulong  a, ulong  b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static nuint  ModuloByFloor(nuint  a, nuint  b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static sbyte  ModuloByFloor(sbyte  a, sbyte  b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static short  ModuloByFloor(short  a, short  b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static int    ModuloByFloor(int    a, int    b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static long   ModuloByFloor(long   a, long   b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static nint   ModuloByFloor(nint   a, nint   b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static float  ModuloByFloor(float  a, float  b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    /** <inheritdoc cref="ModuloByFloor(byte, byte)" /> */ [MethodImpl(_inlining)] public static double ModuloByFloor(double a, double b) { DivRemByFloor(a, b, out var remainder); return remainder; }
    
    #endregion
}
#pragma warning restore format
