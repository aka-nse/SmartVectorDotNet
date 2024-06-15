namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Asinh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates asinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Asinh<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x + Sqrt(x * x + Asinh_<T>._1));
}
