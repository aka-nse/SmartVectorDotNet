using GenericSpecialization;

namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <inheritdoc cref="Floor_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Floor_default))]
    public static partial Vector<T> Floor<T>(Vector<T> x)
        where T : unmanaged;

    /// <summary> Calculates Floor. </summary>
    private static Vector<T> Floor_default<T>(Vector<T> x) where T : unmanaged
        => throw new NotSupportedException();

    private static partial Vector<double> Floor(Vector<double> x);
    private static partial Vector<float> Floor(Vector<float> x);

#if NET6_0_OR_GREATER

    private static partial Vector<double> Floor(Vector<double> x)
        => Vector.Floor(x);

    private static partial Vector<float> Floor(Vector<float> x)
        => Vector.Floor(x);

#else

    private readonly struct Floor_<T> : IVectorEmulationOp1<T>
        where T : unmanaged
    {
        public readonly T Calculate(T x) => ScalarOp.Floor(x);
    }

    private static partial Vector<double> Floor(Vector<double> x)
        => Emulate<double, Floor_<double>>(x);

    private static partial Vector<float> Floor(Vector<float> x)
        => Emulate<float, Floor_<float>>(x);

#endif
}
