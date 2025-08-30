using GenericSpecialization;

namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <inheritdoc cref="Truncate_default"/>
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Truncate_default))]
    public static partial Vector<T> Truncate<T>(Vector<T> d)
        where T : unmanaged;

    /// <summary> Calculates truncate. </summary>
    private static Vector<T> Truncate_default<T>(Vector<T> d)
        where T : unmanaged => throw new NotSupportedException();

    private static Vector<double> Truncate(Vector<double> d)
        => ConvertToDouble(ConvertToInt64(d));

    private static Vector<float> Truncate(Vector<float> d)
        => ConvertToSingle(ConvertToInt32(d));

}
