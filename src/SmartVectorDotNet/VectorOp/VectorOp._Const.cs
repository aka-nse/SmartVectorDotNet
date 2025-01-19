namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
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

        /// <summary> Gets the vector which has <c>0</c> values. </summary>
        public static readonly Vector<T> Zero = new(ScalarOp.Const<T>.Zero);
        
        /// <summary> Gets the vector which has <c>1</c> values. </summary>
        public static readonly Vector<T> One = new(ScalarOp.Const<T>.One);

        /// <summary> Gets the vector which has <c>T.MinValue</c> values. </summary>
        public static readonly Vector<T> MinValue = new(ScalarOp.Const<T>.MinValue);

        /// <summary> Gets the vector which has <c>T.MaxValue</c> values. </summary>
        public static readonly Vector<T> MaxValue = new(ScalarOp.Const<T>.MaxValue);

        /// <summary> The vector which returns as <c>true</c> by conditional operators. </summary>
        public static readonly Vector<T> TrueValue = Vector.Equals(Vector<T>.Zero, Vector<T>.Zero);

        /// <summary> The vector which returns as <c>false</c> by conditional operators. </summary>
        public static readonly Vector<T> FalseValue = Vector.Equals(Vector<T>.Zero, Vector<T>.One);

        /// <summary> Gets the vector which has values whose MSB is 1. </summary>
        public static readonly Vector<T> Msb = new(ScalarOp.Const<T>.Msb);
        
        /// <summary> Gets the vector which has values whose MSB is 1. </summary>
        public static readonly Vector<T> Lsb = new(ScalarOp.Const<T>.Lsb);

        /// <summary> Vectorized Napier's constant. </summary>
        public static readonly Vector<T> E = new(ScalarOp.Const<T>.E);

        /// <summary> Vectorized Pi. </summary>
        public static readonly Vector<T> PI = new(ScalarOp.Const<T>.PI);

        /// <summary> NaN value. </summary>
        public static readonly Vector<T> NaN = new(ScalarOp.Const<T>.NaN);

        /// <summary> Positive infinity. </summary>
        public static readonly Vector<T> PInf = new(ScalarOp.Const<T>.PInf);

        /// <summary> Negative infinity. </summary>
        public static readonly Vector<T> NInf = new(ScalarOp.Const<T>.NInf);

        private protected Const() { }
    }
}