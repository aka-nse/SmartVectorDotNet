#pragma warning disable xUnit1042
namespace SmartVectorDotNet;

public partial class ScalarOpTest
{
    public static IEnumerable<object[]> ConvertTestCases()
    {
        static object[] core<TFrom, TTo>(TFrom from, TTo to)
            => new object[] { from, to };

        yield return core<byte  , byte  >(123, 123);
        yield return core<byte  , ushort>(123, 123);
        yield return core<byte  , uint  >(123, 123);
        yield return core<byte  , ulong >(123, 123);
        yield return core<byte  , nuint >(123, 123);
        yield return core<byte  , sbyte >(123, 123);
        yield return core<byte  , short >(123, 123);
        yield return core<byte  , int   >(123, 123);
        yield return core<byte  , long  >(123, 123);
        yield return core<byte  , nint  >(123, 123);
        yield return core<byte  , float >(123, 123);
        yield return core<byte  , double>(123, 123);
        yield return core<ushort, byte  >(123, 123);
        yield return core<ushort, ushort>(123, 123);
        yield return core<ushort, uint  >(123, 123);
        yield return core<ushort, ulong >(123, 123);
        yield return core<ushort, nuint >(123, 123);
        yield return core<ushort, sbyte >(123, 123);
        yield return core<ushort, short >(123, 123);
        yield return core<ushort, int   >(123, 123);
        yield return core<ushort, long  >(123, 123);
        yield return core<ushort, nint  >(123, 123);
        yield return core<ushort, float >(123, 123);
        yield return core<ushort, double>(123, 123);
        yield return core<uint  , byte  >(123, 123);
        yield return core<uint  , ushort>(123, 123);
        yield return core<uint  , uint  >(123, 123);
        yield return core<uint  , ulong >(123, 123);
        yield return core<uint  , nuint >(123, 123);
        yield return core<uint  , sbyte >(123, 123);
        yield return core<uint  , short >(123, 123);
        yield return core<uint  , int   >(123, 123);
        yield return core<uint  , long  >(123, 123);
        yield return core<uint  , nint  >(123, 123);
        yield return core<uint  , float >(123, 123);
        yield return core<uint  , double>(123, 123);
        yield return core<ulong , byte  >(123, 123);
        yield return core<ulong , ushort>(123, 123);
        yield return core<ulong , uint  >(123, 123);
        yield return core<ulong , ulong >(123, 123);
        yield return core<ulong , nuint >(123, 123);
        yield return core<ulong , sbyte >(123, 123);
        yield return core<ulong , short >(123, 123);
        yield return core<ulong , int   >(123, 123);
        yield return core<ulong , long  >(123, 123);
        yield return core<ulong , nint  >(123, 123);
        yield return core<ulong , float >(123, 123);
        yield return core<ulong , double>(123, 123);
        yield return core<nuint , byte  >(123, 123);
        yield return core<nuint , ushort>(123, 123);
        yield return core<nuint , uint  >(123, 123);
        yield return core<nuint , ulong >(123, 123);
        yield return core<nuint , nuint >(123, 123);
        yield return core<nuint , sbyte >(123, 123);
        yield return core<nuint , short >(123, 123);
        yield return core<nuint , int   >(123, 123);
        yield return core<nuint , long  >(123, 123);
        yield return core<nuint , nint  >(123, 123);
        yield return core<nuint , float >(123, 123);
        yield return core<nuint , double>(123, 123);
        yield return core<sbyte , byte  >(123, 123);
        yield return core<sbyte , ushort>(123, 123);
        yield return core<sbyte , uint  >(123, 123);
        yield return core<sbyte , ulong >(123, 123);
        yield return core<sbyte , nuint >(123, 123);
        yield return core<sbyte , sbyte >(123, 123);
        yield return core<sbyte , short >(123, 123);
        yield return core<sbyte , int   >(123, 123);
        yield return core<sbyte , long  >(123, 123);
        yield return core<sbyte , nint  >(123, 123);
        yield return core<sbyte , float >(123, 123);
        yield return core<sbyte , double>(123, 123);
        yield return core<short , byte  >(123, 123);
        yield return core<short , ushort>(123, 123);
        yield return core<short , uint  >(123, 123);
        yield return core<short , ulong >(123, 123);
        yield return core<short , nuint >(123, 123);
        yield return core<short , sbyte >(123, 123);
        yield return core<short , short >(123, 123);
        yield return core<short , int   >(123, 123);
        yield return core<short , long  >(123, 123);
        yield return core<short , nint  >(123, 123);
        yield return core<short , float >(123, 123);
        yield return core<short , double>(123, 123);
        yield return core<int   , byte  >(123, 123);
        yield return core<int   , ushort>(123, 123);
        yield return core<int   , uint  >(123, 123);
        yield return core<int   , ulong >(123, 123);
        yield return core<int   , nuint >(123, 123);
        yield return core<int   , sbyte >(123, 123);
        yield return core<int   , short >(123, 123);
        yield return core<int   , int   >(123, 123);
        yield return core<int   , long  >(123, 123);
        yield return core<int   , nint  >(123, 123);
        yield return core<int   , float >(123, 123);
        yield return core<int   , double>(123, 123);
        yield return core<long  , byte  >(123, 123);
        yield return core<long  , ushort>(123, 123);
        yield return core<long  , uint  >(123, 123);
        yield return core<long  , ulong >(123, 123);
        yield return core<long  , nuint >(123, 123);
        yield return core<long  , sbyte >(123, 123);
        yield return core<long  , short >(123, 123);
        yield return core<long  , int   >(123, 123);
        yield return core<long  , long  >(123, 123);
        yield return core<long  , nint  >(123, 123);
        yield return core<long  , float >(123, 123);
        yield return core<long  , double>(123, 123);
        yield return core<nint  , byte  >(123, 123);
        yield return core<nint  , ushort>(123, 123);
        yield return core<nint  , uint  >(123, 123);
        yield return core<nint  , ulong >(123, 123);
        yield return core<nint  , nuint >(123, 123);
        yield return core<nint  , sbyte >(123, 123);
        yield return core<nint  , short >(123, 123);
        yield return core<nint  , int   >(123, 123);
        yield return core<nint  , long  >(123, 123);
        yield return core<nint  , nint  >(123, 123);
        yield return core<nint  , float >(123, 123);
        yield return core<nint  , double>(123, 123);
        yield return core<float , byte  >(123, 123);
        yield return core<float , ushort>(123, 123);
        yield return core<float , uint  >(123, 123);
        yield return core<float , ulong >(123, 123);
        yield return core<float , nuint >(123, 123);
        yield return core<float , sbyte >(123, 123);
        yield return core<float , short >(123, 123);
        yield return core<float , int   >(123, 123);
        yield return core<float , long  >(123, 123);
        yield return core<float , nint  >(123, 123);
        yield return core<float , float >(123, 123);
        yield return core<float , double>(123, 123);
        yield return core<double, byte  >(123, 123);
        yield return core<double, ushort>(123, 123);
        yield return core<double, uint  >(123, 123);
        yield return core<double, ulong >(123, 123);
        yield return core<double, nuint >(123, 123);
        yield return core<double, sbyte >(123, 123);
        yield return core<double, short >(123, 123);
        yield return core<double, int   >(123, 123);
        yield return core<double, long  >(123, 123);
        yield return core<double, nint  >(123, 123);
        yield return core<double, float >(123, 123);
        yield return core<double, double>(123, 123);

        yield break;
    }

    [Theory, MemberData(nameof(ConvertTestCases))]
    public void Convert<TFrom, TTo>(TFrom from, TTo to)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        Assert.Equal(to, ScalarOp.Convert<TFrom, TTo>(from));
    }
}
