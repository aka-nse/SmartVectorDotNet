using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SmartVectorBenchmarks;

public partial class TrigonometricBenchmarkContext
{
    public static void RunBenchmark()
    {
#if DEBUG
        var errorVariances = new TrigonometricBenchmarkContext().EvaluateErrors();
        Console.Error.WriteLine("| N  | sin       | cos       |");
        Console.Error.WriteLine("|:--:|:---------:|:---------:|");
        for (var i = 1; i < 20; ++i)
        {
            var sinError = errorVariances[$"Sin_{i}"];
            var cosError = errorVariances[$"Cos_{i}"];
            Console.Error.WriteLine($"| {i,2} | {sinError:0.000e-00} | {cosError:0.000e-00} |");
        }
#else
        BenchmarkRunner.Run<TrigonometricBenchmarkContext>();
#endif
    }


    public const int BenchmarkSize = 1 << 16;

    public static readonly Vector<double>[] SinDenom
        = Enumerable
        .Range(1, 20)
        .Select(i => new Vector<double>(1.0 / ((2 * i) * (2 * i + 1))))
        .ToArray();

    public static readonly Vector<double>[] CosDenom
        = Enumerable
        .Range(1, 20)
        .Select(i => new Vector<double>(1.0 / ((2 * i - 1) * (2 * i))))
        .ToArray();

    private static readonly double[] _X
        = Enumerable
        .Range(0, BenchmarkSize)
        .Select(i => 0.5 * Math.PI * i / BenchmarkSize)
        .ToArray();

    public static ReadOnlySpan<double> XAsScalar => _X;

    public static ReadOnlySpan<Vector<double>> XAsVector => MemoryMarshal.Cast<double, Vector<double>>(_X);


    [Benchmark]
    public double[] Use_Sin_Emulated()
    {
        var retval = new double[BenchmarkSize];
        for(int i = 0; i < XAsScalar.Length; ++i)
        {
            retval[i] = Math.Sin(XAsScalar[i]);
        }
        return retval;
    }

    [Benchmark]
    public double[] Use_Cos_Emulated()
    {
        var retval = new double[BenchmarkSize];
        for (int i = 0; i < XAsScalar.Length; ++i)
        {
            retval[i] = Math.Cos(XAsScalar[i]);
        }
        return retval;
    }


}