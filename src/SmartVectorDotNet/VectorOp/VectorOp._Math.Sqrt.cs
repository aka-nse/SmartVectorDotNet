namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorOp
{
    /// <summary> Calculates sqrt. </summary>
    [VectorOp]
    public static Vector<T> Sqrt<T>(Vector<T> d)
        where T : unmanaged
        => OP.SquareRoot(d);
}
