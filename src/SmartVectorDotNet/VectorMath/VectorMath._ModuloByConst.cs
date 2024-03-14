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
}
