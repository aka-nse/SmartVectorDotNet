using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

namespace SmartVectorDotNet;

internal static class InternalHelpers
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SizeOf<T>()
    {
        unsafe
        {
            return Unsafe.SizeOf<T>();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> CreateVector<T>(Span<T> elements)
        where T : unmanaged
        => CreateVector((ReadOnlySpan<T>)elements);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> CreateVector<T>(ReadOnlySpan<T> elements)
        where T : unmanaged
    {
        Guard.DebugValidArgument(
            elements.Length >= Vector<T>.Count,
            $"The length of elements span must be at least {Vector<T>.Count} for creating {nameof(Vector<>)}.");
        unsafe
        {
            return MemoryMarshal.Cast<T, Vector<T>>(elements)[0];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSameBuffer<T1, T2>(ReadOnlySpan<T1> source, Span<T2> destination)
        where T1 : unmanaged
        where T2 : unmanaged
    {
        unsafe
        {
            return SizeOf<T1>() == SizeOf<T2>()
                && Unsafe.ByteOffset(
                    ref Unsafe.As<T1, byte>(ref Unsafe.AsRef(in source[0])),
                    ref Unsafe.As<T2, byte>(ref destination[0])) == IntPtr.Zero;
        }
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
        Guard.DebugValidArgument(
            elements.Length >= Vector128<T>.Count,
            $"The length of elements span must be at least {Vector128<T>.Count} for creating {nameof(Vector128<>)}.");
        unsafe
        {
            return MemoryMarshal.Cast<T, Vector128<T>>(elements)[0];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> CreateVector256<T>(Span<T> elements)
        where T : unmanaged
        => CreateVector256((ReadOnlySpan<T>)elements);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> CreateVector256<T>(ReadOnlySpan<T> elements)
        where T : unmanaged
    {
        Guard.DebugValidArgument(
            elements.Length >= Vector256<T>.Count,
            $"The length of elements span must be at least {Vector256<T>.Count} for creating {nameof(Vector256<>)}.");
        unsafe
        {
            return MemoryMarshal.Cast<T, Vector256<T>>(elements)[0];
        }
    }

#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TTo BitCast<TFrom, TTo>(TFrom x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
#if NET8_0_OR_GREATER
        unsafe
        {
            return Unsafe.BitCast<TFrom, TTo>(x);
        }
#else
        Guard.DebugValidArgument(
            SizeOf<TFrom>() == SizeOf<TTo>(), 
            $"Cannot reinterpret from {typeof(TFrom)} to {typeof(TTo)} because their sizes are different.");
        unsafe
        {
            return Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(in x));
        }
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref TTo Reinterpret<TFrom, TTo>(ref TFrom x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        Guard.DebugValidArgument(
            SizeOf<TFrom>() == SizeOf<TTo>(),
            $"Cannot reinterpret from {typeof(TFrom)} to {typeof(TTo)} because their sizes are different.");
        unsafe
        {
            return ref Unsafe.As<TFrom, TTo>(ref x);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TTo> BitCastV<TFrom, TTo>(Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
#if NET8_0_OR_GREATER
        unsafe
        {
            return Unsafe.BitCast<Vector<TFrom>, Vector<TTo>>(x);
        }
#else
        unsafe
        {
            return Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(in x));
        }
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref Vector<TTo> ReinterpretV<TFrom, TTo>(ref Vector<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        unsafe
        {
            return ref Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref x);
        }
    }

#if NET6_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<TTo> BitCast<TFrom, TTo>(Vector128<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
#if NET8_0_OR_GREATER
        unsafe
        {
            return Unsafe.BitCast<Vector128<TFrom>, Vector128<TTo>>(x);
        }
#else
        unsafe
        {
            return Unsafe.As<Vector128<TFrom>, Vector128<TTo>>(ref Unsafe.AsRef(in x));
        }
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<TTo> BitCast<TFrom, TTo>(Vector256<TFrom> x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
#if NET8_0_OR_GREATER
        unsafe
        {
            return Unsafe.BitCast<Vector256<TFrom>, Vector256<TTo>>(x);
        }
#else
        unsafe
        {
            return ref Unsafe.As<Vector256<TFrom>, Vector256<TTo>>(ref Unsafe.AsRef(in x));
        }
#endif
    }

#endif

        /// <remarks>
        /// This method is only for generic type conversion where actual type parameter is same with closed type parameter.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<TTo>[] ReinterpretVArray<TFrom, TTo>(Vector<TFrom>[] x)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        Guard.DebugValidArgument(
            typeof(TFrom) == typeof(TTo),
            $"Cannot reinterpret from {typeof(TFrom)} to {typeof(TTo)} because their sizes are different.");
        unsafe
        {
            return Unsafe.As<Vector<TFrom>[], Vector<TTo>[]>(ref Unsafe.AsRef(in x));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<byte> AsUnsigned(in Vector<byte> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ushort> AsUnsigned(in Vector<ushort> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<uint> AsUnsigned(in Vector<uint> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ulong> AsUnsigned(in Vector<ulong> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<nuint> AsUnsigned(in Vector<nuint> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<byte> AsUnsigned(in Vector<sbyte> v)
        => BitCast<Vector<sbyte>, Vector<byte>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ushort> AsUnsigned(in Vector<short> v)
        => BitCast<Vector<short>, Vector<ushort>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<uint> AsUnsigned(in Vector<int> v)
        => BitCast<Vector<int>, Vector<uint>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<ulong> AsUnsigned(in Vector<long> v)
        => BitCast<Vector<long>, Vector<ulong>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<nuint> AsUnsigned(in Vector<nint> v)
        => BitCast<Vector<nint>, Vector<nuint>>(v);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<sbyte> AsSigned(in Vector<sbyte> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<short> AsSigned(in Vector<short> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<int> AsSigned(in Vector<int> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<long> AsSigned(in Vector<long> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<nint> AsSigned(in Vector<nint> v) => v;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<sbyte> AsSigned(in Vector<byte> v)
        => BitCast<Vector<byte>, Vector<sbyte>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<short> AsSigned(in Vector<ushort> v)
        => BitCast<Vector<ushort>, Vector<short>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<int> AsSigned(in Vector<uint> v)
        => BitCast<Vector<uint>, Vector<int>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<long> AsSigned(in Vector<ulong> v)
        => BitCast<Vector<ulong>, Vector<long>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<nint> AsSigned(in Vector<nuint> v)
        => BitCast<Vector<nuint>, Vector<nint>>(v);


#if NETCOREAPP3_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> AsVector<T>(Vector256<T> v)
        where T : unmanaged
        => BitCast<Vector256<T>, Vector<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> AsVector<T>(Vector128<T> v)
        where T : unmanaged
        => BitCast<Vector128<T>, Vector<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector256<T> AsVector256<T>(Vector<T> v)
        where T : unmanaged
        => BitCast<Vector<T>, Vector256<T>>(v);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector128<T> AsVector128<T>(Vector<T> v)
        where T : unmanaged
        => BitCast<Vector<T>, Vector128<T>>(v);

#endif
}
