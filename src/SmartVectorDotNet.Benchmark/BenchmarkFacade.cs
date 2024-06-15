using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using static System.Net.Mime.MediaTypeNames;

namespace SmartVectorDotNet.Benchmark;

public static class BenchmarkFacade
{
    private readonly ref struct ConsoleScope
    {
        private readonly ConsoleColor _prevForeground;
        public ConsoleScope(ConsoleColor foreground)
        {
            _prevForeground = Console.ForegroundColor;
            Console.ForegroundColor = foreground;
        }
        public void Dispose()
        {
            Console.ForegroundColor = _prevForeground;
        }
    }

    public static void Run<T>()
        where T : new()
    {
#pragma warning disable CS8321
        static void warnNoTest()
        {
            using var _ = new ConsoleScope(ConsoleColor.Yellow);
            Console.Error.WriteLine("\u001b[31m" + "No benchmark was specified." + "\u001b[0m");
        }
#pragma warning restore CS8321

#if RELEASE
        Console.Error.WriteLine("This project run on release build.");
        Console.Error.WriteLine("Performance benchmark will be run.");
        BenchmarkRunner.Run<T>();
#elif DEBUG
        var type = typeof(T);
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        var controlMethods = methods
            .Where(m => m.GetCustomAttribute<PrecisionControlAttribute>() is { });
        var subjectMethods = methods
            .Where(m => m.GetCustomAttribute<PrecisionSubjectAttribute>() is { })
            .ToArray();
        var testGroups = new Dictionary<MethodInfo, List<MethodInfo>>();
        foreach (var controlMethod in controlMethods)
        {
            var targetType = controlMethod.ReturnType;
            testGroups.Add(controlMethod, new());
            foreach (var subjectMethod in subjectMethods.Where(m => m.ReturnType == targetType))
            {
                testGroups[controlMethod].Add(subjectMethod);
            }
        }
        if (testGroups.Count == 0)
        {
            warnNoTest();
            return;
        }
        Console.Error.WriteLine("This project run on debug build.");
        Console.Error.WriteLine("Precision benchmark will be run.");
        Console.Error.WriteLine();
        var benchmarkInstance = new T();
        using (new ConsoleScope(ConsoleColor.Magenta))
        {
            Console.WriteLine($"# Precision evaluation for {typeof(T).Name}");
        }
        Console.WriteLine();
        foreach (var test in testGroups)
        {
            var controlValues = test.Key.Invoke(benchmarkInstance, Array.Empty<object>());
            var nameLength = test.Value.Select(m => m.Name.Length).Max();
            using (new ConsoleScope(ConsoleColor.Magenta))
            {
                Console.WriteLine($"## Return type: {test.Key.ReturnType.GetElementType()}");
            }

            Console.WriteLine();
            Console.WriteLine($"Control method is `{test.Key.Name}`");
            Console.WriteLine();
            using (new ConsoleScope(ConsoleColor.Cyan))
            {
                Console.WriteLine("| method name" + new string('-', Math.Max(0, nameLength - 11)) + " | residual sum of squares |");
                Console.WriteLine("|:" + new string('-', Math.Max(11, nameLength)) + "-|-------------------------|");
                foreach (var subject in test.Value)
                {
                    var subjectValues = subject.Invoke(benchmarkInstance, Array.Empty<object>());
                    var rss = CalculatePrecision(controlValues!, subjectValues!);
                    Console.WriteLine($"| {subject.Name.PadRight(Math.Max(11, nameLength))} | {Format(rss),23} |");
                }
            }
            Console.WriteLine();
        }
#else
        WarnNoTest();
#endif
    }


    private static string Format(double value)
    {
        if (double.IsPositiveInfinity(value))
        {
            return "+Inf";
        }
        if (double.IsNegativeInfinity(value))
        {
            return "-Inf";
        }
        if (double.IsNaN(value))
        {
            return "NaN";
        }
        return value.ToString("0.00000+e000");
    }


    private static double CalculatePrecision(object control, object subject)
    {
        switch ((control, subject))
        {
        case (double[] control_f64, double[] subject_f64):
            if (control_f64.Length != subject_f64.Length)
            {
                throw new ArgumentException();
            }
            return Enumerable.Zip(control_f64, subject_f64, (c, s) => (c - s) * (c - s)).Sum();
        case (float[] control_f32, float[] subject_f32):
            if (control_f32.Length != subject_f32.Length)
            {
                throw new ArgumentException();
            }
            return Enumerable.Zip(control_f32, subject_f32, (c, s) => (c - s) * (c - s)).Sum();
        default:
            throw new ArgumentException();
        }
    }
}

[AttributeUsage(AttributeTargets.Method)]
public class PrecisionControlAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Method)]
public class PrecisionSubjectAttribute : Attribute { }