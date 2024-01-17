#nullable enable
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;

#region unaryplus
partial class Vectorization
{
    /// <summary>
    /// Operates unaryplus for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    public void UnaryPlus<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        UnaryPlusCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="UnaryPlus" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    protected internal virtual void UnaryPlusCore<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.UnaryPlus(x[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void UnaryPlusCore<T>(ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] =  vectorX[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) =  Unsafe.As<T, Vector<T>>(ref vx[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion


#region unaryminus
partial class Vectorization
{
    /// <summary>
    /// Operates unaryminus for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    public void UnaryMinus<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        UnaryMinusCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="UnaryMinus" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    protected internal virtual void UnaryMinusCore<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.UnaryMinus(x[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void UnaryMinusCore<T>(ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = -vectorX[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = -Unsafe.As<T, Vector<T>>(ref vx[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion


#region complement
partial class Vectorization
{
    /// <summary>
    /// Operates complement for each corresponding elements of <paramref name="x"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="x"/> and <paramref name="ans"/> must have same length.
    /// </exception>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    public void Complement<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        ComplementCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Complement" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException">
    /// The operation for <typeparamref name="T"/> is not supported.
    /// </exception>
    protected internal virtual void ComplementCore<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Complement(x[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void ComplementCore<T>(ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = ~vectorX[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = ~Unsafe.As<T, Vector<T>>(ref vx[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion


