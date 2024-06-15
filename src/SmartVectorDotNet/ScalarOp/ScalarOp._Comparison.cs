namespace SmartVectorDotNet;

partial class ScalarOp
{
    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is less than <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool LessThan<T>(in T x, in T y)
        where T : unmanaged
        => Compare(x, y) < 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is less than or equals to <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool LessThanOrEquals<T>(in T x, in T y)
        where T : unmanaged
        => Compare(x, y) <= 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is greater than <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool GreaterThan<T>(in T x, in T y)
        where T : unmanaged
        => Compare(x, y) > 0;


    /// <summary>
    /// Returns <c>true</c> if <paramref name="x"/> is greater than or equals to <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool GreaterThanOrEquals<T>(in T x, in T y)
        where T : unmanaged
        => Compare(x, y) >= 0;
}
