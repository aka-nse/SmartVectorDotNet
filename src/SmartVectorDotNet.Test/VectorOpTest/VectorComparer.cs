using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartVectorDotNet;

internal class VectorComparer<T> : IEqualityComparer<Vector<T>>
    where T : unmanaged
{
    public static VectorComparer<T> Instance { get; } = new();

    public bool Equals(Vector<T> x, Vector<T> y)
    {
        var x_ = (stackalloc Vector<T>[1] { x });
        var y_ = (stackalloc Vector<T>[1] { y });
        var xx = (stackalloc byte[Unsafe.SizeOf<T>() * Vector<T>.Count]);
        var yy = (stackalloc byte[Unsafe.SizeOf<T>() * Vector<T>.Count]);
        MemoryMarshal.Cast<Vector<T>, byte>(x_).CopyTo(xx);
        MemoryMarshal.Cast<Vector<T>, byte>(y_).CopyTo(yy);
        return xx.SequenceEqual(yy);
    }

    public int GetHashCode([DisallowNull] Vector<T> obj)
    {
        var x = 0u;
        for(var i = 0; i < Vector<T>.Count; ++i)
        {
            x ^= (uint)obj[i].GetHashCode();
        }
        return (int)x;
    }
}
