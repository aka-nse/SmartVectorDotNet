using System;
using System.Collections.Generic;
using System.Text;
using GenericSpecialization;

namespace SmartVectorDotNet;

#pragma warning disable format
partial class ScalarOp
{
    #region Abs

    /// <inheritdoc cref="Abs_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Abs_default))]
    public static partial T Abs<T>(T x) where T : unmanaged;

    /// <summary>
    /// Returns the absolute value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    private static T Abs_default<T>(T x) where T : unmanaged => throw new NotSupportedException();

    /** <inheritdoc cref="Abs_default" /> */ public static sbyte  Abs(sbyte  x) => Math.Abs(x);
    /** <inheritdoc cref="Abs_default" /> */ public static short  Abs(short  x) => Math.Abs(x);
    /** <inheritdoc cref="Abs_default" /> */ public static int    Abs(int    x) => Math.Abs(x);
    /** <inheritdoc cref="Abs_default" /> */ public static long   Abs(long   x) => Math.Abs(x);
    /** <inheritdoc cref="Abs_default" /> */ public static float  Abs(float  x) => Math.Abs(x);
    /** <inheritdoc cref="Abs_default" /> */ public static double Abs(double x) => Math.Abs(x);

    /// <inheritdoc cref="Abs_default" />
    public static nint Abs(nint x)
#if NET6_0_OR_GREATER
        => Math.Abs(x);
#else
        => IntPtr.Size switch
        {
            4 => (nint)Math.Abs((int)x),
            8 => (nint)Math.Abs((long)x),
            _ => throw new NotSupportedException()
        };
#endif

    #endregion Abs

    #region Min

    /// <inheritdoc cref="Min_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Min_default))]
    public static partial T Min<T>(T x, T y) where T : unmanaged;

    /// <summary>
    /// Returns the smaller.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static T Min_default<T>(T x, T y) where T : unmanaged => throw new NotSupportedException();

    /** <inheritdoc cref="Min_default" /> */ public static byte   Min(byte   x, byte   y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static ushort Min(ushort x, ushort y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static uint   Min(uint   x, uint   y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static ulong  Min(ulong  x, ulong  y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static sbyte  Min(sbyte  x, sbyte  y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static short  Min(short  x, short  y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static int    Min(int    x, int    y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static long   Min(long   x, long   y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static float  Min(float  x, float  y) => Math.Min(x, y);
    /** <inheritdoc cref="Min_default" /> */ public static double Min(double x, double y) => Math.Min(x, y);
    
    /** <inheritdoc cref="Min_default" /> */
    public static nint Min(nint x, nint y)
#if NET6_0_OR_GREATER
            => Math.Min(x, y);
#else
            => IntPtr.Size switch
            {
                4 => (nint)Math.Min((int)x, (int)y),
                8 => (nint)Math.Min((long)x, (long)y),
                _ => throw new NotSupportedException()
            };
#endif

    /** <inheritdoc cref="Min_default" /> */
    public static nuint Min(nuint x, nuint y)
#if NET6_0_OR_GREATER
            => Math.Min(x, y);
#else
            => UIntPtr.Size switch
            {
                4 => (nuint)Math.Min((uint)x, (uint)y),
                8 => (nuint)Math.Min((ulong)x, (ulong)y),
                _ => throw new NotSupportedException()
            };
#endif

    #endregion

    #region Max

    /// <inheritdoc cref="Max_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Max_default))]
    public static partial T Max<T>(T x, T y) where T : unmanaged;

    /// <summary>
    /// Returns the larger.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static T Max_default<T>(T x, T y) where T : unmanaged => throw new NotSupportedException();

    /** <inheritdoc cref="Max_default" /> */ public static byte   Max(byte   x, byte   y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static ushort Max(ushort x, ushort y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static uint   Max(uint   x, uint   y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static ulong  Max(ulong  x, ulong  y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static sbyte  Max(sbyte  x, sbyte  y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static short  Max(short  x, short  y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static int    Max(int    x, int    y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static long   Max(long   x, long   y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static float  Max(float  x, float  y) => Math.Max(x, y);
    /** <inheritdoc cref="Max_default" /> */ public static double Max(double x, double y) => Math.Max(x, y);
    
    /** <inheritdoc cref="Max_default" /> */
    public static nint Max(nint x, nint y)
#if NET6_0_OR_GREATER
            => Math.Max(x, y);
#else
            => IntPtr.Size switch
            {
                4 => (nint)Math.Max((int)x, (int)y),
                8 => (nint)Math.Max((long)x, (long)y),
                _ => throw new NotSupportedException()
            };
#endif

    /** <inheritdoc cref="Max_default" /> */
    public static nuint Max(nuint x, nuint y)
#if NET6_0_OR_GREATER
            => Math.Max(x, y);
#else
            => UIntPtr.Size switch
            {
                4 => (nuint)Math.Max((uint)x, (uint)y),
                8 => (nuint)Math.Max((ulong)x, (ulong)y),
                _ => throw new NotSupportedException()
            };
#endif

    #endregion

    #region Clamp

    /// <inheritdoc cref="Clamp_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Clamp_default))]
    public static partial T Clamp<T>(T x, T min, T max) where T : unmanaged;

    /// <summary>
    /// Returns <paramref name="x"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private static T Clamp_default<T>(T x, T min, T max) where T : unmanaged => Min(Max(x, min), max);

#if NET6_0_OR_GREATER
    /** <inheritdoc cref="Clamp_default" /> */ public static byte   Clamp(byte   x, byte   min, byte   max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static ushort Clamp(ushort x, ushort min, ushort max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static uint   Clamp(uint   x, uint   min, uint   max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static ulong  Clamp(ulong  x, ulong  min, ulong  max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static sbyte  Clamp(sbyte  x, sbyte  min, sbyte  max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static short  Clamp(short  x, short  min, short  max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static int    Clamp(int    x, int    min, int    max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static long   Clamp(long   x, long   min, long   max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static float  Clamp(float  x, float  min, float  max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static double Clamp(double x, double min, double max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static nint   Clamp(nint   x, nint   min, nint   max) => Math.Clamp(x, min, max);
    /** <inheritdoc cref="Clamp_default" /> */ public static nuint  Clamp(nuint  x, nuint  min, nuint  max) => Math.Clamp(x, min, max);
#endif

#endregion Clamp
}
#pragma warning restore format
