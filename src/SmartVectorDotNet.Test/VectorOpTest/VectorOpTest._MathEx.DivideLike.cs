using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;
using TestCases = IEnumerable<object[]>;

#pragma warning disable IDE0079
#pragma warning disable xUnit1042
public partial class VectorOpTest
{
    public static TestCases DivideRoundingTestCases()
    {
#pragma warning disable format
        static object[] core<T>(T x, T y) where T : unmanaged => [x, y];

        yield return core(5, -3);

        for (var i = 0u; i <= 3u; ++i)
        {
            yield return core<byte  >((byte  )i, 3);
            yield return core<ushort>((ushort)i, 3);
            yield return core<uint  >((uint  )i, 3);
            yield return core<ulong >((ulong )i, 3);
#if NET6_0_OR_GREATER
            yield return core<nuint>((nuint)i, 3);
#endif
        }
        for (var i = -3; i <= 3; ++i)
        {
            yield return core<sbyte >((sbyte )i, +3);
            yield return core<short >((short )i, +3);
            yield return core<int   >((int   )i, +3);
            yield return core<long  >((long  )i, +3);
            yield return core<float >((float )i, +3);
            yield return core<double>((double)i, +3);
            yield return core<sbyte >((sbyte )i, -3);
            yield return core<short >((short )i, -3);
            yield return core<int   >((int   )i, -3);
            yield return core<long  >((long  )i, -3);
            yield return core<float >((float )i, -3);
            yield return core<double>((double)i, -3);
#if NET6_0_OR_GREATER
            yield return core<nint>((nint)i, +3);
            yield return core<nint>((nint)i, -3);
#endif
#pragma warning restore format
        }
    }
    [Theory, MemberData(nameof(DivideRoundingTestCases))]
    public void DivideFloor<T>(T x, T y)
        where T : unmanaged
    {
        var exp = ScalarOp.DivideFloor(x, y);
        Assert.Equal(exp, VectorOp.DivideFloor<T>(new(x), new(y))[0]);
    }
    [Theory, MemberData(nameof(DivideRoundingTestCases))]
    public void DivideCeiling<T>(T x, T y)
        where T : unmanaged
    {
        var exp = ScalarOp.DivideCeiling(x, y);
        Assert.Equal(exp, VectorOp.DivideCeiling<T>(new(x), new(y))[0]);
    }


    public static TestCases DivRemTestCases()
    {
        object[] core<T>(T x, T y) where T : unmanaged
            => new object[] { x, y };

#pragma warning disable format
        yield return core<byte  >(123, 45);
        yield return core<ushort>(123, 45);
        yield return core<uint  >(123, 45);
        yield return core<ulong >(123, 45);
        yield return core<sbyte >(123, 45);
        yield return core<short >(123, 45);
        yield return core<int   >(123, 45);
        yield return core<long  >(123, 45);
        yield return core<float >(12.3f, 4.5f);
        yield return core<double>(12.30, 4.50);
        yield return core<sbyte >(-123, 45);
        yield return core<short >(-123, 45);
        yield return core<int   >(-123, 45);
        yield return core<long  >(-123, 45);
        yield return core<float >(-12.3f, 4.5f);
        yield return core<double>(-12.30, 4.50);
        yield return core<sbyte >(123, -45);
        yield return core<short >(123, -45);
        yield return core<int   >(123, -45);
        yield return core<long  >(123, -45);
        yield return core<float >(12.3f, -4.5f);
        yield return core<double>(12.30, -4.50);
        yield return core<sbyte >(-123, -45);
        yield return core<short >(-123, -45);
        yield return core<int   >(-123, -45);
        yield return core<long  >(-123, -45);
        yield return core<float >(-12.3f, -4.5f);
        yield return core<double>(-12.30, -4.50);
#if NET6_0_OR_GREATER
        yield return core<nuint >(123, 45);
        yield return core<nint  >(123, 45);
        yield return core<nint  >(-123, 45);
        yield return core<nint  >(123, -45);
        yield return core<nint  >(-123, -45);
#endif
#pragma warning restore format
        yield break;
    }

    [Theory, MemberData(nameof(DivRemTestCases))]
    public void DivRem<T>(T x, T y)
        where T : unmanaged
    {
        var expDiv = ScalarOp.DivRem(x, y, out var expRem);
        var actDiv = VectorOp.DivRem(new Vector<T>(x), new Vector<T>(y), out var actRem);
        Assert.Equal(expDiv, actDiv[0]);
        Assert.Equal(expRem, actRem[0]);
    }

    [Theory, MemberData(nameof(DivRemTestCases))]
    public void DivRemByFloor<T>(T x, T y)
        where T : unmanaged
    {
        var expDiv = ScalarOp.DivRemByFloor(x, y, out var expRem);
        var actDiv = VectorOp.DivRemByFloor(new Vector<T>(x), new Vector<T>(y), out var actRem);
        Assert.Equal(expDiv, actDiv[0]);
        Assert.Equal(expRem, actRem[0]);
    }

    [Theory, MemberData(nameof(DivRemTestCases))]
    public void ModuloByFloor<T>(T x, T y)
        where T : unmanaged
    {
        Assert.Equal(ScalarOp.ModuloByFloor(x, y), VectorOp.ModuloByFloor(new Vector<T>(x), new Vector<T>(y))[0]);
    }

    [Fact]
    public void DivideLike_ThrowsNotSupportedException()
    {
        Assert.Throws<NotSupportedException>(() => VectorOp.DivideFloor<decimal>(default, default));
        Assert.Throws<NotSupportedException>(() => VectorOp.DivideCeiling<decimal>(default, default));
        Assert.Throws<NotSupportedException>(() => VectorOp.DivRem<decimal>(default, default, out _));
        Assert.Throws<NotSupportedException>(() => VectorOp.DivRemByFloor<decimal>(default, default, out _));
    }
}
#pragma warning restore xUnit1042
#pragma warning restore IDE0079
