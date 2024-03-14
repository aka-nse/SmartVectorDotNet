#if NETSTANDARD2_0
namespace System
{
    internal static class MathF
    {
        public static float Sqrt(float d) => (float)Math.Sqrt(d);
        public static float Cbrt(float d) => (float)MathEx.Cbrt(d);
        public static float Cos(float d) => (float)Math.Cos(d);
        public static float Sin(float d) => (float)Math.Sin(d);
        public static float Tan(float d) => (float)Math.Tan(d);
        public static float Cosh(float d) => (float)Math.Cosh(d);
        public static float Sinh(float d) => (float)Math.Sinh(d);
        public static float Tanh(float d) => (float)Math.Tanh(d);
        public static float Acos(float d) => (float)Math.Acos(d);
        public static float Asin(float d) => (float)Math.Asin(d);
        public static float Atan(float d) => (float)Math.Atan(d);
        public static float Acosh(float d) => (float)MathEx.Acosh(d);
        public static float Asinh(float d) => (float)MathEx.Asinh(d);
        public static float Atanh(float d) => (float)MathEx.Atanh(d);
        public static float Atan2(float y, float x) => (float)Math.Atan2(y, x);
        public static float Exp(float d) => (float)Math.Exp(d);
        public static float Pow(float x, float y) => (float)Math.Pow(x, y);
        public static float Log(float d) => (float)Math.Log(d);
        public static float Log(float a, float newBase) => (float)Math.Log(a, newBase);
        public static float Log10(float d) => (float)Math.Log10(d);

        public static float Ceiling(float a) => (float)Math.Ceiling(a);
        public static float Floor(float a) => (float)Math.Floor(a);
        public static float Round(float a) => (float)Math.Round(a);
        public static float Truncate(float a) => (float)Math.Truncate(a);

    }

    internal static class MathEx
    {
        public static T Cbrt<T>(T d) => throw new NotImplementedException();
        public static T Acosh<T>(T d) => throw new NotImplementedException();
        public static T Asinh<T>(T d) => throw new NotImplementedException();
        public static T Atanh<T>(T d) => throw new NotImplementedException();
    }
}
#endif
