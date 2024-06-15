namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary> Calculates abs. </summary>
    public static Vector<T> Abs<T>(in Vector<T> d)
        where T : unmanaged
        => Vector.Abs(d);
}
