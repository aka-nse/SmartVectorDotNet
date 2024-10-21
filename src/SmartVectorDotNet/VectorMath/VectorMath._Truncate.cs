namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates truncate. </summary>
    [VectorMath]
    public static partial Vector<T> Truncate<T>(Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate(Vector<double> d)
        => OP.ConvertToDouble(OP.ConvertToInt64(d));

    private static Vector<float> Truncate(Vector<float> d)
        => OP.ConvertToSingle(OP.ConvertToInt32(d));

}
