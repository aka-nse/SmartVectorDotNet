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
    public static IEnumerable<object[]> Permute4TestCases()
    {
        static object[] core<T>(T[] expected, T[] input, byte m1, byte m2, byte m3, byte m4)
            where T : unmanaged
            => [expected, input, m1, m2, m3, m4, ];

        { // byte
            byte[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
                08, 09, 10, 11, 12, 13, 14, 15,
                16, 17, 18, 19, 20, 21, 22, 23,
                24, 25, 26, 27, 28, 29, 30, 31,
            ];
            yield return core<byte>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                    08, 09, 10, 11, 12, 13, 14, 15,
                    16, 17, 18, 19, 20, 21, 22, 23,
                    24, 25, 26, 27, 28, 29, 30, 31,
                ], v, 0, 1, 2, 3);
            yield return core<byte>([
                    03, 02, 01, 00, 07, 06, 05, 04,
                    11, 10, 09, 08, 15, 14, 13, 12,
                    19, 18, 17, 16, 23, 22, 21, 20,
                    27, 26, 25, 24, 31, 30, 29, 28,
                ], v, 3, 2, 1, 0);
            yield return core<byte>([
                    00, 00, 00, 00, 04, 04, 04, 04,
                    08, 08, 08, 08, 12, 12, 12, 12,
                    16, 16, 16, 16, 20, 20, 20, 20,
                    24, 24, 24, 24, 28, 28, 28, 28,
                ], v, 0, 0, 0, 0);
            yield return core<byte>([
                    01, 01, 01, 01, 05, 05, 05, 05,
                    09, 09, 09, 09, 13, 13, 13, 13,
                    17, 17, 17, 17, 21, 21, 21, 21,
                    25, 25, 25, 25, 29, 29, 29, 29,
                ], v, 1, 1, 1, 1);
            yield return core<byte>([
                    02, 02, 02, 02, 06, 06, 06, 06,
                    10, 10, 10, 10, 14, 14, 14, 14,
                    18, 18, 18, 18, 22, 22, 22, 22,
                    26, 26, 26, 26, 30, 30, 30, 30,
                ], v, 2, 2, 2, 2);
            yield return core<byte>([
                    03, 03, 03, 03, 07, 07, 07, 07,
                    11, 11, 11, 11, 15, 15, 15, 15,
                    19, 19, 19, 19, 23, 23, 23, 23,
                    27, 27, 27, 27, 31, 31, 31, 31,
                ], v, 3, 3, 3, 3);
        }
        { // ushort
            ushort[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
                08, 09, 10, 11, 12, 13, 14, 15,
            ];
            yield return core<ushort>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                    08, 09, 10, 11, 12, 13, 14, 15,
                ], v, 0, 1, 2, 3);
            yield return core<ushort>([
                    03, 02, 01, 00, 07, 06, 05, 04,
                    11, 10, 09, 08, 15, 14, 13, 12,
                ], v, 3, 2, 1, 0);
            yield return core<ushort>([
                    00, 00, 00, 00, 04, 04, 04, 04,
                    08, 08, 08, 08, 12, 12, 12, 12,
                ], v, 0, 0, 0, 0);
            yield return core<ushort>([
                    01, 01, 01, 01, 05, 05, 05, 05,
                    09, 09, 09, 09, 13, 13, 13, 13,
                ], v, 1, 1, 1, 1);
            yield return core<ushort>([
                    02, 02, 02, 02, 06, 06, 06, 06,
                    10, 10, 10, 10, 14, 14, 14, 14,
                ], v, 2, 2, 2, 2);
            yield return core<ushort>([
                    03, 03, 03, 03, 07, 07, 07, 07,
                    11, 11, 11, 11, 15, 15, 15, 15,
                ], v, 3, 3, 3, 3);
        }
        { // uint
            uint[] v = [
                00, 01, 02, 03, 04, 05, 06, 07,
            ];
            yield return core<uint>([
                    00, 01, 02, 03, 04, 05, 06, 07,
                ], v, 0, 1, 2, 3);
            yield return core<uint>([
                    03, 02, 01, 00, 07, 06, 05, 04,
                ], v, 3, 2, 1, 0);
            yield return core<uint>([
                    00, 00, 00, 00, 04, 04, 04, 04,
                ], v, 0, 0, 0, 0);
            yield return core<uint>([
                    01, 01, 01, 01, 05, 05, 05, 05,
                ], v, 1, 1, 1, 1);
            yield return core<uint>([
                    02, 02, 02, 02, 06, 06, 06, 06,
                ], v, 2, 2, 2, 2);
            yield return core<uint>([
                    03, 03, 03, 03, 07, 07, 07, 07,
                ], v, 3, 3, 3, 3);
        }
        { // ulong
            ulong[] v = [
                00, 01, 02, 03,
            ];
            yield return core<ulong>([
                    00, 01, 02, 03,
                ], v, 0, 1, 2, 3);
            yield return core<ulong>([
                    03, 02, 01, 00,
                ], v, 3, 2, 1, 0);
            yield return core<ulong>([
                    00, 00, 00, 00,
                ], v, 0, 0, 0, 0);
            yield return core<ulong>([
                    01, 01, 01, 01,
                ], v, 1, 1, 1, 1);
            yield return core<ulong>([
                    02, 02, 02, 02,
                ], v, 2, 2, 2, 2);
            yield return core<ulong>([
                    03, 03, 03, 03,
                ], v, 3, 3, 3, 3);
        }
    }

    [Theory]
    [MemberData(nameof(Permute4TestCases))]
    public void Permute4_Vector<T>(T[] expected, T[] input, byte m1, byte m2, byte m3, byte m4)
        where T : unmanaged
    {
        {
            var inputV = InternalHelpers.CreateVector<T>(input);
            var expectedV = InternalHelpers.CreateVector<T>(expected);
            Assert.Equal(expectedV, VectorMath.Permute4(inputV, m1, m2, m3, m4));
        }
        {
            var inputV0 = InternalHelpers.CreateVector<T>(input);
            var inputV1 = InternalHelpers.CreateVector<T>(input);
            var expectedV = InternalHelpers.CreateVector<T>(expected);
            VectorMath.Permute4(ref inputV0, ref inputV1, m1, m2, m3, m4);
            Assert.Equal(expectedV, inputV0);
            Assert.Equal(expectedV, inputV1);
        }
    }

    [Theory]
    [MemberData(nameof(Permute4TestCases))]
    public void Permute4_Vector128<T>(T[] expected, T[] input, byte m1, byte m2, byte m3, byte m4)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector128<T>(input);
        var expectedV = InternalHelpers.CreateVector128<T>(expected);
        if (Vector128<T>.Count >= 4)
        {
            Assert.Equal(expectedV, VectorMath.Permute4(inputV, m1, m2, m3, m4));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => VectorMath.Permute4(inputV, m1, m2, m3, m4));
        }
    }

    [Theory]
    [MemberData(nameof(Permute4TestCases))]
    public void Permute4_Vector256<T>(T[] expected, T[] input, byte m1, byte m2, byte m3, byte m4)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector256<T>(input);
        var expectedV = InternalHelpers.CreateVector256<T>(expected);
        Assert.Equal(expectedV, VectorMath.Permute4(inputV, m1, m2, m3, m4));
    }
}

