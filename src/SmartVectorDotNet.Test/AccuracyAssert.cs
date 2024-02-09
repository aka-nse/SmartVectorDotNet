using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

internal static class AccuracyAssert
{
    public static void Accurate<T>(
        T[] x, T[] yExpected, T[] yActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged, INumber<T>, IMinMaxValue<T>
    {
        static T errorAbs(T exp, T act)
            => T.Abs(exp - act);

        static T errorRel(T exp, T act)
            => T.Abs(exp - act) / (T.Abs(exp) + T.One / T.MaxValue);

        static T errorAbsAndRel(T exp, T act)
            => T.Max(errorAbs(exp, act), errorRel(exp, act));

        static T errorAbsOrRel(T exp, T act)
            => T.Min(errorAbs(exp, act), errorRel(exp, act));

        if(x.Length != yExpected.Length || x.Length != yActual.Length || accuracy < T.Zero)
        {
            throw new ArgumentException();
        }

        var errors = Enumerable.Zip<T, T, T>(
            yExpected,
            yActual,
            mode switch
            {
                AccuracyMode.Absolute => errorAbs,
                AccuracyMode.Relative => errorRel,
                AccuracyMode.AbsoluteAndRelative => errorAbsAndRel,
                AccuracyMode.AbsoluteOrRelative => errorAbsOrRel,
                _ => throw new ArgumentOutOfRangeException(),
            }).ToArray();

        var sb = new StringBuilder();
        sb.AppendLine("x,y-expected,y-actual,error");
        var failedX = new List<T>();
        for(var i = 0; i < errors.Length; ++i)
        {
            sb.AppendLine($"{x[i]},{yExpected[i]},{yActual[i]},{errors[i]}");

            bool isLargeError = !T.IsNaN(errors[i]) && errors[i] > accuracy;
            bool isInvalidNaN = T.IsNaN(yExpected[i]) ^ T.IsNaN(yActual[i]);
            if (isLargeError || isInvalidNaN)
            {
                failedX.Add(x[i]);
            }
        }
        output?.WriteLine(sb.ToString());
        if(failedX.Count > 0)
        {
            Assert.Fail(
                $"Errors are greater than threshold at {failedX.Count} points.\r\n"
                + $"(test type: {typeof(T)})\r\n"
                + $"(maximum error: {errors.Max():0.000e+00})");
        }
    }
}


internal enum AccuracyMode
{
    Absolute,
    Relative,
    AbsoluteAndRelative,
    AbsoluteOrRelative,
}