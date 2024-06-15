using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SmartVectorBenchmarks;

public class PopulationBenchmarkContext
{
    const int _iteration = 1 << 20;
    static readonly Random _random = new Random(1234567);
    static readonly ulong[] _testValues = Enumerable.Range(0, _iteration).Select(_ => (ulong)_random.NextInt64()).ToArray();


    [Benchmark]
    public long UseOptimized()
    {
        var x = 0L;
        foreach(var y in _testValues)
        {
            x += Population(y);
        }
        return x;
    }


    [Benchmark]
    public long UseReference()
    {
        var x = 0L;
        foreach (var y in _testValues)
        {
            x += Population_Reference(y);
        }
        return x;
    }


    private static int Population(ulong x)
    {
        x = (x & 0x5555555555555555) + ((x >> 1) & 0x5555555555555555);
        x = (x & 0x3333333333333333) + ((x >> 2) & 0x3333333333333333);
        x = (x & 0x0F0F0F0F0F0F0F0F) + ((x >> 4) & 0x0F0F0F0F0F0F0F0F);
        x = (x & 0x00FF00FF00FF00FF) + ((x >> 8) & 0x00FF00FF00FF00FF);
        x = (x & 0x0000FFFF0000FFFF) + ((x >> 16) & 0x0000FFFF0000FFFF);
        x = (x & 0x00000000FFFFFFFF) + (x >> 32);
        return (int)(x & 127);
    }


    private static int Population_Reference(ulong x)
    {
        var n = 0;
        for (var i = 0; i < 64; ++i, x >>= 1)
        {
            n += (int)(x & 1);
        }
        return n;
    }
}
