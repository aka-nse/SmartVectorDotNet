using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using H = InternalHelpers;
using OP = VectorOp;

partial class VectorMath
{
    /// <summary>
    /// Calculates <c>sign(x)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sign<T>(Vector<T> x)
        where T : unmanaged
        => OP.ConditionalSelect(
            OP.Equals(x, Sign_<T>._0),
            Sign_<T>._0,
            SignFast(x));

    /// <summary>
    /// Like <c>sign(x)</c>, but returns <c>1</c> if <c>x == 0</c> for performance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Vector<T> SignFast<T>(Vector<T> x)
        where T : unmanaged
        => OP.ConditionalSelect(
            IsNonNegative(x),
            Sign_<T>._1,
            Sign_<T>._m1);

    /// <summary>
    /// For signed <typeparamref name="T"/>, determines the specified value is negative or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsNegative<T>(Vector<T> x)
        where T : unmanaged
        => OP.OnesComplement(IsNonNegative(x));


    private static Vector<T> IsNonNegative<T>(Vector<T> x)
        where T : unmanaged
    {
        static Vector<T> core<S>(Vector<T> x)
            where S : unmanaged
            => H.Reinterpret<S, T>(OP.Equals(OP.BitwiseAnd(Sign_<S>.SignBit, H.Reinterpret<T, S>(x)), Sign_<S>._0));

        return Unsafe.SizeOf<T>() switch
        {
            1 => core<sbyte>(x),
            2 => core<short>(x),
            4 => core<int>(x),
            8 => core<long>(x),
            _ => throw new NotSupportedException(),
        };
    }


    private class Sign_<T> : Const<T> where T : unmanaged
    {
        internal static readonly Vector<T> SignBit = GetSignBit();
        static Vector<T> GetSignBit() => Unsafe.SizeOf<T>() switch
        {
            1 => As<byte  >(new(0x80)),
            2 => As<ushort>(new(0x8000)),
            4 => As<uint  >(new(0x80000000)),
            8 => As<ulong >(new(0x8000000000000000)),
            _ => throw new NotSupportedException(),
        };
    }
}
