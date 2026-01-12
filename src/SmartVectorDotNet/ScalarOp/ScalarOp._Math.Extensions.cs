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

    #endregion Sign

    #region FusedMultiplyAdd

    /// <inheritdoc cref="FusedMultiplyAdd_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(FusedMultiplyAdd_default))]
    public static partial T FusedMultiplyAdd<T>(T x, T y, T z)
        where T : unmanaged;

    /// <summary>
    /// Calculates FMA <c>(x * y) + z</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    private static T FusedMultiplyAdd_default<T>(T x, T y, T z)
        where T : unmanaged
        => OP.Add(OP.Multiply(x, y), z);

#if NET6_0_OR_GREATER
    /// <inheritdoc cref="FusedMultiplyAdd_default" />
    public static float FusedMultiplyAdd(float x, float y, float z)
        => MathF.FusedMultiplyAdd(x, y, z);
        
    /// <inheritdoc cref="FusedMultiplyAdd_default" />
    public static double FusedMultiplyAdd(double x, double y, double z)
        => Math.FusedMultiplyAdd(x, y, z);
#endif

    #endregion FuseMultiplyAdd
}