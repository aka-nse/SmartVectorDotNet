#nullable enable
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;

#region add
partial class VectorOperation
{
    /// <summary>
    /// Operates add for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Add<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        AddCore(x, y, ans);
    }

    /// <summary>
    /// Operates add for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Add<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        AddCore(x, y, ans);
    }

    /// <summary>
    /// Operates add for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Add<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        AddCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="Add" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void AddCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Add(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Add" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void AddCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Add(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Add" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void AddCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Add(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void AddCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX + vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX + Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void AddCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] + vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) + vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void AddCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] + vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) + Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region subtract
partial class VectorOperation
{
    /// <summary>
    /// Operates subtract for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Subtract<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        SubtractCore(x, y, ans);
    }

    /// <summary>
    /// Operates subtract for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Subtract<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        SubtractCore(x, y, ans);
    }

    /// <summary>
    /// Operates subtract for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Subtract<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        SubtractCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="Subtract" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void SubtractCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Subtract(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Subtract" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void SubtractCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Subtract(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Subtract" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void SubtractCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Subtract(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void SubtractCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX - vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX - Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void SubtractCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] - vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) - vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void SubtractCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] - vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) - Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region multiply
partial class VectorOperation
{
    /// <summary>
    /// Operates multiply for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Multiply<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        MultiplyCore(x, y, ans);
    }

    /// <summary>
    /// Operates multiply for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Multiply<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        MultiplyCore(x, y, ans);
    }

    /// <summary>
    /// Operates multiply for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Multiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        MultiplyCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="Multiply" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void MultiplyCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Multiply(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Multiply" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void MultiplyCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Multiply(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Multiply" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void MultiplyCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Multiply(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void MultiplyCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX * vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX * Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void MultiplyCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] * vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) * vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void MultiplyCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] * vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) * Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region divide
partial class VectorOperation
{
    /// <summary>
    /// Operates divide for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Divide<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        DivideCore(x, y, ans);
    }

    /// <summary>
    /// Operates divide for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Divide<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        DivideCore(x, y, ans);
    }

    /// <summary>
    /// Operates divide for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Divide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        DivideCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="Divide" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void DivideCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Divide(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Divide" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void DivideCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Divide(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Divide" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void DivideCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Divide(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void DivideCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX / vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX / Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void DivideCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] / vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) / vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void DivideCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] / vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) / Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region bitwiseand
partial class VectorOperation
{
    /// <summary>
    /// Operates bitwiseand for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseAnd<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseAndCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwiseand for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseAnd<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        BitwiseAndCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwiseand for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseAnd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseAndCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseAndCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseAnd(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseAndCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseAnd(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseAndCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseAnd(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void BitwiseAndCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX & vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX & Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseAndCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] & vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) & vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseAndCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] & vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) & Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region bitwiseor
partial class VectorOperation
{
    /// <summary>
    /// Operates bitwiseor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseOr<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseOrCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwiseor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseOr<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        BitwiseOrCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwiseor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseOr<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseOrCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="BitwiseOr" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseOrCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseOr(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseOr" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseOrCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseOr(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseOr" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseOrCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseOr(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void BitwiseOrCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX | vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX | Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseOrCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] | vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) | vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseOrCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] | vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) | Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region bitwisexor
partial class VectorOperation
{
    /// <summary>
    /// Operates bitwisexor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseXor<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseXorCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwisexor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseXor<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        BitwiseXorCore(x, y, ans);
    }

    /// <summary>
    /// Operates bitwisexor for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void BitwiseXor<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        BitwiseXorCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="BitwiseXor" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseXorCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseXor(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseXor" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseXorCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseXor(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseXor" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void BitwiseXorCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.BitwiseXor(x[i], y[i]);
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void BitwiseXorCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX ^ vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = vectorX ^ Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseXorCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] ^ vectorY;
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) ^ vectorY; 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void BitwiseXorCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = vectorX[i] ^ vectorY[i];
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Unsafe.As<T, Vector<T>>(ref vx[0]) ^ Unsafe.As<T, Vector<T>>(ref vy[0]); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region equals
partial class VectorOperation
{
    /// <summary>
    /// Operates equals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Equals<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        EqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates equals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Equals<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        EqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates equals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Equals<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        EqualsCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="Equals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void EqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Equals(x, y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void EqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Equals(x[i], y)
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void EqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.Equals(x[i], y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void EqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.Equals(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.Equals(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void EqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.Equals(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.Equals(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void EqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.Equals(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.Equals(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region lessthan
partial class VectorOperation
{
    /// <summary>
    /// Operates lessthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThan<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        LessThanCore(x, y, ans);
    }

    /// <summary>
    /// Operates lessthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThan<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        LessThanCore(x, y, ans);
    }

    /// <summary>
    /// Operates lessthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        LessThanCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="LessThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThan(x, y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThan(x[i], y)
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThan(x[i], y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void LessThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThan(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThan(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void LessThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThan(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThan(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void LessThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThan(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThan(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region lessthanorequals
partial class VectorOperation
{
    /// <summary>
    /// Operates lessthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThanOrEquals<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        LessThanOrEqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates lessthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThanOrEquals<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        LessThanOrEqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates lessthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void LessThanOrEquals<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        LessThanOrEqualsCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="LessThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanOrEqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThanOrEquals(x, y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanOrEqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThanOrEquals(x[i], y)
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void LessThanOrEqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.LessThanOrEquals(x[i], y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void LessThanOrEqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThanOrEqual(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThanOrEqual(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void LessThanOrEqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThanOrEqual(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThanOrEqual(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void LessThanOrEqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.LessThanOrEqual(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.LessThanOrEqual(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region greaterthan
partial class VectorOperation
{
    /// <summary>
    /// Operates greaterthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThan<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        GreaterThanCore(x, y, ans);
    }

    /// <summary>
    /// Operates greaterthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThan<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        GreaterThanCore(x, y, ans);
    }

    /// <summary>
    /// Operates greaterthan for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        GreaterThanCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="GreaterThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThan(x, y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThan(x[i], y)
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThan(x[i], y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void GreaterThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThan(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThan(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void GreaterThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThan(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThan(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void GreaterThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThan(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThan(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region greaterthanorequals
partial class VectorOperation
{
    /// <summary>
    /// Operates greaterthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="y"/> and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThanOrEquals<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        GreaterThanOrEqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates greaterthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThanOrEquals<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        GreaterThanOrEqualsCore(x, y, ans);
    }

    /// <summary>
    /// Operates greaterthanorequals for each corresponding elements of <paramref name="x"/> and <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="x"/>, <paramref name="y"/>, and <paramref name="ans"/> must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void GreaterThanOrEquals<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        if(x.Length != ans.Length)
            throw new ArgumentException("`x` and `ans` must have same length.");
        if(y.Length != ans.Length)
            throw new ArgumentException("`y` and `ans` must have same length.");
        GreaterThanOrEqualsCore(x, y, ans);
    }


    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanOrEqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThanOrEquals(x, y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanOrEqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThanOrEquals(x[i], y)
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEquals" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The left hand side operand elements. </param>
    /// <param name="y"> The right hand side operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    protected internal virtual void GreaterThanOrEqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ValueOperation.GreaterThanOrEquals(x[i], y[i])
                ? ValueOperation.True<T>()
                : ValueOperation.False<T>();
    }

}

partial class SimdVectorOperation
{
    /// <inheritdoc />
    protected internal override sealed void GreaterThanOrEqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThanOrEqual(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThanOrEqual(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void GreaterThanOrEqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThanOrEqual(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThanOrEqual(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    protected internal override sealed void GreaterThanOrEqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = Vector.GreaterThanOrEqual(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = Vector.GreaterThanOrEqual(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

