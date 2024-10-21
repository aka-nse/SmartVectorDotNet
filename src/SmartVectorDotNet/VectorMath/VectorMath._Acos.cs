using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

file class Acos_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
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
        => Acos_<T>._2 * Atan(Sqrt(Acos_<T>._1 - x * x) / (Acos_<T>._1 + x));
}
