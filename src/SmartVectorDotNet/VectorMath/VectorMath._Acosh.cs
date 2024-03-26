namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Acosh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates acosh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Acosh<T>(in Vector<T> x)
        where T : unmanaged
    {
        var d = (x * x) - Acosh_<T>._1;
        return Log(x + Sqrt(d));
    }
}
