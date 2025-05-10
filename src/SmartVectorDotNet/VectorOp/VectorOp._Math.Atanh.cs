using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


file class Atanh_<T> : VectorOp.Const<T> where T : unmanaged { }


partial class VectorOp
{
    /// <summary>
    /// Calculates atanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Atanh<T>(Vector<T> x)
        where T : unmanaged
    {
        Guard.OnlyRealSupported<T>();
        var a = Atanh_<T>._1 + x;
        var b = Atanh_<T>._1 - x;
        return ConditionalSelect(
            LessThan(Abs(x), Atanh_<T>._1),
            Atanh_<T>._1p2 * Log(a / b),
            Atanh_<T>.NaN);
        ;
    }
}
