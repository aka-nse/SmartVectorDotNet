#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;


file class Permute_
{
    private static readonly byte[] _MaskBaseUInt8_Permute2;
    public static ReadOnlySpan<byte> MaskBaseUInt8_Permute2 => _MaskBaseUInt8_Permute2;
    private static readonly byte[] _MaskBaseUInt8_Permute4;
    public static ReadOnlySpan<byte> MaskBaseUInt8_Permute4 => _MaskBaseUInt8_Permute4;

    static Permute_()
    {
        _MaskBaseUInt8_Permute2 = new byte[Vector256<byte>.Count];
        for (var i = 0; i < Vector256<byte>.Count; i += 2)
        {
            _MaskBaseUInt8_Permute2[i + 0] = (byte)i;
            _MaskBaseUInt8_Permute2[i + 1] = (byte)i;
        }

        _MaskBaseUInt8_Permute4 = new byte[Vector256<byte>.Count];
        for(var i = 0; i < Vector256<byte>.Count; i += 4)
        {
            _MaskBaseUInt8_Permute4[i + 0] = (byte)i;
            _MaskBaseUInt8_Permute4[i + 1] = (byte)i;
            _MaskBaseUInt8_Permute4[i + 2] = (byte)i;
            _MaskBaseUInt8_Permute4[i + 3] = (byte)i;
        }
    }
}


partial class VectorMath
{
    internal static Vector128<T> Permute2<T>(in Vector128<T> v, byte m0, byte m1)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
#pragma warning disable format
            sizeof(byte  ) => H.Reinterpret<byte  , T>(PermuteX(H.Reinterpret<T, byte  >(v), m0, m1, m0, m1, Permute_.MaskBaseUInt8_Permute2)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Permute4(H.Reinterpret<T, ushort>(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Permute4(H.Reinterpret<T, uint  >(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Permute4(H.Reinterpret<T, ulong >(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
#pragma warning restore format
            _ => throw new NotSupportedException(),
        };

    internal static Vector128<T> Permute4<T>(in Vector128<T> v, byte m0, byte m1, byte m2, byte m3)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
#pragma warning disable format
            sizeof(byte  ) => H.Reinterpret<byte  , T>(PermuteX(H.Reinterpret<T, byte  >(v), m0, m1, m2, m3, Permute_.MaskBaseUInt8_Permute4)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Permute4(H.Reinterpret<T, ushort>(v), m0, m1, m2, m3)),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Permute4(H.Reinterpret<T, uint  >(v), m0, m1, m2, m3)),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Permute4(H.Reinterpret<T, ulong >(v), m0, m1, m2, m3)),
#pragma warning restore format
            _ => throw new NotSupportedException(),
        };

    internal static Vector128<byte> PermuteX(in Vector128<byte> v, byte m0, byte m1, byte m2, byte m3, ReadOnlySpan<byte> maskBase)
    {
        var maskOffsets = (stackalloc byte[4] { (byte)(m0 & 0b11), (byte)(m1 & 0b11), (byte)(m2 & 0b11), (byte)(m3 & 0b11), });
        if(Sse3.IsSupported)
        {
            var maskBytes = (stackalloc byte[Vector128<byte>.Count]);
            MemoryMarshal.Cast<byte, uint>(maskBytes).Fill(MemoryMarshal.Cast<byte, uint>(maskOffsets)[0]);
            var mask = Sse2.Add(
                H.CreateVector128(maskBase),
                H.CreateVector128(maskBytes));
            return Ssse3.Shuffle(v, mask);
        }
        else
        {
            var retval = (stackalloc byte[Vector128<byte>.Count]);
            for (var i = 0; i < Vector128<byte>.Count; ++i)
            {
                retval[i] = v.GetElement(maskBase[i] + maskOffsets[i & 0b11]);
            }
            return H.CreateVector128(retval);
        }
    }

    internal static Vector128<ushort> Permute4(in Vector128<ushort> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Sse2.IsSupported)
        {
            var mask = ((m3 & 0b11u) << 6) | ((m2 & 0b11u) << 4) | ((m1 & 0b11u) << 2) | (m0 & 0b11u);
            var lo = (stackalloc ushort[Vector128<ushort>.Count]);
            var hi = (stackalloc ushort[Vector128<ushort>.Count]);
            MemoryMarshal.Cast<ushort, Vector128<ushort>>(lo)[0] = Sse2.ShuffleLow(v, (byte)mask);
            MemoryMarshal.Cast<ushort, Vector128<ushort>>(hi)[0] = Sse2.ShuffleHigh(v, (byte)mask);
            lo.Slice(0, 4).CopyTo(hi.Slice(0, 4));
            return MemoryMarshal.Cast<ushort, Vector128<ushort>>(hi)[0];
        }
        else
        {
            var retval = (stackalloc ushort[Vector128<ushort>.Count]);
            for (var i = 0; i < Vector128<ushort>.Count; i += 4)
            {
                retval[i + 0] = v.GetElement(i + m0);
                retval[i + 1] = v.GetElement(i + m1);
                retval[i + 2] = v.GetElement(i + m2);
                retval[i + 3] = v.GetElement(i + m3);
            }
            return H.CreateVector128(retval);
        }
    }

    internal static Vector128<uint> Permute4(in Vector128<uint> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Sse2.IsSupported)
        {
            var mask = ((m3 & 0b11u) << 6) | ((m2 & 0b11u) << 4) | ((m1 & 0b11u) << 2) | (m0 & 0b11u);
            return Sse2.Shuffle(v, (byte)mask);
        }
        else
        {
            var retval = (stackalloc uint[Vector128<uint>.Count]);
            for (var i = 0; i < Vector128<uint>.Count; i += 4)
            {
                retval[i + 0] = v.GetElement(i + m0);
                retval[i + 1] = v.GetElement(i + m1);
                retval[i + 2] = v.GetElement(i + m2);
                retval[i + 3] = v.GetElement(i + m3);
            }
            return H.CreateVector128(retval);
        }
    }

    internal static Vector128<ulong> Permute4(in Vector128<ulong> v, byte m0, byte m1, byte m2, byte m3)
    {
        throw new NotSupportedException();
    }
    



    internal static Vector256<T> Permute2<T>(in Vector256<T> v, byte m0, byte m1)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
#pragma warning disable format
            sizeof(byte  ) => H.Reinterpret<byte  , T>(PermuteX(H.Reinterpret<T, byte  >(v), m0, m1, m0, m1, Permute_.MaskBaseUInt8_Permute2)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Permute4(H.Reinterpret<T, ushort>(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Permute4(H.Reinterpret<T, uint  >(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Permute4(H.Reinterpret<T, ulong >(v), m0, m1, (byte)(0b10 | m0), (byte)(0b10 | m1))),
#pragma warning restore format
            _ => throw new NotSupportedException(),
        };

    internal static Vector256<T> Permute4<T>(in Vector256<T> v, byte m0, byte m1, byte m2, byte m3)
        where T : unmanaged
        => Unsafe.SizeOf<T>() switch
        {
#pragma warning disable format
            sizeof(byte  ) => H.Reinterpret<byte  , T>(PermuteX(H.Reinterpret<T, byte  >(v), m0, m1, m2, m3, Permute_.MaskBaseUInt8_Permute4)),
            sizeof(ushort) => H.Reinterpret<ushort, T>(Permute4(H.Reinterpret<T, ushort>(v), m0, m1, m2, m3)),
            sizeof(uint  ) => H.Reinterpret<uint  , T>(Permute4(H.Reinterpret<T, uint  >(v), m0, m1, m2, m3)),
            sizeof(ulong ) => H.Reinterpret<ulong , T>(Permute4(H.Reinterpret<T, ulong >(v), m0, m1, m2, m3)),
#pragma warning restore format
            _ => throw new NotSupportedException(),
        };

    internal static Vector256<byte> PermuteX(in Vector256<byte> v, byte m0, byte m1, byte m2, byte m3, ReadOnlySpan<byte> maskBase)
    {
        var maskOffsets = (stackalloc byte[4] { (byte)(m0 & 0b11), (byte)(m1 & 0b11), (byte)(m2 & 0b11), (byte)(m3 & 0b11), });
        if (Avx2.IsSupported)
        {
            var maskBytes = (stackalloc byte[Vector256<byte>.Count]);
            MemoryMarshal.Cast<byte, uint>(maskBytes).Fill(MemoryMarshal.Cast<byte, uint>(maskOffsets)[0]);
            var mask = Avx2.Add(
                H.CreateVector256(maskBase),
                H.CreateVector256(maskBytes));
            return Avx2.Shuffle(v, mask);
        }
        else
        {
            var retval = (stackalloc byte[Vector256<byte>.Count]);
            for (var i = 0; i < Vector256<byte>.Count; ++i)
            {
                retval[i] = v.GetElement(maskBase[i] + maskOffsets[i & 0b11]);
            }
            return H.CreateVector256(retval);
        }
    }

    internal static Vector256<ushort> Permute4(in Vector256<ushort> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            var mask = ((m3 & 0b11u) << 6) | ((m2 & 0b11u) << 4) | ((m1 & 0b11u) << 2) | (m0 & 0b11u);
            var lo = (stackalloc ushort[Vector256<ushort>.Count]);
            var hi = (stackalloc ushort[Vector256<ushort>.Count]);
            MemoryMarshal.Cast<ushort, Vector256<ushort>>(lo)[0] = Avx2.ShuffleLow(v, (byte)mask);
            MemoryMarshal.Cast<ushort, Vector256<ushort>>(hi)[0] = Avx2.ShuffleHigh(v, (byte)mask);
            lo.Slice(0, 4).CopyTo(hi.Slice(0, 4));
            lo.Slice(8, 4).CopyTo(hi.Slice(8, 4));
            return MemoryMarshal.Cast<ushort, Vector256<ushort>>(hi)[0];
        }
        else
        {
            var retval = (stackalloc ushort[Vector256<ushort>.Count]);
            for (var i = 0; i < Vector256<ushort>.Count; i += 4)
            {
                retval[i + 0] = v.GetElement(i + m0);
                retval[i + 1] = v.GetElement(i + m1);
                retval[i + 2] = v.GetElement(i + m2);
                retval[i + 3] = v.GetElement(i + m3);
            }
            return H.CreateVector256(retval);
        }
    }

    internal static Vector256<uint> Permute4(in Vector256<uint> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            var mask = ((m3 & 0b11u) << 6) | ((m2 & 0b11u) << 4) | ((m1 & 0b11u) << 2) | (m0 & 0b11u);
            return Avx2.Shuffle(v, (byte)mask);
        }
        else
        {
            var retval = (stackalloc uint[Vector256<uint>.Count]);
            for (var i = 0; i < Vector256<uint>.Count; i += 4)
            {
                retval[i + 0] = v.GetElement(i + m0);
                retval[i + 1] = v.GetElement(i + m1);
                retval[i + 2] = v.GetElement(i + m2);
                retval[i + 3] = v.GetElement(i + m3);
            }
            return H.CreateVector256(retval);
        }
    }

    internal static Vector256<ulong> Permute4(in Vector256<ulong> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            var mask = ((m3 & 0b11u) << 6) | ((m2 & 0b11u) << 4) | ((m1 & 0b11u) << 2) | (m0 & 0b11u);
            return Avx2.Permute4x64(v, (byte)mask);
        }
        else
        {
            var retval = (stackalloc ulong[Vector256<ulong>.Count]);
            for (var i = 0; i < Vector256<ulong>.Count; i += 4)
            {
                retval[i + 0] = v.GetElement(i + m0);
                retval[i + 1] = v.GetElement(i + m1);
                retval[i + 2] = v.GetElement(i + m2);
                retval[i + 3] = v.GetElement(i + m3);
            }
            return H.CreateVector256(retval);
        }
    }
}
#endif