using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public class VectorMathTest
{

    public static IEnumerable<object[]> ModuloTestCases()
    {
        static object[] core(double x) => new object[] { x };

        for(var x = -1000; x <= 1000; x += 10)
        {
            yield return core(x);
        }
    }

    [Theory]
    [MemberData(nameof(ModuloTestCases))]
    public void ModuloTest(double x)
    {
        var expected = x % Math.PI;
        var actual = VectorMath.Modulo<double>(new (x), new(Math.PI))[0];
        Assert.Equal(1.0, 1.0 + (expected - actual), 15);
    }


    public static IEnumerable<object[]> TrigonometicTestCases()
    {
        static object[] core(double x) => new object[] { x };

        for(var i = -1000; i <= 1000; ++i)
        {
            var x = i / 1000.0;
            yield return core(Math.PI * x);
        }
        /*
        for(var x = -1000; x <= 1000; x += 10)
        {
            yield return core(x);
        }
        */
    }

    [Theory]
    [MemberData(nameof(TrigonometicTestCases))]
    public void SinTest(double x)
    {
        var expected = Math.Sin(x);
        var actual = VectorMath.Sin(new Vector<double>(x))[0];
        Assert.Equal(1.0, 1.0 + (expected - actual), 10);
    }
}