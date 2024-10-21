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
            if (Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector256<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return H.Reinterpret<double, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, double>(x).AsVector256(),
                                H.Reinterpret<T, double>(y).AsVector256(),
                                H.Reinterpret<T, double>(z).AsVector256())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.Reinterpret<float, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, float>(x).AsVector256(),
                                H.Reinterpret<T, float>(y).AsVector256(),
                                H.Reinterpret<T, float>(z).AsVector256())
                            )
                        );
                }
            }
            if (Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector128<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return H.Reinterpret<double, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, double>(x).AsVector128(),
                                H.Reinterpret<T, double>(y).AsVector128(),
                                H.Reinterpret<T, double>(z).AsVector128())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.Reinterpret<float, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, float>(x).AsVector128(),
                                H.Reinterpret<T, float>(y).AsVector128(),
                                H.Reinterpret<T, float>(z).AsVector128())
                            )
                        );
                }
            }
        }
#endif
        return Emulate<T, FusedMultiplyAdd_<T>>(x, y, z);
    }

    private struct FusedMultiplyAdd_<T> : IOperation3<T>
        where T : unmanaged
    {
        public readonly T Calculate(T x, T y, T z)
            => ScalarMath.FusedMultiplyAdd(x, y, z);
    }
}
