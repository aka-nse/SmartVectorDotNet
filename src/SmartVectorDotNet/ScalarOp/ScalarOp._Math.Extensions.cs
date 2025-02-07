namespace SmartVectorDotNet;
using OP = ScalarOp;

partial class ScalarOp
{
    /// <summary>
    /// Returns an integer that indicates the sign of a number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T Sign<T>(T value)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) return Reinterpret<sbyte , T>((sbyte)Math.Sign(Reinterpret<T, sbyte >(value)));
        if(typeof(T) == typeof(short )) return Reinterpret<short , T>((short)Math.Sign(Reinterpret<T, short >(value)));
        if(typeof(T) == typeof(int   )) return Reinterpret<int   , T>(Math.Sign(Reinterpret<T, int   >(value)));
        if(typeof(T) == typeof(long  )) return Reinterpret<long  , T>(Math.Sign(Reinterpret<T, long  >(value)));
        if(typeof(T) == typeof(float )) return Reinterpret<float , T>(Math.Sign(Reinterpret<T, float >(value)));
        if(typeof(T) == typeof(double)) return Reinterpret<double, T>(Math.Sign(Reinterpret<T, double>(value)));
        #if NET6_0_OR_GREATER
        if(typeof(T) == typeof(nint  )) return Reinterpret<nint  , T>(Math.Sign(Reinterpret<T, nint  >(value)));
        #else
        if (typeof(T) == typeof(nint))
            return IntPtr.Size switch
            {
                4 => Reinterpret<int , T>(Math.Sign(Reinterpret<T, int >(value))),
                8 => Reinterpret<long, T>(Math.Sign(Reinterpret<T, long>(value))),
                _ => throw new NotSupportedException(),
            };
        #endif
        throw new NotSupportedException();
    }


    /// <summary>
    /// Calculates FMA <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
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
        return OP.Add(OP.Multiply(x, y), z);
    }
}