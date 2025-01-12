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
            return LessThanOrEquals(rhs, Subtract(MaxValue<T>(), lhs))
                ? add
                : MaxValue<T>();
        }
        if (typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) ||
            typeof(T) == typeof(int) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(nint))
        {
            if (GreaterThanOrEquals(lhs, Zero<T>()))
            {
                return LessThanOrEquals(rhs, Subtract(MaxValue<T>(), lhs))
                    ? add
                    : MaxValue<T>();
            }
            else
            {
                return LessThanOrEquals(Subtract(MinValue<T>(), lhs), rhs)
                    ? add
                    : MinValue<T>();
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
            return GreaterThanOrEquals(lhs, rhs)
                ? sub
                : MinValue<T>();
        }
        if (typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) ||
            typeof(T) == typeof(int) ||
            typeof(T) == typeof(long) ||
            typeof(T) == typeof(nint))
        {
            if (GreaterThanOrEquals(rhs, Zero<T>()))
            {
                return LessThanOrEquals(Add(MinValue<T>(), rhs), lhs)
                    ? sub
                    : MinValue<T>();
            }
            else
            {
                return LessThanOrEquals(lhs, Add(MaxValue<T>(), rhs))
                    ? sub
                    : MaxValue<T>();
            }
        }
        return sub;
    }
}
