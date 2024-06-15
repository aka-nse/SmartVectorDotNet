namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class ModuloByConst_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> OnePerTau
        = AsVector(0.159154943091895);
    internal static readonly Vector<T> TauUpper
        = IsT<double>()
            ? As<double>(new(6.28318530717958))
        : IsT<float>()
            ? As<float>(new(6.283185f))
        : default;
    internal static readonly Vector<T> TauLower
        = IsT<double>()
            ? As<double>(new(0.00000000000000647692528676655901))
        : IsT<float>()
            ? As<float>(new(0.0000003071796f))
        : default;

    internal static readonly Vector<T> OnePerPi
        = AsVector(0.3183098861837907);
    internal static readonly Vector<T> PIUpper
        = IsT<double>()
            ? As<double>(new(3.141592653589793))
        : IsT<float>()
            ? As<float>(new(3.1415925f))
        : default;
    internal static readonly Vector<T> PILower
        = IsT<double>()
            ? As<double>(new(0.00000000000000023846264338327950))
        : IsT<float>()
            ? As<float>(new(0.0000001509958f))
        : default;
}


partial class VectorMath
{
    private static Vector<T> ModuloByTau<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * ModuloByConst_<T>.OnePerTau);
        var reminderHigh = FusedMultiplyAdd(quotient, -ModuloByConst_<T>.TauUpper, x);
        return FusedMultiplyAdd(quotient, -ModuloByConst_<T>.TauLower, reminderHigh);
    }


    private static Vector<T> ModuloByPI<T>(in Vector<T> x)
        where T : unmanaged
    {
        var quotient = Floor(x * ModuloByConst_<T>.OnePerPi);
        var reminderHigh = FusedMultiplyAdd(quotient, -ModuloByConst_<T>.PIUpper, x);
        return FusedMultiplyAdd(quotient, -ModuloByConst_<T>.PILower, reminderHigh);
    }


    private static Vector<T> ModuloBy2<T>(in Vector<T> x)
        where T : unmanaged
    {
#pragma warning disable format
        if (typeof(T) == typeof(byte  )) return H.Reinterpret<byte  , T>(H.Reinterpret<T, byte  >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(ushort)) return H.Reinterpret<ushort, T>(H.Reinterpret<T, ushort>(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(uint  )) return H.Reinterpret<uint  , T>(H.Reinterpret<T, uint  >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(ulong )) return H.Reinterpret<ulong , T>(H.Reinterpret<T, ulong >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(sbyte )) return H.Reinterpret<sbyte , T>(H.Reinterpret<T, sbyte >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(short )) return H.Reinterpret<short , T>(H.Reinterpret<T, short >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(int   )) return H.Reinterpret<int   , T>(H.Reinterpret<T, int   >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(long  )) return H.Reinterpret<long  , T>(H.Reinterpret<T, long  >(x & ModuloByConst_<T>._1));
        if (typeof(T) == typeof(float )) return H.Reinterpret<float , T>(ModuloBy2(H.Reinterpret<T, float >(x)));
        if (typeof(T) == typeof(double)) return H.Reinterpret<double, T>(ModuloBy2(H.Reinterpret<T, double>(x)));
#pragma warning restore format
        throw new NotSupportedException();
    }


    private static Vector<float> ModuloBy2(in Vector<float> x)
    {
        var xx = x * ModuloByConst_<float>._1p2;
        xx = xx - Floor(xx);
        return xx * ModuloByConst_<float>._2;
    }


    private static Vector<double> ModuloBy2(in Vector<double> x)
    {
        var xx = x * ModuloByConst_<double>._1p2;
        xx = xx - Floor(xx);
        return xx * ModuloByConst_<double>._2;
    }
}
