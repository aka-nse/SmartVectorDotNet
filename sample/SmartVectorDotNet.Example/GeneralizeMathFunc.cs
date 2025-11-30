internal class GeneralizeMathFunc : IExample
{
    public void Run(TextWriter logger)
    {
        var x = new Vector<double>([0.0, 1.0, 2.0, 3.0]);
        var sin_x_pi = VectorOp.Sin(VectorOp.Multiply(x, VectorOp.Const<double>.PI));
        logger.WriteLine(sin_x_pi);
    }
}
