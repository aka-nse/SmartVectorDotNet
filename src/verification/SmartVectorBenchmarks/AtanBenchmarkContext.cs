using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace SmartVectorBenchmarks;

[Config(typeof(Config))]
public partial class AtanBenchmarkContext
{
    private class Config : ManualConfig
    {
        public Config()
        {
            AddJob(Job.Default);
        }
    }


    public static void RunBenchmark()
    {
#if DEBUG
        var errorVariances = new AtanBenchmarkContext().EvaluateErrors();
        Console.Error.WriteLine("| N        | error     |");
        Console.Error.WriteLine("|:---------|:---------:|");
        foreach(var kv in errorVariances)
        {
            var expError = kv.Value;
            Console.Error.WriteLine($"| {kv.Key,-8} | {expError:0.000e-00} |");
        }
#else
        BenchmarkRunner.Run<AtanBenchmarkContext>();
#endif
    }


    public const int BenchmarkSize = 1 << 16;

    private static readonly double[] _X
        = Enumerable
        .Range(0, BenchmarkSize)
        .Select(i => 20 * (i + 0.5 - BenchmarkSize / 2) / BenchmarkSize)
        .ToArray();

    public static ReadOnlySpan<double> XAsScalar => _X;
    public static ReadOnlySpan<Vector<double>> XAsVector => MemoryMarshal.Cast<double, Vector<double>>(_X);


    private static readonly Vector<double>[] _AtanCoeffs =
        Enumerable
        .Range(1, 20)
        .Select(n => Math.Pow(-1, n) / (2 * n + 1))
        .Select(a => new Vector<double>(a))
        .ToArray();
    private static readonly Vector<double> _CoreXReflectThreshold
        = new(Math.Sqrt(2) - 1);
    private static readonly Vector<double> _CoreOffset
        = new(Math.PI / 4);
    private static readonly Vector<double> _WrapperOffset
        = new(Math.PI / 2);

    private static Vector<T> SignFast<T>(in Vector<T> x)
        where T : unmanaged
    {
        static Vector<T> core<U>(in Vector<T> x, in Vector<U> signBit)
            where U : unmanaged
        {
            var sign = Vector.BitwiseAnd(x, Unsafe.As<Vector<U>, Vector<T>>(ref Unsafe.AsRef(in signBit)));
            return Vector.BitwiseOr(Vector<T>.One, sign);
        }

        return Unsafe.SizeOf<T>() switch
        {
            1 => core(x, _SignBit8),
            2 => core(x, _SignBit16),
            4 => core(x, _SignBit32),
            8 => core(x, _SignBit64),
            _ => throw new NotSupportedException(),
        };
    }
    private static readonly Vector<byte  > _SignBit8  = new(0x80);
    private static readonly Vector<ushort> _SignBit16 = new(0x8000);
    private static readonly Vector<uint  > _SignBit32 = new(0x80000000);
    private static readonly Vector<ulong > _SignBit64 = new(0x8000000000000000);



    [Benchmark]
    public double[] Use_Atan_Emulated()
    {
        var retval = new double[BenchmarkSize];
        for (int i = 0; i < XAsScalar.Length; ++i)
        {
            retval[i] = Math.Atan(XAsScalar[i]);
        }
        return retval;
    }
}
