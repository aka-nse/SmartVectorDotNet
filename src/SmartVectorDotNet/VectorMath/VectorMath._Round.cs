using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary>
    /// Calculates Round.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Round<T>(in Vector<T> x)
        where T : unmanaged;

    private struct Round_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Round(x);
    }

    /// <summary> Calculates round. </summary>
    private static Vector<double> Round(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        if (Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector256<double>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector128<double>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<double, Round_<double>>(x);
    }


    private static Vector<float> Round(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector256<float>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector128<float>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<float, Round_<float>>(x);
    }
}
