using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


file class Acos_<T> : VectorOp.Const<T> where T : unmanaged { }


partial class VectorOp
{
    /// <summary>
    /// Calculates acos(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Acos<T>(Vector<T> x)
        where T : unmanaged
    {
        Guard.OnlyRealSupported<T>();
        return Acos_<T>._2 * Atan(Sqrt(Acos_<T>._1 - x * x) / (Acos_<T>._1 + x));
    }
}
