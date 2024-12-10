namespace SmartVectorDotNet;

public partial class VectorizationTest
{
    private static partial UnaryOperatorTestSuite[] UnaryOperatorTestSuites()
        => [
            new Unary_UnaryPlus(),
            new Unary_UnaryMinus(),
            new Unary_Complement(),
            new Unary_Abs(),
            new Unary_Acos(),
            new Unary_Acosh(),
            new Unary_Asin(),
            new Unary_Asinh(),
            new Unary_Atan(),
            new Unary_Atanh(),
            new Unary_Cbrt(),
            new Unary_Ceiling(),
            new Unary_Cos(),
            new Unary_Cosh(),
            new Unary_Exp(),
            new Unary_Floor(),
            new Unary_Log(),
            new Unary_Log10(),
            new Unary_Log2(),
            new Unary_Round(),
            new Unary_Sign(),
            new Unary_Sin(),
            new Unary_Sinh(),
            new Unary_Sqrt(),
            new Unary_Tan(),
            new Unary_Tanh(),
            new Unary_Truncate(),
        ];

    private static partial BinaryOperatorTestSuite[] BinaryOperatorTestSuites()
        => [
            new Binary_BitwiseOr(),
            new Binary_BitwiseXor(),
            new Binary_Equals(),
            new Binary_LessThan(),
            new Binary_LessThanOrEquals(),
            new Binary_GreaterThan(),
            new Binary_GreaterThanOrEquals(),
            new Binary_Atan2(),
            new Binary_Log(),
            new Binary_Pow(),
            new Binary_Add(),
            new Binary_Subtract(),
            new Binary_Multiply(),
            new Binary_Divide(),
            new Binary_BitwiseAnd(),
        ];

    #region unary operators

    private class Unary_UnaryPlus : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.UnaryPlus<T>(x, result);
    }

    private class Unary_UnaryMinus : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.UnaryMinus<T>(x, result);
    }

    private class Unary_Complement : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Complement<T>(x, result);
    }

    private class Unary_Abs : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Abs<T>(x, result);
    }

    private class Unary_Acos : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Acos<T>(x, result);
    }

    private class Unary_Acosh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Acosh<T>(x, result);
    }

    private class Unary_Asin : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Asin<T>(x, result);
    }

    private class Unary_Asinh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Asinh<T>(x, result);
    }

    private class Unary_Atan : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Atan<T>(x, result);
    }

    private class Unary_Atanh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Atanh<T>(x, result);
    }

    private class Unary_Cbrt : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Cbrt<T>(x, result);
    }

    private class Unary_Ceiling : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Ceiling<T>(x, result);
    }

    private class Unary_Cos : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Cos<T>(x, result);
    }

    private class Unary_Cosh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Cosh<T>(x, result);
    }

    private class Unary_Exp : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Exp<T>(x, result);
    }

    private class Unary_Floor : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Floor<T>(x, result);
    }

    private class Unary_Log : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Log<T>(x, result);
    }

    private class Unary_Log10 : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Log10<T>(x, result);
    }

    private class Unary_Log2 : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Log2<T>(x, result);
    }

    private class Unary_Round : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Round<T>(x, result);
    }

    private class Unary_Sign : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Sign<T>(x, result);
    }

    private class Unary_Sin : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Sin<T>(x, result);
    }

    private class Unary_Sinh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Sinh<T>(x, result);
    }

    private class Unary_Sqrt : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Sqrt<T>(x, result);
    }

    private class Unary_Tan : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Tan<T>(x, result);
    }

    private class Unary_Tanh : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Tanh<T>(x, result);
    }

    private class Unary_Truncate : UnaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, Span<T> result) => vectorization.Truncate<T>(x, result);
    }

    #endregion

    #region binary operators
    
    private class Binary_BitwiseOr : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseOr<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.BitwiseOr<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseOr<T>(x, y, result);
    }

    private class Binary_BitwiseXor : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseXor<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.BitwiseXor<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseXor<T>(x, y, result);
    }

    private class Binary_Equals : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Equals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Equals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Equals<T>(x, y, result);
    }

    private class Binary_LessThan : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.LessThan<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.LessThan<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.LessThan<T>(x, y, result);
    }

    private class Binary_LessThanOrEquals : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.LessThanOrEquals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.LessThanOrEquals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.LessThanOrEquals<T>(x, y, result);
    }

    private class Binary_GreaterThan : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.GreaterThan<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.GreaterThan<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.GreaterThan<T>(x, y, result);
    }

    private class Binary_GreaterThanOrEquals : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.GreaterThanOrEquals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.GreaterThanOrEquals<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.GreaterThanOrEquals<T>(x, y, result);
    }

    private class Binary_Atan2 : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Atan2<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Atan2<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Atan2<T>(x, y, result);
    }

    private class Binary_Log : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Log<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Log<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Log<T>(x, y, result);
    }

    private class Binary_Pow : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Pow<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Pow<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Pow<T>(x, y, result);
    }

    private class Binary_Add : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Add<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Add<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Add<T>(x, y, result);
    }

    private class Binary_Subtract : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Subtract<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Subtract<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Subtract<T>(x, y, result);
    }

    private class Binary_Multiply : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Multiply<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Multiply<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Multiply<T>(x, y, result);
    }

    private class Binary_Divide : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Divide<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.Divide<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.Divide<T>(x, y, result);
    }

    private class Binary_BitwiseAnd : BinaryOperatorTestSuite
    {
        protected override void Operate<T>(Vectorization vectorization, T x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseAnd<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, T y, Span<T> result) => vectorization.BitwiseAnd<T>(x, y, result);
        protected override void Operate<T>(Vectorization vectorization, ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> result) => vectorization.BitwiseAnd<T>(x, y, result);
    }

    #endregion
}
