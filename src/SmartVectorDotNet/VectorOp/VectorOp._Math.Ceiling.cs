namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates Ceiling. </summary>
    [VectorOp]
    public static partial Vector<T> Ceiling<T>(Vector<T> d)
        where T : unmanaged;

    private static partial Vector<double> Ceiling_double(Vector<double> x);
    private static partial Vector<float> Ceiling_float(Vector<float> x);


#if NET6_0_OR_GREATER

    private static partial Vector<double> Ceiling_double(Vector<double> x)
        => Vector.Ceiling(x);

    private static partial Vector<float> Ceiling_float(Vector<float> x)
        => Vector.Ceiling(x);

#else

    private struct Ceiling_<T> : IVectorEmulationOp1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarOp.Ceiling(x);
    }
    
    private static partial Vector<double> Ceiling_double(Vector<double> x)
        => Emulate<double, Ceiling_<double>>(x);

    private static partial Vector<float> Ceiling_float(Vector<float> x)
        => Emulate<float, Ceiling_<float>>(x);

#endif
}
