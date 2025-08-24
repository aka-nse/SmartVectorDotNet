// See https://aka.ms/new-console-template for more information
using SmartVectorDotNet;

float[] x = [.. Enumerable.Range(0, 1000).Select(i => (float)i)];
float[] y = [.. Enumerable.Range(0, 1000).Select(i => (float)i)];
var ans = new float[x.Length];

Vectorization.SIMD.Calculate<float>(
    static (x, y) => x + y,
    x,
    y,
    ans
);

Console.WriteLine("Hello, World!");
