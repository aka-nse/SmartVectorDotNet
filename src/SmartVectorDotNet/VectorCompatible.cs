using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using NVector = System.Numerics.Vector;

namespace SmartVectorDotNet;


/// <summary></summary>
public static partial class VectorCompatible
{
    private const MethodImplOptions _inlining = MethodImplOptions.AggressiveInlining;

    [MethodImpl(_inlining)]
    public static Vector<T> Abs<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.Abs(value);


    [MethodImpl(_inlining)]
    public static Vector<T> Add<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Add(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> AndNot<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.AndNot(left, right);


    [MethodImpl(_inlining)]
    public static Vector<TTo> As<TFrom, TTo>(in Vector<TFrom> vector)
        where TFrom : unmanaged
        where TTo : unmanaged
#if NET6_0_OR_GREATER
        => NVector.As<TFrom, TTo>(vector);
#else
        => Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(vector));
#endif


    [MethodImpl(_inlining)]
    public static Vector<byte> AsVectorByte<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorByte(value);


    [MethodImpl(_inlining)]
    public static Vector<double> AsVectorDouble<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorDouble(value);


    [MethodImpl(_inlining)]
    public static Vector<short> AsVectorInt16<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt16(value);


    [MethodImpl(_inlining)]
    public static Vector<int> AsVectorInt32<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt32(value);


    [MethodImpl(_inlining)]
    public static Vector<long> AsVectorInt64<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt64(value);


    [MethodImpl(_inlining)]
    public static Vector<nint> AsVectorNInt<T>(in Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nint>>(ref Unsafe.AsRef(value));
#endif


    [MethodImpl(_inlining)]
    public static Vector<nuint> AsVectorNUInt<T>(in Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNUInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nuint>>(ref Unsafe.AsRef(value));
#endif


    [MethodImpl(_inlining)]
    public static Vector<sbyte> AsVectorSByte<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSByte(value);


    [MethodImpl(_inlining)]
    public static Vector<float> AsVectorSingle<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSingle(value);


    [MethodImpl(_inlining)]
    public static Vector<ushort> AsVectorUInt16<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt16(value);


    [MethodImpl(_inlining)]
    public static Vector<uint> AsVectorUInt32<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt32(value);


    [MethodImpl(_inlining)]
    public static Vector<ulong> AsVectorUInt64<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt64(value);


    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseAnd<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseAnd(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseOr<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseOr(left, right);


    [MethodImpl(_inlining)]
    public static partial Vector<double> Ceiling(in Vector<double> value);

    [MethodImpl(_inlining)]
    public static partial Vector<float> Ceiling(in Vector<float> value);

    [MethodImpl(_inlining)]
    public static Vector<T> ConditionalSelect<T>(in Vector<T> condition, in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.ConditionalSelect(condition, left, right);


    [MethodImpl(_inlining)]
    public static Vector<float> ConditionalSelect(in Vector<int> condition, in Vector<float> left, in Vector<float> right)
        => NVector.ConditionalSelect(condition, left, right);


    [MethodImpl(_inlining)]
    public static Vector<double> ConditionalSelect(in Vector<long> condition, in Vector<double> left, in Vector<double> right)
        => NVector.ConditionalSelect(condition, left, right);


    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(in Vector<long> value)
        => NVector.ConvertToDouble(value);


    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(in Vector<ulong> value)
        => NVector.ConvertToDouble(value);


    [MethodImpl(_inlining)]
    public static Vector<int> ConvertToInt32(in Vector<float> value)
        => NVector.ConvertToInt32(value);


    [MethodImpl(_inlining)]
    public static Vector<long> ConvertToInt64(in Vector<double> value)
        => NVector.ConvertToInt64(value);


    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(in Vector<int> value)
        => NVector.ConvertToSingle(value);


    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(in Vector<uint> value)
        => NVector.ConvertToSingle(value);


    [MethodImpl(_inlining)]
    public static Vector<uint> ConvertToUInt32(in Vector<float> value)
        => NVector.ConvertToUInt32(value);


    [MethodImpl(_inlining)]
    public static Vector<ulong> ConvertToUInt64(in Vector<double> value)
        => NVector.ConvertToUInt64(value);


    [MethodImpl(_inlining)]
    public static Vector<T> Divide<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Divide(left, right);


    [MethodImpl(_inlining)]
    public static T Dot<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Dot(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Equals<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Equals(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> Equals(in Vector<double> left, in Vector<double> right)
        => NVector.Equals(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> Equals(in Vector<int> left, in Vector<int> right)
        => NVector.Equals(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> Equals(in Vector<long> left, in Vector<long> right)
        => NVector.Equals(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> Equals(in Vector<float> left, in Vector<float> right)
        => NVector.Equals(left, right);


    [MethodImpl(_inlining)]
    public static bool EqualsAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAll(left, right);


    [MethodImpl(_inlining)]
    public static bool EqualsAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAny(left, right);


    [MethodImpl(_inlining)]
    public static partial Vector<double> Floor(in Vector<double> value);


    [MethodImpl(_inlining)]
    public static partial Vector<float> Floor(in Vector<float> value);


    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThan<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(in Vector<double> left, in Vector<double> right)
        => NVector.GreaterThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(in Vector<int> left, in Vector<int> right)
        => NVector.GreaterThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(in Vector<long> left, in Vector<long> right)
        => NVector.GreaterThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(in Vector<float> left, in Vector<float> right)
        => NVector.GreaterThan(left, right);


    [MethodImpl(_inlining)]
    public static bool GreaterThanAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAll(left, right);


    [MethodImpl(_inlining)]
    public static bool GreaterThanAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAny(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThanOrEqual<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(in Vector<double> left, in Vector<double> right)
        => NVector.GreaterThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(in Vector<int> left, in Vector<int> right)
        => NVector.GreaterThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(in Vector<long> left, in Vector<long> right)
        => NVector.GreaterThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(in Vector<float> left, in Vector<float> right)
        => NVector.GreaterThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAll(left, right);


    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAny(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> LessThan<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(in Vector<double> left, in Vector<double> right)
        => NVector.LessThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(in Vector<int> left, in Vector<int> right)
        => NVector.LessThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(in Vector<long> left, in Vector<long> right)
        => NVector.LessThan(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(in Vector<float> left, in Vector<float> right)
        => NVector.LessThan(left, right);


    [MethodImpl(_inlining)]
    public static bool LessThanAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAll(left, right);


    [MethodImpl(_inlining)]
    public static bool LessThanAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAny(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> LessThanOrEqual<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(in Vector<double> left, in Vector<double> right)
        => NVector.LessThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(in Vector<int> left, in Vector<int> right)
        => NVector.LessThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(in Vector<long> left, in Vector<long> right)
        => NVector.LessThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(in Vector<float> left, in Vector<float> right)
        => NVector.LessThanOrEqual(left, right);


    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAll(left, right);


    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAny(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Max<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Max(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Min<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Min(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(in Vector<T> left, T right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(T left, in Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    [MethodImpl(_inlining)]
    public static Vector<float> Narrow(in Vector<double> low, in Vector<double> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<sbyte> Narrow(in Vector<short> low, in Vector<short> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<short> Narrow(in Vector<int> low, in Vector<int> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<int> Narrow(in Vector<long> low, in Vector<long> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<byte> Narrow(in Vector<ushort> low, in Vector<ushort> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<ushort> Narrow(in Vector<uint> low, in Vector<uint> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<uint> Narrow(in Vector<ulong> low, in Vector<ulong> high)
        => NVector.Narrow(low, high);


    [MethodImpl(_inlining)]
    public static Vector<T> Negate<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.Negate(value);


    [MethodImpl(_inlining)]
    public static Vector<T> OnesComplement<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.OnesComplement(value);


    [MethodImpl(_inlining)]
    public static partial Vector<byte> ShiftLeft(in Vector<byte> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<short> ShiftLeft(in Vector<short> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<int> ShiftLeft(in Vector<int> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<long> ShiftLeft(in Vector<long> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<nint> ShiftLeft(in Vector<nint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<nuint> ShiftLeft(in Vector<nuint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<sbyte> ShiftLeft(in Vector<sbyte> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<ushort> ShiftLeft(in Vector<ushort> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<uint> ShiftLeft(in Vector<uint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<ulong> ShiftLeft(in Vector<ulong> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<short> ShiftRightArithmetic(in Vector<short> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<int> ShiftRightArithmetic(in Vector<int> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<long> ShiftRightArithmetic(in Vector<long> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<nint> ShiftRightArithmetic(in Vector<nint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<sbyte> ShiftRightArithmetic(in Vector<sbyte> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<byte> ShiftRightLogical(in Vector<byte> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<short> ShiftRightLogical(in Vector<short> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<int> ShiftRightLogical(in Vector<int> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<long> ShiftRightLogical(in Vector<long> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<nint> ShiftRightLogical(in Vector<nint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<nuint> ShiftRightLogical(in Vector<nuint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<sbyte> ShiftRightLogical(in Vector<sbyte> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<ushort> ShiftRightLogical(in Vector<ushort> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<uint> ShiftRightLogical(in Vector<uint> value, int shiftCount);


    [MethodImpl(_inlining)]
    public static partial Vector<ulong> ShiftRightLogical(in Vector<ulong> value, int shiftCount);








    [MethodImpl(_inlining)]
    public static Vector<T> SquareRoot<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.SquareRoot(value);


    [MethodImpl(_inlining)]
    public static Vector<T> Subtract<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Subtract(left, right);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<byte> source, out Vector<ushort> low, out Vector<ushort> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<short> source, out Vector<int> low, out Vector<int> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<int> source, out Vector<long> low, out Vector<long> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<sbyte> source, out Vector<short> low, out Vector<short> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<float> source, out Vector<double> low, out Vector<double> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<ushort> source, out Vector<uint> low, out Vector<uint> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static void Widen(in Vector<uint> source, out Vector<ulong> low, out Vector<ulong> high)
        => NVector.Widen(source, out low, out high);


    [MethodImpl(_inlining)]
    public static Vector<T> Xor<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Xor(left, right);


    [MethodImpl(_inlining)]
    public static T Sum<T>(in Vector<T> value)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        return NVector.Sum(value);
#else
        switch(Vector<T>.Count)
        {
        default:
            {
                var retval = ValueOperation.Zero<T>();
                for(var i = 0; i < Vector<T>.Count; ++i)
                {
                    retval = ValueOperation.Add(retval, value[i]);
                }
                return retval;
            }
        }
#endif
    }

}
