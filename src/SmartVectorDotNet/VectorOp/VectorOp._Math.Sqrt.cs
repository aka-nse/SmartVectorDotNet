namespace SmartVectorDotNet;


partial class VectorOp
{
    /// <summary> Calculates sqrt. </summary>
    [VectorOp]
    public static Vector<T> Sqrt<T>(Vector<T> d)
        where T : unmanaged
        => SquareRoot(d);
}
