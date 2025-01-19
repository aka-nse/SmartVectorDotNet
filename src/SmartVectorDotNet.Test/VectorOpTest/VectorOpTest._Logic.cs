using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

file static class Helpers
{
#if NETCOREAPP2_1_OR_GREATER
    public static bool IsNormal(float x) => float.IsNormal(x);
    public static bool IsNormal(double x) => double.IsNormal(x);
    public static bool IsSubnormal(float x) => float.IsSubnormal(x);
    public static bool IsSubnormal(double x) => double.IsSubnormal(x);
#else
    private static readonly float MinNormal32 = ScalarOp.Scale(0, 1, 0);
    private static readonly double MinNormal64 = ScalarOp.Scale(0L, 1L, 0L);

    public static bool IsNormal(float x)
    {
        if(float.IsNaN(x) || float.IsInfinity(x) || x == 0)
        {
            return false;
        }
        return Math.Abs(x) >= MinNormal32;
    }

    public static bool IsNormal(double x)
    {
        if (double.IsNaN(x) || double.IsInfinity(x) || x == 0)
        {
            return false;
        }
        return Math.Abs(x) >= MinNormal64;
    }

    public static bool IsSubnormal(float x)
    {
        if (float.IsNaN(x) || float.IsInfinity(x) || x == 0)
        {
            return false;
        }
        return Math.Abs(x) < MinNormal32;
    }

    public static bool IsSubnormal(double x)
    {
        if (double.IsNaN(x) || double.IsInfinity(x) || x == 0)
        {
            return false;
        }
        return Math.Abs(x) < MinNormal64;
    }

#endif
}

public partial class VectorOpTest
{
    private static class Cache<T>
        where T : unmanaged
    {
        public static readonly Vector<T> True = Vector.Equals(Vector<T>.Zero, Vector<T>.Zero);
        public static readonly Vector<T> False = Vector.Equals(Vector<T>.One, Vector<T>.Zero);
        public static Vector<T> AsVector(bool value)
            => value ? True : False;
    }

    private static object[] CreateTestCase(double x)
        => new object[] { x };

    public static IEnumerable<object[]> IEEE754TestCases()
    {
        yield return CreateTestCase(0);
        yield return CreateTestCase(+1);
        yield return CreateTestCase(-1);
        yield return CreateTestCase(+double.Epsilon);
        yield return CreateTestCase(-double.Epsilon);
        yield return CreateTestCase(float.MaxValue);
        yield return CreateTestCase(float.MinValue);
        yield return CreateTestCase(1 / float.MaxValue);
        yield return CreateTestCase(1 / float.MinValue);
        yield return CreateTestCase(double.MaxValue);
        yield return CreateTestCase(double.MinValue);
        yield return CreateTestCase(1/double.MaxValue);
        yield return CreateTestCase(1/double.MinValue);
        yield return CreateTestCase(double.PositiveInfinity);
        yield return CreateTestCase(double.NegativeInfinity);
        yield return CreateTestCase(double.NaN);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsNormalizedTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(Helpers.IsNormal(x)),
            VectorOp.IsNormalized<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(Helpers.IsNormal((float)x)),
            VectorOp.IsNormalized<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsSubnormalizedTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(Helpers.IsSubnormal(x)),
            VectorOp.IsSubnormalized<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(Helpers.IsSubnormal((float)x)),
            VectorOp.IsSubnormalized<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsInfinity(x)),
            VectorOp.IsInfinity<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(float.IsInfinity((float)x)),
            VectorOp.IsInfinity<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsPsotiveInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsPositiveInfinity(x)),
            VectorOp.IsPositiveInfinity<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(float.IsPositiveInfinity((float)x)),
            VectorOp.IsPositiveInfinity<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsNegativeInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsNegativeInfinity(x)),
            VectorOp.IsNegativeInfinity<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(float.IsNegativeInfinity((float)x)),
            VectorOp.IsNegativeInfinity<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsNaNTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsNaN(x)),
            VectorOp.IsNaN<double>(new(x)),
            VectorComparer<double>.Instance);
        Assert.Equal(
            Cache<float>.AsVector(float.IsNaN((float)x)),
            VectorOp.IsNaN<float>(new((float)x)),
            VectorComparer<float>.Instance);
    }
}
