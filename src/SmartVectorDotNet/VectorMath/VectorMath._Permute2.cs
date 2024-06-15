namespace SmartVectorDotNet;
using OP = VectorOp;
using H = InternalHelpers;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.CompilerServices;
#endif

partial class VectorMath
{
    /// <summary>
    /// Permutes elements in chunk by considering 2 consecutive elements as one chunk.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="v"></param>
    /// <param name="m0"> Only bits 0-1 are considered. </param>
    /// <param name="m1"> Only bits 0-1 are considered. </param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="T"/> is unsupported type.<br/>
    /// or<br/>
    /// <see cref="Vector{T}.Count"/> is less than 2.
    /// </exception>
    public static Vector<T> Permute2<T>(in Vector<T> v, byte m0, byte m1)
        where T : unmanaged
    {
#if NET6_0_OR_GREATER
        if (Vector<T>.Count == Vector256<T>.Count)
        {
            return Permute2(v.AsVector256(), m0, m1).AsVector();
        }
        if (Vector<T>.Count == Vector128<T>.Count && Vector128<T>.Count >= 4)
        {
            return Permute2(v.AsVector128(), m0, m1).AsVector();
        }
#endif
        if(Vector<T>.Count >= 2)
        {
            var retval = (stackalloc T[Vector<T>.Count]);
            for (var i = 0; i < Vector<T>.Count; i += 2)
            {
                retval[i + 0] = v[i + (m0 & 0b11)];
                retval[i + 1] = v[i + (m1 & 0b11)];
            }
            return H.CreateVector(retval);
        }
        throw new NotSupportedException();
    }
}
