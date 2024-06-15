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
    public record TestCase(int Size, double[] A, double[] B)
    {
        public override string ToString() => Size.ToString();
    }

    private static readonly Random _Random = new Random(1234567);
    public static IEnumerable<object[]> TestCases()
    {
        static object[] core(int size)
            => new object[] { new TestCase(
                size,
                Enumerable.Range(0, size).Select(_ => _Random.NextDouble()).ToArray(),
                Enumerable.Range(0, size).Select(_ => _Random.NextDouble()).ToArray())
            };

        var rand = new Random(1234567);
        foreach(var i in new[] {1, 4, 15, 16, 17, 255, 256, 257, })
        {
            yield return core(i);
        }
        yield break;
    }


    private ITestOutputHelper Output { get; }

    public VectorizationTest(ITestOutputHelper output) => Output = output;



    [Theory]
    [MemberData(nameof(TestCases))]
    public void Test(TestCase testCase)
    {
        var (_, a, b) = testCase;
        if (a.Length != b.Length)
        {
            throw new ArgumentException();
        }

        var ansExpected = new double[a.Length];
        var ansActual = new double[a.Length];

        Vectorization.Emulated.Add<double>(a, b, ansExpected);
        Vectorization.SIMD.Add<double>(a, b, ansActual);
        Assert.Equal(ansExpected, ansActual);

        Vectorization.Emulated.Subtract<double>(a, b, ansExpected);
        Vectorization.SIMD.Subtract<double>(a, b, ansActual);
        Assert.Equal(ansExpected, ansActual);

        Vectorization.Emulated.Multiply<double>(a, b, ansExpected);
        Vectorization.SIMD.Multiply<double>(a, b, ansActual);
        Assert.Equal(ansExpected, ansActual);

        Vectorization.Emulated.Divide<double>(a, b, ansExpected);
        Vectorization.SIMD.Divide<double>(a, b, ansActual);
        Assert.Equal(ansExpected, ansActual);

        Vectorization.Emulated.Sqrt<double>(a, ansExpected);
        Vectorization.SIMD.Sqrt<double>(a, ansActual);
        Assert.Equal(ansExpected, ansActual);


    }
}