namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates Floor. </summary>
    [VectorOp]
    public static partial Vector<T> Floor<T>(Vector<T> d)
        where T : unmanaged;

    private struct Floor_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarOp.Floor(x);
    }

    private static Vector<double> Floor_double(Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return Vector.Floor(x);
#else
        return Emulate<double, Floor_<double>>(x);
#endif
    }

    private static Vector<float> Floor_float(Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return Vector.Floor(x);
#else
        return Emulate<float, Floor_<float>>(x);
#endif
    }
}
