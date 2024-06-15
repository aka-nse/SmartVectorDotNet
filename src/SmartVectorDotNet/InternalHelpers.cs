using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

namespace SmartVectorDotNet;

internal static class InternalHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> CreateVector<T>(Span<T> elements)
        where T : unmanaged
        => CreateVector((ReadOnlySpan<T>)elements);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> CreateVector<T>(ReadOnlySpan<T> elements)
        where T : unmanaged
    {
        return MemoryMarshal.Cast<T, Vector<T>>(elements)[0];
    }

#if NET6_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> CreateVector128<T>(Span<T> elements)
        where T : unmanaged
        => CreateVector128((ReadOnlySpan<T>)elements);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> CreateVector128<T>(ReadOnlySpan<T> elements)
        where T : unmanaged
    {
        return MemoryMarshal.Cast<T, Vector128<T>>(elements)[0];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> CreateVector256<T>(Span<T> elements)
        where T : unmanaged
        => CreateVector256((ReadOnlySpan<T>)elements);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> CreateVector256<T>(ReadOnlySpan<T> elements)
        where T : unmanaged
    {
        return MemoryMarshal.Cast<T, Vector256<T>>(elements)[0];
    }

#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly TTo Reinterpret<TFrom, TTo>(in TFrom x)
        => ref Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(x));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<TTo> Reinterpret<TFrom, TTo>(in Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(x));

#if NET6_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector128<TTo> Reinterpret<TFrom, TTo>(in Vector128<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector128<TFrom>, Vector128<TTo>>(ref Unsafe.AsRef(x));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector256<TTo> Reinterpret<TFrom, TTo>(in Vector256<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector256<TFrom>, Vector256<TTo>>(ref Unsafe.AsRef(x));

#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<TTo>[] ReinterpretVArray<TFrom, TTo>(in Vector<TFrom>[] x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>[], Vector<TTo>[]>(ref Unsafe.AsRef(x));


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<byte> AsUnsigned(in Vector<byte> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<ushort> AsUnsigned(in Vector<ushort> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<uint> AsUnsigned(in Vector<uint> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<ulong> AsUnsigned(in Vector<ulong> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<nuint> AsUnsigned(in Vector<nuint> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<byte> AsUnsigned(in Vector<sbyte> v)
        => ref Reinterpret<Vector<sbyte>, Vector<byte>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<ushort> AsUnsigned(in Vector<short> v)
        => ref Reinterpret<Vector<short>, Vector<ushort>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<uint> AsUnsigned(in Vector<int> v)
        => ref Reinterpret<Vector<int>, Vector<uint>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<ulong> AsUnsigned(in Vector<long> v)
        => ref Reinterpret<Vector<long>, Vector<ulong>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<nuint> AsUnsigned(in Vector<nint> v)
        => ref Reinterpret<Vector<nint>, Vector<nuint>>(v);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<sbyte> AsSigned(in Vector<sbyte> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<short> AsSigned(in Vector<short> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<int> AsSigned(in Vector<int> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<long> AsSigned(in Vector<long> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<nint> AsSigned(in Vector<nint> v) => ref v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<sbyte> AsSigned(in Vector<byte> v)
        => ref Reinterpret<Vector<byte>, Vector<sbyte>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<short> AsSigned(in Vector<ushort> v)
        => ref Reinterpret<Vector<ushort>, Vector<short>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<int> AsSigned(in Vector<uint> v)
        => ref Reinterpret<Vector<uint>, Vector<int>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<long> AsSigned(in Vector<ulong> v)
        => ref Reinterpret<Vector<ulong>, Vector<long>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<nint> AsSigned(in Vector<nuint> v)
        => ref Reinterpret<Vector<nuint>, Vector<nint>>(v);


#if NETCOREAPP3_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<T> AsVector<T>(in Vector256<T> v)
        where T : unmanaged
        => ref Reinterpret<Vector256<T>, Vector<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<T> AsVector<T>(in Vector128<T> v)
        where T : unmanaged
        => ref Reinterpret<Vector128<T>, Vector<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector256<T> AsVector256<T>(in Vector<T> v)
        where T : unmanaged
        => ref Reinterpret<Vector<T>, Vector256<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector128<T> AsVector128<T>(in Vector<T> v)
        where T : unmanaged
        => ref Reinterpret<Vector<T>, Vector128<T>>(v);

#endif
}
