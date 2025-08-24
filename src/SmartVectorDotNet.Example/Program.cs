// See https://aka.ms/new-console-template for more information
using SmartVectorDotNet;

int[] x = [.. Enumerable.Range(0, 1000).Select(i => (int)i)];
var y = new int[x.Length];
var z = new int[x.Length];

Vectorization.SIMD.Calculate(
    static x => x * x,
    x.AsSpan(),
    y.AsSpan()
);

Vectorization.SIMD.Calculate(
    static (x, y) => x + y,
    x.AsSpan(),
    y.AsSpan(),
    z.AsSpan()
);

Console.WriteLine(string.Join(", ", z.Take(10)));
