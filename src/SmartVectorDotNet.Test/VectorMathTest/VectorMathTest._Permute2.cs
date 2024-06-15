using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SmartVectorDotNet;

public partial class VectorMathTest
{
    public static IEnumerable<object[]> Permute2TestCases()
    {
        static object[] core<T>(T[] expected, T[] input, byte m1, byte m2)
            where T : unmanaged
            => [expected, input, m1, m2,];

        { // byte
            byte[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
                08, 09, 10, 11, 12, 13, 14, 15,
                16, 17, 18, 19, 20, 21, 22, 23,
                24, 25, 26, 27, 28, 29, 30, 31,
            ];
            yield return core<byte>([
                    00, 00, 02, 02, 04, 04, 06, 06,
                    08, 08, 10, 10, 12, 12, 14, 14,
                    16, 16, 18, 18, 20, 20, 22, 22,
                    24, 24, 26, 26, 28, 28, 30, 30,
                ], v, 0, 0);
            yield return core<byte>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                    08, 09, 10, 11, 12, 13, 14, 15,
                    16, 17, 18, 19, 20, 21, 22, 23,
                    24, 25, 26, 27, 28, 29, 30, 31,
                ], v, 0, 1);
            yield return core<byte>([
                    01, 00, 03, 02, 05, 04, 07, 06,
                    09, 08, 11, 10, 13, 12, 15, 14,
                    17, 16, 19, 18, 21, 20, 23, 22,
                    25, 24, 27, 26, 29, 28, 31, 30,
                ], v, 1, 0);
            yield return core<byte>([
                    01, 01, 03, 03, 05, 05, 07, 07,
                    09, 09, 11, 11, 13, 13, 15, 15,
                    17, 17, 19, 19, 21, 21, 23, 23,
                    25, 25, 27, 27, 29, 29, 31, 31,
                ], v, 1, 1);
        }
        { // ushort
            ushort[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
                08, 09, 10, 11, 12, 13, 14, 15,
            ];
            yield return core<ushort>([
                    00, 00, 02, 02, 04, 04, 06, 06,
                    08, 08, 10, 10, 12, 12, 14, 14,
                ], v, 0, 0);
            yield return core<ushort>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                    08, 09, 10, 11, 12, 13, 14, 15,
                ], v, 0, 1);
            yield return core<ushort>([
                    01, 00, 03, 02, 05, 04, 07, 06,
                    09, 08, 11, 10, 13, 12, 15, 14,
                ], v, 1, 0);
            yield return core<ushort>([
                    01, 01, 03, 03, 05, 05, 07, 07,
                    09, 09, 11, 11, 13, 13, 15, 15,
                ], v, 1, 1);
        }
        { // uint
            uint[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
            ];
            yield return core<uint>([
                    00, 00, 02, 02, 04, 04, 06, 06,
                ], v, 0, 0);
            yield return core<uint>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                ], v, 0, 1);
            yield return core<uint>([
                    01, 00, 03, 02, 05, 04, 07, 06,
                ], v, 1, 0);
            yield return core<uint>([
                    01, 01, 03, 03, 05, 05, 07, 07,
                ], v, 1, 1);
        }
        { // ulong
            ulong[] v = [
                00, 01, 02, 03,
            ];
            yield return core<ulong>([
                    00, 00, 02, 02,
                ], v, 0, 0);
            yield return core<ulong>([
                    00, 01, 02, 03,
                ], v, 0, 1);
            yield return core<ulong>([
                    01, 00, 03, 02,
                ], v, 1, 0);
            yield return core<ulong>([
                    01, 01, 03, 03,
                ], v, 1, 1);
        }
    }

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    public void Permute2_Vector<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector<T>(input);
        var expectedV = InternalHelpers.CreateVector<T>(expected);
        Assert.Equal(expectedV, VectorMath.Permute2(inputV, m1, m2));
    }

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    public void Permute2_Vector128<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector128<T>(input);
        var expectedV = InternalHelpers.CreateVector128<T>(expected);
        if (Vector128<T>.Count >= 4)
        {
            Assert.Equal(expectedV, VectorMath.Permute2(inputV, m1, m2));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => VectorMath.Permute2(inputV, m1, m2));
        }
    }

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    public void Permute2_Vector256<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector256<T>(input);
        var expectedV = InternalHelpers.CreateVector256<T>(expected);
        Assert.Equal(expectedV, VectorMath.Permute2(inputV, m1, m2));
    }
}

