using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmartVectorDotNet;


public partial class VectorizationTest
{
    public static TheoryData<byte[]> WidenTestCases()
        => new()
        {
            { [] },
            { [0] },
            { [0, 1, 2, 3] },
            { Enumerable.Range(0, 255).Select(x => (byte)x).ToArray() },
            { Enumerable.Range(0, 256).Select(x => (byte)x).ToArray() },
            { Enumerable.Range(0, 257).Select(x => (byte)x).ToArray() },
            { Enumerable.Range(0, 65535).Select(x => (byte)x).ToArray() },
            { Enumerable.Range(0, 65536).Select(x => (byte)x).ToArray() },
            { Enumerable.Range(0, 65537).Select(x => (byte)x).ToArray() },
        };


    [Theory]
    [MemberData(nameof(WidenTestCases))]
    public void WidenTest(byte[] data)
    {
        var tmp1 = new ushort[data.Length];
        var tmp2 = new uint[data.Length];
        var expected = new ulong[data.Length];
        var actual = new ulong[data.Length];

        Vectorization.Emulated.Widen(data, tmp1);
        Vectorization.Emulated.Widen(tmp1, tmp2);
        Vectorization.Emulated.Widen(tmp2, expected);

        Vectorization.SIMD.Widen(data, tmp1);
        Vectorization.SIMD.Widen(tmp1, tmp2);
        Vectorization.SIMD.Widen(tmp2, actual);

        Assert.Equal(expected, actual);
    }


    public static TheoryData<ulong[]> NarrowTestCases()
        => new()
        {
            { [] },
            { [0] },
            { [0, 1, 2, 3] },
            { Enumerable.Range(0, 255).Select(x => (ulong)x).ToArray() },
            { Enumerable.Range(0, 256).Select(x => (ulong)x).ToArray() },
            { Enumerable.Range(0, 257).Select(x => (ulong)x).ToArray() },
            { Enumerable.Range(0, 65535).Select(x => (ulong)x).ToArray() },
            { Enumerable.Range(0, 65536).Select(x => (ulong)x).ToArray() },
            { Enumerable.Range(0, 65537).Select(x => (ulong)x).ToArray() },
        };


    [Theory]
    [MemberData(nameof(NarrowTestCases))]
    public void NarrowTest(ulong[] data)
    {
        var tmp1 = new uint[data.Length];
        var tmp2 = new ushort[data.Length];
        var expected = new byte[data.Length];
        var actual = new byte[data.Length];

        Vectorization.Emulated.Narrow(data, tmp1);
        Vectorization.Emulated.Narrow(tmp1, tmp2);
        Vectorization.Emulated.Narrow(tmp2, expected);

        Vectorization.SIMD.Narrow(data, tmp1);
        Vectorization.SIMD.Narrow(tmp1, tmp2);
        Vectorization.SIMD.Narrow(tmp2, actual);

        Assert.Equal(expected, actual);
    }
}
