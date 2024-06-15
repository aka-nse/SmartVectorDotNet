namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


partial class VectorMath
{
    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Vector<T> Min<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => Vector.Min(left, right);


    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static Vector<T> Max<T>(in Vector<T> left, in Vector<T> right)
        where T : unmanaged
        => Vector.Max(left, right);


    /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/> and <paramref name="max"/>.
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector<T> Clamp<T>(in Vector<T> value, in Vector<T> min, in Vector<T> max)
        where T : unmanaged
        => Vector.Min(Vector.Max(value, min), max);
}
