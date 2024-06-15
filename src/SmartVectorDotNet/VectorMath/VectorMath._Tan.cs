namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Tan_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> PI_1p4 = AsVector(0.25) * PI;
}


partial class VectorMath
{
    /// <summary>
    /// Calculates tan(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Tan<T>(in Vector<T> x)
        where T : unmanaged
    {
        var xx = ModuloByPI(x);
        var shouldReverse = OP.GreaterThan(xx, Tan_<T>.PI_1p2);
        var sign = OP.ConditionalSelect(
            shouldReverse,
            Tan_<T>._m1,
            Tan_<T>._1);
        xx = OP.ConditionalSelect(
            shouldReverse,
            Tan_<T>.PI - xx,
            xx);
        var modifiesByAdditionTheorem = OP.GreaterThan(xx, Tan_<T>.PI_1p4);
        xx = OP.ConditionalSelect(
            modifiesByAdditionTheorem,
            xx - Tan_<T>.PI_1p4,
            xx);
        var tanxx = TanBounded(xx);
        return sign * OP.ConditionalSelect(
            modifiesByAdditionTheorem,
            (Tan_<T>._1 + tanxx) / (Tan_<T>._1 - tanxx),
            tanxx);
    }

    private static Vector<T> TanBounded<T>(in Vector<T> x)
        where T : unmanaged
        => SinBounded(x) / CosBounded(x);

}
