#pragma warning disable xUnit1042
namespace SmartVectorDotNet;

public partial class ScalarOpTest
{
    public static IEnumerable<object[]> Convert_NormalTestCases()
    {
        object[] testValues = [
            (byte  )123,
            (ushort)123,
            (uint  )123,
            (ulong )123,
            (nuint )123,
            (sbyte )123,
            (short )123,
            (int   )123,
            (long  )123,
            (nint  )123,
            (float )123,
            (double)123,
        ];
        foreach (var fromValue in testValues)
        {
            foreach(var toValue in testValues)
            {
                yield return [fromValue, toValue];
            }
        }
    }

    [Theory, MemberData(nameof(Convert_NormalTestCases))]
    public void Convert_Normal<TFrom, TTo>(TFrom from, TTo to)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        Assert.Equal(to, ScalarOp.Convert<TFrom, TTo>(from));
    }

    public static IEnumerable<object[]> Convert_NotSupportedTypeTestCases()
    {
        object[] testValues = [
            (byte  )123,
            (ushort)123,
            (uint  )123,
            (ulong )123,
            (nuint )123,
            (sbyte )123,
            (short )123,
            (int   )123,
            (long  )123,
            (nint  )123,
            (float )123,
            (double)123,
        ];
        foreach (var fromValue in testValues)
        {
            yield return [fromValue, default(ValueTuple)];
        }
        foreach (var toValue in testValues)
        {
            yield return [default(ValueTuple), toValue];
        }
    }

    [Theory, MemberData(nameof(Convert_NotSupportedTypeTestCases))]
    public void Convert_NotSupportedType<TFrom, TTo>(TFrom from, TTo toTypeDummy)
        where TFrom : unmanaged
        where TTo : unmanaged
    {
        TextWriter.Null.WriteLine(toTypeDummy);
        Assert.Throws<NotSupportedException>(() => ScalarOp.Convert<TFrom, TTo>(from));
    }
}
