namespace SmartVectorDotNet;
using static ScalarMath.Const;

partial class ScalarMath
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
                ? Reinterpret(Scale<double>(-DoubleExpBitOffset, 1))
            : IsT<float>()
                ? Reinterpret(Scale<float>(-SingleExpBitOffset, 1))
            : default;

        /// <summary>
        /// Gets positive minimum normalized number.
        /// </summary>
        public static readonly T PositiveMinimumNormalizedNumber
            = IsT<double>()
                ? Reinterpret(Scale<double>(1.0 - DoubleExpPartBias, 1))
            : IsT<float>()
                ? Reinterpret(Scale<float>(1.0f - SingleExpPartBias, 1))
            : default;
    }


    /// <summary>
    /// Calculates the pair of <c>n</c> and <c>a</c>
    /// which satisfies <c>x = pow(2, n) * a</c>
    /// (<c>n</c> is non-negative integer, and <c>1 &lt;= abs(a) &lt; 2 or a == 0</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    public static void Decompose<T>(T x, out T n, out T a)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            Decompose(Reinterpret<T, double>(x), out long nn, out double aa);
            n = Reinterpret<double, T>(nn);
            a = Reinterpret<double, T>(aa);
            return;
        }
        if (typeof(T) == typeof(float))
        {
            Decompose(Reinterpret<T, float>(x), out int nn, out float aa);
            n = Reinterpret<float, T>(nn);
            a = Reinterpret<float, T>(aa);
            return;
        }
        throw new NotSupportedException();
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
        var bin = Reinterpret<double, long>(x);
        sign = (bin & DoubleSignPartMask) >> DoubleSignBitOffset;
        expo = (bin & DoubleExpPartMask) >> DoubleExpBitOffset;
        frac = bin & DoubleFracPartMask;
    }


    internal static void Decompose(double x, out long n, out double a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var s = sign == 0 ? 1.0 : -1.0;
        n = Max(expo, 1L) - DoubleExpPartBias;
        a = s * ((expo > 0 ? 1.0 : 0.0) + frac * DoubleFracPartOffsetDenom);
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
        var bin = Reinterpret<float, int>(x);
        sign = (bin & SingleSignPartMask) >> SingleSignBitOffset;
        expo = (bin & SingleExpPartMask) >> SingleExpBitOffset;
        frac = bin & SingleFracPartMask;
    }


    internal static void Decompose(float x, out int n, out float a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var s = (sign == 0 ? 1.0f : -1.0f);
        n = Max(expo, 1) - SingleExpPartBias;
        a = s * ((expo > 0 ? 1.0f : 0.0f) + frac * SingleFracPartOffsetDenom);
    }


    /// <summary>
    /// Calculates <c>pow(2, n) * x</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static T Scale<T>(T n, T x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var nn = Reinterpret<T, double>(n);
            var xx = Reinterpret<T, double>(x);
            return Reinterpret<double, T>(Reinterpret<long, double>(((long)nn + DoubleExpPartBias) << DoubleExpBitOffset) * xx);
        }
        if (typeof(T) == typeof(float))
        {
            var nn = Reinterpret<T, float>(n);
            var xx = Reinterpret<T, float>(x);
            return Reinterpret<float, T>(Reinterpret<int, float>(((int)nn + SingleExpPartBias) << SingleExpBitOffset) * xx);
        }
        throw new NotSupportedException();
    }


    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static double Scale(long sign, long expo, long frac)
        => Reinterpret<long, double>((sign << DoubleSignBitOffset) | (expo << DoubleExpBitOffset) | frac);


    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static float Scale(int sign, int expo, int frac)
        => Reinterpret<int, float>((sign << SingleSignBitOffset) | (expo << SingleExpBitOffset) | frac);
}