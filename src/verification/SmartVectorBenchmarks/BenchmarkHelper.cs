using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorBenchmarks;

public static class BenchmarkHelper
{
    public static double CalculateErrorRelative(double[] expected, double[] actual)
    {
        var x = new double[expected.Length];
        var y = expected.Zip(actual, (exp, act) => (exp - act) / (Math.Abs(exp) + 1)).ToArray();
        return Math.Sqrt(ResidualSumOfSquares(x, y) / expected.Length);
    }


    public static double CalculateErrorAbsolute(double[] expected, double[] actual)
        => Math.Sqrt(ResidualSumOfSquares(expected, actual) / expected.Length);

    public static double ResidualSumOfSquares(double[] x, double[] y)
    {
        var retval = 0.0;
        for (var i = 0; i < x.Length; ++i)
        {
            var residual = x[i] - y[i];
            retval += residual * residual;
        }
        return retval;
    }
}