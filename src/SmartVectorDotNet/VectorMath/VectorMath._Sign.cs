using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorMath
{
    /// <summary>
    /// Like <c>sign(x)</c>, but returns <c>1</c> if <c>x == 0</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Vector<T> SignFast<T>(in Vector<T> x)
        where T : unmanaged
    {
        var sign = Vector.BitwiseAnd(x, Sign_<T>.SignBit);
        return Vector.BitwiseOr(Sign_<T>._1, sign);
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
