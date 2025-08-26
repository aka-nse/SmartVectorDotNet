// See https://aka.ms/new-console-template for more information
using SmartVectorDotNet;

int[] x = [.. Enumerable.Range(0, 1000).Select(i => (int)i)];
var y = new int[x.Length];
var z = new int[x.Length];
var vectorization = new InterceptVectorization(Console.Out);

vectorization.Calculate(
    static x => x * x,
    x.AsSpan(),
    y.AsSpan()
);

vectorization.Calculate(
    static (x, y) => ScalarOp.Add(x, y),
    x.AsSpan(),
    y.AsSpan(),
    z.AsSpan()
);

Console.WriteLine(string.Join(", ", z.Take(10)));


class InterceptVectorization(TextWriter logger) : SimdVectorization
{
    protected override void CalculateCore<T, TFormula>(TFormula formula, ReadOnlySpan<T> x1, Span<T> ans)
    {
        logger.WriteLine("Intercepterd by CalculateCore`2(formula, x1, ans)");
        base.CalculateCore(formula, x1, ans);
    }

    protected override void CalculateCore<T, TFormula>(TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> ans)
    {
        logger.WriteLine("Intercepterd by CalculateCore`2(formula, x1, x2, ans)");
        base.CalculateCore(formula, x1, x2, ans);
    }
}