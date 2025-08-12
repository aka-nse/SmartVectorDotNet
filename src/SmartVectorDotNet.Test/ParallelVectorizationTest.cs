using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public class ParallelVectorizationTest
{

    public static TheoryData<uint[]> OnesComplementTestCases()
        => [
            [ 0x00000000 ],
            [ 0xFFFFFFFF ],
            [ 0x00000000, 0xFFFFFFFF ],
            [ 0xFFFFFFFF, 0x00000000 ],
            [ 0x12345678, 0x9ABCDEF0, 0x00000000, 0xFFFFFFFF ],
            [ 0xFFFFFFFF, 0x12345678, 0x9ABCDEF0, 0x00000000 ],
            [.. Enumerable.Range(0, 100).Select(x => (uint)x)],
            [.. Enumerable.Range(0, 1000).Select(x => (uint)x)],
            [.. Enumerable.Range(0, 9999).Select(x => (uint)x)],
            [.. Enumerable.Range(0, 10000).Select(x => (uint)x)],
            [.. Enumerable.Range(0, 10001).Select(x => (uint)x)],
        ];

    [Theory]
    [MemberData(nameof(OnesComplementTestCases))]
    public void OnesComplement(uint[] input)
    {
        var expected = input.Select(x => ~x).ToArray();
        var actual = new uint[input.Length];
        new ParallelVectorization(Vectorization.Emulated, 8).Complement<uint>(input, actual);
        Assert.Equal(expected, actual);
    }
}
