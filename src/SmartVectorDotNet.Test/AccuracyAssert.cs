using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

internal static partial class AccuracyAssert
{
    public static void Accurate<T>(
        T[]? x, T[] yExpected, T[] yActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        x ??= yExpected;
        if(x.Length != yExpected.Length || x.Length != yActual.Length || !Core<T>.IsValidAccuracy(accuracy))
        {
            throw new ArgumentException();
        }

        var errors = Enumerable.Zip<T, T, T>(
            yExpected,
            yActual,
            (mode & AccuracyMode.AccuracyTest) switch
            {
                AccuracyMode.Absolute => Core<T>.ErrorAbs,
                AccuracyMode.Relative => Core<T>.ErrorRel,
                AccuracyMode.AbsoluteAndRelative => Core<T>.ErrorAbsAndRel,
                AccuracyMode.AbsoluteOrRelative => Core<T>.ErrorAbsOrRel,
                _ => throw new ArgumentOutOfRangeException(),
            }).ToArray();

        var sb = new StringBuilder();
        sb.AppendLine($"x({typeof(T).Name}),y-expected,y-actual,error");
        var failedX = new List<T>();
        for(var i = 0; i < errors.Length; ++i)
        {
            sb.AppendLine($"{x[i]},{yExpected[i]},{yActual[i]},{errors[i]:0.000e+00}");

            bool isAccurate = Core<T>.GetIsAccurate(accuracy, errors[i]);
            bool isValidNaN = Core<T>.GetIsValidNaN(yExpected[i], yActual[i], mode);
            if (isAccurate || isValidNaN)
            {
                continue;
            }
            failedX.Add(x[i]);
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

    private static partial class Core<T>
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        public static partial bool IsValidAccuracy(T accuracy);

        public static partial bool GetIsAccurate(T accuracy, T error);

        public static partial bool GetIsValidNaN(T yExpected, T yActual, AccuracyMode mode);

        public static partial T ErrorAbs(T exp, T act);

        public static partial T ErrorRel(T exp, T act);

        public static partial T ErrorAbsAndRel(T exp, T act);

        public static partial T ErrorAbsOrRel(T exp, T act);
    }
}


[Flags]
internal enum AccuracyMode
{
    AccuracyTest = 0b1111,
    Absolute            = 0b0001,
    Relative            = 0b0010,
    AbsoluteAndRelative = 0b0100,
    AbsoluteOrRelative  = 0b1000,

    RelaxNaNCheck = 0b1_0000,
}