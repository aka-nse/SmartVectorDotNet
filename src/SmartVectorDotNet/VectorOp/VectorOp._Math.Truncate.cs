namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates truncate. </summary>
    [VectorOp]
    public static partial Vector<T> Truncate<T>(Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate_double(Vector<double> d)
        => ConvertToDouble(ConvertToInt64(d));

    private static Vector<float> Truncate_float(Vector<float> d)
        => ConvertToSingle(ConvertToInt32(d));

}
