using GenericSpecialization;
namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class VectorOp
{

#pragma warning disable format
    
    /// <inheritdoc cref="CountTrailingZeros_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(CountTrailingZeros_default))]
    public static partial Vector<T> CountTrailingZeros<T>(Vector<T> x)
        where T : unmanaged;
    
    /// <summary> Count the number of trailing zero bits in a mask. </summary>
    private static Vector<T> CountTrailingZeros_default<T>(Vector<T> x)
        where T : unmanaged
        => throw new NotSupportedException();

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<byte> CountTrailingZeros(Vector<byte> x)
        => CountPopulation(Subtract(BitwiseAnd(x, -x), Const<byte>._1));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<ushort> CountTrailingZeros(Vector<ushort> x)
        => CountPopulation(Subtract(BitwiseAnd(x, -x), Const<ushort>._1));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<uint> CountTrailingZeros(Vector<uint> x)
        => CountPopulation(Subtract(BitwiseAnd(x, -x), Const<uint>._1));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<ulong> CountTrailingZeros(Vector<ulong> x)
        => CountPopulation(Subtract(BitwiseAnd(x, -x), Const<ulong>._1));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<nuint> CountTrailingZeros(Vector<nuint> x)
        => CountPopulation(Subtract(BitwiseAnd(x, -x), Const<nuint>._1));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<sbyte> CountTrailingZeros(Vector<sbyte> x)
        => H.BitCastV<byte, sbyte>(CountTrailingZeros(H.BitCastV<sbyte, byte>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<short> CountTrailingZeros(Vector<short> x)
        => H.BitCastV<ushort, short>(CountTrailingZeros(H.BitCastV<short, ushort>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<int> CountTrailingZeros(Vector<int> x)
        => H.BitCastV<uint, int>(CountTrailingZeros(H.BitCastV<int, uint>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<long> CountTrailingZeros(Vector<long> x)
        => H.BitCastV<ulong, long>(CountTrailingZeros(H.BitCastV<long, ulong>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<nint> CountTrailingZeros(Vector<nint> x)
        => H.BitCastV<nuint, nint>(CountTrailingZeros(H.BitCastV<nint, nuint>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<float> CountTrailingZeros(Vector<float> x)
        => Vector.ConvertToSingle(CountTrailingZeros(H.BitCastV<float, uint>(x)));

    /** <inheritdoc cref="CountTrailingZeros_default" /> */
    public static Vector<double> CountTrailingZeros(Vector<double> x)
        => Vector.ConvertToDouble(CountTrailingZeros(H.BitCastV<double, ulong>(x)));

#pragma warning restore format
}
