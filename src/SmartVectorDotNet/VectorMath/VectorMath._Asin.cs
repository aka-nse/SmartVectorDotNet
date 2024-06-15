namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Asin_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates asin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Asin<T>(in Vector<T> x)
        where T : unmanaged
        => Asin_<T>._2 * Atan(x / (x + Sqrt(Asin_<T>._1 - x * x)));
}
