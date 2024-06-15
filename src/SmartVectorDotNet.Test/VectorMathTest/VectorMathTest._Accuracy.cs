using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    internal void TestAccuracy(double[] x, Func<double, double> exp, Func<double, double> act, double accuracy, AccuracyMode mode)
    {
        var expected = x.Select(exp).ToArray();
        var actual = x.Select(act).ToArray();
        AccuracyAssert.Accurate(x, expected, actual, accuracy, mode, Output);
    }

    internal void TestAccuracy(double[] x, Func<float, float> exp, Func<float, float> act, float accuracy, AccuracyMode mode)
    {
        var xx = x.Select(x => (float)x).ToArray();
        var expected = xx.Select(x => exp(x)).ToArray();
        var actual = xx.Select(x => act(x)).ToArray();
        AccuracyAssert.Accurate(xx, expected, actual, accuracy, mode, Output);
    }

    [Fact]
    public void CbrtAccuracyTest()
    {
        var x = Enumerable.Range(-100, 201).Select(x => Math.E * x).ToArray();
        TestAccuracy(
            x,
            x => Math.Cbrt(x),
            x => VectorMath.Cbrt<double>(new(x))[0],
            1e-10, AccuracyMode.AbsoluteOrRelative);
    }


    [Fact]
    public void ModuloAccuracyTest()
    {
        var x = Enumerable.Range(-100, 201).Select(x => 10.0 * x).ToArray();
        TestAccuracy(
            x,
            x => x % Math.PI,
            x => VectorMath.Modulo<double>(new(x), new(Math.PI))[0],
            1e-10, AccuracyMode.Relative);
    }

    private static double[] TrigonometicTestCase { get; }
        = Enumerable
            .Range(-5, 11)
            .SelectMany(n => Enumerable.Range(-100, 201).Select(i => Math.PI * (2 * Math.Pow(n, 3) + i / 100.0)))
            .ToArray();

    [Fact]
    public void SinAccuracyTest()
    {
        var testCase = TrigonometicTestCase;
        TestAccuracy(
            testCase,
            x => Math.Sin(x),
            x => VectorMath.Sin(new Vector<double>(x))[0],
            1e-10, AccuracyMode.Absolute);
        TestAccuracy(
            testCase,
            x => MathF.Sin(x),
            x => VectorMath.Sin(new Vector<float>(x))[0],
            1e-5f, AccuracyMode.Absolute);
    }

    [Fact]
    public void CosAccuracyTest()
    {
        var testCase = TrigonometicTestCase;
        TestAccuracy(
            testCase,
            x => Math.Cos(x),
            x => VectorMath.Cos(new Vector<double>(x))[0],
            1e-10, AccuracyMode.Absolute);
        TestAccuracy(
            testCase,
            x => MathF.Cos(x),
            x => VectorMath.Cos(new Vector<float>(x))[0],
            1e-5f, AccuracyMode.Absolute);
    }

    [Fact]
    public void TanAccuracyTest()
    {
        var testCase = TrigonometicTestCase
            .Where(x => Math.Abs(Math.Abs(x / Math.PI) % 1 - 0.5) > 1e-5)
            .ToArray();
        TestAccuracy(
            testCase,
            x => Math.Tan(x),
            x => VectorMath.Tan(new Vector<double>(x))[0],
            1e-10, AccuracyMode.AbsoluteOrRelative);
        TestAccuracy(
            testCase,
            x => MathF.Tan(x),
            x => VectorMath.Tan(new Vector<float>(x))[0],
            1e-5f, AccuracyMode.AbsoluteOrRelative);
    }

    [Fact]
    public void AtanAccuracyTest()
    {
        var testCase = ArrayNumericUtils
            .LinspaceMaxInclusive(-2.0, +2.0, 1001)
            .Select(x => Math.Pow(x, 3))
            .ToArray();
        TestAccuracy(
            testCase,
            x => Math.Atan(x),
            x => VectorMath.Atan(new Vector<double>(x))[0],
            1e-10, AccuracyMode.AbsoluteOrRelative);
        TestAccuracy(
            testCase,
            x => MathF.Atan(x),
            x => VectorMath.Atan(new Vector<float>(x))[0],
            1e-5f, AccuracyMode.AbsoluteOrRelative);
    }

    [Fact]
    public void LogAccuracyTest()
    {
        var x = new double[] {
            // 1,
            // 2,
            // 3,
            // 5,
            // 1e+1,
            // 1e+2,
            // 1e+3,
            // 1e+5,
            // 1e-1,
            // 1e-2,
            // 1e-3,
            // 1e-5,
            float.Epsilon * 1e+0,
            float.Epsilon * 1e+1,
            float.Epsilon * 1e+2,
            float.Epsilon * 1e+3,
            float.Epsilon * 1e+4,
            float.Epsilon * 1e+5,
            double.Epsilon * 1e+0,
            double.Epsilon * 1e+1,
            double.Epsilon * 1e+2,
            double.Epsilon * 1e+3,
            double.Epsilon * 1e+4,
            double.Epsilon * 1e+5,
            0,
            -1,
        };

        TestAccuracy(
            x,
            x => Math.Log(x),
            x => VectorMath.Log(new Vector<double>(x))[0],
            1e-6, AccuracyMode.Relative);
        TestAccuracy(
            x,
            x => MathF.Log(x),
            x => VectorMath.Log(new Vector<float>(x))[0],
            1e-6f, AccuracyMode.Relative);
    }
}