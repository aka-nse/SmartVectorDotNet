namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates Floor. </summary>
    [VectorOp]
    public static partial Vector<T> Floor<T>(Vector<T> d)
        where T : unmanaged;

    private static partial Vector<double> Floor_double(Vector<double> x);
    private static partial Vector<float> Floor_float(Vector<float> x);

#if NET6_0_OR_GREATER

    private static partial Vector<double> Floor_double(Vector<double> x)
        => Vector.Floor(x);

    private static partial Vector<float> Floor_float(Vector<float> x)
        => Vector.Floor(x);

#else

    private struct Floor_<T> : IVectorEmulationOp1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarOp.Floor(x);
    }

    private static partial Vector<double> Floor_double(Vector<double> x)
        => Emulate<double, Floor_<double>>(x);

    private static partial Vector<float> Floor_float(Vector<float> x)
        => Emulate<float, Floor_<float>>(x);

#endif
}
