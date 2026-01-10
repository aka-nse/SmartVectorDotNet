#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
    /// <inheritdoc cref="Round_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Round_default))]
    public static partial Vector<T> Round<T>(Vector<T> x)
        where T : unmanaged;

    /// <summary>
    /// Calculates Round.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    private static Vector<T> Round_default<T>(Vector<T> x)
        where T : unmanaged => throw new NotSupportedException();

    private readonly struct Round_<T> : IVectorEmulationOp1<T>
        where T : unmanaged
    {
        public readonly T Calculate(T x) => ScalarOp.Round(x);
    }

    /// <summary> Calculates round. </summary>
    private static Vector<double> Round(Vector<double> x)
    {
#if NET6_0_OR_GREATER
        if (H.SizeOf<Vector<double>>() == H.SizeOf<Vector256<double>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (H.SizeOf<Vector<double>>() == H.SizeOf<Vector128<double>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<double, Round_<double>>(x);
    }


    private static Vector<float> Round(Vector<float> x)
    {
#if NET6_0_OR_GREATER
        if (H.SizeOf<Vector<float>>() == H.SizeOf<Vector256<float>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (H.SizeOf<Vector<float>>() == H.SizeOf<Vector128<float>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<float, Round_<float>>(x);
    }
}
