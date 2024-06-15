#if !NET7_0_OR_GREATER
#pragma warning disable CS1574
#endif

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using NVector = System.Numerics.Vector;
using H = SmartVectorDotNet.InternalHelpers;

namespace SmartVectorDotNet;


/// <summary>
/// Provides compatibility of CSharp operators for <see cref="System.Numerics.Vector{T}"/>.
/// </summary>
public static partial class VectorOp
{
    private const MethodImplOptions _inlining = MethodImplOptions.AggressiveInlining;


    /** <summary> Operates Add. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Add<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Add(left, right);


    /** <summary> Operates AndNot. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> AndNot<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.AndNot(left, right);


    /** <summary> Operates As. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<TTo> As<TFrom, TTo>(in Vector<TFrom> vector)
        where TFrom : unmanaged
        where TTo : unmanaged
#if NET6_0_OR_GREATER
        => NVector.As<TFrom, TTo>(vector);
#else
        => Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(vector));
#endif


    /** <summary> Operates AsVectorByte. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<byte> AsVectorByte<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorByte(value);


    /** <summary> Operates AsVectorDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> AsVectorDouble<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorDouble(value);


    /** <summary> Operates AsVectorInt16. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<short> AsVectorInt16<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt16(value);


    /** <summary> Operates AsVectorInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> AsVectorInt32<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt32(value);


    /** <summary> Operates AsVectorInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> AsVectorInt64<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt64(value);


    /** <summary> Operates AsVectorNInt. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<nint> AsVectorNInt<T>(in Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nint>>(ref Unsafe.AsRef(value));
#endif


    /** <summary> Operates AsVectorNUInt. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<nuint> AsVectorNUInt<T>(in Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNUInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nuint>>(ref Unsafe.AsRef(value));
#endif


    /** <summary> Operates AsVectorSByte. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<sbyte> AsVectorSByte<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSByte(value);


    /** <summary> Operates AsVectorSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> AsVectorSingle<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSingle(value);


    /** <summary> Operates AsVectorUInt16. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ushort> AsVectorUInt16<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt16(value);


    /** <summary> Operates AsVectorUInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> AsVectorUInt32<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt32(value);


    /** <summary> Operates AsVectorUInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ulong> AsVectorUInt64<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt64(value);


    /** <summary> Operates BitwiseAnd. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseAnd<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseAnd(left, right);


    /** <summary> Operates BitwiseOr. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseOr<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseOr(left, right);


    /** <summary> Operates Ceiling. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<double> Ceiling(in Vector<double> value);

    /** <summary> Operates Ceiling. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<float> Ceiling(in Vector<float> value);

    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ConditionalSelect<T>(in Vector<T> condition, in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConditionalSelect(in Vector<int> condition, in Vector<float> left, in Vector<float> right)
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConditionalSelect(in Vector<long> condition, in Vector<double> left, in Vector<double> right)
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConvertToDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(in Vector<long> value)
        => NVector.ConvertToDouble(value);


    /** <summary> Operates ConvertToDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(in Vector<ulong> value)
        => NVector.ConvertToDouble(value);


    /** <summary> Operates ConvertToInt3. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> ConvertToInt32(in Vector<float> value)
        => NVector.ConvertToInt32(value);


    /** <summary> Operates ConvertToInt6. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> ConvertToInt64(in Vector<double> value)
        => NVector.ConvertToInt64(value);


    /** <summary> Operates ConvertToSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(in Vector<int> value)
        => NVector.ConvertToSingle(value);


    /** <summary> Operates ConvertToSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(in Vector<uint> value)
        => NVector.ConvertToSingle(value);


    /** <summary> Operates ConvertToUInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> ConvertToUInt32(in Vector<float> value)
        => NVector.ConvertToUInt32(value);


    /** <summary> Operates ConvertToUInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ulong> ConvertToUInt64(in Vector<double> value)
        => NVector.ConvertToUInt64(value);


    /** <summary> Operates Divide. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Divide<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Divide(left, right);


    /** <summary> Operates Dot. </summary> **/
    [MethodImpl(_inlining)]
    public static T Dot<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Dot(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Equals<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> Equals(in Vector<double> left, in Vector<double> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Equals(in Vector<int> left, in Vector<int> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> Equals(in Vector<long> left, in Vector<long> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Equals(in Vector<float> left, in Vector<float> right)
        => NVector.Equals(left, right);


    /** <summary> Operates EqualsAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool EqualsAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAll(left, right);


    /** <summary> Operates EqualsAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool EqualsAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAny(left, right);


    /** <summary> Operates Floor. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<double> Floor(in Vector<double> value);


    /** <summary> Operates Floor. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<float> Floor(in Vector<float> value);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThan<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(in Vector<double> left, in Vector<double> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(in Vector<int> left, in Vector<int> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(in Vector<long> left, in Vector<long> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(in Vector<float> left, in Vector<float> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThanAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAll(left, right);


    /** <summary> Operates GreaterThanAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAny(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThanOrEqual<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(in Vector<double> left, in Vector<double> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(in Vector<int> left, in Vector<int> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(in Vector<long> left, in Vector<long> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(in Vector<float> left, in Vector<float> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqualAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAll(left, right);


    /** <summary> Operates GreaterThanOrEqualAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAny(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> LessThan<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(in Vector<double> left, in Vector<double> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(in Vector<int> left, in Vector<int> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(in Vector<long> left, in Vector<long> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(in Vector<float> left, in Vector<float> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThanAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAll(left, right);


    /** <summary> Operates LessThanAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAny(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> LessThanOrEqual<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(in Vector<double> left, in Vector<double> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(in Vector<int> left, in Vector<int> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(in Vector<long> left, in Vector<long> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(in Vector<float> left, in Vector<float> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqualAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAll<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAll(left, right);


    /** <summary> Operates LessThanOrEqualAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAny<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAny(left, right);


    /** <summary> Operates Max. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Max<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Max(left, right);


    /** <summary> Operates Min. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Min<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Min(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(in Vector<T> left, T right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(T left, in Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> Narrow(in Vector<double> low, in Vector<double> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<sbyte> Narrow(in Vector<short> low, in Vector<short> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<short> Narrow(in Vector<int> low, in Vector<int> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Narrow(in Vector<long> low, in Vector<long> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<byte> Narrow(in Vector<ushort> low, in Vector<ushort> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ushort> Narrow(in Vector<uint> low, in Vector<uint> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> Narrow(in Vector<ulong> low, in Vector<ulong> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Negate. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Negate<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.Negate(value);


    /** <summary> Operates OnesComplement. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> OnesComplement<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.OnesComplement(value);

#pragma warning disable format
    
    /** <summary> Operates ShiftLeft. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftLeft<T>(in Vector<T> value, int shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>(ShiftLeft(H.Reinterpret<T, byte  >(value), shiftCount)); }
        if(typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>(ShiftLeft(H.Reinterpret<T, ushort>(value), shiftCount)); }
        if(typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>(ShiftLeft(H.Reinterpret<T, uint  >(value), shiftCount)); }
        if(typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>(ShiftLeft(H.Reinterpret<T, ulong >(value), shiftCount)); }
        if(typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>(ShiftLeft(H.Reinterpret<T, nuint >(value), shiftCount)); }
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftLeft(H.Reinterpret<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftLeft(H.Reinterpret<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftLeft(H.Reinterpret<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftLeft(H.Reinterpret<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftLeft(H.Reinterpret<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftLeft. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftLeft<T>(in Vector<T> value, in Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>(ShiftLeft(H.Reinterpret<T, byte  >(value), H.Reinterpret<T, byte  >(shiftCount))); }
        if(typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>(ShiftLeft(H.Reinterpret<T, ushort>(value), H.Reinterpret<T, ushort>(shiftCount))); }
        if(typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>(ShiftLeft(H.Reinterpret<T, uint  >(value), H.Reinterpret<T, uint  >(shiftCount))); }
        if(typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>(ShiftLeft(H.Reinterpret<T, ulong >(value), H.Reinterpret<T, ulong >(shiftCount))); }
        if(typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>(ShiftLeft(H.Reinterpret<T, nuint >(value), H.Reinterpret<T, nuint >(shiftCount))); }
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftLeft(H.Reinterpret<T, sbyte >(value), H.Reinterpret<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftLeft(H.Reinterpret<T, short >(value), H.Reinterpret<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftLeft(H.Reinterpret<T, int   >(value), H.Reinterpret<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftLeft(H.Reinterpret<T, long  >(value), H.Reinterpret<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftLeft(H.Reinterpret<T, nint  >(value), H.Reinterpret<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightLogical. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightLogical<T>(in Vector<T> value, int shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>(ShiftRightLogical(H.Reinterpret<T, byte  >(value), shiftCount)); }
        if(typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>(ShiftRightLogical(H.Reinterpret<T, ushort>(value), shiftCount)); }
        if(typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>(ShiftRightLogical(H.Reinterpret<T, uint  >(value), shiftCount)); }
        if(typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>(ShiftRightLogical(H.Reinterpret<T, ulong >(value), shiftCount)); }
        if(typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>(ShiftRightLogical(H.Reinterpret<T, nuint >(value), shiftCount)); }
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftRightLogical(H.Reinterpret<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftRightLogical(H.Reinterpret<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftRightLogical(H.Reinterpret<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftRightLogical(H.Reinterpret<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftRightLogical(H.Reinterpret<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightLogical. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightLogical<T>(in Vector<T> value, in Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.Reinterpret<byte  , T>(ShiftRightLogical(H.Reinterpret<T, byte  >(value), H.Reinterpret<T, byte  >(shiftCount))); }
        if(typeof(T) == typeof(ushort)) { return H.Reinterpret<ushort, T>(ShiftRightLogical(H.Reinterpret<T, ushort>(value), H.Reinterpret<T, ushort>(shiftCount))); }
        if(typeof(T) == typeof(uint  )) { return H.Reinterpret<uint  , T>(ShiftRightLogical(H.Reinterpret<T, uint  >(value), H.Reinterpret<T, uint  >(shiftCount))); }
        if(typeof(T) == typeof(ulong )) { return H.Reinterpret<ulong , T>(ShiftRightLogical(H.Reinterpret<T, ulong >(value), H.Reinterpret<T, ulong >(shiftCount))); }
        if(typeof(T) == typeof(nuint )) { return H.Reinterpret<nuint , T>(ShiftRightLogical(H.Reinterpret<T, nuint >(value), H.Reinterpret<T, nuint >(shiftCount))); }
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftRightLogical(H.Reinterpret<T, sbyte >(value), H.Reinterpret<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftRightLogical(H.Reinterpret<T, short >(value), H.Reinterpret<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftRightLogical(H.Reinterpret<T, int   >(value), H.Reinterpret<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftRightLogical(H.Reinterpret<T, long  >(value), H.Reinterpret<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftRightLogical(H.Reinterpret<T, nint  >(value), H.Reinterpret<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightArithmetic. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightArithmetic<T>(in Vector<T> value, int shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftRightArithmetic(H.Reinterpret<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftRightArithmetic(H.Reinterpret<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftRightArithmetic(H.Reinterpret<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftRightArithmetic(H.Reinterpret<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftRightArithmetic(H.Reinterpret<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightArithmetic. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightArithmetic<T>(in Vector<T> value, in Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftRightArithmetic(H.Reinterpret<T, sbyte >(value), H.Reinterpret<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftRightArithmetic(H.Reinterpret<T, short >(value), H.Reinterpret<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftRightArithmetic(H.Reinterpret<T, int   >(value), H.Reinterpret<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftRightArithmetic(H.Reinterpret<T, long  >(value), H.Reinterpret<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftRightArithmetic(H.Reinterpret<T, nint  >(value), H.Reinterpret<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }

    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftLeft(in Vector<byte  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftLeft(in Vector<ushort> value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftLeft(in Vector<uint  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftLeft(in Vector<ulong > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftLeft(in Vector<nuint > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftLeft(in Vector<sbyte > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftLeft(in Vector<short > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftLeft(in Vector<int   > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftLeft(in Vector<long  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftLeft(in Vector<nint  > value, int shiftCount);

    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftLeft(in Vector<byte  > value, in Vector<byte  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftLeft(in Vector<ushort> value, in Vector<ushort> shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftLeft(in Vector<uint  > value, in Vector<uint  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftLeft(in Vector<ulong > value, in Vector<ulong > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftLeft(in Vector<nuint > value, in Vector<nuint > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftLeft(in Vector<sbyte > value, in Vector<sbyte > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftLeft(in Vector<short > value, in Vector<short > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftLeft(in Vector<int   > value, in Vector<int   > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftLeft(in Vector<long  > value, in Vector<long  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftLeft(in Vector<nint  > value, in Vector<nint  > shiftCount);


    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte> ShiftRightArithmetic(in Vector<sbyte> value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short> ShiftRightArithmetic(in Vector<short> value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int  > ShiftRightArithmetic(in Vector<int  > value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long > ShiftRightArithmetic(in Vector<long > value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint > ShiftRightArithmetic(in Vector<nint > value, int shiftCount);

    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte> ShiftRightArithmetic(in Vector<sbyte> value, in Vector<sbyte> shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short> ShiftRightArithmetic(in Vector<short> value, in Vector<short> shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int  > ShiftRightArithmetic(in Vector<int  > value, in Vector<int  > shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long > ShiftRightArithmetic(in Vector<long > value, in Vector<long > shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint > ShiftRightArithmetic(in Vector<nint > value, in Vector<nint > shiftCount);


    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftRightLogical(in Vector<byte  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftRightLogical(in Vector<short > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftRightLogical(in Vector<int   > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftRightLogical(in Vector<long  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftRightLogical(in Vector<nint  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftRightLogical(in Vector<nuint > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftRightLogical(in Vector<sbyte > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftRightLogical(in Vector<ushort> value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftRightLogical(in Vector<uint  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftRightLogical(in Vector<ulong > value, int shiftCount);

    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftRightLogical(in Vector<byte  > value, in Vector<byte  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftRightLogical(in Vector<short > value, in Vector<short > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftRightLogical(in Vector<int   > value, in Vector<int   > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftRightLogical(in Vector<long  > value, in Vector<long  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftRightLogical(in Vector<nint  > value, in Vector<nint  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftRightLogical(in Vector<nuint > value, in Vector<nuint > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftRightLogical(in Vector<sbyte > value, in Vector<sbyte > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftRightLogical(in Vector<ushort> value, in Vector<ushort> shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftRightLogical(in Vector<uint  > value, in Vector<uint  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftRightLogical(in Vector<ulong > value, in Vector<ulong > shiftCount);

#pragma warning restore format

    /** <summary> Operates SquareRoot. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> SquareRoot<T>(in Vector<T> value)
        where T : unmanaged
        => NVector.SquareRoot(value);


    /** <summary> Operates Subtract. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Subtract<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Subtract(left, right);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<byte> source, out Vector<ushort> low, out Vector<ushort> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<short> source, out Vector<int> low, out Vector<int> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<int> source, out Vector<long> low, out Vector<long> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<sbyte> source, out Vector<short> low, out Vector<short> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<float> source, out Vector<double> low, out Vector<double> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<ushort> source, out Vector<uint> low, out Vector<uint> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(in Vector<uint> source, out Vector<ulong> low, out Vector<ulong> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Xor. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Xor<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => NVector.Xor(left, right);


    /** <summary> Operates Sum. </summary> **/
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
                var retval = ScalarOp.Zero<T>();
                for(var i = 0; i < Vector<T>.Count; ++i)
                {
                    retval = ScalarOp.Add(retval, value[i]);
                }
                return retval;
            }
        }
#endif
    }

}
