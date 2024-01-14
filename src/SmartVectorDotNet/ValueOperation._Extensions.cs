using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

partial class ValueOperation
{
    public static T Scale<T>(T n, T x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
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