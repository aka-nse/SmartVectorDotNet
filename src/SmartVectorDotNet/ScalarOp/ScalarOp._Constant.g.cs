// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY T4. DO NOT CHANGE IT. CHANGE THE .tt FILE INSTEAD.
// </auto-generated>
using System.Numerics;
using System.Runtime.CompilerServices;
namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class ScalarOp
{
    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to <c>0</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T Zero<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(0);
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(0);
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(0);
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(0);
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(0);
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(0);
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(0);
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(0);
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(0);
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(0);
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to <c>1</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T One<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(1);
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(1);
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(1);
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(1);
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(1);
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(1);
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(1);
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(1);
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(1);
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(1);
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to <c>false</c> on SIMD operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T False<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(Constant.False_byte     );
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(Constant.False_ushort   );
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(Constant.False_uint     );
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(Constant.False_ulong    );
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(Constant.False_sbyte    );
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(Constant.False_short    );
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(Constant.False_int      );
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(Constant.False_long     );
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(Constant.False_float    );
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(Constant.False_double   );
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to <c>true</c> on SIMD operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T True<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(Constant.True_byte     );
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(Constant.True_ushort   );
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(Constant.True_uint     );
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(Constant.True_ulong    );
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(Constant.True_sbyte    );
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(Constant.True_short    );
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(Constant.True_int      );
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(Constant.True_long     );
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(Constant.True_float    );
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(Constant.True_double   );
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to minimum value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T MinValue<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(byte     .MinValue);
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(ushort   .MinValue);
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(uint     .MinValue);
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(ulong    .MinValue);
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(sbyte    .MinValue);
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(short    .MinValue);
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(int      .MinValue);
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(long     .MinValue);
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(float    .MinValue);
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(double   .MinValue);
        throw new NotSupportedException();
    }


    /// <summary>
    /// Gets a value of <typeparamref name="T"/> which is corresponding to maximum value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T MaxValue<T>() where T : unmanaged
    {
        if(typeof(T) == typeof(byte     )) return Reinterpret<byte     , T>(byte     .MaxValue);
        if(typeof(T) == typeof(ushort   )) return Reinterpret<ushort   , T>(ushort   .MaxValue);
        if(typeof(T) == typeof(uint     )) return Reinterpret<uint     , T>(uint     .MaxValue);
        if(typeof(T) == typeof(ulong    )) return Reinterpret<ulong    , T>(ulong    .MaxValue);
        if(typeof(T) == typeof(sbyte    )) return Reinterpret<sbyte    , T>(sbyte    .MaxValue);
        if(typeof(T) == typeof(short    )) return Reinterpret<short    , T>(short    .MaxValue);
        if(typeof(T) == typeof(int      )) return Reinterpret<int      , T>(int      .MaxValue);
        if(typeof(T) == typeof(long     )) return Reinterpret<long     , T>(long     .MaxValue);
        if(typeof(T) == typeof(float    )) return Reinterpret<float    , T>(float    .MaxValue);
        if(typeof(T) == typeof(double   )) return Reinterpret<double   , T>(double   .MaxValue);
        throw new NotSupportedException();
    }
}

file class Constant
{
    public static readonly byte      False_byte      = Vector.Equals(Vector<byte     >.Zero, Vector<byte     >.One )[0];
    public static readonly byte      True_byte       = Vector.Equals(Vector<byte     >.Zero, Vector<byte     >.Zero)[0];
    public static readonly ushort    False_ushort    = Vector.Equals(Vector<ushort   >.Zero, Vector<ushort   >.One )[0];
    public static readonly ushort    True_ushort     = Vector.Equals(Vector<ushort   >.Zero, Vector<ushort   >.Zero)[0];
    public static readonly uint      False_uint      = Vector.Equals(Vector<uint     >.Zero, Vector<uint     >.One )[0];
    public static readonly uint      True_uint       = Vector.Equals(Vector<uint     >.Zero, Vector<uint     >.Zero)[0];
    public static readonly ulong     False_ulong     = Vector.Equals(Vector<ulong    >.Zero, Vector<ulong    >.One )[0];
    public static readonly ulong     True_ulong      = Vector.Equals(Vector<ulong    >.Zero, Vector<ulong    >.Zero)[0];
    public static readonly sbyte     False_sbyte     = Vector.Equals(Vector<sbyte    >.Zero, Vector<sbyte    >.One )[0];
    public static readonly sbyte     True_sbyte      = Vector.Equals(Vector<sbyte    >.Zero, Vector<sbyte    >.Zero)[0];
    public static readonly short     False_short     = Vector.Equals(Vector<short    >.Zero, Vector<short    >.One )[0];
    public static readonly short     True_short      = Vector.Equals(Vector<short    >.Zero, Vector<short    >.Zero)[0];
    public static readonly int       False_int       = Vector.Equals(Vector<int      >.Zero, Vector<int      >.One )[0];
    public static readonly int       True_int        = Vector.Equals(Vector<int      >.Zero, Vector<int      >.Zero)[0];
    public static readonly long      False_long      = Vector.Equals(Vector<long     >.Zero, Vector<long     >.One )[0];
    public static readonly long      True_long       = Vector.Equals(Vector<long     >.Zero, Vector<long     >.Zero)[0];
    public static readonly float     False_float     = H.Reinterpret<int , float >(Vector.Equals(Vector<float    >.Zero, Vector<float    >.One )[0]);
    public static readonly float     True_float      = H.Reinterpret<int , float >(Vector.Equals(Vector<float    >.Zero, Vector<float    >.Zero)[0]);
    public static readonly double    False_double    = H.Reinterpret<long, double>(Vector.Equals(Vector<double   >.Zero, Vector<double   >.One )[0]);
    public static readonly double    True_double     = H.Reinterpret<long, double>(Vector.Equals(Vector<double   >.Zero, Vector<double   >.Zero)[0]);
}