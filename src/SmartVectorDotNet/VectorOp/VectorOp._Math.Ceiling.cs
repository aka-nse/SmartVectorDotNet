using GenericSpecialization;

namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <inheritdoc cref="Ceiling_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Ceiling_default))]
    public static partial Vector<T> Ceiling<T>(Vector<T> x)
        where T : unmanaged;

    /// <summary> Calculates Ceiling. </summary>
    private static Vector<T> Ceiling_default<T>(Vector<T> x)
        where T : unmanaged => throw new NotSupportedException();

    private static partial Vector<double> Ceiling(Vector<double> x);
    private static partial Vector<float> Ceiling(Vector<float> x);


#if NET6_0_OR_GREATER

    private static partial Vector<double> Ceiling(Vector<double> x)
        => Vector.Ceiling(x);

    private static partial Vector<float> Ceiling(Vector<float> x)
        => Vector.Ceiling(x);

#else

    private readonly struct Ceiling_<T> : IVectorEmulationOp1<T>
        where T : unmanaged
    {
        public readonly T Calculate(T x) => ScalarOp.Ceiling(x);
    }
    
    private static partial Vector<double> Ceiling(Vector<double> x)
        => Emulate<double, Ceiling_<double>>(x);

    private static partial Vector<float> Ceiling(Vector<float> x)
        => Emulate<float, Ceiling_<float>>(x);

#endif
}
