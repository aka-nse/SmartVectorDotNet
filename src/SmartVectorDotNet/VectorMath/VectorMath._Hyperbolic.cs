using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmartVectorDotNet;

partial class VectorMath
{
    #region Sinh

    /// <summary>
    /// Calculates sinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Sinh<T>(in Vector<T> x)
        where T : unmanaged
        => (Exp(x) - Exp(-x)) * Const<T>.Half;

    #endregion

    #region Cosh

    /// <summary>
    /// Calculates cosh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Cosh<T>(in Vector<T> x)
        where T : unmanaged
        => (Exp(x) + Exp(-x)) * Const<T>.Half;

    #endregion

    #region Tanh

    /// <summary>
    /// Calculates tanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Tanh<T>(in Vector<T> x)
        where T : unmanaged
    {
        var p = Exp(x);
        var n = Exp(-x);
        return (p - n) / (p + n);
    }

    #endregion

    #region Asinh

    /// <summary>
    /// Calculates asinh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Asinh<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x + Sqrt(x * x + Vector<T>.One));

    #endregion

    #region Acosh

    /// <summary>
    /// Calculates acosh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Acosh<T>(in Vector<T> x)
        where T : unmanaged
        => Log(x + Sqrt(x * x - Vector<T>.One));

    #endregion

    #region Atanh

    /// <summary>
    /// Calculates atanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Atanh<T>(in Vector<T> x)
        where T : unmanaged
        => Const<T>.Half * Log((Vector<T>.One + x) / (Vector<T>.One - x));

    #endregion
}
