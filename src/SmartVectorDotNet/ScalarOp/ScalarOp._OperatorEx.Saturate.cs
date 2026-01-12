using GenericSpecialization;

namespace SmartVectorDotNet;

partial class ScalarOp
{
    #region AddSaturate

    /// <inheritdoc cref="AddSaturate_default" />
    /// <exception cref="NotSupportedException"></exception>
    [PrimaryGeneric(nameof(AddSaturate_default))]
    public static partial T AddSaturate<T>(T lhs, T rhs)
        where T : unmanaged;

    /// <summary>
    /// Adds two values and saturates the result if can; otherwise returns simply add.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    private static T AddSaturate_default<T>(T lhs, T rhs) where T : unmanaged => Add(lhs, rhs);

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static byte AddSaturate(byte lhs, byte rhs)
        => rhs <= byte.MaxValue - lhs
        ? (byte)(lhs + rhs)
        : byte.MaxValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static ushort AddSaturate(ushort lhs, ushort rhs)
        => rhs <= ushort.MaxValue - lhs
        ? (ushort)(lhs + rhs)
        : ushort.MaxValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static uint AddSaturate(uint lhs, uint rhs)
        => rhs <= uint.MaxValue - lhs
        ? lhs + rhs
        : uint.MaxValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static ulong AddSaturate(ulong lhs, ulong rhs)
        => rhs <= ulong.MaxValue - lhs
        ? lhs + rhs
        : ulong.MaxValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static nuint AddSaturate(nuint lhs, nuint rhs)
        => rhs <= Const<nuint>.MaxValue - lhs
        ? lhs + rhs
        : Const<nuint>.MaxValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static sbyte AddSaturate(sbyte lhs, sbyte rhs)
        => rhs >= 0
        ? lhs <= sbyte.MaxValue - rhs
            ? (sbyte)(lhs + rhs)
            : sbyte.MaxValue
        : lhs >= sbyte.MinValue - rhs
            ? (sbyte)(lhs + rhs)
            : sbyte.MinValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static short AddSaturate(short lhs, short rhs)
        => rhs >= 0
        ? lhs <= short.MaxValue - rhs
            ? (short)(lhs + rhs)
            : short.MaxValue
        : lhs >= short.MinValue - rhs
            ? (short)(lhs + rhs)
            : short.MinValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static int AddSaturate(int lhs, int rhs)
        => rhs >= 0
        ? lhs <= int.MaxValue - rhs
            ? lhs + rhs
            : int.MaxValue
        : lhs >= int.MinValue - rhs
            ? lhs + rhs
            : int.MinValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static long AddSaturate(long lhs, long rhs)
        => rhs >= 0
        ? lhs <= long.MaxValue - rhs
            ? lhs + rhs
            : long.MaxValue
        : lhs >= long.MinValue - rhs
            ? lhs + rhs
            : long.MinValue;

    /** <inheritdoc cref="AddSaturate_default" /> */
    public static nint AddSaturate(nint lhs, nint rhs)
        => rhs >= 0
        ? lhs <= Const<nint>.MaxValue - rhs
            ? lhs + rhs
            : Const<nint>.MaxValue
        : lhs >= Const<nint>.MinValue - rhs
            ? lhs + rhs
            : Const<nint>.MinValue;

    #endregion AddSaturate

    #region SubtractSaturate

    /// <inheritdoc cref="SubtractSaturate_default" />
    /// <exception cref="NotSupportedException"></exception>
    [PrimaryGeneric(nameof(SubtractSaturate_default))]
    public static partial T SubtractSaturate<T>(T lhs, T rhs)
        where T : unmanaged;

    /// <summary>
    /// Subtracts two values and saturates the result if can; otherwise returns simply subtract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    private static T SubtractSaturate_default<T>(T lhs, T rhs) where T : unmanaged => Subtract(lhs, rhs);

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static byte SubtractSaturate(byte lhs, byte rhs)
        => lhs >= rhs
        ? (byte)(lhs - rhs)
        : byte.MinValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static ushort SubtractSaturate(ushort lhs, ushort rhs)
        => lhs >= rhs
        ? (ushort)(lhs - rhs)
        : ushort.MinValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static uint SubtractSaturate(uint lhs, uint rhs)
        => lhs >= rhs
        ? lhs - rhs
        : uint.MinValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static ulong SubtractSaturate(ulong lhs, ulong rhs)
        => lhs >= rhs
        ? lhs - rhs
        : ulong.MinValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static nuint SubtractSaturate(nuint lhs, nuint rhs)
        => lhs >= rhs
        ? lhs - rhs
        : Const<nuint>.MinValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static sbyte SubtractSaturate(sbyte lhs, sbyte rhs)
        => rhs >= 0
        ? sbyte.MinValue + rhs <= lhs
            ? (sbyte)(lhs - rhs)
            : sbyte.MinValue
        : lhs <= sbyte.MaxValue + rhs
            ? (sbyte)(lhs - rhs)
            : sbyte.MaxValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static short SubtractSaturate(short lhs, short rhs)
        => rhs >= 0
        ? short.MinValue + rhs <= lhs
            ? (short)(lhs - rhs)
            : short.MinValue
        : lhs <= short.MaxValue + rhs
            ? (short)(lhs - rhs)
            : short.MaxValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static int SubtractSaturate(int lhs, int rhs)
        => rhs >= 0
        ? int.MinValue + rhs <= lhs
            ? lhs - rhs
            : int.MinValue
        : lhs <= int.MaxValue + rhs
            ? lhs - rhs
            : int.MaxValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static long SubtractSaturate(long lhs, long rhs)
        => rhs >= 0
        ? long.MinValue + rhs <= lhs
            ? lhs - rhs
            : long.MinValue
        : lhs <= long.MaxValue + rhs
            ? lhs - rhs
            : long.MaxValue;

    /** <inheritdoc cref="SubtractSaturate_default" /> */
    public static nint SubtractSaturate(nint lhs, nint rhs)
        => rhs >= 0
        ? Const<nint>.MinValue + rhs <= lhs
            ? lhs - rhs
            : Const<nint>.MinValue
        : lhs <= Const<nint>.MaxValue + rhs
            ? lhs - rhs
            : Const<nint>.MaxValue;

    #endregion SubtractSaturate
}
