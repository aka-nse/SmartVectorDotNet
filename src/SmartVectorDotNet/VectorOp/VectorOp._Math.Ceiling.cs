namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates Ceiling. </summary>
    [VectorOp]
    public static partial Vector<T> Ceiling<T>(Vector<T> d)
        where T : unmanaged;
    private struct Ceiling_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarOp.Ceiling(x);
    }

    private static Vector<double> Ceiling_double(Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return Vector.Ceiling(x);
#else
        return Emulate<double, Ceiling_<double>>(x);
#endif
    }

    private static Vector<float> Ceiling_float(Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return Vector.Ceiling(x);
#else
        return Emulate<float, Ceiling_<float>>(x);
#endif
    }
}
