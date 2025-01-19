using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    public class SpecialValue
    {
        #region Acos

        public static TheoryData<double> Acos_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Acos_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Acos_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Acos_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Acos(operand);
                var act = VectorOp.Acos<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Acos_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Acos_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Acos(operand);
                var act = VectorOp.Acos<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Acosh

        public static TheoryData<double> Acosh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Acosh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Acosh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Acosh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Acosh(operand);
                var act = VectorOp.Acosh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Acosh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Acosh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Acosh(operand);
                var act = VectorOp.Acosh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Asin

        public static TheoryData<double> Asin_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Asin_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Asin_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Asin_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Asin(operand);
                var act = VectorOp.Asin<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Asin_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Asin_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Asin(operand);
                var act = VectorOp.Asin<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Asinh

        public static TheoryData<double> Asinh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Asinh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Asinh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Asinh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Asinh(operand);
                var act = VectorOp.Asinh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Asinh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Asinh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Asinh(operand);
                var act = VectorOp.Asinh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Atan

        public static TheoryData<double> Atan_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Atan_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Atan_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Atan_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Atan(operand);
                var act = VectorOp.Atan<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Atan_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Atan_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Atan(operand);
                var act = VectorOp.Atan<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Cos

        public static TheoryData<double> Cos_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Cos_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Cos_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Cos_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Cos(operand);
                var act = VectorOp.Cos<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Cos_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Cos_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Cos(operand);
                var act = VectorOp.Cos<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Cosh

        public static TheoryData<double> Cosh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Cosh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Cosh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Cosh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Cosh(operand);
                var act = VectorOp.Cosh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Cosh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Cosh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Cosh(operand);
                var act = VectorOp.Cosh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Exp

        public static TheoryData<double> Exp_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Exp_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Exp_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Exp_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Exp(operand);
                var act = VectorOp.Exp<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Exp_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Exp_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Exp(operand);
                var act = VectorOp.Exp<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Atanh

        public static TheoryData<double> Atanh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Atanh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Atanh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Atanh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Atanh(operand);
                var act = VectorOp.Atanh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Atanh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Atanh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Atanh(operand);
                var act = VectorOp.Atanh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Log

        public static TheoryData<double> Log_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Log_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Log_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Log_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Log(operand);
                var act = VectorOp.Log<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Log_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Log_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Log(operand);
                var act = VectorOp.Log<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Log10

        public static TheoryData<double> Log10_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Log10_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Log10_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Log10_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Log10(operand);
                var act = VectorOp.Log10<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Log10_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Log10_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Log10(operand);
                var act = VectorOp.Log10<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Log2

        public static TheoryData<double> Log2_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Log2_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Log2_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Log2_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Log2(operand);
                var act = VectorOp.Log2<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Log2_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Log2_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Log2(operand);
                var act = VectorOp.Log2<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Sin

        public static TheoryData<double> Sin_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Sin_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Sin_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Sin_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Sin(operand);
                var act = VectorOp.Sin<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Sin_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Sin_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Sin(operand);
                var act = VectorOp.Sin<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Sinh

        public static TheoryData<double> Sinh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Sinh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Sinh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Sinh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Sinh(operand);
                var act = VectorOp.Sinh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Sinh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Sinh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Sinh(operand);
                var act = VectorOp.Sinh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Tan

        public static TheoryData<double> Tan_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Tan_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Tan_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Tan_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Tan(operand);
                var act = VectorOp.Tan<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Tan_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Tan_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Tan(operand);
                var act = VectorOp.Tan<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

        #region Tanh

        public static TheoryData<double> Tanh_SpecialValueTestCase_Unary_Double_TestCases()
            => new () { 
                { default(double) },
                { -default(double) },
                { double.Epsilon },
                { -double.Epsilon },
                { double.MaxValue },
                { double.MinValue },
                { 1/double.MaxValue },
                { 1/double.MinValue },
                { double.PositiveInfinity },
                { double.NegativeInfinity },
                { double.NaN },
            };

        public static TheoryData<float> Tanh_SpecialValueTestCase_Unary_Single_TestCases()
            => new () {
                { default(float) },
                { -default(float) },
                { float.Epsilon },
                { -float.Epsilon },
                { float.MaxValue },
                { float.MinValue },
                { 1/float.MaxValue },
                { 1/float.MinValue },
                { float.PositiveInfinity },
                { float.NegativeInfinity },
                { float.NaN },
            };

        [Theory]
        [MemberData(nameof(Tanh_SpecialValueTestCase_Unary_Double_TestCases))]
        public void Tanh_Double(double operand)
        {
            try
            {
                var exp = ScalarOp.Tanh(operand);
                var act = VectorOp.Tanh<double>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        [Theory]
        [MemberData(nameof(Tanh_SpecialValueTestCase_Unary_Single_TestCases))]
        public void Tanh_Single(float operand)
        {
            try
            {
                var exp = ScalarOp.Tanh(operand);
                var act = VectorOp.Tanh<float>(new (operand))[0];
                Assert.Equal(exp, act, SpecialValueComparer.Instance);
            }
            catch(NotSupportedException)
            {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_1_OR_GREATER
                throw;
#endif
            }
        }

        #endregion

    }
}

