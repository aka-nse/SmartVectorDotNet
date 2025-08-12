using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorDotNet.Benchmark;

public class ParallelVectorizationBenchmark
{
    private static readonly Random _random = new(1234567);

    public static readonly uint[] _x = [
        .. Enumerable
            .Range(0, 1 << 20)
            .Select(_ => (uint)_random.Next())
        ];

    public static readonly uint[] _y = [
        .. Enumerable
            .Range(0, 1 << 20)
            .Select(_ => (uint)_random.Next())
        ];

    [Benchmark]
    public uint[] Emulated()
    {
        var ans = new uint[_x.Length];
        Vectorization.Emulated.Add<uint>(_x, _y, ans);
        return ans;
    }

    [Benchmark]
    public uint[] SIMD()
    {
        var ans = new uint[_x.Length];
        Vectorization.SIMD.Add<uint>(_x, _y, ans);
        return ans;
    }

    [Benchmark]
    public uint[] Parallel_Emulated()
    {
        var ans = new uint[_x.Length];
        new ParallelVectorization(Vectorization.Emulated, 8).Add<uint>(_x, _y, ans);
        return ans;
    }

    [Benchmark]
    public uint[] Parallel_SIMD()
    {
        var ans = new uint[_x.Length];
        new ParallelVectorization(Vectorization.SIMD, 8).Add<uint>(_x, _y, ans);
        return ans;
    }
}
