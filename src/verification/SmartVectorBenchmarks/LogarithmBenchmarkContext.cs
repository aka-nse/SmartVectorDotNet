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

public partial class LogarithmBenchmarkContext
{
    public static void RunBenchmark()
    {
#if DEBUG
        var errorVariances = new LogarithmBenchmarkContext().EvaluateErrors();
        Console.Error.WriteLine("| N        | log       |");
        Console.Error.WriteLine("|:---------|:---------:|");
        foreach(var kv in errorVariances)
        {
            var expError = kv.Value;
            Console.Error.WriteLine($"| {kv.Key,-8} | {expError:0.000e-00} |");
        }
#else
        BenchmarkRunner.Run<LogarithmBenchmarkContext>();
#endif
    }

    public const int BenchmarkSize = 1 << 18;

    public static readonly Vector<double> OnePerRoot2 = new(1 / Math.Sqrt(2));
    public static readonly Vector<double> LogRoot2 = new(Math.Log(Math.Sqrt(2)));

    public static readonly Vector<double>[] LogCoeffs
        = Enumerable
        .Range(1, 100)
        .Select(i => new Vector<double>(Math.Pow(-1.0, i - 1) / (double)i))
        .ToArray();

    private static readonly double[] _X
        = Enumerable
        .Range(0, BenchmarkSize)
        .Select(i => 1.0 + 1.0 * i / BenchmarkSize)
        .ToArray();

    public static ReadOnlySpan<double> XAsScalar => _X;

    public static ReadOnlySpan<Vector<double>> XAsVector => MemoryMarshal.Cast<double, Vector<double>>(_X);


    [Benchmark]
    public double[] Use_Log_Emulated()
    {
        var retval = new double[BenchmarkSize];
        for (int i = 0; i < XAsScalar.Length; ++i)
        {
            retval[i] = Math.Log(XAsScalar[i]);
        }
        return retval;
    }
}