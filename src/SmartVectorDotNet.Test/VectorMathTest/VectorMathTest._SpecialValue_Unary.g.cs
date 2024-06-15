using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    public class SpecialValue
    {
        public static IEnumerable<object[]> SpecialValueTestCase_Unary_Double()
            => IEEE754DoubleSpecials.Select(x => new object[]{x});
        
        public static IEnumerable<object[]> SpecialValueTestCase_Unary_Single()
            => IEEE754SingleSpecials.Select(x => new object[]{x});

        #region Acos
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Acos_Double(double operand)
        {
            var exp = Math.Acos(operand);
            var act = VectorMath.Acos<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Acos_Single(float operand)
        {
            var exp = MathF.Acos(operand);
            var act = VectorMath.Acos<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Acosh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Acosh_Double(double operand)
        {
            var exp = Math.Acosh(operand);
            var act = VectorMath.Acosh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Acosh_Single(float operand)
        {
            var exp = MathF.Acosh(operand);
            var act = VectorMath.Acosh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Asin
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Asin_Double(double operand)
        {
            var exp = Math.Asin(operand);
            var act = VectorMath.Asin<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Asin_Single(float operand)
        {
            var exp = MathF.Asin(operand);
            var act = VectorMath.Asin<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Asinh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Asinh_Double(double operand)
        {
            var exp = Math.Asinh(operand);
            var act = VectorMath.Asinh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Asinh_Single(float operand)
        {
            var exp = MathF.Asinh(operand);
            var act = VectorMath.Asinh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Atan
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Atan_Double(double operand)
        {
            var exp = Math.Atan(operand);
            var act = VectorMath.Atan<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Atan_Single(float operand)
        {
            var exp = MathF.Atan(operand);
            var act = VectorMath.Atan<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Cos
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Cos_Double(double operand)
        {
            var exp = Math.Cos(operand);
            var act = VectorMath.Cos<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Cos_Single(float operand)
        {
            var exp = MathF.Cos(operand);
            var act = VectorMath.Cos<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Cosh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Cosh_Double(double operand)
        {
            var exp = Math.Cosh(operand);
            var act = VectorMath.Cosh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Cosh_Single(float operand)
        {
            var exp = MathF.Cosh(operand);
            var act = VectorMath.Cosh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Exp
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Exp_Double(double operand)
        {
            var exp = Math.Exp(operand);
            var act = VectorMath.Exp<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Exp_Single(float operand)
        {
            var exp = MathF.Exp(operand);
            var act = VectorMath.Exp<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Atanh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Atanh_Double(double operand)
        {
            var exp = Math.Atanh(operand);
            var act = VectorMath.Atanh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Atanh_Single(float operand)
        {
            var exp = MathF.Atanh(operand);
            var act = VectorMath.Atanh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Log
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Log_Double(double operand)
        {
            var exp = Math.Log(operand);
            var act = VectorMath.Log<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Log_Single(float operand)
        {
            var exp = MathF.Log(operand);
            var act = VectorMath.Log<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Log10
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Log10_Double(double operand)
        {
            var exp = Math.Log10(operand);
            var act = VectorMath.Log10<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Log10_Single(float operand)
        {
            var exp = MathF.Log10(operand);
            var act = VectorMath.Log10<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Log2
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Log2_Double(double operand)
        {
            var exp = Math.Log2(operand);
            var act = VectorMath.Log2<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Log2_Single(float operand)
        {
            var exp = MathF.Log2(operand);
            var act = VectorMath.Log2<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Sin
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Sin_Double(double operand)
        {
            var exp = Math.Sin(operand);
            var act = VectorMath.Sin<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Sin_Single(float operand)
        {
            var exp = MathF.Sin(operand);
            var act = VectorMath.Sin<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Sinh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Sinh_Double(double operand)
        {
            var exp = Math.Sinh(operand);
            var act = VectorMath.Sinh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Sinh_Single(float operand)
        {
            var exp = MathF.Sinh(operand);
            var act = VectorMath.Sinh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Tan
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Tan_Double(double operand)
        {
            var exp = Math.Tan(operand);
            var act = VectorMath.Tan<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Tan_Single(float operand)
        {
            var exp = MathF.Tan(operand);
            var act = VectorMath.Tan<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

        #region Tanh
        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Double))]
        public void Tanh_Double(double operand)
        {
            var exp = Math.Tanh(operand);
            var act = VectorMath.Tanh<double>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        [Theory]
        [MemberData(nameof(SpecialValueTestCase_Unary_Single))]
        public void Tanh_Single(float operand)
        {
            var exp = MathF.Tanh(operand);
            var act = VectorMath.Tanh<float>(new (operand))[0];
            Assert.Equal(exp, act, SpecialValueComparer.Instance);
        }

        #endregion

    }
}
