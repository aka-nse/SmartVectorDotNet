namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{
    /// <summary>
    /// Calculates tanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Tanh<T>(in Vector<T> x)
        where T : unmanaged
    {
        var p = Exp(x);
        var n = Exp(-x);
        return (p - n) / (p + n);
    }
}
