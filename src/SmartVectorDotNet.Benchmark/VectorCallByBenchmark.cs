using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorDotNet.Benchmark;

public class VectorCallByBenchmark
{
    public static readonly Vector<int>[] Values = Enumerable.Range(0, 256).Select(x => new Vector<int>(x)).ToArray();

    [Benchmark]
    public Vector<int> CallByValue()
    {
        var result = Vector<int>.Zero;
        foreach (var value in Values)
        {
            result = Add1(result, value);
        }
        return result;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add1<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => Add2(x, y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add2<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => Add3(x, y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add3<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => x + y;


    [Benchmark]
    public Vector<int> CallByReference()
    {
        var result = Vector<int>.Zero;
        foreach (var value in Values)
        {
            result = Add1(in result, in value);
        }
        return result;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add1<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => Add2(in x, in y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add2<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => Add3(in x, in y);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<T> Add3<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => x + y;


    [Benchmark]
    public Vector<int> CallByValueNoInlining()
    {
        var result = Vector<int>.Zero;
        foreach (var value in Values)
        {
            result = AddNoInlining1(result, value);
        }
        return result;
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining1<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => AddNoInlining2(x, y);
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining2<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => AddNoInlining3(x, y);
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining3<T>(Vector<T> x, Vector<T> y)
        where T : struct
        => x + y;


    [Benchmark]
    public Vector<int> CallByReferenceNoInlining()
    {
        var result = Vector<int>.Zero;
        foreach (var value in Values)
        {
            result = AddNoInlining1(in result, in value);
        }
        return result;
    }
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining1<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => AddNoInlining2(in x, in y);
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining2<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => AddNoInlining3(in x, in y);
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Vector<T> AddNoInlining3<T>(in Vector<T> x, in Vector<T> y)
        where T : struct
        => x + y;
}
