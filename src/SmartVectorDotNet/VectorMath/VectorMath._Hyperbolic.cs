using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;

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
        => (Exp(x) - Exp(-x)) * Sinh_<T>._1p2;

    private class Sinh_<T> : Const<T> where T : unmanaged { }

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
        => (Exp(x) + Exp(-x)) * Cosh_<T>._1p2;

    private class Cosh_<T> : Const<T> where T : unmanaged { }

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
        => Log(x + Sqrt(x * x + Asinh_<T>._1));

    private class Asinh_<T> : Const<T> where T : unmanaged { }

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
        => Log(x + Sqrt(x * x - Acosh_<T>._1));

    private class Acosh_<T> : Const<T> where T : unmanaged { }

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
        => Atanh_<T>._1p2 * Log((Atanh_<T>._1 + x) / (Atanh_<T>._1 - x));

    private class Atanh_<T> : Const<T> where T : unmanaged { }

    #endregion
}
