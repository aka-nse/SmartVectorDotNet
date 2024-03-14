namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{
    /// <summary>
    /// Calculates pow(a, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Pow<T>(in Vector<T> a, in Vector<T> x)
        where T : unmanaged
        => Exp(x * Log(a));
}
