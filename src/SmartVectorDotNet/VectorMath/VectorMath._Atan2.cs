using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Atan2_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
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
        var a = Atan(y / x);
        Vector<T> signY = SignFast(y);
        return OP.ConditionalSelect(
            OP.Equals(x, Atan2_<T>._0),
            OP.ConditionalSelect(
                OP.Equals(y, Atan2_<T>._0),
                Vector<T>.Zero,
                signY * Atan2_<T>.PI_1p2
                ),
            OP.ConditionalSelect(
                OP.GreaterThan(x, Vector<T>.Zero),
                a,
                a + signY * Atan2_<T>.PI_2p2
                )
            );
    }
}
