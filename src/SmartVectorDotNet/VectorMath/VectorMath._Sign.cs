using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorMath
{
    partial class Const
    {
        public static readonly Vector<byte  > SignBit8  = new(0x80);
        public static readonly Vector<ushort> SignBit16 = new(0x8000);
        public static readonly Vector<uint  > SignBit32 = new(0x80000000);
        public static readonly Vector<ulong > SignBit64 = new(0x8000000000000000);
    }


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
        static Vector<T> core<U>(in Vector<T> x, in Vector<U> signBit)
            where U : unmanaged
        {
            var sign = Vector.BitwiseAnd(x, Unsafe.As<Vector<U>, Vector<T>>(ref Unsafe.AsRef(signBit)));
            return Vector.BitwiseOr(Vector<T>.One, sign);
        }

        return Unsafe.SizeOf<T>() switch
        {
            1 => core(x, Const.SignBit8),
            2 => core(x, Const.SignBit16),
            4 => core(x, Const.SignBit32),
            8 => core(x, Const.SignBit64),
            _ => throw new NotSupportedException(),
        };
    }
}
