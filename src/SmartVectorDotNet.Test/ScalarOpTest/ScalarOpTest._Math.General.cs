namespace SmartVectorDotNet;
using TestCases = IEnumerable<object?[]>;

public partial class ScalarOpTest
{
    public static TestCases AbsTestCases()
    {
        yield return TestCase<StubType>(false, default(StubType));
#pragma warning disable format
        yield return TestCase<byte  >(false, default(byte  ));
        yield return TestCase<ushort>(false, default(ushort));
        yield return TestCase<uint  >(false, default(uint  ));
        yield return TestCase<ulong >(false, default(ulong ));
        yield return TestCase<nuint >(false, default(nuint ));
        yield return TestCase<sbyte >(true, -123, 123);
        yield return TestCase<short >(true, -123, 123);
        yield return TestCase<int   >(true, -123, 123);
        yield return TestCase<long  >(true, -123, 123);
        yield return TestCase<nint  >(true, -123, 123);
        yield return TestCase<float >(true, -123, 123);
        yield return TestCase<double>(true, -123, 123);
#pragma warning restore format
    }
    [Theory, MemberData(nameof(AbsTestCases))]
    public void Abs<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Abs, isSupported, input, expected);


    public static TestCases MaxTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        yield return TestCase<byte  >(true, (12, 34), 34);
        yield return TestCase<ushort>(true, (12, 34), 34);
        yield return TestCase<uint  >(true, (12, 34), 34);
        yield return TestCase<ulong >(true, (12, 34), 34);
        yield return TestCase<nuint >(true, (12, 34), 34);
        yield return TestCase<sbyte >(true, (12, 34), 34);
        yield return TestCase<short >(true, (12, 34), 34);
        yield return TestCase<int   >(true, (12, 34), 34);
        yield return TestCase<long  >(true, (12, 34), 34);
        yield return TestCase<nint  >(true, (12, 34), 34);
        yield return TestCase<float >(true, (12, 34), 34);
        yield return TestCase<double>(true, (12, 34), 34);
#pragma warning restore format
    }
    [Theory, MemberData(nameof(MaxTestCases))]
    public void Max<T>(bool isSupported, (T, T) inputs, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Max, isSupported, inputs, expected);


    public static TestCases MinTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        yield return TestCase<byte  >(true, (12, 34), 12);
        yield return TestCase<ushort>(true, (12, 34), 12);
        yield return TestCase<uint  >(true, (12, 34), 12);
        yield return TestCase<ulong >(true, (12, 34), 12);
        yield return TestCase<nuint >(true, (12, 34), 12);
        yield return TestCase<sbyte >(true, (12, 34), 12);
        yield return TestCase<short >(true, (12, 34), 12);
        yield return TestCase<int   >(true, (12, 34), 12);
        yield return TestCase<long  >(true, (12, 34), 12);
        yield return TestCase<nint  >(true, (12, 34), 12);
        yield return TestCase<float >(true, (12, 34), 12);
        yield return TestCase<double>(true, (12, 34), 12);
#pragma warning restore format
    }
    [Theory, MemberData(nameof(MinTestCases))]
    public void Min<T>(bool isSupported, (T, T) inputs, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Min, isSupported, inputs, expected);


    public static TestCases ClampTestCases()
    {
        yield return TestCase<StubType>(false, (default, default, default));
#pragma warning disable format
        yield return TestCase<byte  >(true, (12, 34, 56), 34);
        yield return TestCase<ushort>(true, (12, 34, 56), 34);
        yield return TestCase<uint  >(true, (12, 34, 56), 34);
        yield return TestCase<ulong >(true, (12, 34, 56), 34);
        yield return TestCase<nuint >(true, (12, 34, 56), 34);
        yield return TestCase<sbyte >(true, (12, 34, 56), 34);
        yield return TestCase<short >(true, (12, 34, 56), 34);
        yield return TestCase<int   >(true, (12, 34, 56), 34);
        yield return TestCase<long  >(true, (12, 34, 56), 34);
        yield return TestCase<nint  >(true, (12, 34, 56), 34);
        yield return TestCase<float >(true, (12, 34, 56), 34);
        yield return TestCase<double>(true, (12, 34, 56), 34);
        yield return TestCase<byte  >(true, (78, 34, 56), 56);
        yield return TestCase<ushort>(true, (78, 34, 56), 56);
        yield return TestCase<uint  >(true, (78, 34, 56), 56);
        yield return TestCase<ulong >(true, (78, 34, 56), 56);
        yield return TestCase<nuint >(true, (78, 34, 56), 56);
        yield return TestCase<sbyte >(true, (78, 34, 56), 56);
        yield return TestCase<short >(true, (78, 34, 56), 56);
        yield return TestCase<int   >(true, (78, 34, 56), 56);
        yield return TestCase<long  >(true, (78, 34, 56), 56);
        yield return TestCase<nint  >(true, (78, 34, 56), 56);
        yield return TestCase<float >(true, (78, 34, 56), 56);
        yield return TestCase<double>(true, (78, 34, 56), 56);
#pragma warning restore format
    }
    [Theory, MemberData(nameof(ClampTestCases))]
    public void Clamp<T>(bool isSupported, (T, T, T) inputs, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Clamp, isSupported, inputs, expected);
}
