#nullable enable
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;

#region add
partial class Vectorization
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
    /// Core implementation for <see cref="Add{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Add(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Add{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.Add(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Add{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Add(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="Subtract{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Subtract(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Subtract{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.Subtract(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Subtract{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Subtract(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="Multiply{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Multiply(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Multiply{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.Multiply(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Multiply{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Multiply(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="Divide{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Divide(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Divide{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.Divide(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Divide{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Divide(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="BitwiseAnd{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseAnd(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseAnd(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseAnd(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="BitwiseOr{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseOr(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseOr{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseOr(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseOr{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseOr(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="BitwiseXor{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseXor(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseXor{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseXor(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseXor{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.BitwiseXor(x[i], y[i]);
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="Equals{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Equals(x, y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.Equals(x[i], y)
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.Equals(x[i], y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="LessThan{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.LessThan(x, y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.LessThan(x[i], y)
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.LessThan(x[i], y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="LessThanOrEquals{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.LessThanOrEquals(x, y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEquals{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.LessThanOrEquals(x[i], y)
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEquals{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.LessThanOrEquals(x[i], y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="GreaterThan{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThan(x, y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThan(x[i], y)
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThan(x[i], y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }

}

partial class SimdVectorization
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
partial class Vectorization
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
    /// Core implementation for <see cref="GreaterThanOrEquals{T}(T, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThanOrEquals(x, y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEquals{T}(ReadOnlySpan{T}, T, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThanOrEquals(x[i], y)
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEquals{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
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
            ans[i] = ScalarOp.GreaterThanOrEquals(x[i], y[i])
                ? ScalarOp.True<T>()
                : ScalarOp.False<T>();
    }

}

partial class SimdVectorization
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

