#if !NET7_0_OR_GREATER
#pragma warning disable CS1574
#endif

using System.Runtime.CompilerServices;
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
        if(typeof(T) == typeof(byte  )) { return H.BitCastV<byte  , T>(ShiftLeft(H.BitCastV<T, byte  >(value), shiftCount)); }
        if(typeof(T) == typeof(ushort)) { return H.BitCastV<ushort, T>(ShiftLeft(H.BitCastV<T, ushort>(value), shiftCount)); }
        if(typeof(T) == typeof(uint  )) { return H.BitCastV<uint  , T>(ShiftLeft(H.BitCastV<T, uint  >(value), shiftCount)); }
        if(typeof(T) == typeof(ulong )) { return H.BitCastV<ulong , T>(ShiftLeft(H.BitCastV<T, ulong >(value), shiftCount)); }
        if(typeof(T) == typeof(nuint )) { return H.BitCastV<nuint , T>(ShiftLeft(H.BitCastV<T, nuint >(value), shiftCount)); }
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftLeft(H.BitCastV<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftLeft(H.BitCastV<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftLeft(H.BitCastV<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftLeft(H.BitCastV<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftLeft(H.BitCastV<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftLeft. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftLeft<T>(Vector<T> value, Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.BitCastV<byte  , T>(ShiftLeft(H.BitCastV<T, byte  >(value), H.BitCastV<T, byte  >(shiftCount))); }
        if(typeof(T) == typeof(ushort)) { return H.BitCastV<ushort, T>(ShiftLeft(H.BitCastV<T, ushort>(value), H.BitCastV<T, ushort>(shiftCount))); }
        if(typeof(T) == typeof(uint  )) { return H.BitCastV<uint  , T>(ShiftLeft(H.BitCastV<T, uint  >(value), H.BitCastV<T, uint  >(shiftCount))); }
        if(typeof(T) == typeof(ulong )) { return H.BitCastV<ulong , T>(ShiftLeft(H.BitCastV<T, ulong >(value), H.BitCastV<T, ulong >(shiftCount))); }
        if(typeof(T) == typeof(nuint )) { return H.BitCastV<nuint , T>(ShiftLeft(H.BitCastV<T, nuint >(value), H.BitCastV<T, nuint >(shiftCount))); }
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftLeft(H.BitCastV<T, sbyte >(value), H.BitCastV<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftLeft(H.BitCastV<T, short >(value), H.BitCastV<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftLeft(H.BitCastV<T, int   >(value), H.BitCastV<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftLeft(H.BitCastV<T, long  >(value), H.BitCastV<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftLeft(H.BitCastV<T, nint  >(value), H.BitCastV<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightLogical. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightLogical<T>(Vector<T> value, int shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.BitCastV<byte  , T>(ShiftRightLogical(H.BitCastV<T, byte  >(value), shiftCount)); }
        if(typeof(T) == typeof(ushort)) { return H.BitCastV<ushort, T>(ShiftRightLogical(H.BitCastV<T, ushort>(value), shiftCount)); }
        if(typeof(T) == typeof(uint  )) { return H.BitCastV<uint  , T>(ShiftRightLogical(H.BitCastV<T, uint  >(value), shiftCount)); }
        if(typeof(T) == typeof(ulong )) { return H.BitCastV<ulong , T>(ShiftRightLogical(H.BitCastV<T, ulong >(value), shiftCount)); }
        if(typeof(T) == typeof(nuint )) { return H.BitCastV<nuint , T>(ShiftRightLogical(H.BitCastV<T, nuint >(value), shiftCount)); }
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftRightLogical(H.BitCastV<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftRightLogical(H.BitCastV<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftRightLogical(H.BitCastV<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftRightLogical(H.BitCastV<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftRightLogical(H.BitCastV<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightLogical. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightLogical<T>(Vector<T> value, Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(byte  )) { return H.BitCastV<byte  , T>(ShiftRightLogical(H.BitCastV<T, byte  >(value), H.BitCastV<T, byte  >(shiftCount))); }
        if(typeof(T) == typeof(ushort)) { return H.BitCastV<ushort, T>(ShiftRightLogical(H.BitCastV<T, ushort>(value), H.BitCastV<T, ushort>(shiftCount))); }
        if(typeof(T) == typeof(uint  )) { return H.BitCastV<uint  , T>(ShiftRightLogical(H.BitCastV<T, uint  >(value), H.BitCastV<T, uint  >(shiftCount))); }
        if(typeof(T) == typeof(ulong )) { return H.BitCastV<ulong , T>(ShiftRightLogical(H.BitCastV<T, ulong >(value), H.BitCastV<T, ulong >(shiftCount))); }
        if(typeof(T) == typeof(nuint )) { return H.BitCastV<nuint , T>(ShiftRightLogical(H.BitCastV<T, nuint >(value), H.BitCastV<T, nuint >(shiftCount))); }
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftRightLogical(H.BitCastV<T, sbyte >(value), H.BitCastV<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftRightLogical(H.BitCastV<T, short >(value), H.BitCastV<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftRightLogical(H.BitCastV<T, int   >(value), H.BitCastV<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftRightLogical(H.BitCastV<T, long  >(value), H.BitCastV<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftRightLogical(H.BitCastV<T, nint  >(value), H.BitCastV<T, nint  >(shiftCount))); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightArithmetic. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightArithmetic<T>(Vector<T> value, int shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftRightArithmetic(H.BitCastV<T, sbyte >(value), shiftCount)); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftRightArithmetic(H.BitCastV<T, short >(value), shiftCount)); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftRightArithmetic(H.BitCastV<T, int   >(value), shiftCount)); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftRightArithmetic(H.BitCastV<T, long  >(value), shiftCount)); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftRightArithmetic(H.BitCastV<T, nint  >(value), shiftCount)); }
        throw new NotSupportedException();
    }
    
    /** <summary> Operates ShiftRightArithmetic. </summary> **/
    [MethodImpl(_inlining)]
    public static Vector<T> ShiftRightArithmetic<T>(Vector<T> value, Vector<T> shiftCount)
        where T : unmanaged
    {
        if(typeof(T) == typeof(sbyte )) { return H.BitCastV<sbyte , T>(ShiftRightArithmetic(H.BitCastV<T, sbyte >(value), H.BitCastV<T, sbyte >(shiftCount))); }
        if(typeof(T) == typeof(short )) { return H.BitCastV<short , T>(ShiftRightArithmetic(H.BitCastV<T, short >(value), H.BitCastV<T, short >(shiftCount))); }
        if(typeof(T) == typeof(int   )) { return H.BitCastV<int   , T>(ShiftRightArithmetic(H.BitCastV<T, int   >(value), H.BitCastV<T, int   >(shiftCount))); }
        if(typeof(T) == typeof(long  )) { return H.BitCastV<long  , T>(ShiftRightArithmetic(H.BitCastV<T, long  >(value), H.BitCastV<T, long  >(shiftCount))); }
        if(typeof(T) == typeof(nint  )) { return H.BitCastV<nint  , T>(ShiftRightArithmetic(H.BitCastV<T, nint  >(value), H.BitCastV<T, nint  >(shiftCount))); }
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
                var retval = ScalarOp.Const<T>.Zero;
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
