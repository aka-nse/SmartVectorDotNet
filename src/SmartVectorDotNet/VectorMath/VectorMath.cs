using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.InteropServices;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using H = InternalHelpers;


/// <summary>
/// Provides vectorized operators of <see cref="System.Math"/>.
/// </summary>
public static partial class VectorMath
{
    #region utilities

    [AttributeUsage(AttributeTargets.Method)]
    private class VectorMathAttribute : Attribute { }


    /// <summary>
    /// Provides constant definitions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Const<T>
        where T : unmanaged
    {
        private protected static bool IsT<TEntity>()
            => typeof(T) == typeof(TEntity);

#pragma warning disable format
        private protected static Vector<T> AsVector(double x)
            => IsT<double>() ? As(new Vector<double>(x))
             : IsT<float >() ? As(new Vector<float >((float )x))
             : IsT<byte  >() ? As(new Vector<byte  >((byte  )x))
             : IsT<ushort>() ? As(new Vector<ushort>((ushort)x))
             : IsT<uint  >() ? As(new Vector<uint  >((uint  )x))
             : IsT<ulong >() ? As(new Vector<ulong >((ulong )x))
             : IsT<sbyte >() ? As(new Vector<sbyte >((sbyte )x))
             : IsT<short >() ? As(new Vector<short >((short )x))
             : IsT<int   >() ? As(new Vector<int   >((int   )x))
             : IsT<long  >() ? As(new Vector<long  >((long  )x))
            : default;
#pragma warning restore format

        private protected static Vector<T> As<TFrom>(in Vector<TFrom> x)
            where TFrom : unmanaged
            => H.Reinterpret<TFrom, T>(x);

        /// <summary> Vectorized Napier's constant. </summary>
        public static readonly Vector<T> E = new (ScalarMath.Const<T>.E);

        /// <summary> Vectorized Pi. </summary>
        public static readonly Vector<T> PI = new(ScalarMath.Const<T>.PI);

        internal static readonly Vector<T> PI_1p2 = AsVector(0.5) * PI;
        internal static readonly Vector<T> PI_2p2 = AsVector(1.0) * PI;
        internal static readonly Vector<T> PI_3p2 = AsVector(1.5) * PI;
        internal static readonly Vector<T> PI_4p2 = AsVector(2.0) * PI;

        internal static readonly Vector<T> NaN
            = IsT<double>()
                ? As(new Vector<double>(double.NaN))
            : IsT<float>()
                ? As(new Vector<float>(float.NaN))
            : default;

        internal static readonly Vector<T> PInf
            = IsT<double>()
                ? As(new Vector<double>(double.PositiveInfinity))
            : IsT<float>()
                ? As(new Vector<float>(float.PositiveInfinity))
            : default;

        internal static readonly Vector<T> NInf
            = IsT<double>()
                ? As(new Vector<double>(double.NegativeInfinity))
            : IsT<float>()
                ? As(new Vector<float>(float.NegativeInfinity))
            : default;


        internal static readonly Vector<T> _m1
            = AsVector(-1.0);

        internal static readonly Vector<T> _0
            = AsVector(0.0);

        internal static readonly Vector<T> _1p2
            = AsVector(0.5);

        internal static readonly Vector<T> _2p3
            = AsVector(2.0/3.0);

        internal static readonly Vector<T> _1
            = AsVector(1.0);

        internal static readonly Vector<T> _Sqrt2
            = AsVector(ScalarMath.Sqrt(2.0));

        internal static readonly Vector<T> _2
            = AsVector(2.0);

        internal static readonly Vector<T> _3
            = AsVector(3.0);


        private protected Const() { }
    }


    private interface IOperation1<T> { public T Calculate(T x); }
    private static Vector<T> Emulate<T, TOperation>(in Vector<T> x)
        where T : unmanaged
        where TOperation : unmanaged, IOperation1<T>
    {
        switch (Vector<T>.Count)
        {
        case 2:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
            });
        case 4:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
                default(TOperation).Calculate(x[0x02]),
                default(TOperation).Calculate(x[0x03]),
            });
        case 8:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
                default(TOperation).Calculate(x[0x02]),
                default(TOperation).Calculate(x[0x03]),
                default(TOperation).Calculate(x[0x04]),
                default(TOperation).Calculate(x[0x05]),
                default(TOperation).Calculate(x[0x06]),
                default(TOperation).Calculate(x[0x07]),
            });
        case 16:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
                default(TOperation).Calculate(x[0x02]),
                default(TOperation).Calculate(x[0x03]),
                default(TOperation).Calculate(x[0x04]),
                default(TOperation).Calculate(x[0x05]),
                default(TOperation).Calculate(x[0x06]),
                default(TOperation).Calculate(x[0x07]),
                default(TOperation).Calculate(x[0x08]),
                default(TOperation).Calculate(x[0x09]),
                default(TOperation).Calculate(x[0x0A]),
                default(TOperation).Calculate(x[0x0B]),
                default(TOperation).Calculate(x[0x0C]),
                default(TOperation).Calculate(x[0x0D]),
                default(TOperation).Calculate(x[0x0E]),
                default(TOperation).Calculate(x[0x0F]),
            });
        case 32:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
                default(TOperation).Calculate(x[0x02]),
                default(TOperation).Calculate(x[0x03]),
                default(TOperation).Calculate(x[0x04]),
                default(TOperation).Calculate(x[0x05]),
                default(TOperation).Calculate(x[0x06]),
                default(TOperation).Calculate(x[0x07]),
                default(TOperation).Calculate(x[0x08]),
                default(TOperation).Calculate(x[0x09]),
                default(TOperation).Calculate(x[0x0A]),
                default(TOperation).Calculate(x[0x0B]),
                default(TOperation).Calculate(x[0x0C]),
                default(TOperation).Calculate(x[0x0D]),
                default(TOperation).Calculate(x[0x0E]),
                default(TOperation).Calculate(x[0x0F]),
                default(TOperation).Calculate(x[0x10]),
                default(TOperation).Calculate(x[0x11]),
                default(TOperation).Calculate(x[0x12]),
                default(TOperation).Calculate(x[0x13]),
                default(TOperation).Calculate(x[0x14]),
                default(TOperation).Calculate(x[0x15]),
                default(TOperation).Calculate(x[0x16]),
                default(TOperation).Calculate(x[0x17]),
                default(TOperation).Calculate(x[0x18]),
                default(TOperation).Calculate(x[0x19]),
                default(TOperation).Calculate(x[0x1A]),
                default(TOperation).Calculate(x[0x1B]),
                default(TOperation).Calculate(x[0x1C]),
                default(TOperation).Calculate(x[0x1D]),
                default(TOperation).Calculate(x[0x1E]),
                default(TOperation).Calculate(x[0x1F]),
            });
        case 64:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00]),
                default(TOperation).Calculate(x[0x01]),
                default(TOperation).Calculate(x[0x02]),
                default(TOperation).Calculate(x[0x03]),
                default(TOperation).Calculate(x[0x04]),
                default(TOperation).Calculate(x[0x05]),
                default(TOperation).Calculate(x[0x06]),
                default(TOperation).Calculate(x[0x07]),
                default(TOperation).Calculate(x[0x08]),
                default(TOperation).Calculate(x[0x09]),
                default(TOperation).Calculate(x[0x0A]),
                default(TOperation).Calculate(x[0x0B]),
                default(TOperation).Calculate(x[0x0C]),
                default(TOperation).Calculate(x[0x0D]),
                default(TOperation).Calculate(x[0x0E]),
                default(TOperation).Calculate(x[0x0F]),
                default(TOperation).Calculate(x[0x10]),
                default(TOperation).Calculate(x[0x11]),
                default(TOperation).Calculate(x[0x12]),
                default(TOperation).Calculate(x[0x13]),
                default(TOperation).Calculate(x[0x14]),
                default(TOperation).Calculate(x[0x15]),
                default(TOperation).Calculate(x[0x16]),
                default(TOperation).Calculate(x[0x17]),
                default(TOperation).Calculate(x[0x18]),
                default(TOperation).Calculate(x[0x19]),
                default(TOperation).Calculate(x[0x1A]),
                default(TOperation).Calculate(x[0x1B]),
                default(TOperation).Calculate(x[0x1C]),
                default(TOperation).Calculate(x[0x1D]),
                default(TOperation).Calculate(x[0x1E]),
                default(TOperation).Calculate(x[0x1F]),
                default(TOperation).Calculate(x[0x20]),
                default(TOperation).Calculate(x[0x21]),
                default(TOperation).Calculate(x[0x22]),
                default(TOperation).Calculate(x[0x23]),
                default(TOperation).Calculate(x[0x24]),
                default(TOperation).Calculate(x[0x25]),
                default(TOperation).Calculate(x[0x26]),
                default(TOperation).Calculate(x[0x27]),
                default(TOperation).Calculate(x[0x28]),
                default(TOperation).Calculate(x[0x29]),
                default(TOperation).Calculate(x[0x2A]),
                default(TOperation).Calculate(x[0x2B]),
                default(TOperation).Calculate(x[0x2C]),
                default(TOperation).Calculate(x[0x2D]),
                default(TOperation).Calculate(x[0x2E]),
                default(TOperation).Calculate(x[0x2F]),
                default(TOperation).Calculate(x[0x30]),
                default(TOperation).Calculate(x[0x31]),
                default(TOperation).Calculate(x[0x32]),
                default(TOperation).Calculate(x[0x33]),
                default(TOperation).Calculate(x[0x34]),
                default(TOperation).Calculate(x[0x35]),
                default(TOperation).Calculate(x[0x36]),
                default(TOperation).Calculate(x[0x37]),
                default(TOperation).Calculate(x[0x38]),
                default(TOperation).Calculate(x[0x39]),
                default(TOperation).Calculate(x[0x3A]),
                default(TOperation).Calculate(x[0x3B]),
                default(TOperation).Calculate(x[0x3C]),
                default(TOperation).Calculate(x[0x3D]),
                default(TOperation).Calculate(x[0x3E]),
                default(TOperation).Calculate(x[0x3F]),
            });
        default:
            {
                var buffer = (stackalloc T[Vector<T>.Count]);
                for (var i = 0; i < buffer.Length; ++i)
                {
                    buffer[i] = default(TOperation).Calculate(x[i]);
                }
                return H.CreateVector<T>(buffer);
            }
        }
    }

    private interface IOperation2<T> { public T Calculate(T x, T y); }
    private static Vector<T> Emulate<T, TOperation>(in Vector<T> x, in Vector<T> y)
        where T : unmanaged
        where TOperation : unmanaged, IOperation2<T>
    {
        switch (Vector<T>.Count)
        {
        case 2:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
            });
        case 4:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03]),
            });
        case 8:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07]),
            });
        case 16:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07]),
                default(TOperation).Calculate(x[0x08], y[0x08]),
                default(TOperation).Calculate(x[0x09], y[0x09]),
                default(TOperation).Calculate(x[0x0A], y[0x0A]),
                default(TOperation).Calculate(x[0x0B], y[0x0B]),
                default(TOperation).Calculate(x[0x0C], y[0x0C]),
                default(TOperation).Calculate(x[0x0D], y[0x0D]),
                default(TOperation).Calculate(x[0x0E], y[0x0E]),
                default(TOperation).Calculate(x[0x0F], y[0x0F]),
            });
        case 32:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07]),
                default(TOperation).Calculate(x[0x08], y[0x08]),
                default(TOperation).Calculate(x[0x09], y[0x09]),
                default(TOperation).Calculate(x[0x0A], y[0x0A]),
                default(TOperation).Calculate(x[0x0B], y[0x0B]),
                default(TOperation).Calculate(x[0x0C], y[0x0C]),
                default(TOperation).Calculate(x[0x0D], y[0x0D]),
                default(TOperation).Calculate(x[0x0E], y[0x0E]),
                default(TOperation).Calculate(x[0x0F], y[0x0F]),
                default(TOperation).Calculate(x[0x10], y[0x10]),
                default(TOperation).Calculate(x[0x11], y[0x11]),
                default(TOperation).Calculate(x[0x12], y[0x12]),
                default(TOperation).Calculate(x[0x13], y[0x13]),
                default(TOperation).Calculate(x[0x14], y[0x14]),
                default(TOperation).Calculate(x[0x15], y[0x15]),
                default(TOperation).Calculate(x[0x16], y[0x16]),
                default(TOperation).Calculate(x[0x17], y[0x17]),
                default(TOperation).Calculate(x[0x18], y[0x18]),
                default(TOperation).Calculate(x[0x19], y[0x19]),
                default(TOperation).Calculate(x[0x1A], y[0x1A]),
                default(TOperation).Calculate(x[0x1B], y[0x1B]),
                default(TOperation).Calculate(x[0x1C], y[0x1C]),
                default(TOperation).Calculate(x[0x1D], y[0x1D]),
                default(TOperation).Calculate(x[0x1E], y[0x1E]),
                default(TOperation).Calculate(x[0x1F], y[0x1F]),
            });
        case 64:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07]),
                default(TOperation).Calculate(x[0x08], y[0x08]),
                default(TOperation).Calculate(x[0x09], y[0x09]),
                default(TOperation).Calculate(x[0x0A], y[0x0A]),
                default(TOperation).Calculate(x[0x0B], y[0x0B]),
                default(TOperation).Calculate(x[0x0C], y[0x0C]),
                default(TOperation).Calculate(x[0x0D], y[0x0D]),
                default(TOperation).Calculate(x[0x0E], y[0x0E]),
                default(TOperation).Calculate(x[0x0F], y[0x0F]),
                default(TOperation).Calculate(x[0x10], y[0x10]),
                default(TOperation).Calculate(x[0x11], y[0x11]),
                default(TOperation).Calculate(x[0x12], y[0x12]),
                default(TOperation).Calculate(x[0x13], y[0x13]),
                default(TOperation).Calculate(x[0x14], y[0x14]),
                default(TOperation).Calculate(x[0x15], y[0x15]),
                default(TOperation).Calculate(x[0x16], y[0x16]),
                default(TOperation).Calculate(x[0x17], y[0x17]),
                default(TOperation).Calculate(x[0x18], y[0x18]),
                default(TOperation).Calculate(x[0x19], y[0x19]),
                default(TOperation).Calculate(x[0x1A], y[0x1A]),
                default(TOperation).Calculate(x[0x1B], y[0x1B]),
                default(TOperation).Calculate(x[0x1C], y[0x1C]),
                default(TOperation).Calculate(x[0x1D], y[0x1D]),
                default(TOperation).Calculate(x[0x1E], y[0x1E]),
                default(TOperation).Calculate(x[0x1F], y[0x1F]),
                default(TOperation).Calculate(x[0x20], y[0x20]),
                default(TOperation).Calculate(x[0x21], y[0x21]),
                default(TOperation).Calculate(x[0x22], y[0x22]),
                default(TOperation).Calculate(x[0x23], y[0x23]),
                default(TOperation).Calculate(x[0x24], y[0x24]),
                default(TOperation).Calculate(x[0x25], y[0x25]),
                default(TOperation).Calculate(x[0x26], y[0x26]),
                default(TOperation).Calculate(x[0x27], y[0x27]),
                default(TOperation).Calculate(x[0x28], y[0x28]),
                default(TOperation).Calculate(x[0x29], y[0x29]),
                default(TOperation).Calculate(x[0x2A], y[0x2A]),
                default(TOperation).Calculate(x[0x2B], y[0x2B]),
                default(TOperation).Calculate(x[0x2C], y[0x2C]),
                default(TOperation).Calculate(x[0x2D], y[0x2D]),
                default(TOperation).Calculate(x[0x2E], y[0x2E]),
                default(TOperation).Calculate(x[0x2F], y[0x2F]),
                default(TOperation).Calculate(x[0x30], y[0x30]),
                default(TOperation).Calculate(x[0x31], y[0x31]),
                default(TOperation).Calculate(x[0x32], y[0x32]),
                default(TOperation).Calculate(x[0x33], y[0x33]),
                default(TOperation).Calculate(x[0x34], y[0x34]),
                default(TOperation).Calculate(x[0x35], y[0x35]),
                default(TOperation).Calculate(x[0x36], y[0x36]),
                default(TOperation).Calculate(x[0x37], y[0x37]),
                default(TOperation).Calculate(x[0x38], y[0x38]),
                default(TOperation).Calculate(x[0x39], y[0x39]),
                default(TOperation).Calculate(x[0x3A], y[0x3A]),
                default(TOperation).Calculate(x[0x3B], y[0x3B]),
                default(TOperation).Calculate(x[0x3C], y[0x3C]),
                default(TOperation).Calculate(x[0x3D], y[0x3D]),
                default(TOperation).Calculate(x[0x3E], y[0x3E]),
                default(TOperation).Calculate(x[0x3F], y[0x3F]),
            });
        default:
            {
                var buffer = (stackalloc T[Vector<T>.Count]);
                for (var i = 0; i < buffer.Length; ++i)
                {
                    buffer[i] = default(TOperation).Calculate(x[i], y[i]);
                }
                return H.CreateVector<T>(buffer);
            }
        }
    }

    private interface IOperation3<T> { public T Calculate(T x, T y, T z); }
    private static Vector<T> Emulate<T, TOperation>(in Vector<T> x, in Vector<T> y, in Vector<T> z)
        where T : unmanaged
        where TOperation : unmanaged, IOperation3<T>
    {
        switch (Vector<T>.Count)
        {
        case 2:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00], z[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01], z[0x01]),
            });
        case 4:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00], z[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01], z[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02], z[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03], z[0x03]),
            });
        case 8:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00], z[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01], z[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02], z[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03], z[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04], z[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05], z[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06], z[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07], z[0x07]),
            });
        case 16:
            return H.CreateVector<T>(stackalloc[] {
                default(TOperation).Calculate(x[0x00], y[0x00], z[0x00]),
                default(TOperation).Calculate(x[0x01], y[0x01], z[0x01]),
                default(TOperation).Calculate(x[0x02], y[0x02], z[0x02]),
                default(TOperation).Calculate(x[0x03], y[0x03], z[0x03]),
                default(TOperation).Calculate(x[0x04], y[0x04], z[0x04]),
                default(TOperation).Calculate(x[0x05], y[0x05], z[0x05]),
                default(TOperation).Calculate(x[0x06], y[0x06], z[0x06]),
                default(TOperation).Calculate(x[0x07], y[0x07], z[0x07]),
                default(TOperation).Calculate(x[0x08], y[0x08], z[0x08]),
                default(TOperation).Calculate(x[0x09], y[0x09], z[0x09]),
                default(TOperation).Calculate(x[0x0A], y[0x0A], z[0x0A]),
                default(TOperation).Calculate(x[0x0B], y[0x0B], z[0x0B]),
                default(TOperation).Calculate(x[0x0C], y[0x0C], z[0x0C]),
                default(TOperation).Calculate(x[0x0D], y[0x0D], z[0x0D]),
                default(TOperation).Calculate(x[0x0E], y[0x0E], z[0x0E]),
                default(TOperation).Calculate(x[0x0F], y[0x0F], z[0x0F]),
            });
        default:
            {
                var buffer = (stackalloc T[Vector<T>.Count]);
                for (var i = 0; i < buffer.Length; ++i)
                {
                    buffer[i] = default(TOperation).Calculate(x[i], y[i], z[i]);
                }
                return H.CreateVector<T>(buffer);
            }
        }
    }

#endregion

    #region Abs

    /// <summary> Calculates abs. </summary>
    public static Vector<T> Abs<T>(in Vector<T> d)
        where T : unmanaged
        => VectorOp.Abs(d);

    #endregion

    #region Ceiling

    /// <summary> Calculates Ceiling. </summary>
    [VectorMath]
    public static partial Vector<T> Ceiling<T>(in Vector<T> d)
        where T : unmanaged;
    private struct Ceiling_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Ceiling(x);
    }

    private static Vector<double> Ceiling(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Ceiling(x);
#else
        return Emulate<double, Ceiling_<double>>(x);
#endif
    }

    private static Vector<float> Ceiling(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Ceiling(x);
#else
        return Emulate<float, Ceiling_<float>>(x);
#endif
    }

    #endregion

    #region Floor

    /// <summary> Calculates Floor. </summary>
    [VectorMath]
    public static partial Vector<T> Floor<T>(in Vector<T> d)
        where T : unmanaged;
    private struct Floor_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Floor(x);
    }

    private static Vector<double> Floor(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        return Emulate<double, Floor_<double>>(x);
#endif
    }

    private static Vector<float> Floor(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        return VectorOp.Floor(x);
#else
        return Emulate<float, Floor_<float>>(x);
#endif
    }

    #endregion

    #region FusedMultiplyAdd

    /// <summary>
    /// Calculates fused-multiply-add <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector<T> FusedMultiplyAdd<T>(in Vector<T> x, in Vector<T> y, in Vector<T> z)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Fma.IsSupported)
        {
            if(Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector256<T>>())
            {
                if(typeof(T) == typeof(double))
                {
                    return H.Reinterpret<double, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, double>(x).AsVector256(),
                                H.Reinterpret<T, double>(y).AsVector256(),
                                H.Reinterpret<T, double>(z).AsVector256())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.Reinterpret<float, T>(
                        Vector256.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, float>(x).AsVector256(),
                                H.Reinterpret<T, float>(y).AsVector256(),
                                H.Reinterpret<T, float>(z).AsVector256())
                            )
                        );
                }
            }
            if (Unsafe.SizeOf<Vector<T>>() == Unsafe.SizeOf<Vector128<T>>())
            {
                if (typeof(T) == typeof(double))
                {
                    return H.Reinterpret<double, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, double>(x).AsVector128(),
                                H.Reinterpret<T, double>(y).AsVector128(),
                                H.Reinterpret<T, double>(z).AsVector128())
                            )
                        );
                }
                if (typeof(T) == typeof(float))
                {
                    return H.Reinterpret<float, T>(
                        Vector128.AsVector(
                            Fma.MultiplyAdd(
                                H.Reinterpret<T, float>(x).AsVector128(),
                                H.Reinterpret<T, float>(y).AsVector128(),
                                H.Reinterpret<T, float>(z).AsVector128())
                            )
                        );
                }
            }
        }
#endif
        return Emulate<T, FusedMultiplyAdd_<T>>(x, y, z);
    }
    private struct FusedMultiplyAdd_<T> : IOperation3<T>
        where T : unmanaged
    {
        public T Calculate(T x, T y, T z)
            => ScalarOp.FusedMultiplyAdd(x, y, z);
    }

    #endregion

    #region Round

    /// <summary>
    /// Calculates Round.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Round<T>(in Vector<T> x)
        where T : unmanaged;

    private struct Round_<T> : IOperation1<T>
        where T : unmanaged
    {
        public T Calculate(T x) => ScalarMath.Round(x);
    }

    /// <summary> Calculates round. </summary>
    private static Vector<double> Round(in Vector<double> x)
    {
#if NET6_0_OR_GREATER
        if(Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector256<double>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector128<double>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<double, Round_<double>>(x);
    }


    private static Vector<float> Round(in Vector<float> x)
    {
#if NET6_0_OR_GREATER
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector256<float>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        if (Unsafe.SizeOf<Vector<float>>() == Unsafe.SizeOf<Vector128<float>>() && Sse41.IsSupported)
        {
            var xx = Vector128.AsVector128(x);
            return Sse41.RoundToNearestInteger(xx).AsVector();
        }
#endif
        return Emulate<float, Round_<float>>(x);
    }

    #endregion

    #region Sqrt

    /// <summary> Calculates sqrt. </summary>
    [VectorMath]
    public static Vector<T> Sqrt<T>(in Vector<T> d)
        where T : unmanaged
        => VectorOp.SquareRoot(d);

    #endregion

    #region Truncate

    /// <summary> Calculates truncate. </summary>
    [VectorMath]
    public static partial Vector<T> Truncate<T>(in Vector<T> d)
        where T : unmanaged;

    private static Vector<double> Truncate(in Vector<double> d)
        => VectorOp.ConvertToDouble(VectorOp.ConvertToInt64(d));

    private static Vector<float> Truncate(in Vector<float> d)
        => VectorOp.ConvertToSingle(VectorOp.ConvertToInt32(d));

    #endregion

    #region Cbrt

    /// <summary>
    /// Calculates cbrt(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Cbrt<T>(in Vector<T> x)
        where T : unmanaged
    {
        Decompose(x, out var n, out var a);
        var xn = Scale(Round(n / Const<T>._3), a);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        xn = Const<T>._2p3 * xn + x / (Const<T>._3 * xn * xn);
        return Vector.ConditionalSelect(
            Vector.Equals(x, Vector<T>.Zero),
            Vector<T>.Zero,
            xn);
    }

    #endregion
}