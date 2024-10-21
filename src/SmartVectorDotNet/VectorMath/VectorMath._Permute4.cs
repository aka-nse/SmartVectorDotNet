namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
#endif

partial class VectorMath
{
    /// <summary>
    /// Permutes elements in chunk by considering 4 consecutive elements as one chunk.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="v"></param>
    /// <param name="m0"> Only bits 0-1 are considered. </param>
    /// <param name="m1"> Only bits 0-1 are considered. </param>
    /// <param name="m2"> Only bits 0-1 are considered. </param>
    /// <param name="m3"> Only bits 0-1 are considered. </param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="T"/> is unsupported type<br/>
    /// or<br/>
    /// <see cref="Vector{T}.Count"/> is less than 4.
    /// </exception>
    public static Vector<T> Permute4<T>(Vector<T> v, byte m0, byte m1, byte m2, byte m3)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Vector<T>.Count == Vector256<T>.Count)
        {
            return Permute4(v.AsVector256(), m0, m1, m2, m3).AsVector();
        }
        if (Vector<T>.Count == Vector128<T>.Count && Vector128<T>.Count >= 4)
        {
            return Permute4(v.AsVector128(), m0, m1, m2, m3).AsVector();
        }
#endif
        if(Vector<T>.Count >= 4)
        {
            var retval = (stackalloc T[Vector<T>.Count]);
            for (var i = 0; i < Vector<T>.Count; i += 4)
            {
                retval[i + 0] = v[i + m0];
                retval[i + 1] = v[i + m1];
                retval[i + 2] = v[i + m2];
                retval[i + 3] = v[i + m3];
            }
            return H.CreateVector(retval);
        }
        throw new NotSupportedException();
    }


    /// <summary>
    /// Permutes elements in chunk by considering 4 consecutive elements as one chunk.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="m0"> Only bits 0-1 are considered. </param>
    /// <param name="m1"> Only bits 0-1 are considered. </param>
    /// <param name="m2"> Only bits 0-1 are considered. </param>
    /// <param name="m3"> Only bits 0-1 are considered. </param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="T"/> is unsupported type<br/>
    /// or<br/>
    /// <see cref="Vector{T}.Count"/> is less than 2.
    /// </exception>
    /// <remarks>
    /// This overload is assumed to be used for 64-bit <typeparamref name="T"/>
    /// on the environment who might support only 128-bit SIMD.
    /// </remarks>
    public static void Permute4<T>(ref Vector<T> v1, ref Vector<T> v2, byte m0, byte m1, byte m2, byte m3)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Vector<T>.Count == Vector256<T>.Count)
        {
            v1 = Permute4(v1.AsVector256(), m0, m1, m2, m3).AsVector();
            v2 = Permute4(v2.AsVector256(), m0, m1, m2, m3).AsVector();
            return;
        }
        if (Vector<T>.Count == Vector128<T>.Count && Vector128<T>.Count >= 4)
        {
            v1 = Permute4(v1.AsVector128(), m0, m1, m2, m3).AsVector();
            v2 = Permute4(v2.AsVector128(), m0, m1, m2, m3).AsVector();
            return;
        }
#endif
        if (Vector<T>.Count >= 4)
        {
            v1 = emulate(v1, m0, m1, m2, m3);
            v2 = emulate(v2, m0, m1, m2, m3);
            return;
        }
        if(Vector<T>.Count == 2)
        {
            var input = (stackalloc Vector<T>[2] { v1, v2, });
            var retval = (stackalloc T[4]);
            retval[0] = input[(m0 & 0b10) >> 1][m0 & 0b01];
            retval[1] = input[(m1 & 0b10) >> 1][m1 & 0b01];
            retval[2] = input[(m2 & 0b10) >> 1][m2 & 0b01];
            retval[3] = input[(m3 & 0b10) >> 1][m3 & 0b01];
            v1 = H.Reinterpret<T, Vector<T>>(retval[0]);
            v2 = H.Reinterpret<T, Vector<T>>(retval[1]);
            return;
        }
        throw new NotSupportedException();

        static Vector<T> emulate(Vector<T> v, byte m0, byte m1, byte m2, byte m3)
        {
            var retval = (stackalloc T[Vector<T>.Count]);
            for (var i = 0; i < Vector<T>.Count; i += 4)
            {
                retval[i + 0] = v[i + (m0 & 0b11)];
                retval[i + 1] = v[i + (m1 & 0b11)];
                retval[i + 2] = v[i + (m2 & 0b11)];
                retval[i + 3] = v[i + (m3 & 0b11)];
            }
            return H.CreateVector(retval);
        }
    }
}
