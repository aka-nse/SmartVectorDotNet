#nullable enable
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;

#region add
partial class Vectorization
{
    /// <summary>
    /// Operates add for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Add<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        AddCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates add for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Add<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        AddCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates add for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Add<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        AddCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Add{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates subtract for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Subtract<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        SubtractCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates subtract for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Subtract<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        SubtractCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates subtract for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Subtract<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        SubtractCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Subtract{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates multiply for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Multiply<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        MultiplyCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates multiply for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Multiply<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        MultiplyCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates multiply for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Multiply<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        MultiplyCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Multiply{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates divide for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Divide<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        DivideCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates divide for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Divide<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        DivideCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates divide for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Divide<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        DivideCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Divide{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates bitwiseand for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseAnd<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseAndCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwiseand for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseAnd<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        BitwiseAndCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwiseand for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseAnd<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseAndCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseAnd{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates bitwiseor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseOr<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseOrCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwiseor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseOr<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        BitwiseOrCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwiseor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseOr<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseOrCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseOr{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates bitwisexor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseXor<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseXorCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwisexor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseXor<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        BitwiseXorCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates bitwisexor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void BitwiseXor<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        BitwiseXorCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="BitwiseXor{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates equals for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Equals<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        EqualsCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates equals for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Equals<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        EqualsCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates equals for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Equals<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        EqualsCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void EqualsCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Equals(x, y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void EqualsCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Equals(x[i], y) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="Equals{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void EqualsCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Equals(x[i], y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    /// Operates lessthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThan<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        LessThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates lessthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThan<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        LessThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates lessthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        LessThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThan(x, y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThan(x[i], y) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThan{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThan(x[i], y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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

#region lessthanorequal
partial class Vectorization
{
    /// <summary>
    /// Operates lessthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThanOrEqual<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        LessThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates lessthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        LessThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates lessthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void LessThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        LessThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEqual{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanOrEqualCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThanOrEqual(x, y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEqual{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanOrEqualCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThanOrEqual(x[i], y) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="LessThanOrEqual{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LessThanOrEqualCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.LessThanOrEqual(x[i], y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void LessThanOrEqualCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
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
    [GeneratedCode("T4", null)]
    protected internal override sealed void LessThanOrEqualCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
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
    [GeneratedCode("T4", null)]
    protected internal override sealed void LessThanOrEqualCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
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
    /// Operates greaterthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThan<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        GreaterThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates greaterthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThan<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        GreaterThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates greaterthan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThan<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        GreaterThanCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThan(x, y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThan(x[i], y) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThan{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThan(x[i], y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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
    [GeneratedCode("T4", null)]
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

#region greaterthanorequal
partial class Vectorization
{
    /// <summary>
    /// Operates greaterthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThanOrEqual<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        GreaterThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates greaterthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        GreaterThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates greaterthanorequal for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void GreaterThanOrEqual<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        GreaterThanOrEqualCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEqual{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanOrEqualCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThanOrEqual(x, y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEqual{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanOrEqualCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThanOrEqual(x[i], y) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
    
    /// <summary>
    /// Core implementation for <see cref="GreaterThanOrEqual{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void GreaterThanOrEqualCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.GreaterThanOrEqual(x[i], y[i]) ? ScalarOp.Const<T>.TrueValue : ScalarOp.Const<T>.FalseValue;
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void GreaterThanOrEqualCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
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
    [GeneratedCode("T4", null)]
    protected internal override sealed void GreaterThanOrEqualCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
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
    [GeneratedCode("T4", null)]
    protected internal override sealed void GreaterThanOrEqualCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
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

#region addsaturate
partial class Vectorization
{
    /// <summary>
    /// Operates addsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void AddSaturate<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        AddSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates addsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void AddSaturate<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        AddSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates addsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void AddSaturate<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        AddSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="AddSaturate{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void AddSaturateCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.AddSaturate(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="AddSaturate{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void AddSaturateCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.AddSaturate(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="AddSaturate{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void AddSaturateCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.AddSaturate(x[i], y[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void AddSaturateCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.AddSaturate(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.AddSaturate(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void AddSaturateCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.AddSaturate(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.AddSaturate(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void AddSaturateCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.AddSaturate(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.AddSaturate(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region subtractsaturate
partial class Vectorization
{
    /// <summary>
    /// Operates subtractsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void SubtractSaturate<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        SubtractSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates subtractsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void SubtractSaturate<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        SubtractSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Operates subtractsaturate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void SubtractSaturate<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref y, ans);
        SubtractSaturateCore(x, y, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="SubtractSaturate{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void SubtractSaturateCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.SubtractSaturate(x, y[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="SubtractSaturate{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void SubtractSaturateCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.SubtractSaturate(x[i], y);
    }
    
    /// <summary>
    /// Core implementation for <see cref="SubtractSaturate{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="y"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void SubtractSaturateCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.SubtractSaturate(x[i], y[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void SubtractSaturateCore<T>(T x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.SubtractSaturate(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.SubtractSaturate(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void SubtractSaturateCore<T>(ReadOnlySpan<T> x, T y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.SubtractSaturate(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.SubtractSaturate(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void SubtractSaturateCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> y, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.SubtractSaturate(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            y.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.SubtractSaturate(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region atan2
partial class Vectorization
{
    /// <summary>
    /// Operates atan2 for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Atan2<T>(T y, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref x, ans);
        Atan2Core(y, x, ans);
    }
    
    /// <summary>
    /// Operates atan2 for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Atan2<T>(ReadOnlySpan<T> y, T x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref y, ans);
        Atan2Core(y, x, ans);
    }
    
    /// <summary>
    /// Operates atan2 for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Atan2<T>(ReadOnlySpan<T> y, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(y.Length == ans.Length, "`y` and `ans` must have same length.");
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref y, ans);
        using var safeYBuffer = EnsureSourceSafe(ref x, ans);
        Atan2Core(y, x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Atan2{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void Atan2Core<T>(T y, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Atan2(y, x[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Atan2{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void Atan2Core<T>(ReadOnlySpan<T> y, T x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Atan2(y[i], x);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Atan2{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="y"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void Atan2Core<T>(ReadOnlySpan<T> y, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Atan2(y[i], x[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void Atan2Core<T>(T y, ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = new Vector<T>(y);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Atan2(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Atan2(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void Atan2Core<T>(ReadOnlySpan<T> y, T x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorY = new Vector<T>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Atan2(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Atan2(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void Atan2Core<T>(ReadOnlySpan<T> y, ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(y);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Atan2(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            y.Slice(vectorLength).CopyTo(vx);
            x.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Atan2(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region log
partial class Vectorization
{
    /// <summary>
    /// Operates log for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Log<T>(T x, ReadOnlySpan<T> newBase, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(newBase.Length == ans.Length, "`newBase` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref newBase, ans);
        LogCore(x, newBase, ans);
    }
    
    /// <summary>
    /// Operates log for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Log<T>(ReadOnlySpan<T> x, T newBase, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        LogCore(x, newBase, ans);
    }
    
    /// <summary>
    /// Operates log for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Log<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> newBase, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        Guard.ValidArgument(newBase.Length == ans.Length, "`newBase` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        using var safeYBuffer = EnsureSourceSafe(ref newBase, ans);
        LogCore(x, newBase, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Log{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LogCore<T>(T x, ReadOnlySpan<T> newBase, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log(x, newBase[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Log{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LogCore<T>(ReadOnlySpan<T> x, T newBase, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log(x[i], newBase);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Log{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="newBase"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void LogCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> newBase, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log(x[i], newBase[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void LogCore<T>(T x, ReadOnlySpan<T> newBase, Span<T> ans)
    {
        var vectorX = new Vector<T>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(newBase);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Log(vectorX, vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            newBase.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Log(vectorX, Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void LogCore<T>(ReadOnlySpan<T> x, T newBase, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = new Vector<T>(newBase);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Log(vectorX[i], vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Log(Unsafe.As<T, Vector<T>>(ref vx[0]), vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void LogCore<T>(ReadOnlySpan<T> x, ReadOnlySpan<T> newBase, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(newBase);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Log(vectorX[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vx);
            newBase.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Log(Unsafe.As<T, Vector<T>>(ref vx[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

#region pow
partial class Vectorization
{
    /// <summary>
    /// Operates pow for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Pow<T>(T a, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeYBuffer = EnsureSourceSafe(ref x, ans);
        PowCore(a, x, ans);
    }
    
    /// <summary>
    /// Operates pow for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Pow<T>(ReadOnlySpan<T> a, T x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(a.Length == ans.Length, "`a` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref a, ans);
        PowCore(a, x, ans);
    }
    
    /// <summary>
    /// Operates pow for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    [GeneratedCode("T4", null)]
    public void Pow<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(a.Length == ans.Length, "`a` and `ans` must have same length.");
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref a, ans);
        using var safeYBuffer = EnsureSourceSafe(ref x, ans);
        PowCore(a, x, ans);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Pow{T}(T, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void PowCore<T>(T a, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Pow(x[i], x[i]);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Pow{T}(ReadOnlySpan{T}, T, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void PowCore<T>(ReadOnlySpan<T> a, T x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Pow(x, x);
    }
    
    /// <summary>
    /// Core implementation for <see cref="Pow{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="a"> The 1st operand elements. </param>
    /// <param name="x"> The 2nd operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    [GeneratedCode("T4", null)]
    protected internal virtual void PowCore<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Pow(x[i], x[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void PowCore<T>(T a, ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = new Vector<T>(a);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Pow(vectorY[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            x.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Pow(Unsafe.As<T, Vector<T>>(ref vy[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void PowCore<T>(ReadOnlySpan<T> a, T x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(a);
        var vectorY = new Vector<T>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Pow(vectorY, vectorY);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            a.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Pow(vectorY, vectorY); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }

    /// <inheritdoc />
    [GeneratedCode("T4", null)]
    protected internal override sealed void PowCore<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> x, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(a);
        var vectorY = MemoryMarshal.Cast<T, Vector<T>>(x);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorOp.Pow(vectorY[i], vectorY[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vy = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            a.Slice(vectorLength).CopyTo(vx);
            x.Slice(vectorLength).CopyTo(vy);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorOp.Pow(Unsafe.As<T, Vector<T>>(ref vy[0]), Unsafe.As<T, Vector<T>>(ref vy[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}
#endregion

