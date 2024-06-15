using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorMathTest
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
            Cache<double>.AsVector(double.IsNormal(x)),
            VectorMath.IsNormalized<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsNormal((float)x)),
            VectorMath.IsNormalized<float>(new((float)x)));
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsSubnormalizedTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsSubnormal(x)),
            VectorMath.IsSubnormalized<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsSubnormal((float)x)),
            VectorMath.IsSubnormalized<float>(new((float)x)));
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsInfinity(x)),
            VectorMath.IsInfinity<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsInfinity((float)x)),
            VectorMath.IsInfinity<float>(new((float)x)));
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsPsotiveInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsPositiveInfinity(x)),
            VectorMath.IsPositiveInfinity<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsPositiveInfinity((float)x)),
            VectorMath.IsPositiveInfinity<float>(new((float)x)));
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsNegativeInfinityTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsNegativeInfinity(x)),
            VectorMath.IsNegativeInfinity<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsNegativeInfinity((float)x)),
            VectorMath.IsNegativeInfinity<float>(new((float)x)));
    }


    [Theory]
    [MemberData(nameof(IEEE754TestCases))]
    public void IsNaNTest(double x)
    {
        Assert.Equal(
            Cache<double>.AsVector(double.IsNaN(x)),
            VectorMath.IsNaN<double>(new(x)));
        Assert.Equal(
            Cache<float>.AsVector(float.IsNaN((float)x)),
            VectorMath.IsNaN<float>(new((float)x)));
    }
}
