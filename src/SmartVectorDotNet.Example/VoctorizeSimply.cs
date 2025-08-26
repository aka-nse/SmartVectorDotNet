internal class VoctorizeSimply : IExample
{
    public void Run(TextWriter logger)
    {
        var x = Enumerable.Range(0, 256).Select(i => (double)i).ToArray();
        var tmp = new double[x.Length];
        var ans = new double[x.Length];
        Vectorization.SIMD.Multiply<double>(x, Math.PI, tmp);
        Vectorization.SIMD.Sin<double>(tmp, ans);
        logger.WriteLine(string.Join(", ", ans));
    }
}
