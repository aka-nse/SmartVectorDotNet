namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Exp_<T> : VectorMath.Const<T> where T : unmanaged
{
    internal static readonly Vector<T> Max;
    internal static readonly Vector<T> Min;

    internal static readonly Vector<T> Log_E_2_Upper;
    internal static readonly Vector<T> Log_E_2_Lower;

    internal static ReadOnlySpan<Vector<T>> Coeffs => _expCoeffs;
    private static readonly Vector<T>[] _expCoeffs;

    static Exp_()
    {
        if (IsT<double>())
        {
            Max = AsVector(ScalarMath.Log(double.MaxValue));
            Min = AsVector(ScalarMath.Log(1 / double.MaxValue));   Log_E_2_Upper = AsVector(0.693147180559945);
            Log_E_2_Lower = AsVector(3.0941723212145817656807550013434e-16);
            var expCoeffs = new Vector<double>[12]
            {
                new(1.0),
                new(1.0 / 1),
                new(1.0 / 2),
                new(1.0 / 6),
                new(1.0 / 24),
                new(1.0 / 120),
                new(1.0 / 720),
                new(1.0 / 5040),
                new(1.0 / 40320),
                new(1.0 / 362880),
                new(1.0 / 3628800),
                new(1.0 / 39916800),
            };
            _expCoeffs = H.Reinterpret<Vector<double>[], Vector<T>[]>(expCoeffs);
            return;
        }
        if (IsT<float>())
        {
            Max = AsVector(ScalarMath.Log(float.MaxValue));
            Min = AsVector(ScalarMath.Log(1 / float.MaxValue));
            Log_E_2_Upper = AsVector(0.6931471824646f);
            Log_E_2_Lower = AsVector(-1.9046546905827678785418234319245e-9f);
            var expCoeffs = new Vector<float>[7]
            {
                new(1f),
                new(1f / 1),
                new(1f / 2),
                new(1f / 6),
                new(1f / 24),
                new(1f / 120),
                new(1f / 720),
            };
            _expCoeffs = H.Reinterpret<Vector<float>[], Vector<T>[]>(expCoeffs);
            return;
        }
        _expCoeffs = [];
    }
}


partial class VectorMath
{
    /// <summary> Calculates exp. </summary>
    public static Vector<T> Exp<T>(Vector<T> x)
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
    private static partial Vector<T> ExpCore<T>(Vector<T> d)
        where T : unmanaged;

#pragma warning disable format
    private static Vector<double> ExpCore(Vector<double> x)
    {
        var y = x * Exp_<double>.Log_2_E;
        var n = Round(y);
        var a = FusedMultiplyAdd(
            -n, Exp_<double>.Log_E_2_Lower,
            FusedMultiplyAdd(
                -n, Exp_<double>.Log_E_2_Upper,
                x
                )
            );
        var z = Exp_<double>._0;                              // a_12~
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[11]);  // a_11
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[10]);  // a_10
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 9]);  // a_9
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 8]);  // a_8
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 7]);  // a_7
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 6]);  // a_6
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 5]);  // a_5
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 4]);  // a_4
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 3]);  // a_3
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 2]);  // a_2
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 1]);  // a_1
        z = FusedMultiplyAdd(a, z, Exp_<double>.Coeffs[ 0]);  // a_0
        return Scale(n, z);
    }

    private static Vector<float> ExpCore(Vector<float> x)
    {
        var y = x * Exp_<float>.Log_2_E;
        var n = Round(y);
        var a = FusedMultiplyAdd(
            -n, Exp_<float>.Log_E_2_Lower,
            FusedMultiplyAdd(
                -n, Exp_<float>.Log_E_2_Upper,
                x
                )
            );
        var z = Exp_<float>._0;                             // a_7~
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[6]);  // a_6
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[5]);  // a_5
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[4]);  // a_4
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[3]);  // a_3
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[2]);  // a_2
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[1]);  // a_1
        z = FusedMultiplyAdd(a, z, Exp_<float>.Coeffs[0]);  // a_0
        return Scale(n, z);
    }
#pragma warning restore format
}