using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


file class Asin_<T> : VectorOp.Const<T> where T : unmanaged { }


partial class VectorOp
{
    /// <summary>
    /// Calculates asin(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Asin<T>(Vector<T> x)
        where T : unmanaged
        => Asin_<T>._2 * Atan(x / (Asin_<T>._1 + Sqrt(Asin_<T>._1 - x * x)));
}
