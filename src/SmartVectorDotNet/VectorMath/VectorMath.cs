using System.Runtime.CompilerServices;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace SmartVectorDotNet;
using H = InternalHelpers;


/// <summary>
/// Provides vectorized mathematical and computational functions.
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
             : IsT<nuint >() ? As(new Vector<nuint >((nuint )x))
             : IsT<sbyte >() ? As(new Vector<sbyte >((sbyte )x))
             : IsT<short >() ? As(new Vector<short >((short )x))
             : IsT<int   >() ? As(new Vector<int   >((int   )x))
             : IsT<long  >() ? As(new Vector<long  >((long  )x))
             : IsT<nint  >() ? As(new Vector<nint  >((nint  )x))
            : default;
#pragma warning restore format

        private protected static Vector<T> As<TFrom>(Vector<TFrom> x)
            where TFrom : unmanaged
            => H.Reinterpret<TFrom, T>(x);

        /// <summary> The vector which returns as <c>true</c> by conditional operators. </summary>
        public static readonly Vector<T> TrueValue = Vector.Equals(Vector<T>.Zero, Vector<T>.Zero);

        /// <summary> The vector which returns as <c>false</c> by conditional operators. </summary>
        public static readonly Vector<T> FalseValue = Vector.Equals(Vector<T>.Zero, Vector<T>.One);


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

        internal static readonly Vector<T> Log_2_E = AsVector(ScalarMath.Log(ScalarMath.Const<double>.E, 2));
        internal static readonly Vector<T> Log_E_2 = AsVector(ScalarMath.Log(2, ScalarMath.Const<double>.E));

        private protected Const() { }
    }


    private interface IOperation1<T> { public T Calculate(T x); }
    private static Vector<T> Emulate<T, TOperation>(Vector<T> x)
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
    private static Vector<T> Emulate<T, TOperation>(Vector<T> x, Vector<T> y)
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
    private static Vector<T> Emulate<T, TOperation>(Vector<T> x, Vector<T> y, Vector<T> z)
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
}