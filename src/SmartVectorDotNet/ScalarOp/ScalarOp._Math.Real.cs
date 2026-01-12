namespace SmartVectorDotNet;

#pragma warning disable format
partial class ScalarOp
{
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
    /** <inheritdoc cref="Acosh_default"/> */ public static double Acosh(double d) => Math .Acosh(d);
    /** <inheritdoc cref="Acosh_default"/> */ public static float  Acosh(float  d) => MathF.Acosh(d);
    /** <inheritdoc cref="Asinh_default"/> */ public static double Asinh(double d) => Math .Asinh(d);
    /** <inheritdoc cref="Asinh_default"/> */ public static float  Asinh(float  d) => MathF.Asinh(d);
    /** <inheritdoc cref="Atanh_default"/> */ public static double Atanh(double d) => Math .Atanh(d);
    /** <inheritdoc cref="Atanh_default"/> */ public static float  Atanh(float  d) => MathF.Atanh(d);
    /** <inheritdoc cref="Cbrt_default"/> */ public static double Cbrt(double d) => Math .Cbrt(d);
    /** <inheritdoc cref="Cbrt_default"/> */ public static float  Cbrt(float  d) => MathF.Cbrt(d);
#else
    /** <inheritdoc cref="Acosh_default"/> */ public static double Acosh(double d) => throw new NotSupportedException();
    /** <inheritdoc cref="Acosh_default"/> */ public static float  Acosh(float  d) => throw new NotSupportedException();
    /** <inheritdoc cref="Asinh_default"/> */ public static double Asinh(double d) => throw new NotSupportedException();
    /** <inheritdoc cref="Asinh_default"/> */ public static float  Asinh(float  d) => throw new NotSupportedException();
    /** <inheritdoc cref="Atanh_default"/> */ public static double Atanh(double d) => throw new NotSupportedException();
    /** <inheritdoc cref="Atanh_default"/> */ public static float  Atanh(float  d) => throw new NotSupportedException();
    /** <inheritdoc cref="Cbrt_default"/> */ public static double Cbrt(double d) => throw new NotSupportedException();
    /** <inheritdoc cref="Cbrt_default"/> */ public static float  Cbrt(float  d) => throw new NotSupportedException();
#endif

    /** <inheritdoc cref="Log2_default"/> */ public static double Log2(double d) => Log(d, 2);
    /** <inheritdoc cref="Log2_default"/> */ public static float  Log2(float  d) => Log(d, 2);
}
#pragma warning restore format
