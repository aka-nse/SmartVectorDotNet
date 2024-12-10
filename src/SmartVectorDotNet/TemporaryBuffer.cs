using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;

/// <summary>
/// Provides managed temporary buffer.
/// </summary>
public readonly ref struct TemporaryBuffer<T>
    where T : unmanaged
{
    private readonly ArrayPool<T> _pool;
    private readonly T[] _buffer;
    private readonly int _size;

    /// <summary>
    /// Gets size of the rent resource.
    /// </summary>
    public int Size => _size;

    /// <summary>
    /// Gets span of the rent resource.
    /// </summary>
    public Span<T> Span => _buffer.AsSpan(0, _size);

    /// <summary>
    /// Gets memory of the rent resource.
    /// </summary>
    public Memory<T> Memory => _buffer.AsMemory(0, _size);

    /// <summary>
    /// Creates a new instance op <see cref="TemporaryBuffer{T}"/>
    /// </summary>
    /// <param name="size"></param>
    /// <param name="arrayPool"></param>
    public TemporaryBuffer(int size, ArrayPool<T>? arrayPool = null)
    {
        _pool = arrayPool ?? ArrayPool<T>.Shared;
        _buffer = _pool.Rent(size);
        _size = size;
    }

    /// <summary>
    /// Releases the resource.
    /// </summary>
    public void Dispose()
    {
        if(_pool is null || _buffer is null)
        {
            return;
        }
        _pool.Return(_buffer);
    }
}
