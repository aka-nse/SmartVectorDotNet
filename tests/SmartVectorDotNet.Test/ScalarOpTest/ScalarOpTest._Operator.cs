using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;
using TestCases = IEnumerable<object?[]>;
using H = InternalHelpers;

public partial class ScalarOpTest
{
    public static TestCases UnaryPlusTestCases()
    {
        yield return TestCase<StubType>(false, default(StubType));
#pragma warning disable format
        yield return TestCase<byte  >(true, 123, 123);
        yield return TestCase<ushort>(true, 123, 123);
        yield return TestCase<uint  >(true, 123, 123);
        yield return TestCase<ulong >(true, 123, 123);
        yield return TestCase<nuint >(true, 123, 123);
        yield return TestCase<sbyte >(true, 123, 123);
        yield return TestCase<short >(true, 123, 123);
        yield return TestCase<int   >(true, 123, 123);
        yield return TestCase<long  >(true, 123, 123);
        yield return TestCase<nint  >(true, 123, 123);
        yield return TestCase<float >(true, 123, 123);
        yield return TestCase<double>(true, 123, 123);
#pragma warning restore format
    }
    [Theory, MemberData(nameof(UnaryPlusTestCases))]
    public void UnaryPlus<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.UnaryPlus, isSupported, input, expected);


    public static TestCases UnaryMinusTestCases()
    {
        yield return TestCase<StubType>(false, default(StubType));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, 123, (byte  )(-123));
            yield return TestCase<ushort>(true, 123, (ushort)(-123));
            yield return TestCase<uint  >(true, 123, (uint  )(-123));
            yield return TestCase<ulong >(true, 123, (ulong )(-123));
            yield return TestCase<nuint >(true, 123, (nuint )(-123));
            yield return TestCase<sbyte >(true, 123, (sbyte )(-123));
            yield return TestCase<short >(true, 123, (short )(-123));
            yield return TestCase<int   >(true, 123, (int   )(-123));
            yield return TestCase<long  >(true, 123, (long  )(-123));
            yield return TestCase<nint  >(true, 123, (nint  )(-123));
            yield return TestCase<float >(true, 123, (float )(-123));
            yield return TestCase<double>(true, 123, (double)(-123));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(UnaryMinusTestCases))]
    public void UnaryMinus<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.UnaryMinus, isSupported, input, expected);


    public static TestCases NotTestCases()
    {
        yield return TestCase<StubType>(false, default(StubType));
#pragma warning disable format
        yield return TestCase<byte  >(false, default(byte  ));
        yield return TestCase<ushort>(false, default(ushort));
        yield return TestCase<uint  >(false, default(uint  ));
        yield return TestCase<ulong >(false, default(ulong ));
        yield return TestCase<nuint >(false, default(nuint ));
        yield return TestCase<sbyte >(false, default(sbyte ));
        yield return TestCase<short >(false, default(short ));
        yield return TestCase<int   >(false, default(int   ));
        yield return TestCase<long  >(false, default(long  ));
        yield return TestCase<nint  >(false, default(nint  ));
        yield return TestCase<float >(false, default(float ));
        yield return TestCase<double>(false, default(double));
#pragma warning restore format
    }
    [Theory, MemberData(nameof(NotTestCases))]
    public void Not<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Not, isSupported, input, expected);


    public static TestCases ComplementTestCases()
    {
        yield return TestCase<StubType>(false, default(StubType));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, 123, (byte  )~(byte  )123);
            yield return TestCase<ushort>(true, 123, (ushort)~(ushort)123);
            yield return TestCase<uint  >(true, 123, (uint  )~(uint  )123);
            yield return TestCase<ulong >(true, 123, (ulong )~(ulong )123);
            yield return TestCase<nuint >(true, 123, (nuint )~(nuint )123);
            yield return TestCase<sbyte >(true, 123, (sbyte )~(sbyte )123);
            yield return TestCase<short >(true, 123, (short )~(short )123);
            yield return TestCase<int   >(true, 123, (int   )~(int   )123);
            yield return TestCase<long  >(true, 123, (long  )~(long  )123);
            yield return TestCase<nint  >(true, 123, (nint  )~(nint  )123);
            yield return TestCase<float >(true, 1.23f, H.Reinterpret<uint , float >(~H.Reinterpret<float , uint >(1.23f)));
            yield return TestCase<double>(true, 1.23 , H.Reinterpret<ulong, double>(~H.Reinterpret<double, ulong>(1.23 )));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(ComplementTestCases))]
    public void Complement<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Complement, isSupported, input, expected);


    public static TestCases AddTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 + 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 + 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 + 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 + 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 + 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 + 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 + 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 + 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 + 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 + 34));
            yield return TestCase<float >(true, (12, 34), (float )(12 + 34));
            yield return TestCase<double>(true, (12, 34), (double)(12 + 34));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(AddTestCases))]
    public void Add<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Add, isSupported, input, expected);


    public static TestCases AddCheckedTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 + 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 + 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 + 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 + 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 + 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 + 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 + 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 + 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 + 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 + 34));
            yield return TestCase<float >(true, (12, 34), (float )(12 + 34));
            yield return TestCase<double>(true, (12, 34), (double)(12 + 34));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(AddCheckedTestCases))]
    public void AddChecked<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.AddChecked, isSupported, input, expected);


    public static TestCases SubtractTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 - 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 - 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 - 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 - 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 - 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 - 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 - 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 - 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 - 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 - 34));
            yield return TestCase<float >(true, (12, 34), (float )(12 - 34));
            yield return TestCase<double>(true, (12, 34), (double)(12 - 34));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(SubtractTestCases))]
    public void Subtract<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Subtract, isSupported, input, expected);


    public static TestCases SubtractCheckedTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 3), (byte  )(12 - 3));
            yield return TestCase<ushort>(true, (12, 3), (ushort)(12 - 3));
            yield return TestCase<uint  >(true, (12, 3), (uint  )(12 - 3));
            yield return TestCase<ulong >(true, (12, 3), (ulong )(12 - 3));
            yield return TestCase<nuint >(true, (12, 3), (nuint )(12 - 3));
            yield return TestCase<sbyte >(true, (12, 3), (sbyte )(12 - 3));
            yield return TestCase<short >(true, (12, 3), (short )(12 - 3));
            yield return TestCase<int   >(true, (12, 3), (int   )(12 - 3));
            yield return TestCase<long  >(true, (12, 3), (long  )(12 - 3));
            yield return TestCase<nint  >(true, (12, 3), (nint  )(12 - 3));
            yield return TestCase<float >(true, (12, 3), (float )(12 - 3));
            yield return TestCase<double>(true, (12, 3), (double)(12 - 3));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(SubtractCheckedTestCases))]
    public void SubtractChecked<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.SubtractChecked, isSupported, input, expected);


    public static TestCases MultiplyTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 * 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 * 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 * 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 * 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 * 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 * 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 * 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 * 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 * 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 * 34));
            yield return TestCase<float >(true, (12, 34), (float )(12 * 34));
            yield return TestCase<double>(true, (12, 34), (double)(12 * 34));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(MultiplyTestCases))]
    public void Multiply<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Multiply, isSupported, input, expected);


    public static TestCases MultiplyCheckedTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 3), (byte  )(12 * 3));
            yield return TestCase<ushort>(true, (12, 3), (ushort)(12 * 3));
            yield return TestCase<uint  >(true, (12, 3), (uint  )(12 * 3));
            yield return TestCase<ulong >(true, (12, 3), (ulong )(12 * 3));
            yield return TestCase<nuint >(true, (12, 3), (nuint )(12 * 3));
            yield return TestCase<sbyte >(true, (12, 3), (sbyte )(12 * 3));
            yield return TestCase<short >(true, (12, 3), (short )(12 * 3));
            yield return TestCase<int   >(true, (12, 3), (int   )(12 * 3));
            yield return TestCase<long  >(true, (12, 3), (long  )(12 * 3));
            yield return TestCase<nint  >(true, (12, 3), (nint  )(12 * 3));
            yield return TestCase<float >(true, (12, 3), (float )(12 * 3));
            yield return TestCase<double>(true, (12, 3), (double)(12 * 3));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(MultiplyCheckedTestCases))]
    public void MultiplyChecked<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.MultiplyChecked, isSupported, input, expected);


    public static TestCases DivideTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (78, 34), (byte  )(78 / 34));
            yield return TestCase<ushort>(true, (78, 34), (ushort)(78 / 34));
            yield return TestCase<uint  >(true, (78, 34), (uint  )(78 / 34));
            yield return TestCase<ulong >(true, (78, 34), (ulong )(78 / 34));
            yield return TestCase<nuint >(true, (78, 34), (nuint )(78 / 34));
            yield return TestCase<sbyte >(true, (78, 34), (sbyte )(78 / 34));
            yield return TestCase<short >(true, (78, 34), (short )(78 / 34));
            yield return TestCase<int   >(true, (78, 34), (int   )(78 / 34));
            yield return TestCase<long  >(true, (78, 34), (long  )(78 / 34));
            yield return TestCase<nint  >(true, (78, 34), (nint  )(78 / 34));
            yield return TestCase<float >(true, (78f , 34f ), 78f  / 34f );
            yield return TestCase<double>(true, (78.0, 34.0), 78.0 / 34.0);
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(DivideTestCases))]
    public void Divide<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Divide, isSupported, input, expected);


    public static TestCases DivideCheckedTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (78, 34), (byte  )(78 / 34));
            yield return TestCase<ushort>(true, (78, 34), (ushort)(78 / 34));
            yield return TestCase<uint  >(true, (78, 34), (uint  )(78 / 34));
            yield return TestCase<ulong >(true, (78, 34), (ulong )(78 / 34));
            yield return TestCase<nuint >(true, (78, 34), (nuint )(78 / 34));
            yield return TestCase<sbyte >(true, (78, 34), (sbyte )(78 / 34));
            yield return TestCase<short >(true, (78, 34), (short )(78 / 34));
            yield return TestCase<int   >(true, (78, 34), (int   )(78 / 34));
            yield return TestCase<long  >(true, (78, 34), (long  )(78 / 34));
            yield return TestCase<nint  >(true, (78, 34), (nint  )(78 / 34));
            yield return TestCase<float >(true, (78f , 34f ), 78f  / 34f );
            yield return TestCase<double>(true, (78.0, 34.0), 78.0 / 34.0);
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(DivideCheckedTestCases))]
    public void DivideChecked<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.DivideChecked, isSupported, input, expected);


    public static TestCases ModuloTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (78, 34), (byte  )(78 % 34));
            yield return TestCase<ushort>(true, (78, 34), (ushort)(78 % 34));
            yield return TestCase<uint  >(true, (78, 34), (uint  )(78 % 34));
            yield return TestCase<ulong >(true, (78, 34), (ulong )(78 % 34));
            yield return TestCase<nuint >(true, (78, 34), (nuint )(78 % 34));
            yield return TestCase<sbyte >(true, (78, 34), (sbyte )(78 % 34));
            yield return TestCase<short >(true, (78, 34), (short )(78 % 34));
            yield return TestCase<int   >(true, (78, 34), (int   )(78 % 34));
            yield return TestCase<long  >(true, (78, 34), (long  )(78 % 34));
            yield return TestCase<nint  >(true, (78, 34), (nint  )(78 % 34));
            yield return TestCase<float >(true, (78, 34), (float )(78 % 34));
            yield return TestCase<double>(true, (78, 34), (double)(78 % 34));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(ModuloTestCases))]
    public void Modulo<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.Modulo, isSupported, input, expected);


    public static TestCases BitwiseAndTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 & 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 & 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 & 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 & 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 & 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 & 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 & 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 & 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 & 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 & 34));
            yield return TestCase<float >(true, (12.345f, 67.890f), H.Reinterpret<int , float >(H.Reinterpret<float , int >(12.345f) & H.Reinterpret<float , int >(67.890f)));
            yield return TestCase<double>(true, (12.345 , 67.890 ), H.Reinterpret<long, double>(H.Reinterpret<double, long>(12.345 ) & H.Reinterpret<double, long>(67.890 )));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(BitwiseAndTestCases))]
    public void BitwiseAnd<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.BitwiseAnd, isSupported, input, expected);


    public static TestCases BitwiseOrTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 | 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 | 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 | 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 | 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 | 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 | 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 | 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 | 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 | 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 | 34));
            yield return TestCase<float >(true, (12.345f, 67.890f), H.Reinterpret<uint , float >(H.Reinterpret<float , uint >(12.345f) | H.Reinterpret<float , uint >(67.890f)));
            yield return TestCase<double>(true, (12.345 , 67.890 ), H.Reinterpret<ulong, double>(H.Reinterpret<double, ulong>(12.345 ) | H.Reinterpret<double, ulong>(67.890 )));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(BitwiseOrTestCases))]
    public void BitwiseOr<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.BitwiseOr, isSupported, input, expected);


    public static TestCases BitwiseXorTestCases()
    {
        yield return TestCase<StubType>(false, (default, default));
#pragma warning disable format
        unchecked
        {
            yield return TestCase<byte  >(true, (12, 34), (byte  )(12 ^ 34));
            yield return TestCase<ushort>(true, (12, 34), (ushort)(12 ^ 34));
            yield return TestCase<uint  >(true, (12, 34), (uint  )(12 ^ 34));
            yield return TestCase<ulong >(true, (12, 34), (ulong )(12 ^ 34));
            yield return TestCase<nuint >(true, (12, 34), (nuint )(12 ^ 34));
            yield return TestCase<sbyte >(true, (12, 34), (sbyte )(12 ^ 34));
            yield return TestCase<short >(true, (12, 34), (short )(12 ^ 34));
            yield return TestCase<int   >(true, (12, 34), (int   )(12 ^ 34));
            yield return TestCase<long  >(true, (12, 34), (long  )(12 ^ 34));
            yield return TestCase<nint  >(true, (12, 34), (nint  )(12 ^ 34));
            yield return TestCase<float >(true, (12.345f, 67.890f), H.Reinterpret<uint , float >(H.Reinterpret<float , uint >(12.345f) ^ H.Reinterpret<float , uint >(67.890f)));
            yield return TestCase<double>(true, (12.345 , 67.890 ), H.Reinterpret<ulong, double>(H.Reinterpret<double, ulong>(12.345 ) ^ H.Reinterpret<double, ulong>(67.890 )));
        }
#pragma warning restore format
    }
    [Theory, MemberData(nameof(BitwiseXorTestCases))]
    public void BitwiseXor<T>(bool isSupported, (T, T) input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.BitwiseXor, isSupported, input, expected);



    public static TestCases ShiftLeftTestCases()
    {
        yield return TestCaseEx<StubType, int, StubType>(false, default);
#pragma warning disable format
        yield return TestCaseEx<byte  , int, byte  >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<ushort, int, ushort>(true, (12, 3), 12 << 3);
        yield return TestCaseEx<uint  , int, uint  >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<ulong , int, ulong >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<nuint , int, nuint >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<sbyte , int, sbyte >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<short , int, short >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<int   , int, int   >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<long  , int, long  >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<nint  , int, nint  >(true, (12, 3), 12 << 3);
        yield return TestCaseEx<float , int, float >(true, (-12f , 3), H.Reinterpret<int , float >(H.Reinterpret<float , int >(-12f ) << 3));
        yield return TestCaseEx<double, int, double>(true, (-12.0, 3), H.Reinterpret<long, double>(H.Reinterpret<double, long>(-12.0) << 3));
#pragma warning restore format
    }
    [Theory, MemberData(nameof(ShiftLeftTestCases))]
    public void ShiftLeft<T>(bool isSupported, (T, int) input, T expected)
        where T : unmanaged
        => CommonTestEx(ScalarOp.ShiftLeft, isSupported, input, expected);



    public static TestCases ShiftRightTestCases()
    {
        yield return TestCaseEx<StubType, int, StubType>(false, default);
#pragma warning disable format
        yield return TestCaseEx<byte  , int, byte  >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<ushort, int, ushort>(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<uint  , int, uint  >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<ulong , int, ulong >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<nuint , int, nuint >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<sbyte , int, sbyte >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<short , int, short >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<int   , int, int   >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<long  , int, long  >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<nint  , int, nint  >(true, (12, 3), 12 >> 3);
        yield return TestCaseEx<float , int, float >(true, (-12f , 3), H.Reinterpret<int , float >(H.Reinterpret<float , int >(-12f ) >> 3));
        yield return TestCaseEx<double, int, double>(true, (-12.0, 3), H.Reinterpret<long, double>(H.Reinterpret<double, long>(-12.0) >> 3));
#pragma warning restore format
    }
    [Theory, MemberData(nameof(ShiftRightTestCases))]
    public void ShiftRight<T>(bool isSupported, (T, int) input, T expected)
        where T : unmanaged
        => CommonTestEx(ScalarOp.ShiftRight, isSupported, input, expected);

}
