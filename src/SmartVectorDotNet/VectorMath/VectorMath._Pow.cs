namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

file class Pow_<T> : VectorMath.Const<T> where T : unmanaged
{
}

partial class VectorMath
{
    /// <summary>
    /// Calculates pow(a, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Pow<T>(in Vector<T> a, in Vector<T> x)
        where T : unmanaged
    {
        var isAZero = OP.Equals(a, Pow_<T>._0);
        var isXZero = OP.Equals(x, Pow_<T>._0);
        var isANegative = OP.LessThan(a, Pow_<T>._0);
        var isXNegative = OP.LessThan(x, Pow_<T>._0);

        // if a < 0:
        //   x % 2.0 == 0.0 -> positive
        //   x % 2.0 == 1.0 -> negative
        //   else           -> NaN
        var xModBy2 = ModuloBy2(x);
        var sign = OP.ConditionalSelect(
            isANegative,
            OP.ConditionalSelect(
                OP.Equals(Pow_<T>._0, xModBy2),
                Pow_<T>._1,
                OP.ConditionalSelect(
                    OP.Equals(Pow_<T>._1, xModBy2),
                    Pow_<T>._m1,
                    Pow_<T>.NaN)),
            Pow_<T>._1);

        var aa = Abs(OP.ConditionalSelect(
            isXNegative,
            Pow_<T>._1 / a,
            a));
        var xx = OP.ConditionalSelect(
            isXNegative,
            -x,
            x);
        var exp = Exp(xx * Log(aa));

        return OP.ConditionalSelect(
            isXZero,
            Vector<T>.One,
            OP.ConditionalSelect(
                isAZero,
                OP.ConditionalSelect(isXNegative, Pow_<T>.PInf, Pow_<T>._0),
                sign * exp
                )
            );
    }
}
