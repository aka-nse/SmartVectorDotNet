using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SmartVectorDotNet;

/// <summary>
/// Provides wrapper for "System.Math" which appends genericity and framework compatibility.
/// </summary>
public static partial class ScalarMath
{
    internal static TTo Reinterpret<TFrom, TTo>(in TFrom x)
        where TFrom : unmanaged
        where TTo : unmanaged
        => Unsafe.As<TFrom, TTo>(ref Unsafe.AsRef(x));


    internal static partial class Const
    {
    }


    /// <summary>
    /// Provides mathematical constants.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static partial class Const<T>
        where T : unmanaged
    {
        /// <summary> Mathematical PI. </summary>
        public static readonly T PI
            = typeof(T) == typeof(double)
                ? Reinterpret<double, T>(Math.PI)
            : typeof(T) == typeof(float)
                ? Reinterpret<float, T>((float)Math.PI)
            : default;

        /// <summary> Mathematical E. </summary>
        public static readonly T E
            = typeof(T) == typeof(double)
                ? Reinterpret<double, T>(Math.E)
            : typeof(T) == typeof(float)
                ? Reinterpret<float, T>((float)Math.E)
            : default;
    }

}