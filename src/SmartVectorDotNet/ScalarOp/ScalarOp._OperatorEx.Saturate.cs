namespace SmartVectorDotNet;

partial class ScalarOp
{
    /// <summary>
    /// Adds two values and saturates the result if can; otherwise returns simply add.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T AddSaturate<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var add = Add(lhs, rhs);
        if (typeof(T) == typeof(byte) ||
            typeof(T) == typeof(ushort) ||
            typeof(T) == typeof(uint) ||
            typeof(T) == typeof(ulong) ||
            typeof(T) == typeof(nuint))
        {
            return LessThanOrEqual(rhs, Subtract(Const<T>.MaxValue, lhs))
                ? add
                : Const<T>.MaxValue;
        }
        if (typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) ||
            typeof(T) == typeof(int) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(nint))
        {
            if (GreaterThanOrEqual(lhs, Const<T>.Zero))
            {
                return LessThanOrEqual(rhs, Subtract(Const<T>.MaxValue, lhs))
                    ? add
                    : Const<T>.MaxValue;
            }
            else
            {
                return LessThanOrEqual(Subtract(Const<T>.MinValue, lhs), rhs)
                    ? add
                    : Const<T>.MinValue;
            }
        }
        return add;
    }

    /// <summary>
    /// Subtracts two values and saturates the result if can; otherwise returns simply subtract.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T SubtractSaturate<T>(T lhs, T rhs)
        where T : unmanaged
    {
        var sub = Subtract(lhs, rhs);
        if (typeof(T) == typeof(byte) ||
            typeof(T) == typeof(ushort) ||
            typeof(T) == typeof(uint) ||
            typeof(T) == typeof(ulong) ||
            typeof(T) == typeof(nuint))
        {
            return GreaterThanOrEqual(lhs, rhs)
                ? sub
                : Const<T>.MinValue;
        }
        if (typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) ||
            typeof(T) == typeof(int) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(nint))
        {
            if (GreaterThanOrEqual(rhs, Const<T>.Zero))
            {
                return LessThanOrEqual(Add(Const<T>.MinValue, rhs), lhs)
                    ? sub
                    : Const<T>.MinValue;
            }
            else
            {
                return LessThanOrEqual(lhs, Add(Const<T>.MaxValue, rhs))
                    ? sub
                    : Const<T>.MaxValue;
            }
        }
        return sub;
    }
}
