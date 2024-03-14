namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Log2_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> OnePerLog_E_2 = AsVector(1.0 / ScalarMath.Log(2, ScalarMath.Const<double>.E));
}


partial class VectorMath
{

    /// <summary>
    /// Calculates log2(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Log2<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x) * Log2_<T>.OnePerLog_E_2;
}
