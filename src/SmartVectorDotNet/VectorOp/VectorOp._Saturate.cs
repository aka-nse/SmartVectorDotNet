#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
    #region Add

    /// <summary>
    /// Adds two vectors and saturates the result if can; otherwise returns simply add.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Vector<T> AddSaturate<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
    {
#if NETCOREAPP3_0_OR_GREATER
        if(typeof(T) == typeof(byte))
        {
            return H.Reinterpret<byte, T>(AddSaturateCore(H.Reinterpret<T, byte>( lhs), H.Reinterpret<T, byte>(rhs)));
        }
        if (typeof(T) == typeof(sbyte))
        {
            return H.Reinterpret<sbyte, T>(AddSaturateCore(H.Reinterpret<T, sbyte>(lhs), H.Reinterpret<T, sbyte>(rhs)));
        }
        if (typeof(T) == typeof(ushort))
        {
            return H.Reinterpret<ushort, T>(AddSaturateCore(H.Reinterpret<T, ushort>(lhs), H.Reinterpret<T, ushort>(rhs)));
        }
        if (typeof(T) == typeof(short))
        {
            return H.Reinterpret<short, T>(AddSaturateCore(H.Reinterpret<T, short>(lhs), H.Reinterpret<T, short>(rhs)));
        }
#endif
        if (typeof(T) == typeof(byte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(uint) || typeof(T) == typeof(ulong) || typeof(T) == typeof(nuint))
        {
            return AddSaturateUnsigned(lhs, rhs);
        }
        if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long) || typeof(T) == typeof(nint))
        {
            return AddSaturateSigned(lhs, rhs);
        }
        return lhs + rhs;
    }

#if NETCOREAPP3_0_OR_GREATER

    private static Vector<byte> AddSaturateCore(Vector<byte> lhs, Vector<byte> rhs)
    {
        if(Avx2.IsSupported && Vector<byte>.Count == Vector256<byte>.Count)
        {
            return Avx2.AddSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<byte>.Count == Vector128<byte>.Count)
        {
            return Sse2.AddSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return AddSaturateUnsigned(lhs, rhs);
    }

    private static Vector<ushort> AddSaturateCore(Vector<ushort> lhs, Vector<ushort> rhs)
    {
        if (Avx2.IsSupported && Vector<ushort>.Count == Vector256<ushort>.Count)
        {
            return Avx2.AddSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<ushort>.Count == Vector128<ushort>.Count)
        {
            return Sse2.AddSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return AddSaturateUnsigned(lhs, rhs);
    }

    private static Vector<sbyte> AddSaturateCore(Vector<sbyte> lhs, Vector<sbyte> rhs)
    {
        if (Avx2.IsSupported && Vector<sbyte>.Count == Vector256<sbyte>.Count)
        {
            return Avx2.AddSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<sbyte>.Count == Vector128<sbyte>.Count)
        {
            return Sse2.AddSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return AddSaturateSigned(lhs, rhs);
    }

    private static Vector<short> AddSaturateCore(Vector<short> lhs, Vector<short> rhs)
    {
        if (Avx2.IsSupported && Vector<short>.Count == Vector256<short>.Count)
        {
            return Avx2.AddSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<short>.Count == Vector128<short>.Count)
        {
            return Sse2.AddSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return AddSaturateSigned(lhs, rhs);
    }

#endif

    private static Vector<T> AddSaturateUnsigned<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
        => ConditionalSelect(
            LessThanOrEqual(rhs, Const<T>.MaxValue - lhs),
            lhs + rhs,
            Const<T>.MaxValue);

    private static Vector<T> AddSaturateSigned<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
    {
        var add = lhs + rhs;
        return ConditionalSelect(
            GreaterThanOrEqual(lhs, Const<T>.Zero),
            ConditionalSelect(
                LessThanOrEqual(rhs, Const<T>.MaxValue - lhs),
                add,
                Const<T>.MaxValue),
            ConditionalSelect(
                LessThanOrEqual(Const<T>.MinValue - lhs, rhs),
                add,
                Const<T>.MinValue)
            );
    }

    #endregion

    #region Subtract

    /// <summary>
    /// Subtracts two vectors and saturates the result if can; otherwise returns simply subtract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static Vector<T> SubtractSaturate<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
    {
#if NETCOREAPP3_0_OR_GREATER
        if (typeof(T) == typeof(byte))
        {
            return H.Reinterpret<byte, T>(SubtractSaturateCore(H.Reinterpret<T, byte>(lhs), H.Reinterpret<T, byte>(rhs)));
        }
        if (typeof(T) == typeof(sbyte))
        {
            return H.Reinterpret<sbyte, T>(SubtractSaturateCore(H.Reinterpret<T, sbyte>(lhs), H.Reinterpret<T, sbyte>(rhs)));
        }
        if (typeof(T) == typeof(ushort))
        {
            return H.Reinterpret<ushort, T>(SubtractSaturateCore(H.Reinterpret<T, ushort>(lhs), H.Reinterpret<T, ushort>(rhs)));
        }
        if (typeof(T) == typeof(short))
        {
            return H.Reinterpret<short, T>(SubtractSaturateCore(H.Reinterpret<T, short>(lhs), H.Reinterpret<T, short>(rhs)));
        }
#endif
        if (typeof(T) == typeof(byte) || typeof(T) == typeof(ushort) || typeof(T) == typeof(uint) || typeof(T) == typeof(ulong) || typeof(T) == typeof(nuint))
        {
            return SubtractSaturateUnsigned(lhs, rhs);
        }
        if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(short) || typeof(T) == typeof(int) || typeof(T) == typeof(long) || typeof(T) == typeof(nint))
        {
            return SubtractSaturateSigned(lhs, rhs);
        }
        return lhs - rhs;
    }

#if NETCOREAPP3_0_OR_GREATER

    private static Vector<byte> SubtractSaturateCore(Vector<byte> lhs, Vector<byte> rhs)
    {
        if (Avx2.IsSupported && Vector<byte>.Count == Vector256<byte>.Count)
        {
            return Avx2.SubtractSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<byte>.Count == Vector128<byte>.Count)
        {
            return Sse2.SubtractSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return SubtractSaturateUnsigned(lhs, rhs);
    }

    private static Vector<ushort> SubtractSaturateCore(Vector<ushort> lhs, Vector<ushort> rhs)
    {
        if (Avx2.IsSupported && Vector<ushort>.Count == Vector256<ushort>.Count)
        {
            return Avx2.SubtractSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<ushort>.Count == Vector128<ushort>.Count)
        {
            return Sse2.SubtractSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return SubtractSaturateUnsigned(lhs, rhs);
    }

    private static Vector<sbyte> SubtractSaturateCore(Vector<sbyte> lhs, Vector<sbyte> rhs)
    {
        if (Avx2.IsSupported && Vector<sbyte>.Count == Vector256<sbyte>.Count)
        {
            return Avx2.SubtractSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<sbyte>.Count == Vector128<sbyte>.Count)
        {
            return Sse2.SubtractSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return SubtractSaturateSigned(lhs, rhs);
    }

    private static Vector<short> SubtractSaturateCore(Vector<short> lhs, Vector<short> rhs)
    {
        if (Avx2.IsSupported && Vector<short>.Count == Vector256<short>.Count)
        {
            return Avx2.SubtractSaturate(lhs.AsVector256(), rhs.AsVector256()).AsVector();
        }
        if (Sse2.IsSupported && Vector<short>.Count == Vector128<short>.Count)
        {
            return Sse2.SubtractSaturate(lhs.AsVector128(), rhs.AsVector128()).AsVector();
        }
        return SubtractSaturateSigned(lhs, rhs);
    }

#endif

    private static Vector<T> SubtractSaturateUnsigned<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
        => ConditionalSelect(
            GreaterThanOrEqual(lhs, rhs),
            lhs - rhs,
            Const<T>.MinValue);

    private static Vector<T> SubtractSaturateSigned<T>(Vector<T> lhs, Vector<T> rhs)
        where T : unmanaged
    {
        var sub = lhs - rhs;
        return ConditionalSelect(
                GreaterThanOrEqual(rhs, Const<T>.Zero),
                ConditionalSelect(
                    LessThanOrEqual(Const<T>.MinValue + rhs, lhs),
                    sub,
                    Const<T>.MinValue
                    ),
                ConditionalSelect(
                    LessThanOrEqual(lhs, Const<T>.MaxValue + rhs),
                    sub,
                    Const<T>.MaxValue
                    )
                );
    }

    #endregion
}
