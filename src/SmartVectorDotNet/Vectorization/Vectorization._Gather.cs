using System.Runtime.InteropServices;

namespace SmartVectorDotNet;
using H = SmartVectorDotNet.InternalHelpers;
using OP = SmartVectorDotNet.VectorOp;

file class Gather_
{
    public const string IndexRangeError = "All elements in `indices` must be in range of `table` index.";
}

partial class Vectorization
{
    /// <summary> Looks up the table. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <param name="indices"></param>
    /// <param name="dst"></param>
    /// <exception cref="ArgumentException">
    /// <para> <paramref name="indices"/> and <paramref name="dst" /> operands must have same length. </para>
    /// <para> or </para>
    /// <para> All elements in <paramref name="indices"/> must be in range of <paramref name="table"/> index. </para>
    /// </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Gather<T>(ReadOnlySpan<T> table, ReadOnlySpan<int> indices, Span<T> dst)
        where T : unmanaged
    {
        Guard.ValidArgument(indices.Length == dst.Length, "`indices` and `dst` must have same length.");
        using var safeTableBuffer = EnsureSourceSafe(ref table, dst);
        unsafe
        {
            GatherCore(table, indices, dst);
        }
    }

    /// <summary> Looks up the table. </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="table"></param>
    /// <param name="indices"></param>
    /// <param name="dst"></param>
    /// <exception cref="ArgumentException"> All elements in <paramref name="indices"/> must be in range of <paramref name="table"/> index. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [UnsafeApi]
    protected virtual unsafe void GatherCore<T>(ReadOnlySpan<T> table, ReadOnlySpan<int> indices, Span<T> dst)
        where T : unmanaged
    {
        for(var i = 0; i < dst.Length; ++i)
        {
            Guard.ValidArgument((uint)indices[i] < (uint)table.Length, Gather_.IndexRangeError);
            dst[i] = table[indices[i]];
        }
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected override unsafe void GatherCore<T>(ReadOnlySpan<T> table, ReadOnlySpan<int> indices, Span<T> dst)
    {
        switch(H.SizeOf<T>())
        {
        case sizeof(byte):
            GatherCore1(MemoryMarshal.Cast<T, byte>(table), indices, MemoryMarshal.Cast<T, byte>(dst));
            return;
        case sizeof(ushort):
            GatherCore2(MemoryMarshal.Cast<T, ushort>(table), indices, MemoryMarshal.Cast<T, ushort>(dst));
            return;
        case sizeof(uint):
            GatherCore4(MemoryMarshal.Cast<T, uint>(table), indices, MemoryMarshal.Cast<T, uint>(dst));
            return;
        case sizeof(ulong):
            GatherCore8(MemoryMarshal.Cast<T, ulong>(table), indices, MemoryMarshal.Cast<T, ulong>(dst));
            return;
        default:
            base.GatherCore(table, indices, dst);
            return;
        }
    }

    private static unsafe void GatherCore1(ReadOnlySpan<byte> table, ReadOnlySpan<int> indices, Span<byte> dst)
    {
        var indexLimit = new Vector<uint>((uint)table.Length);
        var vdst = MemoryMarshal.Cast<byte, Vector<byte>>(dst);
        var vindices = MemoryMarshal.Cast<int, Vector<int>>(indices)[..(vdst.Length * 4)];
        fixed (byte* ptr = table)
        {
            for (var i = 0; i < vdst.Length; ++i)
            {
                var j = i * 4;
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 0]), indexLimit), Gather_.IndexRangeError);
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 1]), indexLimit), Gather_.IndexRangeError);
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 2]), indexLimit), Gather_.IndexRangeError);
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 3]), indexLimit), Gather_.IndexRangeError);
                vdst[i] = OP.GatherUnsafe(ptr, vindices[j + 0], vindices[j + 1], vindices[j + 2], vindices[j + 3]);
            }
            for (var i = vdst.Length * Vector<byte>.Count; i < dst.Length; ++i)
            {
                Guard.ValidArgument((uint)indices[i] < (uint)table.Length, Gather_.IndexRangeError);
                dst[i] = table[indices[i]];
            }
        }
    }

    private static unsafe void GatherCore2(ReadOnlySpan<ushort> table, ReadOnlySpan<int> indices, Span<ushort> dst)
    {
        var indexLimit = new Vector<uint>((uint)table.Length);
        var vdst = MemoryMarshal.Cast<ushort, Vector<ushort>>(dst);
        var vindices = MemoryMarshal.Cast<int, Vector<int>>(indices)[..(vdst.Length * 2)];
        fixed (ushort* ptr = table)
        {
            for (var i = 0; i < vdst.Length; ++i)
            {
                var j = i * 2;
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 0]), indexLimit), Gather_.IndexRangeError);
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[j + 1]), indexLimit), Gather_.IndexRangeError);
                vdst[i] = OP.GatherUnsafe(ptr, vindices[j + 0], vindices[j + 1]);
            }
            for (var i = vdst.Length * Vector<ushort>.Count; i < dst.Length; ++i)
            {
                Guard.ValidArgument((uint)indices[i] < (uint)table.Length, Gather_.IndexRangeError);
                dst[i] = table[indices[i]];
            }
        }
    }

    private static unsafe void GatherCore4(ReadOnlySpan<uint> table, ReadOnlySpan<int> indices, Span<uint> dst)
    {
        var indexLimit = new Vector<uint>((uint)table.Length);
        var vindices = MemoryMarshal.Cast<int, Vector<int>>(indices);
        var vdst = MemoryMarshal.Cast<uint, Vector<uint>>(dst);
        fixed (uint* ptr = table)
        {
            for (var i = 0; i < vdst.Length; ++i)
            {
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<int, uint>(vindices[i]), indexLimit), Gather_.IndexRangeError);
                vdst[i] = OP.GatherUnsafe(ptr, vindices[i]);
            }
            for (var i = vdst.Length * Vector<uint>.Count; i < dst.Length; ++i)
            {
                Guard.ValidArgument((uint)indices[i] < (uint)table.Length, Gather_.IndexRangeError);
                dst[i] = table[indices[i]];
            }
        }
    }

    private static unsafe void GatherCore8(ReadOnlySpan<ulong> table, ReadOnlySpan<int> indices, Span<ulong> dst)
    {
        var indexLimit = new Vector<ulong>((ulong)table.Length);
        var vindices = MemoryMarshal.Cast<int, Vector<int>>(indices);
        var vdst = MemoryMarshal.Cast<ulong, Vector<ulong>>(dst)[..(vindices.Length * 2)];
        fixed (ulong* ptr = table)
        {
            for (var i = 0; i < vdst.Length; ++i)
            {
                OP.Widen(vindices[i / 2], out var lo, out var hi);
                var idx = (i & 1) == 0 ? lo : hi;
                Guard.ValidArgument(OP.LessThanAll(H.BitCastV<long, ulong>(idx), indexLimit), Gather_.IndexRangeError);
                vdst[i] = OP.GatherUnsafe(ptr, idx);
            }
            for (var i = vdst.Length * Vector<ulong>.Count; i < dst.Length; ++i)
            {
                Guard.ValidArgument((uint)indices[i] < (uint)table.Length, Gather_.IndexRangeError);
                dst[i] = table[indices[i]];
            }
        }
    }
}

