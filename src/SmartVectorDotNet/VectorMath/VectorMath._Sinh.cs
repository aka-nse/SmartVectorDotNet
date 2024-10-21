namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Sinh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates sinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sinh<T>(Vector<T> x)
        where T : unmanaged
        => (Exp(x) - Exp(-x)) * Sinh_<T>._1p2;
}
