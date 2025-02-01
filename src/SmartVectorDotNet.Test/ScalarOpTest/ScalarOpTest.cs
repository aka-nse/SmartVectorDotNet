using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class ScalarOpTest
{
    private readonly struct StubType;

    private static object?[] TestCase<T>(bool isSupported, T input, T expected = default)
        where T : unmanaged
        => [isSupported, input, expected,];

    private static object?[] TestCase<T>(bool isSupported, (T, T) input, T expected = default)
        where T : unmanaged
        => [isSupported, input, expected,];

    private static object?[] TestCase<T>(bool isSupported, (T, T, T) input, T expected = default)
        where T : unmanaged
        => [isSupported, input, expected,];

    private static void CommonTest<T>(Func<T, T> func, bool isSupported, T arg1, T expected)
        where T : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(arg1));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(arg1));
        }
    }

    private static void CommonTest<T>(Func<T, T, T> func, bool isSupported, (T, T) args, T expected)
        where T : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(args.Item1, args.Item2));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(args.Item1, args.Item2));
        }
    }

    private static void CommonTest<T>(Func<T, T, T, T> func, bool isSupported, (T, T, T) args, T expected)
        where T : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(args.Item1, args.Item2, args.Item3));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(args.Item1, args.Item2, args.Item3));
        }
    }



    private static object?[] TestCaseEx<T1, TResult>(bool isSupported, T1 input, TResult expected = default)
        where T1 : unmanaged
        where TResult : unmanaged
        => [isSupported, input, expected,];

    private static object?[] TestCaseEx<T1, T2, TResult>(bool isSupported, (T1, T2) input, TResult expected = default)
        where T1 : unmanaged
        where T2 : unmanaged
        where TResult : unmanaged
        => [isSupported, input, expected,];

    private static object?[] TestCaseEx<T1, T2, T3, TResult>(bool isSupported, (T1, T2, T3) input, TResult expected = default)
        where T1 : unmanaged
        where T2 : unmanaged
        where T3 : unmanaged
        where TResult : unmanaged
        => [isSupported, input, expected,];

    private static void CommonTestEx<T1, TResult>(Func<T1, TResult> func, bool isSupported, T1 arg1, TResult expected)
        where T1 : unmanaged
        where TResult : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(arg1));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(arg1));
        }
    }

    private static void CommonTestEx<T1, T2, TResult>(Func<T1, T2, TResult> func, bool isSupported, (T1, T2) args, TResult expected)
        where T1 : unmanaged
        where T2 : unmanaged
        where TResult : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(args.Item1, args.Item2));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(args.Item1, args.Item2));
        }
    }

    private static void CommonTestEx<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, bool isSupported, (T1, T2, T3) args, TResult expected)
        where T1 : unmanaged
        where T2 : unmanaged
        where T3 : unmanaged
        where TResult : unmanaged
    {
        if (isSupported)
        {
            Assert.Equal(expected, func(args.Item1, args.Item2, args.Item3));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => func(args.Item1, args.Item2, args.Item3));
        }
    }
}