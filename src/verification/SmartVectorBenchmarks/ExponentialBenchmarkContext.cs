using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorBenchmarks;

public partial class ExponentialBenchmarkContext
{
    public static void RunBenchmark()
    {
#if DEBUG
        var errorVariances = new ExponentialBenchmarkContext().EvaluateErrors();
        Console.Error.WriteLine("| N  | exp       |");
        Console.Error.WriteLine("|:--:|:---------:|");
        for (var i = 1; i < 20; ++i)
        {
            var expError = errorVariances[$"Exp_{i}"];
            Console.Error.WriteLine($"| {i,2} | {expError:0.000e-00} |");
        }
#else
        BenchmarkRunner.Run<ExponentialBenchmarkContext>();
#endif
    }


    public const int BenchmarkSize = 1 << 16;

    public static readonly double ExpMax = Math.Log(double.MaxValue) - 1;
    public static readonly double ExpMin = Math.Log(1 / double.MaxValue) + 1;
    public static readonly Vector<long> _1023 = new(1023);
    public static readonly Vector<double> Log_2_E = new(Math.Log(Math.E, 2));
    public static readonly Vector<double> Log_E_2 = new(Math.Log(2, Math.E));
    public static readonly Vector<double>[] ExpDenom
        = Enumerable
        .Range(1, 19)
        .Select(i => new Vector<double>(1.0 / i))
        .ToArray();

    private static readonly double[] _X
        = Enumerable
        .Range(0, BenchmarkSize)
        .Select(i => (ExpMax - ExpMin) * i / (BenchmarkSize - 1) + ExpMin)
        .ToArray();

    public static ReadOnlySpan<double> XAsScalar => _X;

    public static ReadOnlySpan<Vector<double>> XAsVector => MemoryMarshal.Cast<double, Vector<double>>(_X);


    private static Vector<double> Round(in Vector<double> x)
    {
        if (Unsafe.SizeOf<Vector<double>>() == Unsafe.SizeOf<Vector256<double>>() && Avx.IsSupported)
        {
            var xx = Vector256.AsVector256(x);
            return Avx.RoundToNearestInteger(xx).AsVector();
        }
        throw new NotSupportedException();
    }

    private static Vector<double> Scale(in Vector<double> n, Vector<double> x)
    {
        var nn = Vector.ConvertToInt64(n);
        var pow2n = Vector.ShiftLeft(nn + _1023, 52);
        return x * Vector.AsVectorDouble(pow2n);
    }


    [Benchmark]
    public double[] Use_Exp_Emulated()
    {
        var retval = new double[BenchmarkSize];
        for (int i = 0; i < XAsScalar.Length; ++i)
        {
            retval[i] = Math.Exp(XAsScalar[i]);
        }
        return retval;
    }
}