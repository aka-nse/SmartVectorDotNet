using GenericSpecialization;
namespace SmartVectorDotNet;
using OP = ScalarOp;

partial class ScalarOp
{
    #region Sign

    /// <inheritdoc cref="Sign_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Sign_default))]
    public static partial T Sign<T>(T value)
        where T : unmanaged;

    /// <summary>
    /// Returns an integer that indicates the sign of a number.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    private static T Sign_default<T>(T value) => throw new NotSupportedException();

    /// <inheritdoc cref="Sign_default" />
    public static sbyte Sign(sbyte value) => (sbyte)Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static short Sign(short value) => (short)Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static int Sign(int value) => Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static long Sign(long value) => Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static float Sign(float value) => Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static double Sign(double value) => Math.Sign(value);

    /// <inheritdoc cref="Sign_default" />
    public static nint Sign(nint value)
    {
#if NET6_0_OR_GREATER
        return Math.Sign(value);
#else
        return IntPtr.Size switch
        {
            4 => Math.Sign((int)value),
            8 => Math.Sign((long)value),
            _ => throw new NotSupportedException(),
        };
#endif
    }

    #endregion

    /// <summary>
    /// Calculates FMA <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException" />
    public static T FusedMultiplyAdd<T>(T x, T y, T z)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if(typeof(T) == typeof(double))
        {
            return Reinterpret<double, T>(
                Math.FusedMultiplyAdd(
                    Reinterpret<T, double>(x),
                    Reinterpret<T, double>(y),
                    Reinterpret<T, double>(z)
                    )
                );
        }
        if (typeof(T) == typeof(float))
        {
            return Reinterpret<float, T>(
                MathF.FusedMultiplyAdd(
                    Reinterpret<T, float>(x),
                    Reinterpret<T, float>(y),
                    Reinterpret<T, float>(z)
                    )
                );
        }
#endif
        return OP.Add(OP.Multiply(x, y), z);
    }
}