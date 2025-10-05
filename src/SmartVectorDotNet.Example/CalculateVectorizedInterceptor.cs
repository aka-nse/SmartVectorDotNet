internal class CalculateVectorizedInterceptor : IExample
{
    public void Run(TextWriter logger)
    {
        const int count = 1000;
        int[] x = [.. Enumerable.Range(0, count)];
        int[] y = [.. Enumerable.Range(0, count).Select(i => i % 10)];
        int[] z = [.. Enumerable.Range(0, count).Select(i => (i + 1) /2)];
        int[] w = new int[x.Length];
        var vectorization = new InterceptVectorization(logger);

        vectorization.Calculate(
            static (x, y, z) => ScalarOp.Add(z, ScalarOp.Multiply(x, y)),
            x.AsSpan(),
            y.AsSpan(),
            z.AsSpan(),
            w.AsSpan()
        );

        logger.WriteLine(string.Join(", ", w.Take(10)));
    }
}

file class InterceptVectorization(TextWriter logger) : SimdVectorization
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

    protected override void CalculateCore<T, TFormula>(TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> ans)
    {
        logger.WriteLine("Intercepterd by CalculateCore`2(formula, x1, x2, x3, ans)");
        base.CalculateCore(formula, x1, x2, x3, ans);
    }
}