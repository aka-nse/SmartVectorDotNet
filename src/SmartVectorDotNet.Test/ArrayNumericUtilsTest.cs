using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public class ArrayNumericUtilsTest
{
    public static TheoryData<double, double, int, double[]> LinspaceTestCases()
        => new()
        {
            { 0, 0, 1, [0] },
            { 0, 1, 1, [0] },
            { 0, 0, 2, [0, 0] },
            { 0, 1, 2, [0, 0.5] },
            { 0, 1, 4, [0, 0.25, 0.5, 0.75] },
            { 1, 0, 4, [1, 0.75, 0.5, 0.25] },
            { -5, 0, 5, [-5, -4, -3, -2, -1] },
            { 0, 11, 11, [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10] },
        };

    [Theory]
    [MemberData(nameof(LinspaceTestCases))]
    public void Linspace(double beginInclusive, double endInclusive, int count, double[] expected)
    {
        var result = ArrayNumericUtils.Linspace(beginInclusive, endInclusive, count);
        Assert.Equal(expected.Length, result.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i], 6); // Using 6 decimal places for floating point comparison
        }
    }


    public static TheoryData<double, double, int, double[]> LinspaceMaxInclusiveTestCases()
        => new()
        {
#warning add test cases
        };

    [Theory]
    [MemberData(nameof(LinspaceMaxInclusiveTestCases))]
    public void LinspaceMaxInclusive(double beginInclusive, double endInclusive, int count, double[] expected)
    {
        var result = ArrayNumericUtils.LinspaceMaxInclusive(beginInclusive, endInclusive, count);
        Assert.Equal(expected.Length, result.Length);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Equal(expected[i], result[i], 6); // Using 6 decimal places for floating point comparison
        }

    }
}
