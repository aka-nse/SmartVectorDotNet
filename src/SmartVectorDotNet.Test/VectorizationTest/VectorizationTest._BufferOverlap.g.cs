using System;
using System.Linq;
using Xunit;

namespace SmartVectorDotNet;


public partial class VectorizationTest
{
    public static TheoryData<float[], Range, Range> UnaryOverlapTestCase()
        => new()
        {
            { new float[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}, new Range(0, 8), new Range(1, 9) },
            { new float[] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15}, new Range(1, 9), new Range(0, 8) },
        };


    [Theory]
    [MemberData(nameof(UnaryOverlapTestCase))]
    public void UnaryOverlapTest_UnaryPlus(float[] input, Range source, Range destination)
    {
        var expected = input.ToArray();
        var actual = input.ToArray();
        Vectorization.SIMD.UnaryPlus(input.AsSpan(source), expected.AsSpan(destination));
        Vectorization.SIMD.UnaryPlus(actual.AsSpan(source), actual.AsSpan(destination));
        Assert.Equal(expected, actual);
    }

}
