using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorDotNet.Benchmark.Vectorization;

public class LogBenchmark
{
    public static readonly float[] X_f32
        = Enumerable
        .Range(-10, 21)
        .SelectMany(n => Enumerable.Range(0, 256).Select(i => (float)((1 + i / 256.0) * ScalarMath.Pow(2.0, n))))
        .ToArray();
    public static readonly double[] X_f64
        = Enumerable
        .Range(-10, 21)
        .SelectMany(n => Enumerable.Range(0, 256).Select(i => (1 + i / 256.0) * ScalarMath.Pow(2.0, n)))
        .ToArray();


    [Benchmark]
    [PrecisionControl]
    public double[] Log_Emulated64()
    {
        var retval = new double[X_f64.Length];
        SmartVectorDotNet.Vectorization.Emulated.Log<double>(X_f64, retval);
        return retval;
    }


    [Benchmark]
    [PrecisionSubject]
    public double[] Log_SIMD64()
    {
        var retval = new double[X_f64.Length];
        SmartVectorDotNet.Vectorization.SIMD.Log<double>(X_f64, retval);
        return retval;
    }


    [Benchmark]
    [PrecisionControl]
    public float[] Log_Emulated32()
    {
        var retval = new float[X_f32.Length];
        SmartVectorDotNet.Vectorization.Emulated.Log<float>(X_f32, retval);
        return retval;
    }


    [Benchmark]
    [PrecisionSubject]
    public float[] Log_SIMD32()
    {
        var retval = new float[X_f32.Length];
        SmartVectorDotNet.Vectorization.SIMD.Log<float>(X_f32, retval);
        return retval;
    }
}