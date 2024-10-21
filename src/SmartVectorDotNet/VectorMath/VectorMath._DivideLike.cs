namespace SmartVectorDotNet;

partial class VectorMath
{
    #region DivRem

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="reminder"/> will be same with <c>a</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    public static Vector<T> DivRem<T>(Vector<T> a, Vector<T> b, out Vector<T> reminder)
        where T : unmanaged
    {
        if (typeof(T) == typeof(byte)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(nuint)
            || typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            var quotient = a / b;
            reminder = a - quotient * b;
            return quotient;
        }
        if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
        {
            var quotient = Truncate(a / b);
            reminder = FusedMultiplyAdd(quotient, -b, a);
            return quotient;
        }
        throw new NotSupportedException();
    }

    #endregion

    #region DivRemByFloor

    /// <summary>
    /// Calculates DivRem so that the sign of <paramref name="reminder"/> will be same with <c>b</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="reminder"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Vector<T> DivRemByFloor<T>(Vector<T> a, Vector<T> b, out Vector<T> reminder)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
        {
            var quotient = Floor(a / b);
            reminder = FusedMultiplyAdd(quotient, -b, a);
            return quotient;
        }
        throw new NotSupportedException();
    }

    #endregion

    #region Modulo

    /// <summary>
    /// Calculates <c>a % b</c> so that its sign will be same with <c>a</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector<T> Modulo<T>(Vector<T> a, Vector<T> b)
        where T : unmanaged
    {
        DivRem(a, b, out var reminder);
        return reminder;
    }

    #endregion

    #region ModuloByFloor

    /// <summary>
    /// Calculates <c>a % b</c>so that its sign will be same with <c>b</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Vector<T> ModuloByFloor<T>(Vector<T> a, Vector<T> b)
        where T : unmanaged
    {
        DivRemByFloor(a, b, out var reminder);
        return reminder;
    }

    #endregion

}