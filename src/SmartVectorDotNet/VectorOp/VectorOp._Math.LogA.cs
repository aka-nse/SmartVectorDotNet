namespace SmartVectorDotNet;

partial class VectorOp
{
    /// <summary>
    /// Calculates log(x, newBase).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="newBase"></param>
    /// <returns></returns>
    public static Vector<T> Log<T>(Vector<T> x, Vector<T> newBase)
        where T : unmanaged
        => Log(x) / Log(newBase);
}