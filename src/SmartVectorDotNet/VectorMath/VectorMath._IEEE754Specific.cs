namespace SmartVectorDotNet;
using OP = VectorOp;
using SC = ScalarMath.Const;
using H = InternalHelpers;

partial class VectorMath
{
    internal class IEEE754Double_ : Const<double>
    {
        internal static readonly Vector<long> IntZero = Const<long>._0;
        internal static readonly Vector<long> IntOne = Const<long>._1;
        internal static readonly Vector<long> SignPartMask = new(SC.DoubleSignPartMask);
        internal static readonly Vector<long> ExpPartMask = new(SC.DoubleExpPartMask);
        internal static readonly Vector<long> FracPartMask = new(SC.DoubleFracPartMask);
        internal static readonly Vector<long> ExpPartBias = new(SC.DoubleExpPartBias);
        internal static readonly Vector<long> NRange = new(SC.DoubleExpPartMask >> SC.DoubleExpBitOffset);
        internal static readonly Vector<long> NMin = new(-SC.DoubleExpPartBias);
        internal static readonly Vector<long> NMax = NRange + NMin;

        internal static readonly Vector<double> FracPartOffsetDenom = new (SC.DoubleFracPartOffsetDenom);
    }

    internal class IEEE754Single_ : Const<float>
    {
        internal static readonly Vector<int> IntZero = Const<int>._0;
        internal static readonly Vector<int> IntOne = Const<int>._1;
        internal static readonly Vector<int> SignPartMask = new(SC.SingleSignPartMask);
        internal static readonly Vector<int> ExpPartMask = new(SC.SingleExpPartMask);
        internal static readonly Vector<int> FracPartMask = new(SC.SingleFracPartMask);
        internal static readonly Vector<int> ExpPartBias = new(SC.SingleExpPartBias);
        internal static readonly Vector<int> NRange = new(SC.SingleExpPartMask >> SC.SingleExpBitOffset);
        internal static readonly Vector<int> NMin = new(-SC.SingleExpPartBias);
        internal static readonly Vector<int> NMax = NRange + NMin;

        internal static readonly Vector<float> FracPartOffsetDenom = new (SC.SingleFracPartOffsetDenom);
    }

    #region Decompose

    /// <summary>
    /// Calculates the pair of <c>n</c> and <c>a</c>
    /// which satisfies <c>x = pow(2, n) * a</c>
    /// (<c>n</c> is integer, and <c>1 &lt;= abs(a) &lt; 2 or a == 0</c>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    public static void Decompose<T>(in Vector<T> x, out Vector<T> n, out Vector<T> a)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            Decompose(H.Reinterpret<T, double>(x), out Vector<long> nn, out Vector<double> aa);
            n = H.Reinterpret<double, T>(Vector.ConvertToDouble(nn));
            a = H.Reinterpret<double, T>(aa);
            return;
        }
        if (typeof(T) == typeof(float))
        {
            Decompose(H.Reinterpret<T, float>(x), out Vector<int> nn, out Vector<float> aa);
            n = H.Reinterpret<float, T>(Vector.ConvertToSingle(nn));
            a = H.Reinterpret<float, T>(aa);
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
    public static void Decompose(in Vector<double> x, out Vector<long> sign, out Vector<long> expo, out Vector<long> frac)
    {
        var bin = H.Reinterpret<double, long>(x);
        sign = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Double_.SignPartMask),
            SC.DoubleSignBitOffset);
        expo = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Double_.ExpPartMask),
            SC.DoubleExpBitOffset);
        frac = OP.BitwiseAnd(bin, IEEE754Double_.FracPartMask);
    }

    internal static void Decompose(in Vector<double> x, out Vector<long> n, out Vector<double> a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var xsign = OP.ConditionalSelect(
            OP.Equals(sign, IEEE754Double_.IntOne),
            IEEE754Double_._m1,
            IEEE754Double_._1);
        var economized = OP.ConditionalSelect(
            OP.Equals(expo, IEEE754Double_.IntZero),
            IEEE754Double_._0,
            IEEE754Double_._1);
        n = OP.Max(expo, IEEE754Double_.IntOne) - IEEE754Double_.ExpPartBias;
        a = xsign * (economized + OP.ConvertToDouble(frac) * IEEE754Double_.FracPartOffsetDenom);
    }

    /// <summary>
    /// Splits real number into IEEE754 part.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    public static void Decompose(in Vector<float> x, out Vector<int> sign, out Vector<int> expo, out Vector<int> frac)
    {
        var bin = H.Reinterpret<float, int>(x);
        sign = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Single_.SignPartMask),
            SC.SingleSignBitOffset);
        expo = OP.ShiftRightLogical(
            OP.BitwiseAnd(bin, IEEE754Single_.ExpPartMask),
            SC.SingleExpBitOffset);
        frac = OP.BitwiseAnd(bin, IEEE754Single_.FracPartMask);
    }

    internal static void Decompose(in Vector<float> x, out Vector<int> n, out Vector<float> a)
    {
        Decompose(x, out var sign, out var expo, out var frac);
        var xsign = OP.ConditionalSelect(
            OP.Equals(sign, IEEE754Single_.IntOne),
            IEEE754Single_._m1,
            IEEE754Single_._1);
        var economized = Vector.ConditionalSelect(
            OP.Equals(expo, IEEE754Single_.IntZero),
            IEEE754Single_._0,
            IEEE754Single_._1);
        n = OP.Max(expo, IEEE754Single_.IntOne) - IEEE754Single_.ExpPartBias;
        a = xsign * (economized + OP.ConvertToSingle(frac) * IEEE754Single_.FracPartOffsetDenom);
    }

    #endregion

    #region Scale

    /// <summary>
    /// Calculates <c>pow(2, n) * x</c>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="n">Must be integer.</param>
    /// <param name="x"></param>
    /// <returns></returns>
    [VectorMath]
    public static partial Vector<T> Scale<T>(in Vector<T> n, in Vector<T> x)
        where T : unmanaged;

    // pow(2, n) * x
    static Vector<double> Scale(in Vector<double> n, in Vector<double> x)
    {
        var nn = OP.ConvertToInt64(n);
        var pow2n = OP.ShiftLeft(nn + IEEE754Double_.ExpPartBias, 52);
        return x * OP.AsVectorDouble(pow2n);
    }


    // pow(2, n) * x
    static Vector<float> Scale(in Vector<float> n, in Vector<float> x)
    {
        var nn = OP.ConvertToInt32(n);
        var pow2n = OP.ShiftLeft(nn + IEEE754Single_.ExpPartBias, 23);
        return x * OP.AsVectorSingle(pow2n);
    }


    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static Vector<double> Scale(in Vector<long> sign, in Vector<long> expo, in Vector<long> frac)
        => H.Reinterpret<long, double>(OP.ShiftLeft(sign, SC.DoubleSignBitOffset) | OP.ShiftLeft(expo, SC.DoubleExpBitOffset) | frac);


    /// <summary>
    /// Composes IEEE754 parts into one real number.
    /// </summary>
    /// <param name="sign"></param>
    /// <param name="expo"></param>
    /// <param name="frac"></param>
    /// <returns></returns>
    public static Vector<float> Scale(in Vector<int> sign, in Vector<int> expo, in Vector<int> frac)
        => H.Reinterpret<int, float>(OP.ShiftLeft(sign, SC.SingleSignBitOffset) | OP.ShiftLeft(expo, SC.SingleExpBitOffset) | frac);

    #endregion

    #region IsNormalized

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 normalized number or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsNormalized<T>(in Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            Decompose(xx, out _, out var exp, out _);
            var retval = Vector.BitwiseAnd(
                Vector.GreaterThan(exp, IEEE754Double_.IntZero),
                Vector.LessThan(exp, IEEE754Double_.NRange));
            return H.Reinterpret<long, T>(retval);
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            Decompose(xx, out _, out var exp, out _);
            var retval = Vector.BitwiseAnd(
                Vector.GreaterThan(exp, IEEE754Single_.IntZero),
                Vector.LessThan(exp, IEEE754Single_.NRange));
            return H.Reinterpret<int, T>(retval);
        }
        return default;
    }

    #endregion

    #region IsSubnormalized

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 subnormalized number or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsSubnormalized<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            Decompose(xx, out _, out var exp, out var frac);
            var retval = Vector.BitwiseAnd(
                Vector.Equals(exp, IEEE754Double_.IntZero),
                Vector.OnesComplement(Vector.Equals(frac, IEEE754Double_.IntZero)));
            return H.Reinterpret<long, T>(retval);
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            Decompose(xx, out _, out Vector<int> exp, out var frac);
            var retval = Vector.BitwiseAnd(
                Vector.Equals(exp, IEEE754Single_.IntZero),
                Vector.OnesComplement(Vector.Equals(frac, IEEE754Single_.IntZero)));
            return H.Reinterpret<int, T>(retval);
        }
        return default;
    }

    #endregion

    #region IsInfinity

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 infinity or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsInfinity<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            return H.Reinterpret<long, T>(IsInfinity(xx));
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            return H.Reinterpret<int, T>(IsInfinity(xx));
        }
        return default;
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<long> IsInfinity(in Vector<double> x)
    {
        Decompose(x, out _, out var exp, out var frac);
        return Vector.BitwiseAnd(
            Vector.Equals(exp, IEEE754Double_.NRange),
            Vector.Equals(frac, IEEE754Double_.IntZero));
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<int> IsInfinity(in Vector<float> x)
    {
        Decompose(x, out _, out Vector<int> exp, out var frac);
        return Vector.BitwiseAnd(
            Vector.Equals(exp, IEEE754Single_.NRange),
            Vector.Equals(frac, IEEE754Single_.IntZero));
    }

    #endregion

    #region IsPositiveInfinity

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 positive infinity or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsPositiveInfinity<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            return H.Reinterpret<long, T>(IsPositiveInfinity(xx));
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            return H.Reinterpret<int, T>(IsPositiveInfinity(xx));
        }
        return default;
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 positive infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<long> IsPositiveInfinity(in Vector<double> x)
    {
        Decompose(x, out var sign, out Vector<long> n, out var a);
        return OP.BitwiseAnd(
            OP.BitwiseAnd(
                OP.Equals(n, IEEE754Double_.NRange),
                OP.Equals(a, IEEE754Double_.IntZero)
                ),
            OP.Equals(sign, IEEE754Double_.IntZero)
            );
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 positive infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<int> IsPositiveInfinity(in Vector<float> x)
    {
        Decompose(x, out var sign, out Vector<int> n, out var a);
        return OP.BitwiseAnd(
            OP.BitwiseAnd(
                OP.Equals(n, IEEE754Single_.NRange),
                OP.Equals(a, IEEE754Single_.IntZero)
                ),
            OP.Equals(sign, IEEE754Single_.IntZero)
            );
    }

    #endregion

    #region IsNegativeInfinity

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 negative infinity or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsNegativeInfinity<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            return H.Reinterpret<long, T>(IsNegativeInfinity(xx));
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            return H.Reinterpret<int, T>(IsNegativeInfinity(xx));
        }
        return default;
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 negative infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<long> IsNegativeInfinity(in Vector<double> x)
    {
        Decompose(x, out var sign, out Vector<long> n, out var a);
        return OP.BitwiseAnd(
            OP.BitwiseAnd(
                OP.Equals(n, IEEE754Double_.NRange),
                OP.Equals(a, IEEE754Double_.IntZero)
                ),
            OP.Equals(sign, IEEE754Double_.IntOne)
            );
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 negative infinity or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<int> IsNegativeInfinity(in Vector<float> x)
    {
        Decompose(x, out var sign, out Vector<int> n, out var a);
        return OP.BitwiseAnd(
            OP.BitwiseAnd(
                OP.Equals(n, IEEE754Single_.NRange),
                OP.Equals(a, IEEE754Single_.IntZero)
                ),
            OP.Equals(sign, IEEE754Single_.IntOne)
            );
    }

    #endregion

    #region IsNaN

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 NaN or not.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<T> IsNaN<T>(in Vector<T> x)
        where T : unmanaged
    {
        if (typeof(T) == typeof(double))
        {
            var xx = H.Reinterpret<T, double>(x);
            return H.Reinterpret<long, T>(IsNaN(xx));
        }
        if (typeof(T) == typeof(float))
        {
            var xx = H.Reinterpret<T, float>(x);
            return H.Reinterpret<int, T>(IsNaN(xx));
        }
        return default;
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 NaN or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<long> IsNaN(in Vector<double> x)
    {
        Decompose(x, out _, out Vector<long> n, out var a);
        return Vector.BitwiseAnd(
            Vector.Equals(n, IEEE754Double_.NRange),
            Vector.OnesComplement(Vector.Equals(a, IEEE754Double_.IntZero))
            );
    }

    /// <summary>
    /// Determines <paramref name="x"/> is IEEE754 NaN or not.
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static Vector<int> IsNaN(in Vector<float> x)
    {
        Decompose(x, out _, out Vector<int> n, out var a);
        return Vector.BitwiseAnd(
            Vector.Equals(n, IEEE754Single_.NRange),
            Vector.OnesComplement(Vector.Equals(a, IEEE754Single_.IntZero))
            );
    }

    #endregion
}