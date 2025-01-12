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
    /// Operates unaryplus for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void UnaryPlus<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        UnaryPlusCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="UnaryPlus{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
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
    /// Operates unaryminus for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void UnaryMinus<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        UnaryMinusCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="UnaryMinus{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
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
    /// Operates complement for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Complement<T>(ReadOnlySpan<T> x, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(x.Length == ans.Length, "`x` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref x, ans);
        ComplementCore(x, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Complement{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="x"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
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

#region abs

partial class Vectorization
{
    /// <summary>
    /// Operates abs for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Abs<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AbsCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Abs{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AbsCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Abs(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AbsCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Abs(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Abs(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region acos

partial class Vectorization
{
    /// <summary>
    /// Operates acos for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Acos<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AcosCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Acos{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AcosCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Acos(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AcosCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Acos(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Acos(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region acosh

partial class Vectorization
{
    /// <summary>
    /// Operates acosh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Acosh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AcoshCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Acosh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AcoshCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Acosh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AcoshCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Acosh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Acosh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region asin

partial class Vectorization
{
    /// <summary>
    /// Operates asin for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Asin<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AsinCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Asin{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AsinCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Asin(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AsinCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Asin(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Asin(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region asinh

partial class Vectorization
{
    /// <summary>
    /// Operates asinh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Asinh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AsinhCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Asinh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AsinhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Asinh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AsinhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Asinh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Asinh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region atan

partial class Vectorization
{
    /// <summary>
    /// Operates atan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Atan<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AtanCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Atan{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AtanCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Atan(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AtanCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Atan(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Atan(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region atanh

partial class Vectorization
{
    /// <summary>
    /// Operates atanh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Atanh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        AtanhCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Atanh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void AtanhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Atanh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void AtanhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Atanh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Atanh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region cbrt

partial class Vectorization
{
    /// <summary>
    /// Operates cbrt for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Cbrt<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        CbrtCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Cbrt{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void CbrtCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Cbrt(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void CbrtCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Cbrt(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Cbrt(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region ceiling

partial class Vectorization
{
    /// <summary>
    /// Operates ceiling for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Ceiling<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        CeilingCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Ceiling{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void CeilingCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Ceiling(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void CeilingCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Ceiling(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Ceiling(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region cos

partial class Vectorization
{
    /// <summary>
    /// Operates cos for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Cos<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        CosCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Cos{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void CosCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Cos(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void CosCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Cos(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Cos(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region cosh

partial class Vectorization
{
    /// <summary>
    /// Operates cosh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Cosh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        CoshCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Cosh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void CoshCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Cosh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void CoshCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Cosh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Cosh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region exp

partial class Vectorization
{
    /// <summary>
    /// Operates exp for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Exp<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        ExpCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Exp{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void ExpCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Exp(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void ExpCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Exp(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Exp(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region floor

partial class Vectorization
{
    /// <summary>
    /// Operates floor for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Floor<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        FloorCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Floor{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void FloorCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Floor(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void FloorCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Floor(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Floor(Unsafe.As<T, Vector<T>>(ref vx[0])); 
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
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Log<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        LogCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Log{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void LogCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void LogCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Log(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Log(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region log10

partial class Vectorization
{
    /// <summary>
    /// Operates log10 for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Log10<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        Log10Core(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Log10{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void Log10Core<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log10(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void Log10Core<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Log10(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Log10(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region log2

partial class Vectorization
{
    /// <summary>
    /// Operates log2 for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Log2<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        Log2Core(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Log2{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void Log2Core<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Log2(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void Log2Core<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Log2(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Log2(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region round

partial class Vectorization
{
    /// <summary>
    /// Operates round for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Round<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        RoundCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Round{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void RoundCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Round(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void RoundCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Round(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Round(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region sign

partial class Vectorization
{
    /// <summary>
    /// Operates sign for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Sign<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        SignCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Sign{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void SignCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Sign(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void SignCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Sign(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Sign(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region sin

partial class Vectorization
{
    /// <summary>
    /// Operates sin for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Sin<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        SinCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Sin{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void SinCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Sin(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void SinCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Sin(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Sin(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region sinh

partial class Vectorization
{
    /// <summary>
    /// Operates sinh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Sinh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        SinhCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Sinh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void SinhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Sinh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void SinhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Sinh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Sinh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region sqrt

partial class Vectorization
{
    /// <summary>
    /// Operates sqrt for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Sqrt<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        SqrtCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Sqrt{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void SqrtCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Sqrt(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void SqrtCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Sqrt(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Sqrt(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region tan

partial class Vectorization
{
    /// <summary>
    /// Operates tan for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Tan<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        TanCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Tan{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void TanCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Tan(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void TanCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Tan(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Tan(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region tanh

partial class Vectorization
{
    /// <summary>
    /// Operates tanh for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Tanh<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        TanhCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Tanh{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void TanhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Tanh(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void TanhCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Tanh(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Tanh(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region truncate

partial class Vectorization
{
    /// <summary>
    /// Operates truncate for each corresponding elements of operands.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="ArgumentException"> <paramref name="ans" /> and all span operands must have same length. </exception>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    public void Truncate<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        Guard.ValidArgument(d.Length == ans.Length, "`d` and `ans` must have same length.");
        using var safeXBuffer = EnsureSourceSafe(ref d, ans);
        TruncateCore(d, ans);
    }

    /// <summary>
    /// Core implementation for <see cref="Truncate{T}(ReadOnlySpan{T}, Span{T})" />.
    /// For this method it is ensured that all parameters have same length.
    /// </summary>
    /// <typeparam name="T"> The type of elements. </typeparam>
    /// <param name="d"> The 1st operand elements. </param>
    /// <param name="ans"> The destination of answer. </param>
    /// <exception cref="NotSupportedException"> The operation for <typeparamref name="T"/> is not supported. </exception>
    /// <remarks>
    /// For this method it is ensured followings:
    /// <list type="bullet">
    /// <item> all parameters have same length </item>
    /// <item> there are no offseted overlap between input and output (it means writing to the same index is safe) </item>
    /// </list>
    /// </remarks>
    protected internal virtual void TruncateCore<T>(ReadOnlySpan<T> d, Span<T> ans)
        where T : unmanaged
    {
        for (var i = 0; i < ans.Length; ++i)
            ans[i] = ScalarOp.Truncate(d[i]);
    }
}

partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override sealed void TruncateCore<T>(ReadOnlySpan<T> d, Span<T> ans)
    {
        var vectorX = MemoryMarshal.Cast<T, Vector<T>>(d);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = VectorMath.Truncate(vectorX[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx = (stackalloc T[Vector<T>.Count]);
            var vans = (stackalloc T[Vector<T>.Count]);
            d.Slice(vectorLength).CopyTo(vx);
            Unsafe.As<T, Vector<T>>(ref vans[0]) = VectorMath.Truncate(Unsafe.As<T, Vector<T>>(ref vx[0])); 
            vans.Slice(0, ans.Length - vectorLength).CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

