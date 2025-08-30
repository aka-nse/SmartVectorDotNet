namespace SmartVectorDotNet;
using H = InternalHelpers;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

partial class VectorOp
{
    /// <summary>
    /// Permutes elements in chunk by considering 2 consecutive elements as one chunk.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="v"></param>
    /// <param name="m0"> Only bits 0 are considered. </param>
    /// <param name="m1"> Only bits 0 are considered. </param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="T"/> is unsupported type.<br/>
    /// or<br/>
    /// <see cref="Vector{T}.Count"/> is less than 2.
    /// </exception>
    /// <remarks>
    /// example:
    /// <code><![CDATA[
    /// var x = new Vector<ulong>;([0uL, 1uL, 2uL, 3uL]);
    /// Console.WriteLine(VectorOp.Permute2(x, 0, 0));  // <0, 0, 2, 2>
    /// Console.WriteLine(VectorOp.Permute2(x, 0, 1));  // <0, 1, 2, 3>
    /// Console.WriteLine(VectorOp.Permute2(x, 1, 0));  // <1, 0, 3, 2>
    /// Console.WriteLine(VectorOp.Permute2(x, 1, 1));  // <1, 1, 3, 3>
    /// Console.WriteLine(VectorOp.Permute2(x, 2, 3));  // <0, 0, 2, 2> : b7-b1 are ignored
    /// 
    /// var y = new Vector<uint>([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u]);
    /// Console.WriteLine(VectorOp.Permute2(y, 0, 0));  // <0, 0, 2, 2, 4, 4, 6, 6>
    /// Console.WriteLine(VectorOp.Permute2(y, 0, 1));  // <0, 1, 2, 3, 4, 5, 6, 7>
    /// Console.WriteLine(VectorOp.Permute2(y, 1, 0));  // <1, 0, 3, 2, 5, 4, 7, 6>
    /// Console.WriteLine(VectorOp.Permute2(y, 1, 1));  // <1, 1, 3, 3, 5, 5, 7, 7>
    /// Console.WriteLine(VectorOp.Permute2(y, 2, 3));  // <0, 1, 2, 3, 4, 5, 6, 7> : b7-b1 are ignored
    /// ]]></code>
    /// </remarks>
    public static Vector<T> Permute2<T>(Vector<T> v, byte m0, byte m1)
        where T : unmanaged
    {
        m0 &= 1;
        m1 &= 1;
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
        if (Vector<T>.Count >= 2)
        {
            var retval = (stackalloc T[Vector<T>.Count]);
            for (var i = 0; i < Vector<T>.Count; i += 2)
            {
                retval[i + 0] = v[i + m0];
                retval[i + 1] = v[i + m1];
            }
            return H.CreateVector(retval);
        }
        throw new NotSupportedException();
    }
}
