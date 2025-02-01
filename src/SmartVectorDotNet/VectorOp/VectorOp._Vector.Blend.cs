using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
#if NETCOREAPP3_0_OR_GREATER

    internal static Vector<byte> Blend(Vector<byte> x, Vector<byte> y, [ConstantExpected] byte mask)
    {
        return Blend_Fallback(x, y, mask);
    }

    internal static Vector<ushort> Blend(Vector<ushort> x, Vector<ushort> y, [ConstantExpected] byte mask)
    {
        if (Avx2.IsSupported && Vector<byte>.Count == Vector256<byte>.Count)
            return Avx2.Blend(x.AsVector256(), y.AsVector256(), mask).AsVector();
        if (Sse41.IsSupported && Vector<byte>.Count == Vector128<byte>.Count)
            return Sse41.Blend(x.AsVector128(), y.AsVector128(), mask).AsVector();
        return Blend_Fallback(x, y, mask);
    }

    internal static Vector<uint> Blend(Vector<uint> x, Vector<uint> y, [ConstantExpected] byte mask)
    {
        if (Avx2.IsSupported && Vector<byte>.Count == Vector256<byte>.Count)
            return Avx2.Blend(x.AsVector256(), y.AsVector256(), mask).AsVector();
        return Blend_Fallback(x, y, mask);
    }

    internal static Vector<ulong> Blend(Vector<ulong> x, Vector<ulong> y, [ConstantExpected] byte mask)
    {
        if (Avx.IsSupported && Vector<ulong>.Count == Vector256<ulong>.Count)
            return H.Reinterpret<Vector256<double>, Vector<ulong>>(Avx.Blend(
                H.Reinterpret<Vector<ulong>, Vector256<double>>(x),
                H.Reinterpret<Vector<ulong>, Vector256<double>>(y),
                mask));
        return Blend_Fallback(x, y, mask);
    }

#endif

    internal static Vector<T> Blend_Fallback<T>(Vector<T> x, Vector<T> y, byte mask)
        where T : unmanaged
    {
        var retval = (stackalloc T[Vector<T>.Count]);
        for (var i = 0; i < retval.Length; ++i)
        {
            var z = i & 0b111;
            retval[i] = ((mask >> z) & 1) == 0 ? x[i] : y[i];
        }
        return H.CreateVector(retval);
    }

    /// <summary>
    /// Blends two vectors using a mask.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="mask">
    /// Blending mask.
    /// <list type="bullet">
    /// <item> If b_i = 0, then <c>result[i] = x[i], result[i + 8] = x[i + 8], result[i + 16] = x[i + 16], ...</c> </item>
    /// <item> If b_i = 1, then <c>result[i] = y[i], result[i + 8] = y[i + 8], result[i + 16] = y[i + 16], ...</c> </item>
    /// </list>
    /// </param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
#pragma warning disable format
#if NET7_0_OR_GREATER
    public static Vector<T> Blend<T>(Vector<T> x, Vector<T> y, [ConstantExpected] byte mask)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
            sizeof(byte  ) => H.Reinterpret<byte  , T>(Blend(H.Reinterpret<T, byte  >(x), H.Reinterpret<T, byte  >(y), mask)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Blend(H.Reinterpret<T, ushort>(x), H.Reinterpret<T, ushort>(y), mask)),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Blend(H.Reinterpret<T, uint  >(x), H.Reinterpret<T, uint  >(y), mask)),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Blend(H.Reinterpret<T, ulong >(x), H.Reinterpret<T, ulong >(y), mask)),
            _ => throw new NotSupportedException(),
        };
#elif NETCOREAPP3_0_OR_GREATER
    public static Vector<T> Blend<T>(Vector<T> x, Vector<T> y, byte mask)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
            sizeof(byte  ) => H.Reinterpret<byte  , T>(Blend(H.Reinterpret<T, byte  >(x), H.Reinterpret<T, byte  >(y), mask)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Blend(H.Reinterpret<T, ushort>(x), H.Reinterpret<T, ushort>(y), mask)),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Blend(H.Reinterpret<T, uint  >(x), H.Reinterpret<T, uint  >(y), mask)),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Blend(H.Reinterpret<T, ulong >(x), H.Reinterpret<T, ulong >(y), mask)),
            _ => throw new NotSupportedException(),
        };
#else
    public static Vector<T> Blend<T>(Vector<T> x, Vector<T> y, byte mask)
        where T : unmanaged
        => Blend_Fallback(x, y, mask);
#endif
#pragma warning restore format
}
