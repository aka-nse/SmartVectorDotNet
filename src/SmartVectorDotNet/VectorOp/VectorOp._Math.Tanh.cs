namespace SmartVectorDotNet;

file class Tanh_<T> : VectorOp.Const<T> where T : unmanaged
{
}

partial class VectorOp
{
    /// <summary>
    /// Calculates tanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Tanh<T>(Vector<T> x)
        where T : unmanaged
    {
        Guard.OnlyRealSupported<T>();

        var p = Exp(x);
        var n = Exp(-x);
        var a = p - n;
        var b = p + n;
        return
            ConditionalSelect(BitwiseAnd(IsPositiveInfinity(a), IsPositiveInfinity(b)),
                Tanh_<T>._1,
            ConditionalSelect(BitwiseAnd(IsNegativeInfinity(a), IsPositiveInfinity(b)),
                Tanh_<T>._m1,
                (p - n) / (p + n)
                ));
    }
}
