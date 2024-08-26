#nullable enable
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SmartVectorDotNet;

#region IVectorFormula1

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula1<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <returns></returns>
    public T Calculate(T x1);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula1<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula1<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula1<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula2

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula2<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula2<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula2<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula2<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula3

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula3<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula3<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula3<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula3<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula4

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula4<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula4<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula4<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula4<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula5

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula5<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula5<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula5<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula5<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula6

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula6<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5, T x6);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5, Vector<T> x6);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula6<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula6<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula6<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i], vectorX6[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vx6 = (stackalloc Vector<T>[1]); x6.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx6));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0], vx6[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula7

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula7<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5, Vector<T> x6, Vector<T> x7);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula7<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula7<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula7<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
        var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i], vectorX6[i], vectorX7[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vx6 = (stackalloc Vector<T>[1]); x6.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx6));
            var vx7 = (stackalloc Vector<T>[1]); x7.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx7));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0], vx6[0], vx7[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula8

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula8<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5, Vector<T> x6, Vector<T> x7, Vector<T> x8);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula8<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula8<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula8<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i], x8[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
        var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
        var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i], vectorX6[i], vectorX7[i], vectorX8[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vx6 = (stackalloc Vector<T>[1]); x6.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx6));
            var vx7 = (stackalloc Vector<T>[1]); x7.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx7));
            var vx8 = (stackalloc Vector<T>[1]); x8.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx8));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0], vx6[0], vx7[0], vx8[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula9

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula9<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8, T x9);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5, Vector<T> x6, Vector<T> x7, Vector<T> x8, Vector<T> x9);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula9<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        if(x9.Length != ans.Length) throw new ArgumentException("`x9` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, x9, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula9<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        if(x9.Length != ans.Length) throw new ArgumentException("`x9` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, x9, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula9<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i], x8[i], x9[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
        var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
        var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
        var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i], vectorX6[i], vectorX7[i], vectorX8[i], vectorX9[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vx6 = (stackalloc Vector<T>[1]); x6.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx6));
            var vx7 = (stackalloc Vector<T>[1]); x7.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx7));
            var vx8 = (stackalloc Vector<T>[1]); x8.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx8));
            var vx9 = (stackalloc Vector<T>[1]); x9.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx9));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0], vx6[0], vx7[0], vx8[0], vx9[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

#region IVectorFormula10

/// <summary>
/// Defines a mathematical calculation which can be vectorized.
/// This interface shall be implemented on struct type.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IVectorFormula10<T>
    where T : unmanaged
{
    /// <summary>
    /// Calculates scalar operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="x10"></param>
    /// <returns></returns>
    public T Calculate(T x1, T x2, T x3, T x4, T x5, T x6, T x7, T x8, T x9, T x10);

    /// <summary>
    /// Calculates vector operation.
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="x10"></param>
    /// <returns></returns>
    public Vector<T> Calculate(Vector<T> x1, Vector<T> x2, Vector<T> x3, Vector<T> x4, Vector<T> x5, Vector<T> x6, Vector<T> x7, Vector<T> x8, Vector<T> x9, Vector<T> x10);
}

partial class Vectorization
{
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="x10"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula10<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        if(x9.Length != ans.Length) throw new ArgumentException("`x9` and `ans` must have same length.");
        if(x10.Length != ans.Length) throw new ArgumentException("`x10` and `ans` must have same length.");
        var formula = default(TFormula);
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, ans);
    }
    
    /// <summary>
    /// Calculates the custom vectorizable operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="x10"></param>
    /// <param name="ans"></param>
    public void Calculate<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula10<T>
    {
        if(x1.Length != ans.Length) throw new ArgumentException("`x1` and `ans` must have same length.");
        if(x2.Length != ans.Length) throw new ArgumentException("`x2` and `ans` must have same length.");
        if(x3.Length != ans.Length) throw new ArgumentException("`x3` and `ans` must have same length.");
        if(x4.Length != ans.Length) throw new ArgumentException("`x4` and `ans` must have same length.");
        if(x5.Length != ans.Length) throw new ArgumentException("`x5` and `ans` must have same length.");
        if(x6.Length != ans.Length) throw new ArgumentException("`x6` and `ans` must have same length.");
        if(x7.Length != ans.Length) throw new ArgumentException("`x7` and `ans` must have same length.");
        if(x8.Length != ans.Length) throw new ArgumentException("`x8` and `ans` must have same length.");
        if(x9.Length != ans.Length) throw new ArgumentException("`x9` and `ans` must have same length.");
        if(x10.Length != ans.Length) throw new ArgumentException("`x10` and `ans` must have same length.");
        CalculateCore<T, TFormula>(ref formula, x1, x2, x3, x4, x5, x6, x7, x8, x9, x10, ans);
    }
    
    /// <summary>
    /// Core implementation for
    /// <see cref="Calculate{T, TFormula}(ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />
    /// and
    /// <see cref="Calculate{T, TFormula}(ref TFormula, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, ReadOnlySpan{T}, Span{T})" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFormula"></typeparam>
    /// <param name="formula"></param>
    /// <param name="x1"></param>
    /// <param name="x2"></param>
    /// <param name="x3"></param>
    /// <param name="x4"></param>
    /// <param name="x5"></param>
    /// <param name="x6"></param>
    /// <param name="x7"></param>
    /// <param name="x8"></param>
    /// <param name="x9"></param>
    /// <param name="x10"></param>
    /// <param name="ans"></param>
    protected internal virtual void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> ans)
        where T : unmanaged
        where TFormula : struct, IVectorFormula10<T>
    {
        for(var i = 0; i < ans.Length; ++i)
            ans[i] = formula.Calculate(x1[i], x2[i], x3[i], x4[i], x5[i], x6[i], x7[i], x8[i], x9[i], x10[i]);
    }
}


partial class SimdVectorization
{
    /// <inheritdoc />
    protected internal override void CalculateCore<T, TFormula>(ref TFormula formula, ReadOnlySpan<T> x1, ReadOnlySpan<T> x2, ReadOnlySpan<T> x3, ReadOnlySpan<T> x4, ReadOnlySpan<T> x5, ReadOnlySpan<T> x6, ReadOnlySpan<T> x7, ReadOnlySpan<T> x8, ReadOnlySpan<T> x9, ReadOnlySpan<T> x10, Span<T> ans)
    {
        var vectorX1 = MemoryMarshal.Cast<T, Vector<T>>(x1);
        var vectorX2 = MemoryMarshal.Cast<T, Vector<T>>(x2);
        var vectorX3 = MemoryMarshal.Cast<T, Vector<T>>(x3);
        var vectorX4 = MemoryMarshal.Cast<T, Vector<T>>(x4);
        var vectorX5 = MemoryMarshal.Cast<T, Vector<T>>(x5);
        var vectorX6 = MemoryMarshal.Cast<T, Vector<T>>(x6);
        var vectorX7 = MemoryMarshal.Cast<T, Vector<T>>(x7);
        var vectorX8 = MemoryMarshal.Cast<T, Vector<T>>(x8);
        var vectorX9 = MemoryMarshal.Cast<T, Vector<T>>(x9);
        var vectorX10 = MemoryMarshal.Cast<T, Vector<T>>(x10);
        var vectorAns = MemoryMarshal.Cast<T, Vector<T>>(ans);
        var vectorLength = vectorAns.Length * Vector<T>.Count;
        for(var i = 0; i < vectorAns.Length; ++i)
        {
            vectorAns[i] = formula.Calculate(vectorX1[i], vectorX2[i], vectorX3[i], vectorX4[i], vectorX5[i], vectorX6[i], vectorX7[i], vectorX8[i], vectorX9[i], vectorX10[i]);
        }
        if(vectorLength < ans.Length)
        {
            var vx1 = (stackalloc Vector<T>[1]); x1.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx1));
            var vx2 = (stackalloc Vector<T>[1]); x2.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx2));
            var vx3 = (stackalloc Vector<T>[1]); x3.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx3));
            var vx4 = (stackalloc Vector<T>[1]); x4.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx4));
            var vx5 = (stackalloc Vector<T>[1]); x5.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx5));
            var vx6 = (stackalloc Vector<T>[1]); x6.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx6));
            var vx7 = (stackalloc Vector<T>[1]); x7.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx7));
            var vx8 = (stackalloc Vector<T>[1]); x8.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx8));
            var vx9 = (stackalloc Vector<T>[1]); x9.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx9));
            var vx10 = (stackalloc Vector<T>[1]); x10.Slice(vectorLength).CopyTo(MemoryMarshal.Cast<Vector<T>, T>(vx10));
            var vans = (stackalloc Vector<T>[1]);
            vans[0] = formula.Calculate(vx1[0], vx2[0], vx3[0], vx4[0], vx5[0], vx6[0], vx7[0], vx8[0], vx9[0], vx10[0]);
            MemoryMarshal
                .Cast<Vector<T>, T>(vans)
                .Slice(0, ans.Length - vectorLength)
                .CopyTo(ans.Slice(vectorLength));
        }
    }
}

#endregion

