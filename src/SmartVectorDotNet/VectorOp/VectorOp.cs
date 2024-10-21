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
    public static Vector<T> Add<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Add(left, right);


    /** <summary> Operates AndNot. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> AndNot<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.AndNot(left, right);


    /** <summary> Operates As. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<TTo> As<TFrom, TTo>(Vector<TFrom> vector)
        where TFrom : unmanaged
        where TTo : unmanaged
#if NET6_0_OR_GREATER
        => NVector.As<TFrom, TTo>(vector);
#else
        => Unsafe.As<Vector<TFrom>, Vector<TTo>>(ref Unsafe.AsRef(vector));
#endif


    /** <summary> Operates AsVectorByte. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<byte> AsVectorByte<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorByte(value);


    /** <summary> Operates AsVectorDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> AsVectorDouble<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorDouble(value);


    /** <summary> Operates AsVectorInt16. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<short> AsVectorInt16<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt16(value);


    /** <summary> Operates AsVectorInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> AsVectorInt32<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt32(value);


    /** <summary> Operates AsVectorInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> AsVectorInt64<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorInt64(value);


    /** <summary> Operates AsVectorNInt. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<nint> AsVectorNInt<T>(Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nint>>(ref Unsafe.AsRef(value));
#endif


    /** <summary> Operates AsVectorNUInt. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<nuint> AsVectorNUInt<T>(Vector<T> value)
        where T : unmanaged
#if NET6_0_OR_GREATER
        => NVector.AsVectorNUInt(value);
#else
        => Unsafe.As<Vector<T>, Vector<nuint>>(ref Unsafe.AsRef(value));
#endif


    /** <summary> Operates AsVectorSByte. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<sbyte> AsVectorSByte<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSByte(value);


    /** <summary> Operates AsVectorSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> AsVectorSingle<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorSingle(value);


    /** <summary> Operates AsVectorUInt16. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ushort> AsVectorUInt16<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt16(value);


    /** <summary> Operates AsVectorUInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> AsVectorUInt32<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt32(value);


    /** <summary> Operates AsVectorUInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ulong> AsVectorUInt64<T>(Vector<T> value)
        where T : unmanaged
        => NVector.AsVectorUInt64(value);


    /** <summary> Operates BitwiseAnd. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseAnd<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseAnd(left, right);


    /** <summary> Operates BitwiseOr. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> BitwiseOr<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.BitwiseOr(left, right);


    /** <summary> Operates Ceiling. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<double> Ceiling(Vector<double> value);

    /** <summary> Operates Ceiling. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<float> Ceiling(Vector<float> value);

    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ConditionalSelect<T>(Vector<T> condition, Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConditionalSelect(Vector<int> condition, Vector<float> left, Vector<float> right)
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConditionalSelect. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConditionalSelect(Vector<long> condition, Vector<double> left, Vector<double> right)
        => NVector.ConditionalSelect(condition, left, right);


    /** <summary> Operates ConvertToDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(Vector<long> value)
        => NVector.ConvertToDouble(value);


    /** <summary> Operates ConvertToDouble. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<double> ConvertToDouble(Vector<ulong> value)
        => NVector.ConvertToDouble(value);


    /** <summary> Operates ConvertToInt3. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> ConvertToInt32(Vector<float> value)
        => NVector.ConvertToInt32(value);


    /** <summary> Operates ConvertToInt6. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> ConvertToInt64(Vector<double> value)
        => NVector.ConvertToInt64(value);


    /** <summary> Operates ConvertToSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(Vector<int> value)
        => NVector.ConvertToSingle(value);


    /** <summary> Operates ConvertToSingle. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> ConvertToSingle(Vector<uint> value)
        => NVector.ConvertToSingle(value);


    /** <summary> Operates ConvertToUInt32. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> ConvertToUInt32(Vector<float> value)
        => NVector.ConvertToUInt32(value);


    /** <summary> Operates ConvertToUInt64. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ulong> ConvertToUInt64(Vector<double> value)
        => NVector.ConvertToUInt64(value);


    /** <summary> Operates Divide. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Divide<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Divide(left, right);


    /** <summary> Operates Dot. </summary> **/
    [MethodImpl(_inlining)]
    public static T Dot<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Dot(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Equals<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> Equals(Vector<double> left, Vector<double> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Equals(Vector<int> left, Vector<int> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> Equals(Vector<long> left, Vector<long> right)
        => NVector.Equals(left, right);


    /** <summary> Operates Equals. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Equals(Vector<float> left, Vector<float> right)
        => NVector.Equals(left, right);


    /** <summary> Operates EqualsAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool EqualsAll<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAll(left, right);


    /** <summary> Operates EqualsAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool EqualsAny<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.EqualsAny(left, right);


    /** <summary> Operates Floor. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<double> Floor(Vector<double> value);


    /** <summary> Operates Floor. </summary> **/
    [MethodImpl(_inlining)]
    public static partial Vector<float> Floor(Vector<float> value);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThan<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(Vector<double> left, Vector<double> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(Vector<int> left, Vector<int> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThan(Vector<long> left, Vector<long> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThan(Vector<float> left, Vector<float> right)
        => NVector.GreaterThan(left, right);


    /** <summary> Operates GreaterThanAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanAll<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAll(left, right);


    /** <summary> Operates GreaterThanAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanAny<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanAny(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> GreaterThanOrEqual<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(Vector<double> left, Vector<double> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(Vector<int> left, Vector<int> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> GreaterThanOrEqual(Vector<long> left, Vector<long> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> GreaterThanOrEqual(Vector<float> left, Vector<float> right)
        => NVector.GreaterThanOrEqual(left, right);


    /** <summary> Operates GreaterThanOrEqualAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAll<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAll(left, right);


    /** <summary> Operates GreaterThanOrEqualAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool GreaterThanOrEqualAny<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.GreaterThanOrEqualAny(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> LessThan<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(Vector<double> left, Vector<double> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(Vector<int> left, Vector<int> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThan(Vector<long> left, Vector<long> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThan. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThan(Vector<float> left, Vector<float> right)
        => NVector.LessThan(left, right);


    /** <summary> Operates LessThanAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanAll<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAll(left, right);


    /** <summary> Operates LessThanAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanAny<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThanAny(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> LessThanOrEqual<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(Vector<double> left, Vector<double> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(Vector<int> left, Vector<int> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<long> LessThanOrEqual(Vector<long> left, Vector<long> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqual. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> LessThanOrEqual(Vector<float> left, Vector<float> right)
        => NVector.LessThanOrEqual(left, right);


    /** <summary> Operates LessThanOrEqualAll. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAll<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAll(left, right);


    /** <summary> Operates LessThanOrEqualAny. </summary> **/
    [MethodImpl(_inlining)]
    public static bool LessThanOrEqualAny<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.LessThanOrEqualAny(left, right);


    /** <summary> Operates Max. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Max<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Max(left, right);


    /** <summary> Operates Min. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Min<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Min(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(Vector<T> left, T right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Multiply. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Multiply<T>(T left, Vector<T> right)
        where T : unmanaged
        => NVector.Multiply(left, right);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<float> Narrow(Vector<double> low, Vector<double> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<sbyte> Narrow(Vector<short> low, Vector<short> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<short> Narrow(Vector<int> low, Vector<int> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<int> Narrow(Vector<long> low, Vector<long> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<byte> Narrow(Vector<ushort> low, Vector<ushort> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<ushort> Narrow(Vector<uint> low, Vector<uint> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Narrow. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<uint> Narrow(Vector<ulong> low, Vector<ulong> high)
        => NVector.Narrow(low, high);


    /** <summary> Operates Negate. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Negate<T>(Vector<T> value)
        where T : unmanaged
        => NVector.Negate(value);


    /** <summary> Operates OnesComplement. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> OnesComplement<T>(Vector<T> value)
        where T : unmanaged
        => NVector.OnesComplement(value);

#pragma warning disable format
    
    /** <summary> Operates ShiftLeft. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftLeft<T>(Vector<T> value, int shiftCount)
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
    public static Vector<T> ShiftLeft<T>(Vector<T> value, Vector<T> shiftCount)
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
    public static Vector<T> ShiftRightLogical<T>(Vector<T> value, int shiftCount)
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
    public static Vector<T> ShiftRightLogical<T>(Vector<T> value, Vector<T> shiftCount)
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
    public static Vector<T> ShiftRightArithmetic<T>(Vector<T> value, int shiftCount)
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
    public static Vector<T> ShiftRightArithmetic<T>(Vector<T> value, Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) { return H.Reinterpret<sbyte , T>(ShiftRightArithmetic(H.Reinterpret<T, sbyte >(value), H.Reinterpret<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.Reinterpret<short , T>(ShiftRightArithmetic(H.Reinterpret<T, short >(value), H.Reinterpret<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.Reinterpret<int   , T>(ShiftRightArithmetic(H.Reinterpret<T, int   >(value), H.Reinterpret<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.Reinterpret<long  , T>(ShiftRightArithmetic(H.Reinterpret<T, long  >(value), H.Reinterpret<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.Reinterpret<nint  , T>(ShiftRightArithmetic(H.Reinterpret<T, nint  >(value), H.Reinterpret<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }

    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftLeft(Vector<byte  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftLeft(Vector<ushort> value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftLeft(Vector<uint  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftLeft(Vector<ulong > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftLeft(Vector<nuint > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftLeft(Vector<sbyte > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftLeft(Vector<short > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftLeft(Vector<int   > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftLeft(Vector<long  > value, int shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftLeft(Vector<nint  > value, int shiftCount);

    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftLeft(Vector<byte  > value, Vector<byte  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftLeft(Vector<ushort> value, Vector<ushort> shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftLeft(Vector<uint  > value, Vector<uint  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftLeft(Vector<ulong > value, Vector<ulong > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftLeft(Vector<nuint > value, Vector<nuint > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftLeft(Vector<sbyte > value, Vector<sbyte > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftLeft(Vector<short > value, Vector<short > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftLeft(Vector<int   > value, Vector<int   > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftLeft(Vector<long  > value, Vector<long  > shiftCount);
    /** <summary> Operates ShiftLeft. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftLeft(Vector<nint  > value, Vector<nint  > shiftCount);


    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte> ShiftRightArithmetic(Vector<sbyte> value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short> ShiftRightArithmetic(Vector<short> value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int  > ShiftRightArithmetic(Vector<int  > value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long > ShiftRightArithmetic(Vector<long > value, int shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint > ShiftRightArithmetic(Vector<nint > value, int shiftCount);

    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte> ShiftRightArithmetic(Vector<sbyte> value, Vector<sbyte> shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short> ShiftRightArithmetic(Vector<short> value, Vector<short> shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int  > ShiftRightArithmetic(Vector<int  > value, Vector<int  > shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long > ShiftRightArithmetic(Vector<long > value, Vector<long > shiftCount);
    /** <summary> Operates ShiftRightArithmetic. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint > ShiftRightArithmetic(Vector<nint > value, Vector<nint > shiftCount);


    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftRightLogical(Vector<byte  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftRightLogical(Vector<short > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftRightLogical(Vector<int   > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftRightLogical(Vector<long  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftRightLogical(Vector<nint  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftRightLogical(Vector<nuint > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftRightLogical(Vector<sbyte > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftRightLogical(Vector<ushort> value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftRightLogical(Vector<uint  > value, int shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftRightLogical(Vector<ulong > value, int shiftCount);

    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<byte  > ShiftRightLogical(Vector<byte  > value, Vector<byte  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<short > ShiftRightLogical(Vector<short > value, Vector<short > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<int   > ShiftRightLogical(Vector<int   > value, Vector<int   > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<long  > ShiftRightLogical(Vector<long  > value, Vector<long  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nint  > ShiftRightLogical(Vector<nint  > value, Vector<nint  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<nuint > ShiftRightLogical(Vector<nuint > value, Vector<nuint > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<sbyte > ShiftRightLogical(Vector<sbyte > value, Vector<sbyte > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ushort> ShiftRightLogical(Vector<ushort> value, Vector<ushort> shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<uint  > ShiftRightLogical(Vector<uint  > value, Vector<uint  > shiftCount);
    /** <summary> Operates ShiftRightLogical. </summary> **/ [MethodImpl(_inlining)] public static partial Vector<ulong > ShiftRightLogical(Vector<ulong > value, Vector<ulong > shiftCount);

#pragma warning restore format

    /** <summary> Operates SquareRoot. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> SquareRoot<T>(Vector<T> value)
        where T : unmanaged
        => NVector.SquareRoot(value);


    /** <summary> Operates Subtract. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Subtract<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Subtract(left, right);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<byte> source, out Vector<ushort> low, out Vector<ushort> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<short> source, out Vector<int> low, out Vector<int> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<int> source, out Vector<long> low, out Vector<long> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<sbyte> source, out Vector<short> low, out Vector<short> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<float> source, out Vector<double> low, out Vector<double> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<ushort> source, out Vector<uint> low, out Vector<uint> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Widen. </summary> **/
    [MethodImpl(_inlining)]
    public static void Widen(Vector<uint> source, out Vector<ulong> low, out Vector<ulong> high)
        => NVector.Widen(source, out low, out high);


    /** <summary> Operates Xor. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> Xor<T>(Vector<T> left, Vector<T> right)
        where T : unmanaged
        => NVector.Xor(left, right);


    /** <summary> Operates Sum. </summary> **/
    [MethodImpl(_inlining)]
    public static T Sum<T>(Vector<T> value)
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
