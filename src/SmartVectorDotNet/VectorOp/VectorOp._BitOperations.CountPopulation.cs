using System.Runtime.CompilerServices;
using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;

file class CountPopulation_
{
    public static readonly Vector<byte> UInt8LastMask = new(15);
    public static ReadOnlySpan<Vector<byte>> UInt8Masks => _uInt8Masks;
    private static readonly Vector<byte>[] _uInt8Masks
        = [
            new(0x55),
            new(0x33),
            new(0x0F),
        ];

    public static readonly Vector<ushort> UInt16LastMask = new(31);
    public static ReadOnlySpan<Vector<ushort>> UInt16Masks => _uInt16Masks;
    private static readonly Vector<ushort>[] _uInt16Masks
        = [
            new(0x5555),
            new(0x3333),
            new(0x0F0F),
            new(0x00FF),
        ];

    public static readonly Vector<uint> UInt32LastMask = new(63);
    public static ReadOnlySpan<Vector<uint>> UInt32Masks => _uInt32Masks;
    private static readonly Vector<uint>[] _uInt32Masks
        = [
            new(0x55555555),
            new(0x33333333),
            new(0x0F0F0F0F),
            new(0x00FF00FF),
            new(0x0000FFFF),
        ];

    public static readonly Vector<ulong> UInt64LastMask = new(127);
    public static ReadOnlySpan<Vector<ulong>> UInt64Masks => _uInt64Masks;
    private static readonly Vector<ulong>[] _uInt64Masks
        = [
            new(0x5555555555555555),
            new(0x3333333333333333),
            new(0x0F0F0F0F0F0F0F0F),
            new(0x00FF00FF00FF00FF),
            new(0x0000FFFF0000FFFF),
            new(0x00000000FFFFFFFF),
        ];
}

partial class VectorOp
{

#pragma warning disable format
    
    /// <inheritdoc cref="CountPopulation_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(CountPopulation_default))]
    public static partial Vector<T> CountPopulation<T>(Vector<T> x)
        where T : unmanaged;
    
    /// <summary> Counts <c>1</c> bit. </summary>
    private static Vector<T> CountPopulation_default<T>(Vector<T> x)
        where T : unmanaged
        => throw new NotSupportedException();

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<byte> CountPopulation(Vector<byte> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt8Masks[0]) + (ShiftRightLogical(y, 1) & CountPopulation_.UInt8Masks[0]);
        y = (y & CountPopulation_.UInt8Masks[1]) + (ShiftRightLogical(y, 2) & CountPopulation_.UInt8Masks[1]);
        y = (y & CountPopulation_.UInt8Masks[2]) +  ShiftRightLogical(y, 4);
        return y & CountPopulation_.UInt8LastMask;
    }

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<ushort> CountPopulation(Vector<ushort> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt16Masks[0]) + (ShiftRightLogical(y, 1) & CountPopulation_.UInt16Masks[0]);
        y = (y & CountPopulation_.UInt16Masks[1]) + (ShiftRightLogical(y, 2) & CountPopulation_.UInt16Masks[1]);
        y = (y & CountPopulation_.UInt16Masks[2]) + (ShiftRightLogical(y, 4) & CountPopulation_.UInt16Masks[2]);
        y = (y & CountPopulation_.UInt16Masks[3]) +  ShiftRightLogical(y, 8);
        return y & CountPopulation_.UInt16LastMask;
    }

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<uint> CountPopulation(Vector<uint> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt32Masks[0]) + (ShiftRightLogical(y,  1) & CountPopulation_.UInt32Masks[0]);
        y = (y & CountPopulation_.UInt32Masks[1]) + (ShiftRightLogical(y,  2) & CountPopulation_.UInt32Masks[1]);
        y = (y & CountPopulation_.UInt32Masks[2]) + (ShiftRightLogical(y,  4) & CountPopulation_.UInt32Masks[2]);
        y = (y & CountPopulation_.UInt32Masks[3]) + (ShiftRightLogical(y,  8) & CountPopulation_.UInt32Masks[3]);
        y = (y & CountPopulation_.UInt32Masks[4]) +  ShiftRightLogical(y, 16);
        return y & CountPopulation_.UInt32LastMask;
    }

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<ulong> CountPopulation(Vector<ulong> x)
    {
        var y = x;
        y = (y & CountPopulation_.UInt64Masks[0]) + (ShiftRightLogical(y,  1) & CountPopulation_.UInt64Masks[0]);
        y = (y & CountPopulation_.UInt64Masks[1]) + (ShiftRightLogical(y,  2) & CountPopulation_.UInt64Masks[1]);
        y = (y & CountPopulation_.UInt64Masks[2]) + (ShiftRightLogical(y,  4) & CountPopulation_.UInt64Masks[2]);
        y = (y & CountPopulation_.UInt64Masks[3]) + (ShiftRightLogical(y,  8) & CountPopulation_.UInt64Masks[3]);
        y = (y & CountPopulation_.UInt64Masks[4]) + (ShiftRightLogical(y, 16) & CountPopulation_.UInt64Masks[4]);
        y = (y & CountPopulation_.UInt64Masks[5]) +  ShiftRightLogical(y, 32);
        return y & CountPopulation_.UInt64LastMask;
    }

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<nuint> CountPopulation(Vector<nuint> x)
    {
        if (H.SizeOf<nuint>() == sizeof(ulong)) { return H.BitCastV<ulong, nuint>(CountPopulation(H.BitCastV<nuint, ulong>(x))); }
        if (H.SizeOf<nuint>() == sizeof(uint )) { return H.BitCastV<uint , nuint>(CountPopulation(H.BitCastV<nuint, uint >(x))); }
        throw new NotSupportedException();
    }

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<sbyte> CountPopulation(Vector<sbyte> x)
        => H.BitCastV<byte, sbyte>(CountPopulation(H.BitCastV<sbyte, byte>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<short> CountPopulation(Vector<short> x)
        => H.BitCastV<ushort, short>(CountPopulation(H.BitCastV<short, ushort>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<int> CountPopulation(Vector<int> x)
        => H.BitCastV<uint, int>(CountPopulation(H.BitCastV<int, uint>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<long> CountPopulation(Vector<long> x)
        => H.BitCastV<ulong, long>(CountPopulation(H.BitCastV<long, ulong>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<nint> CountPopulation(Vector<nint> x)
        => H.BitCastV<nuint, nint>(CountPopulation(H.BitCastV<nint, nuint>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<float> CountPopulation(Vector<float> x)
        => Vector.ConvertToSingle(CountPopulation(H.BitCastV<float, uint>(x)));

    /** <inheritdoc cref="CountPopulation_default" /> */
    public static Vector<double> CountPopulation(Vector<double> x)
        => Vector.ConvertToDouble(CountPopulation(H.BitCastV<double, ulong>(x)));


#pragma warning restore format
}
