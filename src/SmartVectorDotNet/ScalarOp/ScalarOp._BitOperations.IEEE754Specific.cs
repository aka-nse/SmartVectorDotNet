using GenericSpecialization;

namespace SmartVectorDotNet;
using H = InternalHelpers;
using static ScalarOp.Const;

partial class ScalarOp
{
    partial class Const
    {
#pragma warning disable IDE1006
        internal const int DoubleSignBitOffset = 63;
        internal const int DoubleExpBitOffset = 52;
        internal const long DoubleSignPartMask = 1L << DoubleSignBitOffset;
        internal const long DoubleExpPartMask = 0x7FFL << DoubleExpBitOffset;
        internal const long DoubleFracPartMask = (1L << DoubleExpBitOffset) - 1;
        internal const long DoubleExpPartBias = 1023L;
        internal const double DoubleFracPartOffsetDenom = 1.0 / (1L << DoubleExpBitOffset);

        internal const int SingleSignBitOffset = 31;
        internal const int SingleExpBitOffset = 23;
        internal const int SingleSignPartMask = 1 << SingleSignBitOffset;
        internal const int SingleExpPartMask = 0xFF << SingleExpBitOffset;
        internal const int SingleFracPartMask = (1 << SingleExpBitOffset) - 1;
        internal const int SingleExpPartBias = 127;
        internal const float SingleFracPartOffsetDenom = 1.0f / (1 << SingleExpBitOffset);
#pragma warning restore IDE1006
    }


    partial class Const<T>
    {
        /// <summary>
        /// Gets machine epsilon.
        /// </summary>
        public static readonly T MachineEpsilon
            = IsT<double>()
                ? H.BitCast<double, T>(Scale<double>(-DoubleExpBitOffset, 1))
            : IsT<float>()
                ? H.BitCast<float, T>(Scale<float>(-SingleExpBitOffset, 1))
            : default;

        /// <summary>
        /// Gets positive minimum normalized number.
        /// </summary>
        public static readonly T PositiveMinimumNormalizedNumber
            = IsT<double>()
                ? H.BitCast<double, T>(Scale<double>(1.0 - DoubleExpPartBias, 1))
            : IsT<float>()
                ? H.BitCast<float, T>(Scale<float>(1.0f - SingleExpPartBias, 1))
            : default;
    }


    /// <see cref="Decompose_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Decompose_default))]
    public static partial void Decompose<T>(T x, out T n, out T a)
        where T : unmanaged;

    /// <summary>
    /// Calculates the pair of <c>n</c> and <c>a</c>
    /// which satisfies <c>x = a * pow(2, n)</c>
    /// (<c>n</c> is an integer, and <c>1 &lt;= abs(a) &lt; 2 or a == 0</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    private static void Decompose_default<T>(T x, out T n, out T a)
        => throw new NotSupportedException();

    /// <see cref="Decompose_default" />
    public static void Decompose(double x, out double n, out double a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var s = sign == 0 ? 1.0 : -1.0;
        n = Max(expo, 1L) - DoubleExpPartBias;
        a = s * ((expo > 0 ? 1.0 : 0.0) + frac * DoubleFracPartOffsetDenom);
    }

    /// <see cref="Decompose_default" />
    public static void Decompose(float x, out float n, out float a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var s = (sign == 0 ? 1.0f : -1.0f);
        n = Max(expo, 1) - SingleExpPartBias;
        a = s * ((expo > 0 ? 1.0f : 0.0f) + frac * SingleFracPartOffsetDenom);
    }

    /// <summary>
    /// Splits real number into IEEE754 part.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    public static void Decompose(double x, out long sign, out long expo, out long frac)
    {
        var bin = H.BitCast<double, long>(x);
        sign = (bin & DoubleSignPartMask) >> DoubleSignBitOffset;
        expo = (bin & DoubleExpPartMask) >> DoubleExpBitOffset;
        frac = bin & DoubleFracPartMask;
    }

    /// <summary>
    /// Splits real number into IEEE754 part.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    public static void Decompose(float x, out int sign, out int expo, out int frac)
    {
        var bin = H.BitCast<float, int>(x);
        sign = (bin & SingleSignPartMask) >> SingleSignBitOffset;
        expo = (bin & SingleExpPartMask) >> SingleExpBitOffset;
        frac = bin & SingleFracPartMask;
    }


    /// <see cref="Scale_default" />
    /// <exception cref="NotSupportedException" />
    [PrimaryGeneric(nameof(Scale_default))]
    public static partial T Scale<T>(T n, T x)
        where T : unmanaged;

    /// <summary>
    /// Calculates <c>x * pow(2, n)</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private static T Scale_default<T>(T n, T x)
        => throw new NotSupportedException();

    /// <see cref="Scale_default" />
    public static double Scale(double n, double x)
        => x * H.BitCast<long, double>(((long)n + DoubleExpPartBias) << DoubleExpBitOffset);

    /// <see cref="Scale_default" />
    public static float Scale(float n, float x)
        => x * H.BitCast<int, float>(((int)n + SingleExpPartBias) << SingleExpBitOffset);


    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static double Scale(long sign, long expo, long frac)
        => H.BitCast<long, double>((sign << DoubleSignBitOffset) | (expo << DoubleExpBitOffset) | frac);

    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static float Scale(int sign, int expo, int frac)
        => H.BitCast<int, float>((sign << SingleSignBitOffset) | (expo << SingleExpBitOffset) | frac);
}