using System;
using System.Collections.Generic;
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



    /// <summary>
    /// Calculates <c>pow(2, n) * x</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T Scale<T>(T n, T x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var nn = Reinterpret<T, double>(n);
            var xx = Reinterpret<T, double>(x);
            return Reinterpret<double, T>(Reinterpret<long, double>(((long)nn + 1023) << 52) * xx);
        }
        if (typeof(T) == typeof(float))
        {
            var nn = Reinterpret<T, float>(n);
            var xx = Reinterpret<T, float>(x);
            return Reinterpret<float, T>(Reinterpret<int, float>(((int)nn + 127) << 23) * xx);
        }
        throw new NotSupportedException();
    }
}