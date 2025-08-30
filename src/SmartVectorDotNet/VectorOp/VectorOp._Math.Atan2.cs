using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;


file class Atan2_<T> : VectorOp.Const<T> where T : unmanaged { }


partial class VectorOp
{
    /// <summary>
    /// Calculates atan2(y, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Atan2<T>(Vector<T> y, Vector<T> x)
        where T : unmanaged
    {
        Guard.OnlyRealSupported<T>();

        var a = Atan(y / x);
        Vector<T> signY = SignFast(y);
        return ConditionalSelect(
            Equals(x, Atan2_<T>._0),
            ConditionalSelect(
                Equals(y, Atan2_<T>._0),
                Vector<T>.Zero,
                signY * Atan2_<T>.PI_1p2
                ),
            ConditionalSelect(
                GreaterThan(x, Vector<T>.Zero),
                a,
                a + signY * Atan2_<T>.PI_2p2
                )
            );
    }
}
