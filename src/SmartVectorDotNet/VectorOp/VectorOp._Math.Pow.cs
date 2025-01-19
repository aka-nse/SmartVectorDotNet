namespace SmartVectorDotNet;

file class Pow_<T> : VectorOp.Const<T> where T : unmanaged
{
}

partial class VectorOp
{
    /// <summary>
    /// Calculates pow(a, x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Pow<T>(Vector<T> a, Vector<T> x)
        where T : unmanaged
    {
        var isAZero = Equals(a, Pow_<T>._0);
        var isXZero = Equals(x, Pow_<T>._0);
        var isANegative = LessThan(a, Pow_<T>._0);
        var isXNegative = LessThan(x, Pow_<T>._0);

        // if a < 0:
        //   x % 2.0 == 0.0 -> positive
        //   x % 2.0 == 1.0 -> negative
        //   else           -> NaN
        var xModBy2 = ModuloBy2(x);
        var sign = ConditionalSelect(
            isANegative,
            ConditionalSelect(
                Equals(Pow_<T>._0, xModBy2),
                Pow_<T>._1,
                ConditionalSelect(
                    Equals(Pow_<T>._1, xModBy2),
                    Pow_<T>._m1,
                    Pow_<T>.NaN)),
            Pow_<T>._1);

        var aa = Abs(ConditionalSelect(
            isXNegative,
            Pow_<T>._1 / a,
            a));
        var xx = ConditionalSelect(
            isXNegative,
            -x,
            x);
        var exp = Exp(xx * Log(aa));

        return ConditionalSelect(
            isXZero,
            Vector<T>.One,
            ConditionalSelect(
                isAZero,
                ConditionalSelect(isXNegative, Pow_<T>.PInf, Pow_<T>._0),
                sign * exp
                )
            );
    }
}
