using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;
using TestCases = IEnumerable<object?[]>;

#pragma warning disable IDE0079
#pragma warning disable xUnit1042
public partial class ScalarOpTest
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
            yield return core<nint>((nint)i, 3);
            yield return core<nint>((nint)i, -3);
#endif
#pragma warning restore format
        }
    }
    [Theory, MemberData(nameof(DivideRoundingTestCases))]
    public void DivideFloor<T>(T x, T y)
        where T : unmanaged
    {
        var exp = Math.Floor(ScalarOp.Convert<T, double>(x) / ScalarOp.Convert<T, double>(y));
        Assert.Equal(exp, ScalarOp.Convert<T, double>(ScalarOp.DivideFloor(x, y)));
    }
    [Theory, MemberData(nameof(DivideRoundingTestCases))]
    public void DivideCeiling<T>(T x, T y)
        where T : unmanaged
    {
        var exp = Math.Ceiling(ScalarOp.Convert<T, double>(x) / ScalarOp.Convert<T, double>(y));
        Assert.Equal(exp, ScalarOp.Convert<T, double>(ScalarOp.DivideCeiling(x, y)));
    }


    public static TestCases DivRemTestCases()
    {
        static object[] core<T>(T a, T b, T expDiv, T expRem)
            where T : unmanaged
            => [a, b, expDiv, expRem];
        
#pragma warning disable format
        yield return core<byte  >(123, 45, 2, 33);
        yield return core<ushort>(123, 45, 2, 33);
        yield return core<uint  >(123, 45, 2, 33);
        yield return core<ulong >(123, 45, 2, 33);
        yield return core<sbyte >(123, 45, 2, 33);
        yield return core<short >(123, 45, 2, 33);
        yield return core<int   >(123, 45, 2, 33);
        yield return core<long  >(123, 45, 2, 33);
        yield return core<float >(123, 45, 2, 33);
        yield return core<double>(123, 45, 2, 33);
        yield return core<sbyte >(-123,  45, -2, -33);
        yield return core<short >(-123,  45, -2, -33);
        yield return core<int   >(-123,  45, -2, -33);
        yield return core<long  >(-123,  45, -2, -33);
        yield return core<float >(-123,  45, -2, -33);
        yield return core<double>(-123,  45, -2, -33);
        yield return core<sbyte >( 123, -45, -2,  33);
        yield return core<short >( 123, -45, -2,  33);
        yield return core<int   >( 123, -45, -2,  33);
        yield return core<long  >( 123, -45, -2,  33);
        yield return core<float >( 123, -45, -2,  33);
        yield return core<double>( 123, -45, -2,  33);
        yield return core<sbyte >(-123, -45,  2, -33);
        yield return core<short >(-123, -45,  2, -33);
        yield return core<int   >(-123, -45,  2, -33);
        yield return core<long  >(-123, -45,  2, -33);
        yield return core<float >(-123, -45,  2, -33);
        yield return core<double>(-123, -45,  2, -33);
#if NET6_0_OR_GREATER
        yield return core<nuint >(123, 45, 2, 33);
        yield return core<nint  >(123, 45, 2, 33);
        yield return core<nint  >(-123,  45, -2, -33);
        yield return core<nint  >( 123, -45, -2,  33);
        yield return core<nint  >(-123, -45,  2, -33);
#endif
#pragma warning restore format
        yield break;
    }
    [Theory, MemberData(nameof(DivRemTestCases))]
    public void DivRem<T>(T a, T b, T expDiv, T expRem)
        where T : unmanaged
    {
        var actDiv = ScalarOp.DivRem(a, b, out var actRem);
        Assert.Equal(expDiv, actDiv);
        Assert.Equal(expRem, actRem);
    }


    public static TestCases DivRemByFloorTestCases()
    {
        static object[] core<T>(T a, T b, T expDiv, T expRem)
            where T : unmanaged
            => [a, b, expDiv, expRem];
        
#pragma warning disable format
        yield return core<byte  >(123, 45, 2, 33);
        yield return core<ushort>(123, 45, 2, 33);
        yield return core<uint  >(123, 45, 2, 33);
        yield return core<ulong >(123, 45, 2, 33);
        yield return core<sbyte >(123, 45, 2, 33);
        yield return core<short >(123, 45, 2, 33);
        yield return core<int   >(123, 45, 2, 33);
        yield return core<long  >(123, 45, 2, 33);
        yield return core<float >(123, 45, 2, 33);
        yield return core<double>(123, 45, 2, 33);

        yield return core<sbyte >(-123,  45, -3,  12);
        yield return core<short >(-123,  45, -3,  12);
        yield return core<int   >(-123,  45, -3,  12);
        yield return core<long  >(-123,  45, -3,  12);
        yield return core<float >(-123,  45, -3,  12);
        yield return core<double>(-123,  45, -3,  12);

        yield return core<sbyte >( 123, -45, -3, -12);
        yield return core<short >( 123, -45, -3, -12);
        yield return core<int   >( 123, -45, -3, -12);
        yield return core<long  >( 123, -45, -3, -12);
        yield return core<float >( 123, -45, -3, -12);
        yield return core<double>( 123, -45, -3, -12);

        yield return core<sbyte >(-123, -45,  2, -33);
        yield return core<short >(-123, -45,  2, -33);
        yield return core<int   >(-123, -45,  2, -33);
        yield return core<long  >(-123, -45,  2, -33);
        yield return core<float >(-123, -45,  2, -33);
        yield return core<double>(-123, -45,  2, -33);
        yield return core<nuint >(123, 45, 2, 33);
#if NET6_0_OR_GREATER
        yield return core<nint  >(123, 45, 2, 33);
        yield return core<nint  >(-123,  45, -3,  12);
        yield return core<nint  >( 123, -45, -3, -12);
        yield return core<nint  >(-123, -45,  2, -33);
#endif
#pragma warning restore format
        yield break;
    }
    [Theory, MemberData(nameof(DivRemByFloorTestCases))]
    public void DivRemByFloor<T>(T a, T b, T expDiv, T expRem)
        where T : unmanaged
    {
        var actDiv = ScalarOp.DivRemByFloor(a, b, out var actRem);
        Assert.Equal(expDiv, actDiv);
        Assert.Equal(expRem, actRem);
    }


    public static TestCases ModuloByFloorTestCases()
    {
        static object[] core<T>(T a, T b, T expRem)
            where T : unmanaged
            => [a, b, expRem];
        
#pragma warning disable format
        yield return core<byte  >(123, 45, 33);
        yield return core<ushort>(123, 45, 33);
        yield return core<uint  >(123, 45, 33);
        yield return core<ulong >(123, 45, 33);
        yield return core<sbyte >(123, 45, 33);
        yield return core<short >(123, 45, 33);
        yield return core<int   >(123, 45, 33);
        yield return core<long  >(123, 45, 33);
        yield return core<float >(123, 45, 33);
        yield return core<double>(123, 45, 33);
        yield return core<sbyte >(-123,  45,  12);
        yield return core<short >(-123,  45,  12);
        yield return core<int   >(-123,  45,  12);
        yield return core<long  >(-123,  45,  12);
        yield return core<float >(-123,  45,  12);
        yield return core<double>(-123,  45,  12);
        yield return core<sbyte >( 123, -45, -12);
        yield return core<short >( 123, -45, -12);
        yield return core<int   >( 123, -45, -12);
        yield return core<long  >( 123, -45, -12);
        yield return core<float >( 123, -45, -12);
        yield return core<double>( 123, -45, -12);
        yield return core<sbyte >(-123, -45, -33);
        yield return core<short >(-123, -45, -33);
        yield return core<int   >(-123, -45, -33);
        yield return core<long  >(-123, -45, -33);
        yield return core<float >(-123, -45, -33);
        yield return core<double>(-123, -45, -33);
#if NET6_0_OR_GREATER
        yield return core<nuint >(123, 45, 33);
        yield return core<nint  >(123, 45, 33);
        yield return core<nint  >(-123,  45,  12);
        yield return core<nint  >( 123, -45, -12);
        yield return core<nint  >(-123, -45, -33);
#endif
#pragma warning restore format
        yield break;
    }
    [Theory, MemberData(nameof(ModuloByFloorTestCases))]
    public void ModuloByFloor<T>(T a, T b, T expRem)
        where T : unmanaged
    {
        var actRem = ScalarOp.ModuloByFloor(a, b);
        Assert.Equal(expRem, actRem);
    }
}
#pragma warning restore xUnit1042
#pragma warning restore IDE0079
