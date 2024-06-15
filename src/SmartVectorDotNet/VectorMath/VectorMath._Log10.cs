namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

partial class VectorMath
{
    internal class Log10_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> OnePerLog_E_10 = AsVector(1.0 / ScalarMath.Log(10, ScalarMath.Const<double>.E));
    }

    /// <summary>
    /// Calculates log10(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Log10<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x) * Log10_<T>.OnePerLog_E_10;
}
