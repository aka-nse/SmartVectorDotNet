namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates Floor. </summary>
    [VectorMath]
    public static partial Vector<T> Floor<T>(Vector<T> d)
        where T : unmanaged;
    private struct Floor_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Floor(x);
    }

    private static Vector<double> Floor(Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        return Emulate<double, Floor_<double>>(x);
#endif
    }

    private static Vector<float> Floor(Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        return Emulate<float, Floor_<float>>(x);
#endif
    }
}
