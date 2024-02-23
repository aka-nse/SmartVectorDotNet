using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SmartVectorDotNet;

internal static class InternalHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> CreateVector<T>(Span<T> elements)
        where T : unmanaged
    {
#if NETSTANDARD2_0
        return MemoryMarshal.Cast<T, Vector<T>>(elements)[0];
#else
        return new Vector<T>(elements);
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly TTo Reinterpret<TFrom, TTo>(in TFrom x)
        => ref Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(x));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<TTo> Reinterpret<TFrom, TTo>(in Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(x));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref readonly Vector<TTo>[] ReinterpretVArray<TFrom, TTo>(in Vector<TFrom>[] x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => ref Unsafe.As<Vector<TFrom>[], Vector<TTo>[]>(ref Unsafe.AsRef(x));

}
