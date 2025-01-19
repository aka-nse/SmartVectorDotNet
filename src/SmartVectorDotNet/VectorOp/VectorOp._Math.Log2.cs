namespace SmartVectorDotNet;


file class Log2_<T> : VectorOp.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> OnePerLog_E_2 = AsVector(1.0 / ScalarOp.Log(2, ScalarOp.Const<double>.E));
}


partial class VectorOp
{

    /// <summary>
    /// Calculates log2(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Log2<T>(Vector<T> x)
        where T : unmanaged
        => Log(x) * Log2_<T>.OnePerLog_E_2;
}
