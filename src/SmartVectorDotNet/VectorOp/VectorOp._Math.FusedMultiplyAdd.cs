#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
    /// <summary>
    /// Calculates fused-multiply-add <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector<T> FusedMultiplyAdd<T>(Vector<T> x, Vector<T> y, Vector<T> z)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Fma.IsSupported)
        {
            if (H.SizeOf<Vector<T>>() == H.SizeOf<Vector256<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return H.BitCastV<double, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.BitCastV<T, double>(x).AsVector256(),
                                H.BitCastV<T, double>(y).AsVector256(),
                                H.BitCastV<T, double>(z).AsVector256())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.BitCastV<float, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.BitCastV<T, float>(x).AsVector256(),
                                H.BitCastV<T, float>(y).AsVector256(),
                                H.BitCastV<T, float>(z).AsVector256())
                            )
                        );
                }
            }
            if (H.SizeOf<Vector<T>>() == H.SizeOf<Vector128<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return H.BitCastV<double, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.BitCastV<T, double>(x).AsVector128(),
                                H.BitCastV<T, double>(y).AsVector128(),
                                H.BitCastV<T, double>(z).AsVector128())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.BitCastV<float, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.BitCastV<T, float>(x).AsVector128(),
                                H.BitCastV<T, float>(y).AsVector128(),
                                H.BitCastV<T, float>(z).AsVector128())
                            )
                        );
                }
            }
        }
#endif
        return Emulate<T, FusedMultiplyAdd_<T>>(x, y, z);
    }

    private struct FusedMultiplyAdd_<T> : IVectorEmulationOp3<T>
        where T : unmanaged
    {
        public readonly T Calculate(T x, T y, T z)
            => ScalarOp.FusedMultiplyAdd(x, y, z);
    }
}
