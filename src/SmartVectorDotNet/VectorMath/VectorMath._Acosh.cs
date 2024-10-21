using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

file class Acosh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates acosh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Acosh<T>(Vector<T> x)
        where T : unmanaged
    {
        // NOTE:
        //    Generally `acosh` is expressed as:
        //    $$
        //    {\rm arccosh}\ x = \ln(x + \sqrt{x^2 - 1})
        //    $$
        //    But it may cause overflow for large `x`.
        //    Therefore this implementation employs following formula:
        //    $$
        //    {\rm arccosh}\ x &= \ln\left\{x\left(1 + \cfrac{\sqrt{x^2 - 1}}{x}\right)\right\}  \\
        //                     &= \ln\ x + \ln\left(1 + \cfrac{\sqrt{x - 1}\sqrt{x + 1}}{x}\right)
        //    $$
        return Log(Acosh_<T>._1 + Sqrt(x - Acosh_<T>._1) * Sqrt(x + Acosh_<T>._1) / x) + Log(x);
    }
}
