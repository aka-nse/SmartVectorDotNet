using System.Runtime.CompilerServices;
using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;

#pragma warning disable format
partial class ScalarOp
{
    #region DivideFloor

    /// <inheritdoc cref="DivideFloor_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(DivideFloor_default))]
    public static partial T DivideFloor<T>(T a, T b)
        where T : unmanaged;

    /// <summary>
    /// Calculates <c>Floor(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static T DivideFloor_default<T>(T a, T b)
        where T : unmanaged
        => throw new NotSupportedException();

    /** <inheritdoc cref="DivideFloor_default" /> */ public static float  DivideFloor(float  a, float  b) => Floor(a / b);
    /** <inheritdoc cref="DivideFloor_default" /> */ public static double DivideFloor(double a, double b) => Floor(a / b);

    /** <inheritdoc cref="DivideFloor_default" /> */ public static byte   DivideFloor(byte   a, byte   b) => Divide(a, b);
    /** <inheritdoc cref="DivideFloor_default" /> */ public static ushort DivideFloor(ushort a, ushort b) => Divide(a, b);
    /** <inheritdoc cref="DivideFloor_default" /> */ public static uint   DivideFloor(uint   a, uint   b) => Divide(a, b);
    /** <inheritdoc cref="DivideFloor_default" /> */ public static ulong  DivideFloor(ulong  a, ulong  b) => Divide(a, b);
    /** <inheritdoc cref="DivideFloor_default" /> */ public static nuint  DivideFloor(nuint  a, nuint  b) => Divide(a, b);
    
    /** <inheritdoc cref="DivideFloor_default" /> */
    public static sbyte DivideFloor(sbyte a, sbyte b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return (sbyte)(aa >= 0 ? aa / bb : ((aa + 1) / bb - 1));
    }

    /** <inheritdoc cref="DivideFloor_default" /> */
    public static short DivideFloor(short a, short b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return (short)(aa >= 0 ? aa / bb : ((aa + 1) / bb - 1));
    }

    /** <inheritdoc cref="DivideFloor_default" /> */
    public static int DivideFloor(int a, int b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa >= 0 ? aa / bb : ((aa + 1) / bb - 1);
    }

    /** <inheritdoc cref="DivideFloor_default" /> */
    public static long DivideFloor(long a, long b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa >= 0 ? aa / bb : ((aa + 1) / bb - 1);
    }

    /** <inheritdoc cref="DivideFloor_default" /> */
    public static nint DivideFloor(nint a, nint b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa >= 0 ? aa / bb : ((aa + 1) / bb - 1);
    }

    #endregion DivideFloor

    #region DivideCeiling

    /// <inheritdoc cref="DivideCeiling_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(DivideCeiling_default))]
    public static partial T DivideCeiling<T>(T a, T b)
        where T : unmanaged;
    
    /// <summary>
    /// Calculates <c>Ceiling(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private static T DivideCeiling_default<T>(T a, T b)
        where T : unmanaged
        => throw new NotSupportedException();
    
    /** <inheritdoc cref="DivideCeiling_default" /> */ public static float  DivideCeiling(float  a, float  b) => Ceiling(a / b);
    /** <inheritdoc cref="DivideCeiling_default" /> */ public static double DivideCeiling(double a, double b) => Ceiling(a / b);

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static sbyte DivideCeiling(sbyte a, sbyte b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return (sbyte)(aa > 0 ? ((aa - 1) / bb + 1) : aa / bb);
    }

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static short DivideCeiling(short a, short b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return (short)(aa > 0 ? ((aa - 1) / bb + 1) : aa / bb);
    }

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static int DivideCeiling(int a, int b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa > 0 ? ((aa - 1) / bb + 1) : aa / bb;
    }

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static long DivideCeiling(long a, long b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa > 0 ? ((aa - 1) / bb + 1) : aa / bb;
    }

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static nint DivideCeiling(nint a, nint b)
    {
        var aa = Sign(b) * a;
        var bb = Sign(b) * b;
        return aa > 0 ? ((aa - 1) / bb + 1) : aa / bb;
    }
    
    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static byte DivideCeiling(byte a, byte b)
        => (byte)(a > 0 ? ((a - 1) / b + 1) : a / b);

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static ushort DivideCeiling(ushort a, ushort b)
        => (ushort)(a > 0 ? ((a - 1) / b + 1) : a / b);

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static uint DivideCeiling(uint a, uint b)
        => a > 0 ? ((a - 1) / b + 1) : a / b;

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static ulong DivideCeiling(ulong a, ulong b)
        => a > 0 ? ((a - 1) / b + 1) : a / b;

    /** <inheritdoc cref="DivideCeiling_default" /> */
    public static nuint DivideCeiling(nuint a, nuint b)
        => a > 0 ? ((a - 1) / b + 1) : a / b;

    #endregion DivideCeiling

    #region DivRem

    /// <inheritdoc cref="DivRem_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(DivRem_default))]
    public static partial T DivRem<T>(T a, T b, out T reminder)
        where T : unmanaged;

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="reminder"/> will be same with <c>a</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    private static T DivRem_default<T>(T a, T b, out T reminder)
        where T : unmanaged
    {
        var div = Truncate(Divide(a, b));
        reminder = Subtract(a, Multiply(div, b));
        return div;
    }

    /// <inheritdoc cref="DivRem_default" />
    public static int DivRem(int a, int b, out int reminder) => Math.DivRem(a, b, out reminder);

    /// <inheritdoc cref="DivRem_default" />
    public static long DivRem(long a, long b, out long reminder) => Math.DivRem(a, b, out reminder);

#if NET6_0_OR_GREATER
    /** <inheritdoc cref="DivRem_default" /> */ public static byte   DivRem(byte   a, byte   b, out byte   reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static ushort DivRem(ushort a, ushort b, out ushort reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static uint   DivRem(uint   a, uint   b, out uint   reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static ulong  DivRem(ulong  a, ulong  b, out ulong  reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static sbyte  DivRem(sbyte  a, sbyte  b, out sbyte  reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static short  DivRem(short  a, short  b, out short  reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static nint   DivRem(nint   a, nint   b, out nint   reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
    /** <inheritdoc cref="DivRem_default" /> */ public static nuint  DivRem(nuint  a, nuint  b, out nuint  reminder) => ((_, reminder) = Math.DivRem(a, b)).Item1;
#else
    /** <inheritdoc cref="DivRem_default" /> */ public static byte   DivRem(byte   a, byte   b, out byte   reminder) { var div = a / b; reminder = (byte  )(a - div * b); return (byte  )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static ushort DivRem(ushort a, ushort b, out ushort reminder) { var div = a / b; reminder = (ushort)(a - div * b); return (ushort)div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static uint   DivRem(uint   a, uint   b, out uint   reminder) { var div = a / b; reminder = (uint  )(a - div * b); return (uint  )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static ulong  DivRem(ulong  a, ulong  b, out ulong  reminder) { var div = a / b; reminder = (ulong )(a - div * b); return (ulong )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static nuint  DivRem(nuint  a, nuint  b, out nuint  reminder) { var div = a / b; reminder = (nuint )(a - div * b); return (nuint )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static sbyte  DivRem(sbyte  a, sbyte  b, out sbyte  reminder) { var div = a / b; reminder = (sbyte )(a - div * b); return (sbyte )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static short  DivRem(short  a, short  b, out short  reminder) { var div = a / b; reminder = (short )(a - div * b); return (short )div; }
    /** <inheritdoc cref="DivRem_default" /> */ public static nint   DivRem(nint   a, nint   b, out nint   reminder) { var div = a / b; reminder = (nint  )(a - div * b); return (nint  )div; }
#endif

    #endregion DivRem

    #region DivRemByFloor

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

    #endregion DivRemByFloor

    #region ModuloByFloor

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

    #endregion ModuloByFloor
}
#pragma warning restore format
