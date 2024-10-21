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

    public static readonly Vector128<byte> Permute4Mask128 = H.CreateVector128(stackalloc byte[16]
    {
        0, 1, 0, 1, 0, 1, 0, 1,
        8, 9, 8, 9, 8, 9, 8, 9,
    });

    public static readonly Vector256<byte> Permute4Mask256 = H.CreateVector256(stackalloc byte[32]
    {
        0, 1, 0, 1, 0, 1, 0, 1,
        8, 9, 8, 9, 8, 9, 8, 9,
        0, 1, 0, 1, 0, 1, 0, 1,
        8, 9, 8, 9, 8, 9, 8, 9,
    });

    static Permute_()
    {
        _MaskBaseUInt8_Permute2 = new byte[Vector256<byte>.Count];
        for (var i = 0; i < Vector256<byte>.Count; i += 2)
        {
            _MaskBaseUInt8_Permute2[i + 0] = (byte)i;
            _MaskBaseUInt8_Permute2[i + 1] = (byte)i;
        }

        _MaskBaseUInt8_Permute4 = new byte[Vector256<byte>.Count];
        for (var i = 0; i < Vector256<byte>.Count; i += 4)
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
    internal static Vector128<T> Permute2<T>(Vector128<T> v, byte m0, byte m1)
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

    internal static Vector128<T> Permute4<T>(Vector128<T> v, byte m0, byte m1, byte m2, byte m3)
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

    internal static Vector128<byte> PermuteX(Vector128<byte> v, byte m0, byte m1, byte m2, byte m3, ReadOnlySpan<byte> maskBase)
    {
        var maskOffsets = (stackalloc byte[4] { (byte)(m0 & 0b11), (byte)(m1 & 0b11), (byte)(m2 & 0b11), (byte)(m3 & 0b11), });
        if (Sse3.IsSupported)
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

    internal static Vector128<ushort> Permute4(Vector128<ushort> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Sse2.IsSupported)
        {
            ref readonly var vv = ref H.Reinterpret<ushort, byte>(in v);
            m0 <<= 1;
            m1 <<= 1;
            m2 <<= 1;
            m3 <<= 1;
            var mask = H.CreateVector128(stackalloc byte[] {
                m0, m0, m1, m1, m2, m2, m3, m3,
                m0, m0, m1, m1, m2, m2, m3, m3,
            });
            mask = Sse2.Add(mask, Permute_.Permute4Mask128);
            return H.Reinterpret<byte, ushort>(Ssse3.Shuffle(vv, mask));
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

    internal static Vector128<uint> Permute4(Vector128<uint> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Sse2.IsSupported)
        {
            ref readonly var vv = ref H.Reinterpret<uint, float>(in v);
            var control = H.CreateVector128(stackalloc int[] { m0, m1, m2, m3, });
            return Vector128.As<float, uint>(Avx.PermuteVar(vv, control));
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

    internal static Vector128<ulong> Permute4(Vector128<ulong> v, byte m0, byte m1, byte m2, byte m3)
    {
        throw new NotSupportedException();
    }




    internal static Vector256<T> Permute2<T>(Vector256<T> v, byte m0, byte m1)
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

    internal static Vector256<T> Permute4<T>(Vector256<T> v, byte m0, byte m1, byte m2, byte m3)
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

    internal static Vector256<byte> PermuteX(Vector256<byte> v, byte m0, byte m1, byte m2, byte m3, ReadOnlySpan<byte> maskBase)
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

    internal static Vector256<ushort> Permute4(Vector256<ushort> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            ref readonly var vv = ref H.Reinterpret<ushort, byte>(in v);
            m0 <<= 1;
            m1 <<= 1;
            m2 <<= 1;
            m3 <<= 1;
            var mask = H.CreateVector256(stackalloc byte[] {
                m0, m0, m1, m1, m2, m2, m3, m3,
                m0, m0, m1, m1, m2, m2, m3, m3,
                m0, m0, m1, m1, m2, m2, m3, m3,
                m0, m0, m1, m1, m2, m2, m3, m3,
            });
            mask = Avx2.Add(mask, Permute_.Permute4Mask256);
            return H.Reinterpret<byte, ushort>(Avx2.Shuffle(vv, mask));
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

    internal static Vector256<uint> Permute4(Vector256<uint> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            ref readonly var vv = ref H.Reinterpret<uint, float>(in v);
            var control = H.CreateVector256(stackalloc int[] { m0, m1, m2, m3, m0 + 4, m1 + 4, m2 + 4, m3 + 4, });
            return Vector256.As<float, uint>(Avx.PermuteVar(vv, control));
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

    internal static Vector256<ulong> Permute4(Vector256<ulong> v, byte m0, byte m1, byte m2, byte m3)
    {
        if (Avx2.IsSupported)
        {
            ref readonly var vv = ref H.Reinterpret<ulong, float>(in v);
            m0 <<= 1;
            m1 <<= 1;
            m2 <<= 1;
            m3 <<= 1;
            var control = H.CreateVector256(stackalloc int[] { m0, m0 + 1, m1, m1 + 1, m2, m2 + 1, m3, m3 + 1, });
            return Vector256.As<float, ulong>(Avx2.PermuteVar8x32(vv, control));
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