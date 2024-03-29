﻿namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Atanh_<T> : VectorMath.Const<T> where T : unmanaged { }


partial class VectorMath
{
    /// <summary>
    /// Calculates atanh(x).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> Atanh<T>(in Vector<T> x)
        where T : unmanaged
        => Atanh_<T>._1p2 * Log((Atanh_<T>._1 + x) / (Atanh_<T>._1 - x));
}
