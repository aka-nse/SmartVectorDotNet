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
        T[] x, T[] zExpected, T[] zActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        if (x.Length != zExpected.Length
            || x.Length != zActual.Length
            || !Core<T>.IsValidAccuracy(accuracy))
        {
            throw new ArgumentException();
        }
        AccurateCore(x, null, zExpected, zActual, accuracy, mode, output);
    }

    public static void Accurate<T>(
        T x, T[] y, T[] zExpected, T[] zActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        if (y.Length != zExpected.Length
            || y.Length != zActual.Length
            || !Core<T>.IsValidAccuracy(accuracy))
        {
            throw new ArgumentException();
        }
        var xx = new T[] { x };
        AccurateCore(xx, y, zExpected, zActual, accuracy, mode, output);
    }

    public static void Accurate<T>(
        T[] x, T y, T[] zExpected, T[] zActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        if (x.Length != zExpected.Length
            || x.Length != zActual.Length
            || !Core<T>.IsValidAccuracy(accuracy))
        {
            throw new ArgumentException();
        }
        var yy = new T[] { y };
        AccurateCore(x, yy, zExpected, zActual, accuracy, mode, output);
    }

    public static void Accurate<T>(
        T[] x, T[] y, T[] zExpected, T[] zActual,
        T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
        ITestOutputHelper? output = null)
        where T : unmanaged
#if NET7_0_OR_GREATER
            , INumber<T>, IMinMaxValue<T>
#endif
    {
        if (x.Length != zExpected.Length
            || x.Length != zActual.Length
            || y.Length != x.Length
            || !Core<T>.IsValidAccuracy(accuracy))
        {
            throw new ArgumentException();
        }
        AccurateCore(x, y, zExpected, zActual, accuracy, mode, output);
    }

    private static void AccurateCore<T>(
    T[]? x, T[]? y, T[] zExpected, T[] zActual,
    T accuracy, AccuracyMode mode = AccuracyMode.Absolute,
    ITestOutputHelper? output = null)
    where T : unmanaged
#if NET7_0_OR_GREATER
        , INumber<T>, IMinMaxValue<T>
#endif
    {

        var errors = Enumerable.Zip<T, T, T>(
            zExpected,
            zActual,
            (mode & AccuracyMode.AccuracyTest) switch
            {
                AccuracyMode.Absolute => Core<T>.ErrorAbs,
                AccuracyMode.Relative => Core<T>.ErrorRel,
                AccuracyMode.AbsoluteAndRelative => Core<T>.ErrorAbsAndRel,
                AccuracyMode.AbsoluteOrRelative => Core<T>.ErrorAbsOrRel,
                _ => throw new ArgumentOutOfRangeException(),
            }).ToArray();

        var sb = new StringBuilder();
        sb.AppendLine($"state,    x,    y,z-exp,z-act,error");
        var failed = new List<string>();
        for (var i = 0; i < errors.Length; ++i)
        {
            bool isAccurate = Core<T>.GetIsAccurate(accuracy, errors[i]);
            bool isValidNaN = Core<T>.GetIsValidNaN(zExpected[i], zActual[i], mode);
            var isOK = isAccurate || isValidNaN;
            var xtxt = x is { } ? $"{x[i % x.Length],5}" : " null";
            var ytxt = y is { } ? $"{y[i % y.Length],5}" : " null";
            sb.AppendLine($"{(isOK ? "OK" : "NG")}   ,{xtxt},{ytxt},{zExpected[i],5},{zActual[i],5},{errors[i]:0.000e+00}");
            if (isOK)
            {
                continue;
            }
            failed.Add($"{xtxt},{ytxt}");
        }
        if (failed.Count > 0)
        {
            output?.WriteLine(sb.ToString());
            Assert.Fail(
                $"Errors are greater than threshold at {failed.Count} points.\r\n"
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

        public static partial bool GetIsValidNaN(T zExpected, T zActual, AccuracyMode mode);

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
    Absolute = 0b0001,
    Relative = 0b0010,
    AbsoluteAndRelative = 0b0100,
    AbsoluteOrRelative = 0b1000,

    RelaxNaNCheck = 0b1_0000,
}