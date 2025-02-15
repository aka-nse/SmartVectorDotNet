using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

partial class ScalarOp
{
    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    public partial class Const
    {
        private protected Const() { }
    }


    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Const<T> : Const
        where T : unmanaged
    {
        private static bool IsT<TEntity>()
            => typeof(T) == typeof(TEntity);

        private static T Reinterpret<TFrom>(TFrom x)
            where TFrom : unmanaged
            => Reinterpret<TFrom, T>(x);

        /// <summary> Mathematical PI. </summary>
        public static readonly T PI
            = IsT<double>()
                ? Reinterpret(Math.PI)
            : IsT<float>()
                ? Reinterpret((float)Math.PI)
            : default;

        /// <summary> Mathematical E. </summary>
        public static readonly T E
            = IsT<double>()
                ? Reinterpret(Math.E)
            : IsT<float>()
                ? Reinterpret((float)Math.E)
            : default;

        /// <summary> NaN value. </summary>
        public static readonly T NaN
            = IsT<double>()
                ? Reinterpret(double.NaN)
            : IsT<float>()
                ? Reinterpret(float.NaN)
            : default;

        /// <summary> Positive infinity. </summary>
        public static readonly T PInf
            = IsT<double>()
                ? Reinterpret(double.PositiveInfinity)
            : IsT<float>()
                ? Reinterpret(float.PositiveInfinity)
            : default;

        /// <summary> Negative infinity. </summary>
        public static readonly T NInf
            = IsT<double>()
                ? Reinterpret(double.NegativeInfinity)
            : IsT<float>()
                ? Reinterpret(float.NegativeInfinity)
            : default;

        private protected Const() { }
    }
}
