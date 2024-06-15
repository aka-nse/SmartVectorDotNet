namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{
    /// <summary>
    /// Calculates log(x, newBase).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="newBase"></param>
    /// <returns></returns>
    public static Vector<T> Log<T>(in Vector<T> x, in Vector<T> newBase)
        where T : unmanaged
        => Log(x) / Log(newBase);
}