namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates truncate. </summary>
    [VectorMath]
    public static partial Vector<T> Truncate<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate(in Vector<double> d)
        => OP.ConvertToDouble(OP.ConvertToInt64(d));

    private static Vector<float> Truncate(in Vector<float> d)
        => OP.ConvertToSingle(OP.ConvertToInt32(d));

}
