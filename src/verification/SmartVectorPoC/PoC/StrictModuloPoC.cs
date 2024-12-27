using System.Numerics;

namespace SmartVectorDotNet.PoC;

internal static class StrictModuloPoC
{
    public static void Test()
    {
        var aa = Enumerable
            .Range(-500, 1001)
            .Select(i => MathF.PI * i)
            .ToArray();
        var bb = Enumerable
            .Range(1, 1000)
            .Select(i => MathF.E * i)
            .ToArray();

        foreach (var a in aa)
        {
            foreach (var b in bb)
            {
                test(a, b);
            }
        }

        static void test(float x, float y)
        {
            float exp = x % y;
            float act = ScalarMathPoC.Modulo(x, y);
            Console.WriteLine($"{(exp == act ? "o" : "x")}: {x} % {y} = {exp} == {act}");
            if(exp != act)
            {
                Console.ReadKey();
            }
        }
    }

}


file static class ScalarMathPoC
{
    public static float Modulo(float x, float y)
    {
        const long economizedBit = 1L << ScalarMath.Const.SingleExpBitOffset;
        const int exponentBits = 32 - ScalarMath.Const.SingleExpBitOffset - 1;

        ScalarMath.Decompose(x, out var s, out var n, out var a);
        ScalarMath.Decompose(y, out var _, out var m, out var b);
        var i = n - m;
        if (i < 0)
        {
            return x;
        }

        var c = (uint)(a | economizedBit);
        var d = (uint)(b | economizedBit);
        while (i > exponentBits)
        {
            c <<= exponentBits;
            c -= d * (c / d);
            i -= exponentBits;
        }
        c <<= (int)i;
        c -= d * (c / d);
        i = 0;

        if (c == 0)
        {
            return 0;
        }
        var eBitShift = BitOperations.LeadingZeroCount(c) - (31 - ScalarMath.Const.SingleExpBitOffset);
        i -= eBitShift;
        c <<= eBitShift;
        // while ((c & economizedBit) == 0)
        // {
        //     c <<= 1;
        //     --i;
        // }
        return ScalarMath.Scale(s, m + i, (int)c & ScalarMath.Const.SingleFracPartMask);
    }

    public static double Modulo(double x, double y)
    {
        const long economizedBit = 1L << ScalarMath.Const.DoubleExpBitOffset;
        const int exponentBits = 64 - ScalarMath.Const.DoubleExpBitOffset - 1;

        ScalarMath.Decompose(x, out var s, out var n, out var a);
        ScalarMath.Decompose(y, out var _, out var m, out var b);
        var i = n - m;
        if (i < 0)
        {
            return x;
        }

        var c = (ulong)(a | economizedBit);
        var d = (ulong)(b | economizedBit);
        while (i > exponentBits)
        {
            c <<= exponentBits;
            c -= d * (c / d);
            i -= exponentBits;
        }
        c <<= (int)i;
        c -= d * (c / d);
        i = 0;
        if (c == 0)
        {
            return 0;
        }
        while ((c & economizedBit) == 0)
        {
            c <<= 1;
            --i;
        }
        return ScalarMath.Scale(s, m + i, (long)c & ScalarMath.Const.DoubleFracPartMask);
    }
}