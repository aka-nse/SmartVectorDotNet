namespace SmartVectorDotNet;


file class Sinh_<T> : VectorOp.Const<T> where T : unmanaged { }


partial class VectorOp
{
    /// <summary>
    /// Calculates sinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sinh<T>(Vector<T> x)
        where T : unmanaged
    {
        Guard.OnlyRealSupported<T>();
        return (Exp(x) - Exp(-x)) * Sinh_<T>._1p2;
    }
}
