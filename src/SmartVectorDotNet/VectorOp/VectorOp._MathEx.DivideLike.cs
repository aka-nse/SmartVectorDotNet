namespace SmartVectorDotNet;
using H = InternalHelpers;

partial class VectorOp
{
    /// <summary>
    /// Calculates <c>Floor(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static Vector<T> DivideFloor<T>(Vector<T> a, Vector<T> b)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
        {
            return H.BitCastV<float, T>(Floor<float>(H.BitCastV<T, float>(a) / H.BitCastV<T, float>(b)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.BitCastV<double, T>(Floor<double>(H.BitCastV<T, double>(a) / H.BitCastV<T, double>(b)));
        }
        if (typeof(T) == typeof(byte)
            || typeof(T) == typeof(ushort)
            || typeof(T) == typeof(uint)
            || typeof(T) == typeof(ulong)
            || typeof(T) == typeof(nuint))
        {
            return Divide(a, b);
        }
        if (typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            a = Multiply(Sign(b), a);
            b = Multiply(Sign(b), b);
            return ConditionalSelect(
                GreaterThanOrEqual(a, Const<T>.Zero),
                Divide(a, b),
                Subtract(Divide(Add(a, Const<T>.One), b), Const<T>.One));
        }
        throw new NotSupportedException();
    }

    /// <summary>
    /// Calculates <c>Ceiling(a / b)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static Vector<T> DivideCeiling<T>(Vector<T> a, Vector<T> b)
        where T : unmanaged
    {
        if (typeof(T) == typeof(float))
        {
            return H.BitCastV<float, T>(Ceiling<float>(H.BitCastV<T, float>(a) / H.BitCastV<T, float>(b)));
        }
        if (typeof(T) == typeof(double))
        {
            return H.BitCastV<double, T>(Ceiling<double>(H.BitCastV<T, double>(a) / H.BitCastV<T, double>(b)));
        }
        if (typeof(T) == typeof(sbyte)
            || typeof(T) == typeof(short)
            || typeof(T) == typeof(int)
            || typeof(T) == typeof(long)
            || typeof(T) == typeof(nint))
        {
            a = Multiply(Sign(b), a);
            b = Multiply(Sign(b), b);
        }
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
            return ConditionalSelect(
                GreaterThan(a, Const<T>.Zero),
                Add(Divide(Subtract(a, Const<T>.One), b), Const<T>.One),
                Divide(a, b));
        }
        throw new NotSupportedException();
    }

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
            var quotient = DivideFloor(a, b);
            reminder = a - quotient * b;
            return quotient;
        }
        if (typeof(T) == typeof(float) || typeof(T) == typeof(double))
        {
            var quotient = Floor(a / b);
            reminder = FusedMultiplyAdd(quotient, -b, a);
            return quotient;
        }
        throw new NotSupportedException();
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