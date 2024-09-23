using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;


public partial class VectorizationTest
{
    private const AccuracyMode TestAccuracyMode
        = AccuracyMode.AbsoluteOrRelative | AccuracyMode.RelaxNaNCheck;

    public static IEnumerable<object[]> UnaryOperatorTestCases()
    {
        static object[] core(params double[] x)
            => new object[]{ x, };

        yield return core();
        yield return core(0);
        yield return core(0, 1, 2, 3);
        yield return core(0, 1, 2, 3, 4);
        yield return core(0, 1, 2, 3, 4, 5, 6, 7);
        yield return core(0, 1, 2, 3, 4, 5, 6, 7, 8);
        yield break;
    }
    
    public static IEnumerable<object[]> BinaryOperatorTestCases()
    {
        static object[] core(double[] x, double[] y)
            => new object[]{ x, y, };

        yield return core(Array.Empty<double>(), Array.Empty<double>());
        yield return core(new[]{-1.0}, new[]{-1.0});
        yield return core(new[]{-1.0}, new[]{+0.0});
        yield return core(new[]{-1.0}, new[]{+1.0});
        yield return core(new[]{+0.0}, new[]{-1.0});
        yield return core(new[]{+0.0}, new[]{+0.0});
        yield return core(new[]{+0.0}, new[]{+1.0});
        yield return core(new[]{+1.0}, new[]{-1.0});
        yield return core(new[]{+1.0}, new[]{+0.0});
        yield return core(new[]{+1.0}, new[]{+1.0});
        yield break;
    }


    // Void Atanh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Atanh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Atanh<double>(x, exp);
            Vectorization.SIMD.Atanh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Atanh<float>(xx, exp);
            Vectorization.SIMD.Atanh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Cbrt[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Cbrt_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Cbrt<double>(x, exp);
            Vectorization.SIMD.Cbrt<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Cbrt<float>(xx, exp);
            Vectorization.SIMD.Cbrt<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Log2[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Log2_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Log2<double>(x, exp);
            Vectorization.SIMD.Log2<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Log2<float>(xx, exp);
            Vectorization.SIMD.Log2<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Ceiling[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Ceiling_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Ceiling<double>(x, exp);
            Vectorization.SIMD.Ceiling<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Ceiling<float>(xx, exp);
            Vectorization.SIMD.Ceiling<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Floor[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Floor_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Floor<double>(x, exp);
            Vectorization.SIMD.Floor<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Floor<float>(xx, exp);
            Vectorization.SIMD.Floor<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Exp[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Exp_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Exp<double>(x, exp);
            Vectorization.SIMD.Exp<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Exp<float>(xx, exp);
            Vectorization.SIMD.Exp<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Log[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Log_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Log<double>(x, exp);
            Vectorization.SIMD.Log<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Log<float>(xx, exp);
            Vectorization.SIMD.Log<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Log10[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Log10_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Log10<double>(x, exp);
            Vectorization.SIMD.Log10<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Log10<float>(xx, exp);
            Vectorization.SIMD.Log10<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Round[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Round_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Round<double>(x, exp);
            Vectorization.SIMD.Round<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Round<float>(xx, exp);
            Vectorization.SIMD.Round<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Truncate[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Truncate_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Truncate<double>(x, exp);
            Vectorization.SIMD.Truncate<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Truncate<float>(xx, exp);
            Vectorization.SIMD.Truncate<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Atan2[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Atan2_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Atan2<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Atan2<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Atan2<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Atan2<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Atan2<double>(x, y, exp);
            Vectorization.SIMD.Atan2<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Atan2<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Atan2<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Atan2<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Atan2<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Atan2<float>(xx, yy, exp);
            Vectorization.SIMD.Atan2<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Pow[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Pow_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Pow<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Pow<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Pow<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Pow<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Pow<double>(x, y, exp);
            Vectorization.SIMD.Pow<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Pow<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Pow<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Pow<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Pow<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Pow<float>(xx, yy, exp);
            Vectorization.SIMD.Pow<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Log[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Log_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Log<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Log<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Log<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Log<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Log<double>(x, y, exp);
            Vectorization.SIMD.Log<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Log<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Log<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Log<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Log<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Log<float>(xx, yy, exp);
            Vectorization.SIMD.Log<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Scale[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Scale_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Scale<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Scale<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Scale<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Scale<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Scale<double>(x, y, exp);
            Vectorization.SIMD.Scale<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Scale<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Scale<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Scale<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Scale<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Scale<float>(xx, yy, exp);
            Vectorization.SIMD.Scale<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void UnaryPlus[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_UnaryPlus_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.UnaryPlus<double>(x, exp);
            Vectorization.SIMD.UnaryPlus<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.UnaryPlus<float>(xx, exp);
            Vectorization.SIMD.UnaryPlus<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void UnaryMinus[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_UnaryMinus_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.UnaryMinus<double>(x, exp);
            Vectorization.SIMD.UnaryMinus<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.UnaryMinus<float>(xx, exp);
            Vectorization.SIMD.UnaryMinus<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Complement[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Complement_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Complement<double>(x, exp);
            Vectorization.SIMD.Complement<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Complement<float>(xx, exp);
            Vectorization.SIMD.Complement<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void BitwiseOr[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_BitwiseOr_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.BitwiseOr<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.BitwiseOr<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseOr<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseOr<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseOr<double>(x, y, exp);
            Vectorization.SIMD.BitwiseOr<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.BitwiseOr<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.BitwiseOr<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseOr<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseOr<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseOr<float>(xx, yy, exp);
            Vectorization.SIMD.BitwiseOr<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void BitwiseXor[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_BitwiseXor_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.BitwiseXor<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.BitwiseXor<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseXor<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseXor<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseXor<double>(x, y, exp);
            Vectorization.SIMD.BitwiseXor<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.BitwiseXor<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.BitwiseXor<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseXor<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseXor<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseXor<float>(xx, yy, exp);
            Vectorization.SIMD.BitwiseXor<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Equals[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Equals_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Equals<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Equals<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Equals<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Equals<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Equals<double>(x, y, exp);
            Vectorization.SIMD.Equals<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Equals<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Equals<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Equals<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Equals<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Equals<float>(xx, yy, exp);
            Vectorization.SIMD.Equals<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void LessThan[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_LessThan_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.LessThan<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.LessThan<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.LessThan<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.LessThan<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.LessThan<double>(x, y, exp);
            Vectorization.SIMD.LessThan<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.LessThan<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.LessThan<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.LessThan<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.LessThan<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.LessThan<float>(xx, yy, exp);
            Vectorization.SIMD.LessThan<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void LessThanOrEquals[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_LessThanOrEquals_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.LessThanOrEquals<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.LessThanOrEquals<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.LessThanOrEquals<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.LessThanOrEquals<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.LessThanOrEquals<double>(x, y, exp);
            Vectorization.SIMD.LessThanOrEquals<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.LessThanOrEquals<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.LessThanOrEquals<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.LessThanOrEquals<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.LessThanOrEquals<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.LessThanOrEquals<float>(xx, yy, exp);
            Vectorization.SIMD.LessThanOrEquals<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void GreaterThan[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_GreaterThan_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.GreaterThan<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.GreaterThan<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.GreaterThan<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.GreaterThan<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.GreaterThan<double>(x, y, exp);
            Vectorization.SIMD.GreaterThan<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.GreaterThan<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.GreaterThan<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.GreaterThan<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.GreaterThan<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.GreaterThan<float>(xx, yy, exp);
            Vectorization.SIMD.GreaterThan<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void GreaterThanOrEquals[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_GreaterThanOrEquals_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.GreaterThanOrEquals<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.GreaterThanOrEquals<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.GreaterThanOrEquals<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.GreaterThanOrEquals<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.GreaterThanOrEquals<double>(x, y, exp);
            Vectorization.SIMD.GreaterThanOrEquals<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.GreaterThanOrEquals<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.GreaterThanOrEquals<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.GreaterThanOrEquals<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.GreaterThanOrEquals<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.GreaterThanOrEquals<float>(xx, yy, exp);
            Vectorization.SIMD.GreaterThanOrEquals<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Sqrt[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Sqrt_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Sqrt<double>(x, exp);
            Vectorization.SIMD.Sqrt<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Sqrt<float>(xx, exp);
            Vectorization.SIMD.Sqrt<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Cos[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Cos_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Cos<double>(x, exp);
            Vectorization.SIMD.Cos<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Cos<float>(xx, exp);
            Vectorization.SIMD.Cos<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Sin[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Sin_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Sin<double>(x, exp);
            Vectorization.SIMD.Sin<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Sin<float>(xx, exp);
            Vectorization.SIMD.Sin<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Tan[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Tan_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Tan<double>(x, exp);
            Vectorization.SIMD.Tan<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Tan<float>(xx, exp);
            Vectorization.SIMD.Tan<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Cosh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Cosh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Cosh<double>(x, exp);
            Vectorization.SIMD.Cosh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Cosh<float>(xx, exp);
            Vectorization.SIMD.Cosh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Sinh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Sinh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Sinh<double>(x, exp);
            Vectorization.SIMD.Sinh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Sinh<float>(xx, exp);
            Vectorization.SIMD.Sinh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Tanh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Tanh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Tanh<double>(x, exp);
            Vectorization.SIMD.Tanh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Tanh<float>(xx, exp);
            Vectorization.SIMD.Tanh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Acos[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Acos_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Acos<double>(x, exp);
            Vectorization.SIMD.Acos<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Acos<float>(xx, exp);
            Vectorization.SIMD.Acos<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Asin[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Asin_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Asin<double>(x, exp);
            Vectorization.SIMD.Asin<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Asin<float>(xx, exp);
            Vectorization.SIMD.Asin<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Atan[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Atan_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Atan<double>(x, exp);
            Vectorization.SIMD.Atan<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Atan<float>(xx, exp);
            Vectorization.SIMD.Atan<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Acosh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Acosh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Acosh<double>(x, exp);
            Vectorization.SIMD.Acosh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Acosh<float>(xx, exp);
            Vectorization.SIMD.Acosh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Asinh[T](System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(UnaryOperatorTestCases))]
    public void Test_Asinh_real(double[] x)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];
            Vectorization.Emulated.Asinh<double>(x, exp);
            Vectorization.SIMD.Asinh<double>(x, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);
        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];
            Vectorization.Emulated.Asinh<float>(xx, exp);
            Vectorization.SIMD.Asinh<float>(xx, act);
            AccuracyAssert.Accurate(xx, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Add[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Add_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Add<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Add<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Add<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Add<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Add<double>(x, y, exp);
            Vectorization.SIMD.Add<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Add<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Add<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Add<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Add<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Add<float>(xx, yy, exp);
            Vectorization.SIMD.Add<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Subtract[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Subtract_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Subtract<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Subtract<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Subtract<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Subtract<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Subtract<double>(x, y, exp);
            Vectorization.SIMD.Subtract<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Subtract<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Subtract<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Subtract<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Subtract<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Subtract<float>(xx, yy, exp);
            Vectorization.SIMD.Subtract<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Multiply[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Multiply_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Multiply<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Multiply<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Multiply<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Multiply<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Multiply<double>(x, y, exp);
            Vectorization.SIMD.Multiply<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Multiply<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Multiply<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Multiply<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Multiply<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Multiply<float>(xx, yy, exp);
            Vectorization.SIMD.Multiply<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void Divide[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_Divide_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.Divide<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.Divide<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Divide<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.Divide<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.Divide<double>(x, y, exp);
            Vectorization.SIMD.Divide<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.Divide<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.Divide<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Divide<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.Divide<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.Divide<float>(xx, yy, exp);
            Vectorization.SIMD.Divide<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }


    // Void BitwiseAnd[T](System.ReadOnlySpan`1[T], System.ReadOnlySpan`1[T], System.Span`1[T])
    [Theory]
    [MemberData(nameof(BinaryOperatorTestCases))]
    public void Test_BitwiseAnd_real_real(double[] x, double[] y)
    {
        {
            var exp = new double[x.Length];
            var act = new double[x.Length];

            Vectorization.Emulated.BitwiseAnd<double>(x.FirstOrDefault(), y, exp);
            Vectorization.SIMD.BitwiseAnd<double>(x.FirstOrDefault(), y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseAnd<double>(x, y.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseAnd<double>(x, y.FirstOrDefault(), act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

            Vectorization.Emulated.BitwiseAnd<double>(x, y, exp);
            Vectorization.SIMD.BitwiseAnd<double>(x, y, act);
            AccuracyAssert.Accurate(x, exp, act, 1e-10, TestAccuracyMode);

        }
        {
            var xx = x.Select(x => (float)x).ToArray();
            var yy = y.Select(y => (float)y).ToArray();
            var exp = new float[xx.Length];
            var act = new float[xx.Length];

            Vectorization.Emulated.BitwiseAnd<float>(xx.FirstOrDefault(), yy, exp);
            Vectorization.SIMD.BitwiseAnd<float>(xx.FirstOrDefault(), yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseAnd<float>(xx, yy.FirstOrDefault(), exp);
            Vectorization.SIMD.BitwiseAnd<float>(xx, yy.FirstOrDefault(), act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);

            Vectorization.Emulated.BitwiseAnd<float>(xx, yy, exp);
            Vectorization.SIMD.BitwiseAnd<float>(xx, yy, act);
            AccuracyAssert.Accurate(null, exp, act, 1e-5f, TestAccuracyMode);
        }
    }

}
