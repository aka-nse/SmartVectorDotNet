using GenericSpecialization;
namespace SmartVectorDotNet;

#pragma warning disable format
partial class ScalarOp
{
    #region Equals

    /// <inheritdoc cref="Equals_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Equals_default))]
    public static partial bool Equals<T>(T x, T y)
        where T : unmanaged;

    /// <summary> Compares 2 values of <typeparamref name="T"/>. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static bool Equals_default<T>(T x, T y) => throw new NotSupportedException();

    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(byte   x, byte   y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(ushort x, ushort y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(uint   x, uint   y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(ulong  x, ulong  y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(nuint  x, nuint  y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(sbyte  x, sbyte  y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(short  x, short  y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(int    x, int    y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(long   x, long   y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(nint   x, nint   y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(float  x, float  y) => x == y;
    /** <inheritdoc cref="Equals_default" /> */ public static bool Equals(double x, double y) => x == y;

    #endregion Equals

    #region Compare

    /// <inheritdoc cref="Compare_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Compare_default))]
    public static partial int Compare<T>(T x, T y)
        where T : unmanaged;

    /// <summary> Compares 2 values of <typeparamref name="T"/>. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private static int Compare_default<T>(T x, T y) => throw new NotSupportedException();

    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(byte   x, byte   y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(ushort x, ushort y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(uint   x, uint   y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(ulong  x, ulong  y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(nuint  x, nuint  y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(sbyte  x, sbyte  y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(short  x, short  y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(int    x, int    y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(long   x, long   y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(nint   x, nint   y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(float  x, float  y) => x == y ? 0 : (x < y ? -1 : +1);
    /** <inheritdoc cref="Compare_default" /> */ public static int Compare(double x, double y) => x == y ? 0 : (x < y ? -1 : +1);

    #endregion Compare


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is less than <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool LessThan<T>(T x, T y)
        where T : unmanaged
        => Compare(x, y) < 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is less than or equals to <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool LessThanOrEqual<T>(T x, T y)
        where T : unmanaged
        => Compare(x, y) <= 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is greater than <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool GreaterThan<T>(T x, T y)
        where T : unmanaged
        => Compare(x, y) > 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is greater than or equals to <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool GreaterThanOrEqual<T>(T x, T y)
        where T : unmanaged
        => Compare(x, y) >= 0;
}
#pragma warning restore format