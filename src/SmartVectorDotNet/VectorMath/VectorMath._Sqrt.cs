namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates sqrt. </summary>
    [VectorMath]
    public static Vector<T> Sqrt<T>(in Vector<T> d)
        where T : unmanaged
        => OP.SquareRoot(d);
}
