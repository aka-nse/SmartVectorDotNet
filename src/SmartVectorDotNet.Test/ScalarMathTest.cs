using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class ScalarMathTest
{
    [Fact]
    public void Constants()
    {
        Assert.Equal(MathF.Pow(2.0f, -23), ScalarMath.Const<float>.MachineEpsilon);
        Assert.Equal(Math.Pow(2.0, -52), ScalarMath.Const<double>.MachineEpsilon);

        Assert.Equal(MathF.Pow(2.0f, -126), ScalarMath.Const<float>.PositiveMinimumNormalizedNumber);
        Assert.Equal(Math.Pow(2.0, -1022), ScalarMath.Const<double>.PositiveMinimumNormalizedNumber);
    }

    public static IEnumerable<object[]> DecomposeTestCases()
    {
        static object[] core(double x)
            => new object[] { x, };

        foreach(var i in new[] {1.0, -1.0})
        {
            for(var j = -10; j <= 10; ++j)
            {
                var e = Math.Pow(10, j);
                for(var k = 1.0; k < 10; k += 0.9)
                {
                    yield return core(i * e * k);
                }
            }
        }
    }

    [Theory]
    [MemberData(nameof(DecomposeTestCases))]
    public void DecomposeTest(double x)
    {
        {
            ScalarMath.Decompose<double>(x, out var n, out var a);
            Assert.True(1 <= Math.Abs(a));
            Assert.True(Math.Abs(a) < 2);
            Assert.Equal(n, Math.Truncate(n), 10);
            Assert.Equal(x, Math.Pow(2, n) * a, 5);
        }
        {
            var y = (float)x;
            ScalarMath.Decompose<float>(y, out var n, out var a);
            Assert.True(1 <= MathF.Abs(a));
            Assert.True(MathF.Abs(a) < 2);
            Assert.Equal(n, MathF.Truncate(n), 10);
            Assert.Equal(y, MathF.Pow(2, n) * a, 5);
        }
    }
}