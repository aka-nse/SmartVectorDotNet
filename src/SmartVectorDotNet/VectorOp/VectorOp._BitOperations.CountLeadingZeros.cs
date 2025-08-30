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
        if (Unsafe.SizeOf<nuint>() == sizeof(ulong)) { return H.Reinterpret<ulong, nuint>(CountLeadingZeros(H.Reinterpret<nuint, ulong>(x))); }
        if (Unsafe.SizeOf<nuint>() == sizeof(uint)) { return H.Reinterpret<uint, nuint>(CountLeadingZeros(H.Reinterpret<nuint, uint>(x))); }
        throw new NotSupportedException();
    }

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<sbyte> CountLeadingZeros(Vector<sbyte> x)
        => H.Reinterpret<byte, sbyte>(CountLeadingZeros(H.Reinterpret<sbyte, byte>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<short> CountLeadingZeros(Vector<short> x)
        => H.Reinterpret<ushort, short>(CountLeadingZeros(H.Reinterpret<short, ushort>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<int> CountLeadingZeros(Vector<int> x)
        => H.Reinterpret<uint, int>(CountLeadingZeros(H.Reinterpret<int, uint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<long> CountLeadingZeros(Vector<long> x)
        => H.Reinterpret<ulong, long>(CountLeadingZeros(H.Reinterpret<long, ulong>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<nint> CountLeadingZeros(Vector<nint> x)
        => H.Reinterpret<nuint, nint>(CountLeadingZeros(H.Reinterpret<nint, nuint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<float> CountLeadingZeros(Vector<float> x)
        => Vector.ConvertToSingle(CountLeadingZeros(H.Reinterpret<float, uint>(x)));

    /// <inheritdoc cref="CountLeadingZeros_default" />
    public static Vector<double> CountLeadingZeros(Vector<double> x)
        => Vector.ConvertToDouble(CountLeadingZeros(H.Reinterpret<double, ulong>(x)));

#pragma warning restore format
}
