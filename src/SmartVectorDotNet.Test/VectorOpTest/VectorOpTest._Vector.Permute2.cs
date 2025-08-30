using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    public static IEnumerable<object[]> Permute2TestCases()
    {
        static object[] core<T>(T[] expected, T[] input, byte m1, byte m2)
            where T : unmanaged
            => [expected, input, m1, m2,];

        { // byte
            byte[] v = [00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31];
            yield return core<byte>([00, 00, 02, 02, 04, 04, 06, 06, 08, 08, 10, 10, 12, 12, 14, 14, 16, 16, 18, 18, 20, 20, 22, 22, 24, 24, 26, 26, 28, 28, 30, 30], v, 0, 0);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0, 1);
            yield return core<byte>([01, 00, 03, 02, 05, 04, 07, 06, 09, 08, 11, 10, 13, 12, 15, 14, 17, 16, 19, 18, 21, 20, 23, 22, 25, 24, 27, 26, 29, 28, 31, 30], v, 1, 0);
            yield return core<byte>([01, 01, 03, 03, 05, 05, 07, 07, 09, 09, 11, 11, 13, 13, 15, 15, 17, 17, 19, 19, 21, 21, 23, 23, 25, 25, 27, 27, 29, 29, 31, 31], v, 1, 1);
        }
        { // ushort
            ushort[] v = [00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15];
            yield return core<ushort>([00, 00, 02, 02, 04, 04, 06, 06, 08, 08, 10, 10, 12, 12, 14, 14], v, 0, 0);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0, 1);
            yield return core<ushort>([01, 00, 03, 02, 05, 04, 07, 06, 09, 08, 11, 10, 13, 12, 15, 14], v, 1, 0);
            yield return core<ushort>([01, 01, 03, 03, 05, 05, 07, 07, 09, 09, 11, 11, 13, 13, 15, 15], v, 1, 1);
        }
        { // uint
            uint[] v = [0, 1, 2, 3, 4, 5, 6, 7];
            yield return core<uint>([0, 0, 2, 2, 4, 4, 6, 6], v, 0, 0);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0, 1);
            yield return core<uint>([1, 0, 3, 2, 5, 4, 7, 6], v, 1, 0);
            yield return core<uint>([1, 1, 3, 3, 5, 5, 7, 7], v, 1, 1);
        }
        { // ulong
            ulong[] v = [0, 1, 2, 3];
            yield return core<ulong>([0, 0, 2, 2], v, 0, 0);
            yield return core<ulong>([0, 1, 2, 3], v, 0, 1);
            yield return core<ulong>([1, 0, 3, 2], v, 1, 0);
            yield return core<ulong>([1, 1, 3, 3], v, 1, 1);
        }
    }

    public static IEnumerable<object[]> Permute2_OutOfRangeTestCases()
    {
        static object[] core<T>(T[] expected, T[] input, byte m1, byte m2)
            where T : unmanaged
            => [expected, input, m1, m2,];

        { // byte
            byte[] v = [00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31];
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b00000010, 0b00000011);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b00000100, 0b00000101);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b00001000, 0b00001001);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b00010000, 0b00010001);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b00100000, 0b00100001);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b01000000, 0b01000001);
            yield return core<byte>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31], v, 0b10000000, 0b10000001);
        }
        { // ushort
            ushort[] v = [00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15];
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b00000010, 0b00000011);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b00000100, 0b00000101);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b00001000, 0b00001001);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b00010000, 0b00010001);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b00100000, 0b00100001);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b01000000, 0b01000001);
            yield return core<ushort>([00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15], v, 0b10000000, 0b10000001);
        }
        { // uint
            uint[] v = [0, 1, 2, 3, 4, 5, 6, 7];
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b00000010, 0b00000011);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b00000100, 0b00000101);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b00001000, 0b00001001);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b00010000, 0b00010001);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b00100000, 0b00100001);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b01000000, 0b01000001);
            yield return core<uint>([0, 1, 2, 3, 4, 5, 6, 7], v, 0b10000000, 0b10000001);
        }
        { // ulong
            ulong[] v = [0, 1, 2, 3];
            yield return core<ulong>([0, 1, 2, 3], v, 0b00000010, 0b00000011);
            yield return core<ulong>([0, 1, 2, 3], v, 0b00000100, 0b00000101);
            yield return core<ulong>([0, 1, 2, 3], v, 0b00001000, 0b00001001);
            yield return core<ulong>([0, 1, 2, 3], v, 0b00010000, 0b00010001);
            yield return core<ulong>([0, 1, 2, 3], v, 0b00100000, 0b00100001);
            yield return core<ulong>([0, 1, 2, 3], v, 0b01000000, 0b01000001);
            yield return core<ulong>([0, 1, 2, 3], v, 0b10000000, 0b10000001);
        }
    }

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    [MemberData(nameof(Permute2_OutOfRangeTestCases))]
    public void Permute2_Vector<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector<T>(input);
        var expectedV = InternalHelpers.CreateVector<T>(expected);
        Assert.Equal(expectedV, VectorOp.Permute2(inputV, m1, m2));
    }

#if NETCOREAPP3_0_OR_GREATER

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    public void Permute2_Vector128<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector128<T>(input);
        var expectedV = InternalHelpers.CreateVector128<T>(expected);
        if (Vector128<T>.Count >= 4)
        {
            Assert.Equal(expectedV, VectorOp.Permute2(inputV, m1, m2));
        }
        else
        {
            Assert.Throws<NotSupportedException>(() => VectorOp.Permute2(inputV, m1, m2));
        }
    }

    [Theory]
    [MemberData(nameof(Permute2TestCases))]
    public void Permute2_Vector256<T>(T[] expected, T[] input, byte m1, byte m2)
        where T : unmanaged
    {
        var inputV = InternalHelpers.CreateVector256<T>(input);
        var expectedV = InternalHelpers.CreateVector256<T>(expected);
        Assert.Equal(expectedV, VectorOp.Permute2(inputV, m1, m2));
    }

#endif
}

