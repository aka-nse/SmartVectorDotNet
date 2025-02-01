using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;
using TestCases = IEnumerable<object?[]>;

public partial class ScalarOpTest
{

    public static TestCases UnaryPlusTestCases()
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
    [Theory, MemberData(nameof(UnaryPlusTestCases))]
    public void UnaryPlus<T>(bool isSupported, T input, T expected)
        where T : unmanaged
        => CommonTest(ScalarOp.UnaryPlus, isSupported, input, expected);

}
