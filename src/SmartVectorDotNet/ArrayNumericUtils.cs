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
    /// <param name="minInclusive"></param>
    /// <param name="maxExclusive"></param>
    /// <param name="pointNum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T[] Linspace<T>(T minInclusive, T maxExclusive, int pointNum)
        where T : unmanaged
    {
        if(OP.GreaterThan(minInclusive, maxExclusive))
        {
            throw new ArgumentException();
        }
        if(pointNum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pointNum));
        }
        var stepNumAsT = OP.Convert<int, T>(pointNum);
        var retval = new T[pointNum];
        var step = OP.Divide(OP.Subtract(maxExclusive, minInclusive), stepNumAsT);
        for(var i = 0; i <  pointNum; ++i)
        {
            retval[i] = OP.Add(minInclusive, OP.Multiply(step, OP.Convert<int, T>(i)));
        }
        return retval;
    }


    /// <summary>
    /// Similar with <see cref="Linspace{T}(T, T, int)"/>, but the return value of this method contains <paramref name="maxInclusive"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minInclusive"></param>
    /// <param name="maxInclusive"></param>
    /// <param name="pointNum"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static T[] LinspaceMaxInclusive<T>(T minInclusive, T maxInclusive, int pointNum)
        where T : unmanaged
    {
        if (OP.GreaterThan(minInclusive, maxInclusive))
        {
            throw new ArgumentException();
        }
        if (pointNum < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pointNum));
        }
        var stepNumAsT = OP.Convert<int, T>(Math.Max(pointNum - 1, 0));
        var retval = new T[pointNum];
        var step = OP.Divide(OP.Subtract(maxInclusive, minInclusive), stepNumAsT);
        for (var i = 0; i < pointNum; ++i)
        {
            retval[i] = OP.Add(minInclusive, OP.Multiply(step, OP.Convert<int, T>(i)));
        }
        return retval;
    }
}
