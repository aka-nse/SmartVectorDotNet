// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY T4. DO NOT CHANGE IT. CHANGE THE .tt FILE INSTEAD.
// </auto-generated>
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace SmartVectorDotNet;
using H = InternalHelpers;


// VectorMath.cs
partial class VectorMath
{
}

// VectorMath._Abs.cs
partial class VectorMath
{
}

// VectorMath._Acos.cs
partial class VectorMath
{
}

// VectorMath._Acosh.cs
partial class VectorMath
{
}

// VectorMath._Asin.cs
partial class VectorMath
{
}

// VectorMath._Asinh.cs
partial class VectorMath
{
}

// VectorMath._Atan.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Atan<T>(Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(Atan(x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(Atan(x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Atan2.cs
partial class VectorMath
{
}

// VectorMath._Atanh.cs
partial class VectorMath
{
}

// VectorMath._Cbrt.cs
partial class VectorMath
{
}

// VectorMath._Ceiling.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Ceiling<T>(Vector<T> d)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var d_ = ref H.Reinterpret<T, double>(d);
            return H.Reinterpret<double, T>(Ceiling(d_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var d_ = ref H.Reinterpret<T, float>(d);
            return H.Reinterpret<float, T>(Ceiling(d_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Cos.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Vector<T> CosBounded<T>(Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(CosBounded(x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(CosBounded(x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Cosh.cs
partial class VectorMath
{
}

// VectorMath._CountLeadingZeros.cs
partial class VectorMath
{
}

// VectorMath._CountPopulation.cs
partial class VectorMath
{
}

// VectorMath._CountTrailingZeros.cs
partial class VectorMath
{
}

// VectorMath._DivideLike.cs
partial class VectorMath
{
}

// VectorMath._Exp.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Vector<T> ExpCore<T>(Vector<T> d)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var d_ = ref H.Reinterpret<T, double>(d);
            return H.Reinterpret<double, T>(ExpCore(d_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var d_ = ref H.Reinterpret<T, float>(d);
            return H.Reinterpret<float, T>(ExpCore(d_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Floor.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Floor<T>(Vector<T> d)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var d_ = ref H.Reinterpret<T, double>(d);
            return H.Reinterpret<double, T>(Floor(d_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var d_ = ref H.Reinterpret<T, float>(d);
            return H.Reinterpret<float, T>(Floor(d_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._FusedMultiplyAdd.cs
partial class VectorMath
{
}

// VectorMath._IEEE754Specific.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Scale<T>(Vector<T> n, Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var n_ = ref H.Reinterpret<T, double>(n);
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(Scale(n_, x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var n_ = ref H.Reinterpret<T, float>(n);
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(Scale(n_, x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Log.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Log<T>(Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(Log(x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(Log(x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Log10.cs
partial class VectorMath
{
}

// VectorMath._Log2.cs
partial class VectorMath
{
}

// VectorMath._LogA.cs
partial class VectorMath
{
}

// VectorMath._MinMax.cs
partial class VectorMath
{
}

// VectorMath._ModuloByConst.cs
partial class VectorMath
{
}

// VectorMath._Permute2.cs
partial class VectorMath
{
}

// VectorMath._Permute4.cs
partial class VectorMath
{
}

// VectorMath._Permute_Internal.cs
partial class VectorMath
{
}

// VectorMath._Pow.cs
partial class VectorMath
{
}

// VectorMath._Round.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Round<T>(Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(Round(x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(Round(x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Sign.cs
partial class VectorMath
{
}

// VectorMath._Sin.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static partial Vector<T> SinBounded<T>(Vector<T> x)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var x_ = ref H.Reinterpret<T, double>(x);
            return H.Reinterpret<double, T>(SinBounded(x_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var x_ = ref H.Reinterpret<T, float>(x);
            return H.Reinterpret<float, T>(SinBounded(x_));
        }
        throw new NotSupportedException();
    }

}

// VectorMath._Sinh.cs
partial class VectorMath
{
}

// VectorMath._Sqrt.cs
partial class VectorMath
{
}

// VectorMath._Tan.cs
partial class VectorMath
{
}

// VectorMath._Tanh.cs
partial class VectorMath
{
}

// VectorMath._Truncate.cs
partial class VectorMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial Vector<T> Truncate<T>(Vector<T> d)
        where T : unmanaged
    {
        if(typeof(T) == typeof(double))
        {
            ref readonly var d_ = ref H.Reinterpret<T, double>(d);
            return H.Reinterpret<double, T>(Truncate(d_));
        }
        if(typeof(T) == typeof(float))
        {
            ref readonly var d_ = ref H.Reinterpret<T, float>(d);
            return H.Reinterpret<float, T>(Truncate(d_));
        }
        throw new NotSupportedException();
    }

}

