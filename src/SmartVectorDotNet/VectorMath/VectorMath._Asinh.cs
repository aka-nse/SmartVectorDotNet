namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Asinh_<T> : VectorMath.Const<T> where T : unmanaged
{
    public static readonly Vector<T> Border =
        typeof(T) == typeof(double)
            ? H.Reinterpret<double, T>(new Vector<double>(1.35e+7))
        : typeof(T) == typeof(float)
            ? H.Reinterpret<float, T>(new Vector<float>(1.35e+7f))
            : throw new ArgumentException();
}


partial class VectorMath
{
    /// <summary>
    /// Calculates asinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Asinh<T>(in Vector<T> x)
        where T : unmanaged
    {
        // NOTE:
        //    Generally `asinh` is expressed as:
        //    $$
        //    {\rm arcsinh}\ x = \ln(x + \sqrt{x^2 + 1})
        //    $$
        //    But it may cause overflow for large `x`.
        //    Therefore this implementation employs following formula:
        //    $$
        //    \mathrm{arsinh}\,x &= \ln(x + \sqrt{x^2+1})  \\
        //                       &\sim \mathrm{Sign}(x) \ln(2|x|) \sim \mathrm{Sign}(x) (2 + \ln|x|) &(x \rightarrow \pm\infty)
        //    $$
        var absX = Abs(x);
        var sign = Sign(x);
        return OP.ConditionalSelect(
            OP.LessThan(absX, Asinh_<T>.Border),
            Log(absX + Sqrt(x * x + Asinh_<T>._1)),
            Asinh_<T>.Log_E_2 + Log(absX)
            ) * sign;
    }
}
