using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using SmartVectorDotNet.Internal;

namespace SmartVectorDotNet;

/// <summary>
/// Provides <see cref="Vectorization"/> implementation which parallelize other <see cref="Vectorization"/> into TPL.
/// </summary>
/// <param name="entity"> Specifies internal vectorization instance which handles for each tasks. </param>
/// <param name="parallelCount"></param>
[ParallelVectorization]
public unsafe sealed partial class ParallelVectorization(Vectorization entity, int parallelCount) : Vectorization
{
    /// <summary>
    /// Gets the internal vectorization instance which handles for each tasks.
    /// </summary>
    public Vectorization Entity { get; } = entity;

    /// <summary>
    /// Gets the number of parallel tasks to run.
    /// </summary>
    public int ParallelCount { get; } = parallelCount;

    /// <inheritdoc />
    internal void ComplementCore_<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        fixed (T* xPtr = x)
        fixed (T* ansPtr = ans)
        {
            var parallelCount = Math.Min(ParallelCount, ans.Length);
            var xPinned = new PinnedBuffer<T>(xPtr, x.Length);
            var ansPinned = new PinnedBuffer<T>(ansPtr, ans.Length);
            var tasks = new Task[ParallelCount];
            for(var i = 0; i < ParallelCount; ++i)
            {
                tasks[i] = Task.Factory.StartNew(static state =>
                {
                    var (@this, i, xPinned, ansPinned) = ((ParallelVectorization, int, PinnedBuffer<T>, PinnedBuffer<T>))state!;
                    var start = i * xPinned.Length / @this.ParallelCount;
                    var end = (i + 1) * xPinned.Length / @this.ParallelCount;
                    var xSpan = xPinned.AsSpan()[start..end];
                    var ansSpan = ansPinned.AsSpan()[start..end];
                    @this.Entity.ComplementCore(xSpan, ansSpan);
                }, (this, i, xPinned, ansPinned));
            }
            Task.WaitAll(tasks);
        }
    }

    private sealed class PinnedBuffer<T>(T* pointer, int length)
        where T : unmanaged
    {
        private readonly T* _pointer = pointer;
        public readonly int Length = length;

        public Span<T> AsSpan() => new(_pointer, Length);
    }
}