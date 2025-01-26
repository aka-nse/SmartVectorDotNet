using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;

file class Gather_
{
    public const string IndexRangeError = "All elements of index must be in range of table index.";
}

partial class VectorOp
{
#if NETCOREAPP3_0_OR_GREATER

    internal static unsafe Vector128<byte> GatherUnsafe(byte* ptr, Vector128<int> index0, Vector128<int> index1, Vector128<int> index2, Vector128<int> index3)
    {
        var z0 = Avx2.GatherVector128((uint*)ptr, index0, 1);
        var z1 = Avx2.GatherVector128((uint*)ptr, index1, 1);
        var z2 = Avx2.GatherVector128((uint*)ptr, index2, 1);
        var z3 = Avx2.GatherVector128((uint*)ptr, index3, 1);
        return Vector128.Narrow(Vector128.Narrow(z0, z1), Vector128.Narrow(z2, z3));
    }

    internal static unsafe Vector128<ushort> GatherUnsafe(ushort* ptr, Vector128<int> indexLo, Vector128<int> indexHi)
    {
        var hi = Avx2.GatherVector128((uint*)ptr, indexHi, 2);
        var lo = Avx2.GatherVector128((uint*)ptr, indexLo, 2);
        return Vector128.Narrow(lo, hi);
    }

    internal static unsafe Vector128<uint> GatherUnsafe(uint* ptr, Vector128<int> index)
    {
        return Avx2.GatherVector128(ptr, index, 4);
    }

    internal static unsafe Vector128<ulong> GatherUnsafe(ulong* ptr, Vector128<long> index)
    {
        return Avx2.GatherVector128(ptr, index, 8);
    }

    internal static unsafe Vector256<byte> GatherUnsafe(byte* ptr, Vector256<int> index0, Vector256<int> index1, Vector256<int> index2, Vector256<int> index3)
    {
        var z0 = Avx2.GatherVector256((uint*)ptr, index0, 1);
        var z1 = Avx2.GatherVector256((uint*)ptr, index1, 1);
        var z2 = Avx2.GatherVector256((uint*)ptr, index2, 1);
        var z3 = Avx2.GatherVector256((uint*)ptr, index3, 1);
        return Vector256.Narrow(Vector256.Narrow(z0, z1), Vector256.Narrow(z2, z3));
    }

    internal static unsafe Vector256<ushort> GatherUnsafe(ushort* ptr, Vector256<int> indexLo, Vector256<int> indexHi)
    {
        var hi = Avx2.GatherVector256((uint*)ptr, indexHi, 2);
        var lo = Avx2.GatherVector256((uint*)ptr, indexLo, 2);
        return Vector256.Narrow(lo, hi);
    }

    internal static unsafe Vector256<uint> GatherUnsafe(uint* ptr, Vector256<int> index)
    {
        return Avx2.GatherVector256(ptr, index, 4);
    }

    internal static unsafe Vector256<ulong> GatherUnsafe(ulong* ptr, Vector256<long> index)
    {
        return Avx2.GatherVector256(ptr, index, 8);
    }

#endif

    internal static unsafe Vector<T> GatherUnsafe<T>(T* ptr, ReadOnlySpan<int> index)
        where T : unmanaged
    {
        var retval = (stackalloc T[Vector<T>.Count]);
        for (var i = 0; i < Vector<T>.Count; ++i)
        {
            retval[i] = ptr[index[i]];
        }
        return H.CreateVector(retval);
    }

    internal static unsafe Vector<T> GatherUnsafe<T>(T* ptr, ReadOnlySpan<long> index)
        where T : unmanaged
    {
        var retval = (stackalloc T[Vector<T>.Count]);
        for (var i = 0; i < Vector<T>.Count; ++i)
        {
            retval[i] = ptr[index[i]];
        }
        return H.CreateVector(retval);
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="index0"></param>
    /// <param name="index1"></param>
    /// <param name="index2"></param>
    /// <param name="index3"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<byte> GatherUnsafe(byte* ptr, Vector<int> index0, Vector<int> index1, Vector<int> index2, Vector<int> index3)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Avx2.IsSupported)
        {
            if(Vector<byte>.Count == Vector256<byte>.Count)
            {
                return H.AsVector(
                    GatherUnsafe(
                        ptr,
                        H.AsVector256(index0),
                        H.AsVector256(index1),
                        H.AsVector256(index2),
                        H.AsVector256(index3))
                    );
            }
            else if (Vector<byte>.Count == Vector128<byte>.Count)
            {
                return H.AsVector(
                    GatherUnsafe(
                        ptr,
                        H.AsVector128(index0),
                        H.AsVector128(index1),
                        H.AsVector128(index2),
                        H.AsVector128(index3))
                    );
            }
        }
#endif
        return fallback(ptr, index0, index1, index2, index3);

        static Vector<byte> fallback(byte* ptr, Vector<int> index0, Vector<int> index1, Vector<int> index2, Vector<int> index3)
        {
            var index = (stackalloc int[Vector<byte>.Count]);
            var vindex = MemoryMarshal.Cast<int, Vector<int>>(index);
            vindex[0] = index0;
            vindex[1] = index1;
            vindex[2] = index2;
            vindex[3] = index3;
            return GatherUnsafe(ptr, index);
        }
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="indexLo"></param>
    /// <param name="indexHi"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<ushort> GatherUnsafe(ushort* ptr, Vector<int> indexLo, Vector<int> indexHi)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Avx2.IsSupported)
        {
            if (Vector<ushort>.Count == Vector256<ushort>.Count)
            {
                return H.AsVector(
                    GatherUnsafe(
                        ptr,
                        H.AsVector256(indexLo),
                        H.AsVector256(indexHi))
                    );
            }
            else if (Vector<ushort>.Count == Vector128<ushort>.Count)
            {
                return H.AsVector(
                    GatherUnsafe(
                        ptr,
                        H.AsVector128(indexLo),
                        H.AsVector128(indexHi))
                    );
            }
        }
#endif
        return fallback(ptr, indexLo, indexHi);

        static Vector<ushort> fallback(ushort* ptr, Vector<int> indexLo, Vector<int> indexHi)
        {
            var index = (stackalloc int[Vector<ushort>.Count]);
            var vindex = MemoryMarshal.Cast<int, Vector<int>>(index);
            vindex[0] = indexLo;
            vindex[1] = indexHi;
            return GatherUnsafe(ptr, index);
        }
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<uint> GatherUnsafe(uint* ptr, Vector<int> index)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Avx2.IsSupported)
        {
            if (Vector<uint>.Count == Vector256<uint>.Count)
            {
                return H.AsVector(GatherUnsafe(ptr,  H.AsVector256(index)));
            }
            else if (Vector<uint>.Count == Vector128<uint>.Count)
            {
                return H.AsVector(GatherUnsafe(ptr, H.AsVector128(index)));
            }
        }
#endif
        return fallback(ptr, index);

        static Vector<uint> fallback(uint* ptr, Vector<int> index)
        {
            var index_ = (stackalloc int[Vector<uint>.Count]);
            var vindex = MemoryMarshal.Cast<int, Vector<int>>(index_);
            vindex[0] = index;
            return GatherUnsafe(ptr, index_);
        }
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<ulong> GatherUnsafe(ulong* ptr, Vector<long> index)
    {
#if NETCOREAPP3_0_OR_GREATER
        if (Avx2.IsSupported)
        {
            if (Vector<ulong>.Count == Vector256<ulong>.Count)
            {
                return H.AsVector(GatherUnsafe(ptr, H.AsVector256(index)));
            }
            else if (Vector<ulong>.Count == Vector128<ulong>.Count)
            {
                return H.AsVector(GatherUnsafe(ptr, H.AsVector128(index)));
            }
        }
#endif
        return fallback(ptr, index);

        static Vector<ulong> fallback(ulong* ptr, Vector<long> index)
        {
            var index_ = (stackalloc long[Vector<ulong>.Count]);
            var vindex = MemoryMarshal.Cast<long, Vector<long>>(index_);
            vindex[0] = index;
            return GatherUnsafe(ptr, index_);
        }
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<nuint> GatherUnsafe(nuint* ptr, Vector<nint> index)
    {
        if (IntPtr.Size == 8)
        {
            return H.Reinterpret<ulong, nuint>(GatherUnsafe((ulong*)ptr, H.Reinterpret<nint, long>(index)));
        }
        else if (IntPtr.Size == 4)
        {
            return H.Reinterpret<uint, nuint>(GatherUnsafe((uint*)ptr, H.Reinterpret<nint, int>(index)));
        }
        throw new NotSupportedException();
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<nint> GatherUnsafe(nint* ptr, Vector<nint> index)
    {
        if(IntPtr.Size == 8)
        {
            return H.Reinterpret<ulong, nint>(GatherUnsafe((ulong*)ptr, H.Reinterpret<nint, long>(index)));
        }
        else if(IntPtr.Size == 4)
        {
            return H.Reinterpret<uint, nint>(GatherUnsafe((uint*)ptr, H.Reinterpret<nint, int>(index)));
        }
        throw new NotSupportedException();
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<nuint> Gather(ReadOnlySpan<nuint> table, Vector<nint> index)
    {
        var length = new Vector<nint>(table.Length);
        Guard.ValidArgument(LessThanOrEqualAll(default, index), Gather_.IndexRangeError);
        Guard.ValidArgument(LessThanAll(index, length), Gather_.IndexRangeError);
        fixed (nuint* ptr = table)
        {
            return GatherUnsafe(ptr, index);
        }
    }

    /// <summary>
    /// Looks up the table.
    /// </summary>
    /// <param name="table"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <remarks> Note that this method will not validate buffer index. </remarks>
    public static unsafe Vector<nint> Gather(ReadOnlySpan<nint> table, Vector<nint> index)
    {
        var length = new Vector<nint>(table.Length);
        Guard.ValidArgument(LessThanOrEqualAll(default, index), Gather_.IndexRangeError);
        Guard.ValidArgument(LessThanAll(index, length), Gather_.IndexRangeError);
        fixed (nint* ptr = table)
        {
            return GatherUnsafe(ptr, index);
        }
    }
}