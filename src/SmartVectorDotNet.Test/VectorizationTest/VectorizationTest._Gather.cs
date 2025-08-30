using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

public partial class VectorizationTest
{
    private static readonly byte[] _TestTable8 = Enumerable
        .Range(0, 256)
        .Select(i => (byte)i)
        .ToArray();

    private static readonly ushort[] _TestTable16 = Enumerable
        .Range(0, 256)
        .Select(i => (ushort)i)
        .ToArray();

    private static readonly uint[] _TestTable32 = Enumerable
        .Range(0, 256)
        .Select(i => (uint)i)
        .ToArray();

    private static readonly ulong[] _TestTable64 = Enumerable
        .Range(0, 256)
        .Select(i => (ulong)i)
        .ToArray();

    private static ReadOnlySpan<byte> TestTableByte => _TestTable8;
    private static ReadOnlySpan<sbyte> TestTableSByte => MemoryMarshal.Cast<byte, sbyte>(_TestTable8);

    private static ReadOnlySpan<ushort> TestTableUInt16 => _TestTable16;
    private static ReadOnlySpan<short> TestTableInt16 => MemoryMarshal.Cast<ushort, short>(_TestTable16);

    private static ReadOnlySpan<uint> TestTableUInt32 => _TestTable32;
    private static ReadOnlySpan<int> TestTableInt32 => MemoryMarshal.Cast<uint, int>(_TestTable32);
    private static ReadOnlySpan<float> TestTableSingle => MemoryMarshal.Cast<uint, float>(_TestTable32);

    private static ReadOnlySpan<ulong> TestTableUInt64 => _TestTable64;
    private static ReadOnlySpan<long> TestTableInt64 => MemoryMarshal.Cast<ulong, long>(_TestTable64);
    private static ReadOnlySpan<double> TestTableDouble => MemoryMarshal.Cast<ulong, double>(_TestTable64);

    private static ReadOnlySpan<nuint> TestTableUIntPtr => IntPtr.Size == 8
        ? MemoryMarshal.Cast<ulong, nuint>(_TestTable64)
        : MemoryMarshal.Cast<uint, nuint>(_TestTable32);
    private static ReadOnlySpan<nint> TestTableIntPtr => MemoryMarshal.Cast<nuint, nint>(TestTableUIntPtr);

    public static TheoryData<int[]> GatherTestCases() => [
            [],
            [0],
            [0, 1],
            [0, 1, 2],
            [0, 1, 2, 3],
            [0, 2, 4, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64],
            [0, 2, 4, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32, 34, 36, 38, 40, 42, 44, 46, 48, 50, 52, 54, 56, 58, 60, 62, 64, 66, 68, 70],
        ];

    private static void TestCore<T>(ReadOnlySpan<T> table, ReadOnlySpan<int> indices)
        where T : unmanaged
    {
        var expected = new T[indices.Length];
        var actual = new T[indices.Length];
        Vectorization.Emulated.Gather(table, indices, expected);
        Vectorization.SIMD.Gather(table, indices, actual);
        Assert.Equal(expected, actual);
    }

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherByte(int[] indices) => TestCore(TestTableByte, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherSByte(int[] indices) => TestCore(TestTableSByte, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherUInt16(int[] indices) => TestCore(TestTableUInt16, indices);
    
    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherInt16(int[] indices) => TestCore(TestTableInt16, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherUInt32(int[] indices) => TestCore(TestTableUInt32, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherInt32(int[] indices) => TestCore(TestTableInt32, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherSingle(int[] indices) => TestCore(TestTableSingle, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherUInt64(int[] indices) => TestCore(TestTableUInt64, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherInt64(int[] indices) => TestCore(TestTableInt64, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherDouble(int[] indices) => TestCore(TestTableDouble, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherUIntPtr(int[] indices) => TestCore(TestTableUIntPtr, indices);

    [Theory, MemberData(nameof(GatherTestCases))]
    public void GatherIntPtr(int[] indices) => TestCore(TestTableIntPtr, indices);
}
