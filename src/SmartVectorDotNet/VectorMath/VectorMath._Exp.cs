namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Exp_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> Max = AsVector(ScalarMath.Log(double.MaxValue));
    internal static readonly Vector<T> Min = AsVector(ScalarMath.Log(1 / double.MaxValue));

    internal static ReadOnlySpan<Vector<T>> Coeffs => _expCoeffs;
    private static readonly Vector<T>[] _expCoeffs = GetExpCoeffs();
    static Vector<T>[] GetExpCoeffs()
    {
        if (IsT<double>())
        {
            var expCoeffs = new Vector<double>[11];
            for (var i = 1; i <= 11; ++i)
            {
                expCoeffs[i - 1] = new Vector<double>(1 / (double)i);
            }
            return H.Reinterpret<Vector<double>[], Vector<T>[]>(expCoeffs);
        }
        if (IsT<float>())
        {
            var expCoeffs = new Vector<float>[6];
            for (var i = 1; i <= 6; ++i)
            {
                expCoeffs[i - 1] = new Vector<float>(1 / (float)i);
            }
            return H.Reinterpret<Vector<float>[], Vector<T>[]>(expCoeffs);
        }
        return default!;
    }
}


partial class VectorMath
{
    /// <summary> Calculates exp. </summary>
    public static Vector<T> Exp<T>(in Vector<T> x)
        where T : unmanaged
    {
        var isSaturatedMax = OP.GreaterThan(x, Exp_<T>.Max);
        var isSaturatedMin = OP.LessThan(x, Exp_<T>.Min);
        var isNormal = OP.OnesComplement(OP.BitwiseOr(isSaturatedMax, isSaturatedMin));

        return OP.ConditionalSelect(
            isNormal,
            ExpCore(x),
            OP.ConditionalSelect(
                isSaturatedMin,
                Exp_<T>._0,
                Exp_<T>.PInf)
            );
    }

    [VectorMath]
    private static partial Vector<T> ExpCore<T>(in Vector<T> d)
        where T : unmanaged;

#pragma warning disable format
    private static Vector<double> ExpCore(in Vector<double> x)
    {
        var y = x * Exp_<double>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Exp_<double>.Log_E_2;
        var z = Exp_<double>._0;                                        // a_11~
        z = (b * Exp_<double>.Coeffs[10 - 1]) * (Exp_<double>._1 + z);  // a_10
        z = (b * Exp_<double>.Coeffs[ 9 - 1]) * (Exp_<double>._1 + z);  // a_9
        z = (b * Exp_<double>.Coeffs[ 8 - 1]) * (Exp_<double>._1 + z);  // a_8
        z = (b * Exp_<double>.Coeffs[ 7 - 1]) * (Exp_<double>._1 + z);  // a_7
        z = (b * Exp_<double>.Coeffs[ 6 - 1]) * (Exp_<double>._1 + z);  // a_6
        z = (b * Exp_<double>.Coeffs[ 5 - 1]) * (Exp_<double>._1 + z);  // a_5
        z = (b * Exp_<double>.Coeffs[ 4 - 1]) * (Exp_<double>._1 + z);  // a_4
        z = (b * Exp_<double>.Coeffs[ 3 - 1]) * (Exp_<double>._1 + z);  // a_3
        z = (b * Exp_<double>.Coeffs[ 2 - 1]) * (Exp_<double>._1 + z);  // a_2
        z = (b * Exp_<double>.Coeffs[ 1 - 1]) * (Exp_<double>._1 + z);  // a_1
        z = z + Exp_<double>._1;                                        // a_0
        return Scale(n, z);
    }

    private static Vector<float> ExpCore(in Vector<float> x)
    {
        var y = x * Exp_<float>.Log_2_E;
        var n = Round(y);
        var a = y - n;
        var b = a * Exp_<float>.Log_E_2;
        var z = Exp_<float>._0;                                         // a_7~
        z = (b * Exp_<float>.Coeffs[6 - 1]) * (Exp_<float>._1 + z);  // a_6
        z = (b * Exp_<float>.Coeffs[5 - 1]) * (Exp_<float>._1 + z);  // a_5
        z = (b * Exp_<float>.Coeffs[4 - 1]) * (Exp_<float>._1 + z);  // a_4
        z = (b * Exp_<float>.Coeffs[3 - 1]) * (Exp_<float>._1 + z);  // a_3
        z = (b * Exp_<float>.Coeffs[2 - 1]) * (Exp_<float>._1 + z);  // a_2
        z = (b * Exp_<float>.Coeffs[1 - 1]) * (Exp_<float>._1 + z);  // a_1
        z = z + Exp_<float>._1;                                         // a_0
        return Scale(n, z);
    }
#pragma warning restore format
}