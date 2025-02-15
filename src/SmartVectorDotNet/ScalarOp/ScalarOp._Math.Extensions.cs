namespace SmartVectorDotNet;

using System.Runtime.CompilerServices;
using OP = ScalarOp;
using H = InternalHelpers;

#pragma warning disable format
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
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(sbyte )) return from(Sign(to<sbyte >(value)));
        if (typeof(T) == typeof(short )) return from(Sign(to<short >(value)));
        if (typeof(T) == typeof(int   )) return from(Sign(to<int   >(value)));
        if (typeof(T) == typeof(long  )) return from(Sign(to<long  >(value)));
        if (typeof(T) == typeof(float )) return from(Sign(to<float >(value)));
        if (typeof(T) == typeof(double)) return from(Sign(to<double>(value)));
        if (typeof(T) == typeof(nint  )) return from(Sign(to<nint  >(value)));
        throw new NotSupportedException();
    }

    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static sbyte  Sign(sbyte  value) => (sbyte )Math.Sign(value);
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static short  Sign(short  value) => (short )Math.Sign(value);
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static int    Sign(int    value) => (int   )Math.Sign(value);
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static long   Sign(long   value) => (long  )Math.Sign(value);
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static float  Sign(float  value) => (float )Math.Sign(value);
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static double Sign(double value) => (double)Math.Sign(value);
    
    /**<see cref="Sign{T}"/>*/ [MethodImpl(_inlining)] public static partial nint Sign(nint value);


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
        static ref readonly TTo to<TTo>(in T x) => ref H.Reinterpret<T, TTo>(in x);
        static ref readonly T from<TFrom>(in TFrom x) => ref H.Reinterpret<TFrom, T>(in x);

        if (typeof(T) == typeof(double)) return from(FusedMultiplyAdd(to<double>(x), to<double>(y), to<double>(z)));
        if (typeof(T) == typeof(float )) return from(FusedMultiplyAdd(to<float >(x), to<float >(y), to<float >(z)));
        throw new NotSupportedException();
    }
    
    /**<see cref="FusedMultiplyAdd{T}"/>*/ [MethodImpl(_inlining)] public static partial float  FusedMultiplyAdd(float  x, float  y, float  z);
    /**<see cref="FusedMultiplyAdd{T}"/>*/ [MethodImpl(_inlining)] public static partial double FusedMultiplyAdd(double x, double y, double z);


#if NET6_0_OR_GREATER
    public static partial nint Sign(nint value) => Math.Sign(value);
    public static partial float  FusedMultiplyAdd(float  x, float  y, float  z) => MathF.FusedMultiplyAdd(x, y, z);
    public static partial double FusedMultiplyAdd(double x, double y, double z) => Math.FusedMultiplyAdd(x, y, z);
#else
    public static partial nint Sign(nint value)
        => IntPtr.Size switch
        {
            4 => Reinterpret<int , nint>(Math.Sign(Reinterpret<nint, int >(value))),
            8 => Reinterpret<long, nint>(Math.Sign(Reinterpret<nint, long>(value))),
            _ => throw new NotSupportedException(),
        };
        
    public static partial float  FusedMultiplyAdd(float  x, float  y, float  z) => OP.Add(OP.Multiply(x, y), z);
    public static partial double FusedMultiplyAdd(double x, double y, double z) => OP.Add(OP.Multiply(x, y), z);
#endif
}
#pragma warning restore format