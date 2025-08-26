internal class CalculateVectorized : IExample
{
    public void Run(TextWriter logger)
    {
        var x = Enumerable.Range(0, 256).Select(i => (float)i).ToArray();
        var y = Enumerable.Range(0, 256).Select(i => (float)i).ToArray();
        var z = Enumerable.Range(0, 256).Select(i => (float)i).ToArray();
        var ans = new float[256];
        Vectorization.SIMD.Calculate<float, Formula>(x, y, z, ans);
        logger.WriteLine(string.Join(", ", ans));
    }
}

file readonly struct Formula : IVectorFormula3<float>
{
    public float Calculate(float x1, float x2, float x3) =>
        x1 * x1 + x2 * x2 + x3 * x3;

    public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3) =>
        x1 * x1 + x2 * x2 + x3 * x3;
}