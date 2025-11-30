using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    public static IEnumerable<object[]> Emulate_FixedTestCases()
    {
        static object[] core<T>(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
        where T : unmanaged
            => [x1, x2, x3, x4, x5, x6, x7, x8];

#pragma warning disable format
        yield return core<byte  >(1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<ushort>(1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<uint  >(1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<ulong >(1, 2, 3, 4, 5, 6, 7, 8);
#pragma warning restore format
        yield break;
    }
    [Theory, MemberData(nameof(Emulate_FixedTestCases))]
    public void Emulate_Fixed<T>(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
        where T : unmanaged
    {
        var vx1 = new Vector<T>(x1);
        var vx2 = new Vector<T>(x2);
        var vx3 = new Vector<T>(x3);
        var vx4 = new Vector<T>(x4);
        var vx5 = new Vector<T>(x5);
        var vx6 = new Vector<T>(x6);
        var vx7 = new Vector<T>(x7);
        var vx8 = new Vector<T>(x8);
        var expected = default(Sum<T>).Calculate(x1, x2, x3, x4, x5, x6, x7, x8);
        var actual = VectorOp.Emulate<T, Sum<T>>(vx1, vx2, vx3, vx4, vx5, vx6, vx7, vx8);
        for(var i = 0; i < Vector<T>.Count; ++i)
            Assert.Equal(expected, actual[i]);
    }

    public static IEnumerable<object[]> Emulate_ParameterizedTestCases()
    {
        static object[] core<T>(WeightedSum<T> op, T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
        where T : unmanaged
            => [op, x1, x2, x3, x4, x5, x6, x7, x8];

#pragma warning disable format
        yield return core<byte  >(new(1, 2, 3, 4, 5, 6, 7, 8), 1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<ushort>(new(1, 2, 3, 4, 5, 6, 7, 8), 1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<uint  >(new(1, 2, 3, 4, 5, 6, 7, 8), 1, 2, 3, 4, 5, 6, 7, 8);
        yield return core<ulong >(new(1, 2, 3, 4, 5, 6, 7, 8), 1, 2, 3, 4, 5, 6, 7, 8);
#pragma warning restore format
        yield break;
    }
    [Theory, MemberData(nameof(Emulate_ParameterizedTestCases))]
    public void Emulate_Parameterized<T, TOperation>(TOperation op, T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
        where T : unmanaged
        where TOperation : unmanaged, IVectorEmulationOp8<T>
    {
        var vx1 = new Vector<T>(x1);
        var vx2 = new Vector<T>(x2);
        var vx3 = new Vector<T>(x3);
        var vx4 = new Vector<T>(x4);
        var vx5 = new Vector<T>(x5);
        var vx6 = new Vector<T>(x6);
        var vx7 = new Vector<T>(x7);
        var vx8 = new Vector<T>(x8);
        var expected = op.Calculate(x1, x2, x3, x4, x5, x6, x7, x8);
        var actual = VectorOp.Emulate(op, vx1, vx2, vx3, vx4, vx5, vx6, vx7, vx8);
        for (var i = 0; i < Vector<T>.Count; ++i)
            Assert.Equal(expected, actual[i]);
    }
}


file readonly struct Sum<T>
    : IVectorEmulationOp8<T>
    where T : unmanaged
{
    public readonly T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
    {
        var sum = ScalarOp.Const<T>.Zero;
        sum = ScalarOp.Add(x1, sum);
        sum = ScalarOp.Add(x2, sum);
        sum = ScalarOp.Add(x3, sum);
        sum = ScalarOp.Add(x4, sum);
        sum = ScalarOp.Add(x5, sum);
        sum = ScalarOp.Add(x6, sum);
        sum = ScalarOp.Add(x7, sum);
        sum = ScalarOp.Add(x8, sum);
        return sum;
    }
}

file readonly struct WeightedSum<T>(T w1, T w2, T w3, T w4, T w5, T w6, T w7, T w8)
    : IVectorEmulationOp8<T>
    where T : unmanaged
{
    public readonly T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8)
    {
        var sum = ScalarOp.Const<T>.Zero;
        sum = ScalarOp.Add(ScalarOp.Multiply(w1, x1), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w2, x2), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w3, x3), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w4, x4), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w5, x5), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w6, x6), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w7, x7), sum);
        sum = ScalarOp.Add(ScalarOp.Multiply(w8, x8), sum);
        return sum;
    }
}
