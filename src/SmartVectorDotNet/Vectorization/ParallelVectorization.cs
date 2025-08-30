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

    private sealed class PinnedBuffer<T>(T* pointer, int length)
        where T : unmanaged
    {
        private readonly T* _pointer = pointer;
        public readonly int Length = length;

        public Span<T> AsSpan() => new(_pointer, Length);
    }
}