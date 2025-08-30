using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using OP = ScalarOp;

/// <summary>
/// Provides array utilities for numerics.
/// </summary>
public static class ArrayNumericUtils
{
    /// <summary>
    /// Creates a new array which contains evenly spaced numbers over a specified interval.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="beginInclusive"></param>
    /// <param name="endExclusive"></param>
    /// <param name="pointNum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T[] Linspace<T>(T beginInclusive, T endExclusive, int pointNum)
        where T : unmanaged
    {
        Guard.ValidRange(pointNum > 0, $"{nameof(pointNum)} must be greater than 0.");

        var stepNumAsT = OP.Convert<int, T>(pointNum);
        var retval = new T[pointNum];
        var step = OP.Divide(OP.Subtract(endExclusive, beginInclusive), stepNumAsT);
        for(var i = 0; i <  pointNum; ++i)
        {
            retval[i] = OP.Add(beginInclusive, OP.Multiply(step, OP.Convert<int, T>(i)));
        }
        return retval;
    }


    /// <summary>
    /// Similar with <see cref="Linspace{T}(T, T, int)"/>, but the return value of this method contains <paramref name="endInclusive"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="beginInclusive"></param>
    /// <param name="endInclusive"></param>
    /// <param name="pointNum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T[] LinspaceMaxInclusive<T>(T beginInclusive, T endInclusive, int pointNum)
        where T : unmanaged
    {
        Guard.ValidRange(pointNum > 1, $"{nameof(pointNum)} must be greater than 1.");

        var stepNumAsT = OP.Convert<int, T>(pointNum - 1);
        var retval = new T[pointNum];
        var step = OP.Divide(OP.Subtract(endInclusive, beginInclusive), stepNumAsT);
        for (var i = 0; i < pointNum; ++i)
        {
            retval[i] = OP.Add(beginInclusive, OP.Multiply(step, OP.Convert<int, T>(i)));
        }
        return retval;
    }
}
