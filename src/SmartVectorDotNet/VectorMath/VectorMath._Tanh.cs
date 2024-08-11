namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

file class Tanh_<T> : VectorMath.Const<T> where T : unmanaged
{
}

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
        var a = p - n;
        var b = p + n;
        return
            OP.ConditionalSelect(OP.BitwiseAnd(IsPositiveInfinity(a), IsPositiveInfinity(b)),
                Tanh_<T>._1,
            OP.ConditionalSelect(OP.BitwiseAnd(IsNegativeInfinity(a), IsPositiveInfinity(b)),
                Tanh_<T>._m1,
                (p - n) / (p + n)
                ));
    }
}
