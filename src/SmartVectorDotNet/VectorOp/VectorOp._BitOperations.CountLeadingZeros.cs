using System.Runtime.CompilerServices;
using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{

#pragma warning disable format
    
    /// <inheritdoc cref="CountLeadingZeros_default" />
    [PrimaryGeneric(nameof(CountLeadingZeros_default))]
    public static partial Vector<T> CountLeadingZeros<T>(Vector<T> x)
        where T : unmanaged;
    
    /// <summary> Counts <c>1</c> bit. </summary>
    private static Vector<T> CountLeadingZeros_default<T>(Vector<T> x) where T : unmanaged => throw new NotSupportedException();

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<byte> CountLeadingZeros(Vector<byte> x)
    {
        Vector<byte> y = x;
        y |= ShiftRightLogical(y, 1);
        y |= ShiftRightLogical(y, 2);
        y |= ShiftRightLogical(y, 4);
        return CountPopulation(~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<ushort> CountLeadingZeros(Vector<ushort> x)
    {
        Vector<ushort> y = x;
        y |= ShiftRightLogical(y, 1);
        y |= ShiftRightLogical(y, 2);
        y |= ShiftRightLogical(y, 4);
        y |= ShiftRightLogical(y, 8);
        return CountPopulation(~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<uint> CountLeadingZeros(Vector<uint> x)
    {
        Vector<uint> y = x;
        y |= ShiftRightLogical(y, 1);
        y |= ShiftRightLogical(y, 2);
        y |= ShiftRightLogical(y, 4);
        y |= ShiftRightLogical(y, 8);
        y |= ShiftRightLogical(y, 16);
        return CountPopulation(~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<ulong> CountLeadingZeros(Vector<ulong> x)
    {
        Vector<ulong> y = x;
        y |= ShiftRightLogical(y, 1);
        y |= ShiftRightLogical(y, 2);
        y |= ShiftRightLogical(y, 4);
        y |= ShiftRightLogical(y, 8);
        y |= ShiftRightLogical(y, 16);
        y |= ShiftRightLogical(y, 32);
        return CountPopulation(~y);
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<nuint> CountLeadingZeros(Vector<nuint> x)
    {
        if (H.SizeOf<nuint>() == sizeof(ulong)) { return H.BitCastV<ulong, nuint>(CountLeadingZeros(H.BitCastV<nuint, ulong>(x))); }
        if (H.SizeOf<nuint>() == sizeof(uint)) { return H.BitCastV<uint, nuint>(CountLeadingZeros(H.BitCastV<nuint, uint>(x))); }
        throw new NotSupportedException();
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<sbyte> CountLeadingZeros(Vector<sbyte> x)
        => H.BitCastV<byte, sbyte>(CountLeadingZeros(H.BitCastV<sbyte, byte>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<short> CountLeadingZeros(Vector<short> x)
        => H.BitCastV<ushort, short>(CountLeadingZeros(H.BitCastV<short, ushort>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<int> CountLeadingZeros(Vector<int> x)
        => H.BitCastV<uint, int>(CountLeadingZeros(H.BitCastV<int, uint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<long> CountLeadingZeros(Vector<long> x)
        => H.BitCastV<ulong, long>(CountLeadingZeros(H.BitCastV<long, ulong>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<nint> CountLeadingZeros(Vector<nint> x)
        => H.BitCastV<nuint, nint>(CountLeadingZeros(H.BitCastV<nint, nuint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<float> CountLeadingZeros(Vector<float> x)
        => Vector.ConvertToSingle(CountLeadingZeros(H.BitCastV<float, uint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<double> CountLeadingZeros(Vector<double> x)
        => Vector.ConvertToDouble(CountLeadingZeros(H.BitCastV<double, ulong>(x)));

#pragma warning restore format
}
