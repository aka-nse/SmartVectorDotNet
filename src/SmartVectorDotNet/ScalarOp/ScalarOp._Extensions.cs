using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

partial class ScalarOp
{
    /// <summary>
    /// Calculates FMA <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static T FusedMultiplyAdd<T>(T x, T y, T z)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if(typeof(T) == typeof(double))
        {
            return Reinterpret<double, T>(
                Math.FusedMultiplyAdd(
                    Reinterpret<T, double>(x),
                    Reinterpret<T, double>(y),
                    Reinterpret<T, double>(z)
                    )
                );
        }
        if (typeof(T) == typeof(float))
        {
            return Reinterpret<float, T>(
                MathF.FusedMultiplyAdd(
                    Reinterpret<T, float>(x),
                    Reinterpret<T, float>(y),
                    Reinterpret<T, float>(z)
                    )
                );
        }
#endif
        return Add(Multiply(x, y), z);
    }
}