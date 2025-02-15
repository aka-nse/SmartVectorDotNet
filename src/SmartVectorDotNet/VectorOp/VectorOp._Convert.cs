using System.Runtime.CompilerServices;

namespace SmartVectorDotNet;
using NVector = System.Numerics.Vector;

partial class VectorOp
{
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

}
