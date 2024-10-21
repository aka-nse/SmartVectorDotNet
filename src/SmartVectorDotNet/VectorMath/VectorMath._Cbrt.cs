using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary>
    /// Calculates cbrt(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Cbrt<T>(Vector<T> x)
        where T : unmanaged
    {
        Decompose(x, out var n, out var a);
        var xn = Scale(Round(n / Const<T>._3), a);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        return Vector.ConditionalSelect(
            Vector.Equals(x, Vector<T>.Zero),
            Vector<T>.Zero,
            xn);
    }
}
