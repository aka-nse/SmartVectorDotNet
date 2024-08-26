using System;
using System.Linq;
using System.Numerics;
using Xunit;

namespace SmartVectorDotNet;


public partial class VectorizationTest
{
    public static TheoryData<float[][]> TestCaseVectorization()
    {
        static float[][] createTestData(int seed, int size)
        {
            var rand = new Random(seed);
            var arguments = new float[10][];
            for(var i = 0; i < 10; ++i)
            {
                arguments[i] = Enumerable.Range(0, size).Select(_ => (float)rand.NextDouble()).ToArray();
            }
            return arguments;
        }

        return new TheoryData<float[][]>()
        {
            { createTestData(12345678 + 0, 0) },
            { createTestData(12345678 + 1, 1) },
            { createTestData(12345678 + 3, 3) },
            { createTestData(12345678 + 4, 4) },
            { createTestData(12345678 + 5, 5) },
            { createTestData(12345678 + 7, 7) },
            { createTestData(12345678 + 8, 8) },
            { createTestData(12345678 + 9, 9) },
            { createTestData(12345678 + 15, 15) },
            { createTestData(12345678 + 16, 16) },
            { createTestData(12345678 + 17, 17) },
            { createTestData(12345678 + 31, 31) },
            { createTestData(12345678 + 32, 32) },
            { createTestData(12345678 + 33, 33) },
            { createTestData(12345678 + 63, 63) },
            { createTestData(12345678 + 64, 64) },
            { createTestData(12345678 + 65, 65) },
            { createTestData(12345678 + 127, 127) },
            { createTestData(12345678 + 128, 128) },
            { createTestData(12345678 + 129, 129) },
            { createTestData(12345678 + 255, 255) },
            { createTestData(12345678 + 256, 256) },
            { createTestData(12345678 + 257, 257) },
        };
    }



    private readonly struct TestOp1 : IVectorFormula1<float>
    {
        public float Calculate(float x1)
            => x1;
            
        public Vector<float> Calculate(Vector<float> x1)
            => x1;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector1(float[][] arguments)
    {
        var op = default(TestOp1);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp1>(arguments[0], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp2 : IVectorFormula2<float>
    {
        public float Calculate(float x1, float x2)
            => x1 + x2;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2)
            => x1 + x2;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector2(float[][] arguments)
    {
        var op = default(TestOp2);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp2>(arguments[0], arguments[1], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp3 : IVectorFormula3<float>
    {
        public float Calculate(float x1, float x2, float x3)
            => x1 + x2 + x3;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3)
            => x1 + x2 + x3;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector3(float[][] arguments)
    {
        var op = default(TestOp3);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp3>(arguments[0], arguments[1], arguments[2], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp4 : IVectorFormula4<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4)
            => x1 + x2 + x3 + x4;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4)
            => x1 + x2 + x3 + x4;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector4(float[][] arguments)
    {
        var op = default(TestOp4);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp4>(arguments[0], arguments[1], arguments[2], arguments[3], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp5 : IVectorFormula5<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5)
            => x1 + x2 + x3 + x4 + x5;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5)
            => x1 + x2 + x3 + x4 + x5;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector5(float[][] arguments)
    {
        var op = default(TestOp5);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp5>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp6 : IVectorFormula6<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5, float x6)
            => x1 + x2 + x3 + x4 + x5 + x6;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5, Vector<float> x6)
            => x1 + x2 + x3 + x4 + x5 + x6;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector6(float[][] arguments)
    {
        var op = default(TestOp6);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i], arguments[5][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp6>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp7 : IVectorFormula7<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5, float x6, float x7)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5, Vector<float> x6, Vector<float> x7)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector7(float[][] arguments)
    {
        var op = default(TestOp7);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i], arguments[5][i], arguments[6][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp7>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp8 : IVectorFormula8<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5, Vector<float> x6, Vector<float> x7, Vector<float> x8)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector8(float[][] arguments)
    {
        var op = default(TestOp8);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i], arguments[5][i], arguments[6][i], arguments[7][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp8>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp9 : IVectorFormula9<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5, Vector<float> x6, Vector<float> x7, Vector<float> x8, Vector<float> x9)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector9(float[][] arguments)
    {
        var op = default(TestOp9);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i], arguments[5][i], arguments[6][i], arguments[7][i], arguments[8][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp9>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], act);
        Assert.Equal(exp, act);
    }



    private readonly struct TestOp10 : IVectorFormula10<float>
    {
        public float Calculate(float x1, float x2, float x3, float x4, float x5, float x6, float x7, float x8, float x9, float x10)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10;
            
        public Vector<float> Calculate(Vector<float> x1, Vector<float> x2, Vector<float> x3, Vector<float> x4, Vector<float> x5, Vector<float> x6, Vector<float> x7, Vector<float> x8, Vector<float> x9, Vector<float> x10)
            => x1 + x2 + x3 + x4 + x5 + x6 + x7 + x8 + x9 + x10;
    }

    [Theory]
    [MemberData(nameof(TestCaseVectorization))]
    public void CalculateVector10(float[][] arguments)
    {
        var op = default(TestOp10);
        var exp = new float[arguments[0].Length];
        var act = new float[arguments[0].Length];
        for(var i = 0; i < exp.Length; ++i)
        {
            exp[i] = op.Calculate(arguments[0][i], arguments[1][i], arguments[2][i], arguments[3][i], arguments[4][i], arguments[5][i], arguments[6][i], arguments[7][i], arguments[8][i], arguments[9][i]);
        }
        Vectorization.SIMD.Calculate<float, TestOp10>(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4], arguments[5], arguments[6], arguments[7], arguments[8], arguments[9], act);
        Assert.Equal(exp, act);
    }



}
