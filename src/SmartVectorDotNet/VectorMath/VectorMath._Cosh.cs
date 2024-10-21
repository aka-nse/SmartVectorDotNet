namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Cosh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates cosh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Cosh<T>(Vector<T> x)
        where T : unmanaged
        => (Exp(x) + Exp(-x)) * Cosh_<T>._1p2;
}
