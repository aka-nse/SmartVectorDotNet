namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates Ceiling. </summary>
    [VectorMath]
    public static partial Vector<T> Ceiling<T>(in Vector<T> d)
        where T : unmanaged;
    private struct Ceiling_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Ceiling(x);
    }

    private static Vector<double> Ceiling(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Ceiling(x);
#else
        return Emulate<double, Ceiling_<double>>(x);
#endif
    }

    private static Vector<float> Ceiling(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Ceiling(x);
#else
        return Emulate<float, Ceiling_<float>>(x);
#endif
    }
}
