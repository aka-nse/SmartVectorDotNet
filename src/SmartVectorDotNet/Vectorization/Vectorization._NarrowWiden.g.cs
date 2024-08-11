#nullable enable
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;

#region Narrow(in ushort, out byte)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<ushort> x, Span<byte> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<ushort> x, Span<byte> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (byte)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<ushort> x, Span<byte> ans)
    {
        var vectorX = MemoryMarshal.Cast<ushort, Vector<ushort>>(x);
        var vectorAns = MemoryMarshal.Cast<byte, Vector<byte>>(ans);
        var vectorLength = vectorAns.Length * Vector<byte>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<ushort>[2]);
            var vvans = (stackalloc Vector<byte>[1]);
            var vx = MemoryMarshal.Cast<Vector<ushort>, ushort>(vvx);
            var vans = MemoryMarshal.Cast<Vector<byte>, byte>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Narrow(in uint, out ushort)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<uint> x, Span<ushort> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<uint> x, Span<ushort> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (ushort)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<uint> x, Span<ushort> ans)
    {
        var vectorX = MemoryMarshal.Cast<uint, Vector<uint>>(x);
        var vectorAns = MemoryMarshal.Cast<ushort, Vector<ushort>>(ans);
        var vectorLength = vectorAns.Length * Vector<ushort>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<uint>[2]);
            var vvans = (stackalloc Vector<ushort>[1]);
            var vx = MemoryMarshal.Cast<Vector<uint>, uint>(vvx);
            var vans = MemoryMarshal.Cast<Vector<ushort>, ushort>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Narrow(in ulong, out uint)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<ulong> x, Span<uint> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<ulong> x, Span<uint> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (uint)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<ulong> x, Span<uint> ans)
    {
        var vectorX = MemoryMarshal.Cast<ulong, Vector<ulong>>(x);
        var vectorAns = MemoryMarshal.Cast<uint, Vector<uint>>(ans);
        var vectorLength = vectorAns.Length * Vector<uint>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<ulong>[2]);
            var vvans = (stackalloc Vector<uint>[1]);
            var vx = MemoryMarshal.Cast<Vector<ulong>, ulong>(vvx);
            var vans = MemoryMarshal.Cast<Vector<uint>, uint>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Narrow(in short, out sbyte)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<short> x, Span<sbyte> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<short> x, Span<sbyte> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (sbyte)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<short> x, Span<sbyte> ans)
    {
        var vectorX = MemoryMarshal.Cast<short, Vector<short>>(x);
        var vectorAns = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(ans);
        var vectorLength = vectorAns.Length * Vector<sbyte>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<short>[2]);
            var vvans = (stackalloc Vector<sbyte>[1]);
            var vx = MemoryMarshal.Cast<Vector<short>, short>(vvx);
            var vans = MemoryMarshal.Cast<Vector<sbyte>, sbyte>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Narrow(in int, out short)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<int> x, Span<short> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<int> x, Span<short> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (short)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<int> x, Span<short> ans)
    {
        var vectorX = MemoryMarshal.Cast<int, Vector<int>>(x);
        var vectorAns = MemoryMarshal.Cast<short, Vector<short>>(ans);
        var vectorLength = vectorAns.Length * Vector<short>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<int>[2]);
            var vvans = (stackalloc Vector<short>[1]);
            var vx = MemoryMarshal.Cast<Vector<int>, int>(vvx);
            var vans = MemoryMarshal.Cast<Vector<short>, short>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Narrow(in long, out int)
partial class Vectorization
{
    /// <summary>
    /// Operates narrow for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Narrow(ReadOnlySpan<long> x, Span<int> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        NarrowCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Narrow" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void NarrowCore(ReadOnlySpan<long> x, Span<int> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (int)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void NarrowCore(ReadOnlySpan<long> x, Span<int> ans)
    {
        var vectorX = MemoryMarshal.Cast<long, Vector<long>>(x);
        var vectorAns = MemoryMarshal.Cast<int, Vector<int>>(ans);
        var vectorLength = vectorAns.Length * Vector<int>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = OP.Narrow(vectorX[2 * i], vectorX[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<long>[2]);
            var vvans = (stackalloc Vector<int>[1]);
            var vx = MemoryMarshal.Cast<Vector<long>, long>(vvx);
            var vans = MemoryMarshal.Cast<Vector<int>, int>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            vvans[0] = OP.Narrow(vvx[0], vvx[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion


#region Widen(in byte, out ushort)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<byte> x, Span<ushort> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<byte> x, Span<ushort> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (ushort)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<byte> x, Span<ushort> ans)
    {
        var vectorX = MemoryMarshal.Cast<byte, Vector<byte>>(x);
        var vectorAns = MemoryMarshal.Cast<ushort, Vector<ushort>>(ans);
        var vectorLength = vectorX.Length * Vector<byte>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<byte>[1]);
            var vvans = (stackalloc Vector<ushort>[2]);
            var vx = MemoryMarshal.Cast<Vector<byte>, byte>(vvx);
            var vans = MemoryMarshal.Cast<Vector<ushort>, ushort>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Widen(in ushort, out uint)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<ushort> x, Span<uint> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<ushort> x, Span<uint> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (uint)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<ushort> x, Span<uint> ans)
    {
        var vectorX = MemoryMarshal.Cast<ushort, Vector<ushort>>(x);
        var vectorAns = MemoryMarshal.Cast<uint, Vector<uint>>(ans);
        var vectorLength = vectorX.Length * Vector<ushort>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<ushort>[1]);
            var vvans = (stackalloc Vector<uint>[2]);
            var vx = MemoryMarshal.Cast<Vector<ushort>, ushort>(vvx);
            var vans = MemoryMarshal.Cast<Vector<uint>, uint>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Widen(in uint, out ulong)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<uint> x, Span<ulong> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<uint> x, Span<ulong> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (ulong)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<uint> x, Span<ulong> ans)
    {
        var vectorX = MemoryMarshal.Cast<uint, Vector<uint>>(x);
        var vectorAns = MemoryMarshal.Cast<ulong, Vector<ulong>>(ans);
        var vectorLength = vectorX.Length * Vector<uint>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<uint>[1]);
            var vvans = (stackalloc Vector<ulong>[2]);
            var vx = MemoryMarshal.Cast<Vector<uint>, uint>(vvx);
            var vans = MemoryMarshal.Cast<Vector<ulong>, ulong>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Widen(in sbyte, out short)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<sbyte> x, Span<short> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<sbyte> x, Span<short> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (short)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<sbyte> x, Span<short> ans)
    {
        var vectorX = MemoryMarshal.Cast<sbyte, Vector<sbyte>>(x);
        var vectorAns = MemoryMarshal.Cast<short, Vector<short>>(ans);
        var vectorLength = vectorX.Length * Vector<sbyte>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<sbyte>[1]);
            var vvans = (stackalloc Vector<short>[2]);
            var vx = MemoryMarshal.Cast<Vector<sbyte>, sbyte>(vvx);
            var vans = MemoryMarshal.Cast<Vector<short>, short>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Widen(in short, out int)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<short> x, Span<int> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<short> x, Span<int> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (int)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<short> x, Span<int> ans)
    {
        var vectorX = MemoryMarshal.Cast<short, Vector<short>>(x);
        var vectorAns = MemoryMarshal.Cast<int, Vector<int>>(ans);
        var vectorLength = vectorX.Length * Vector<short>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<short>[1]);
            var vvans = (stackalloc Vector<int>[2]);
            var vx = MemoryMarshal.Cast<Vector<short>, short>(vvx);
            var vans = MemoryMarshal.Cast<Vector<int>, int>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region Widen(in int, out long)
partial class Vectorization
{
    /// <summary>
    /// Operates widen for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    public void Widen(ReadOnlySpan<int> x, Span<long> ans)
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        WidenCore(x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Widen" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void WidenCore(ReadOnlySpan<int> x, Span<long> ans)
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = (long)x[i];
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void WidenCore(ReadOnlySpan<int> x, Span<long> ans)
    {
        var vectorX = MemoryMarshal.Cast<int, Vector<int>>(x);
        var vectorAns = MemoryMarshal.Cast<long, Vector<long>>(ans);
        var vectorLength = vectorX.Length * Vector<int>.Count;
        for(var i = 0; i < vectorX.Length; ++i)
        {
            OP.Widen(vectorX[i], out vectorAns[2 * i], out vectorAns[2 * i + 1]);
        }
        if(vectorLength < ans.Length)
        {
            var vvx = (stackalloc Vector<int>[1]);
            var vvans = (stackalloc Vector<long>[2]);
            var vx = MemoryMarshal.Cast<Vector<int>, int>(vvx);
            var vans = MemoryMarshal.Cast<Vector<long>, long>(vvans);
            x.Slice(vectorLength).CopyTo(vx);
            OP.Widen(vvx[0], out vvans[0], out vvans[1]);
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion



