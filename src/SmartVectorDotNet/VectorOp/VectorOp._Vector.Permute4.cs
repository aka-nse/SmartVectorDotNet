namespace SmartVectorDotNet;
using H = InternalHelpers;
#if NET6_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

partial class VectorOp
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
    /// <remarks>
    /// example:
    /// <code><![CDATA[
    /// var x = new Vector<ulong>([0uL, 1uL, 2uL, 3uL]);
    /// Console.WriteLine(VectorOp.Permute4(x, 0, 0, 0, 0));  // <0, 0, 0, 0>
    /// Console.WriteLine(VectorOp.Permute4(x, 1, 1, 1, 1));  // <1, 1, 1, 1>
    /// Console.WriteLine(VectorOp.Permute4(x, 2, 2, 2, 2));  // <2, 2, 2, 2>
    /// Console.WriteLine(VectorOp.Permute4(x, 3, 3, 3, 3));  // <3, 3, 3, 3>
    /// Console.WriteLine(VectorOp.Permute4(x, 0, 1, 2, 3));  // <0, 1, 2, 3>
    /// Console.WriteLine(VectorOp.Permute4(x, 3, 2, 1, 0));  // <3, 2, 1, 0>
    /// Console.WriteLine(VectorOp.Permute4(x, 4, 5, 6, 7));  // <0, 1, 2, 3> : b7-b2 are ignored
    ///
    /// var y = new Vector<uint>([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u]);
    /// Console.WriteLine(VectorOp.Permute4(y, 0, 0, 0, 0));  // <0, 0, 0, 0, 4, 4, 4, 4>
    /// Console.WriteLine(VectorOp.Permute4(y, 1, 1, 1, 1));  // <1, 1, 1, 1, 5, 5, 5, 5>
    /// Console.WriteLine(VectorOp.Permute4(y, 2, 2, 2, 2));  // <2, 2, 2, 2, 6, 6, 6, 6>
    /// Console.WriteLine(VectorOp.Permute4(y, 3, 3, 3, 3));  // <3, 3, 3, 3, 7, 7, 7, 7>
    /// Console.WriteLine(VectorOp.Permute4(y, 0, 1, 2, 3));  // <0, 1, 2, 3, 4, 5, 6, 7>
    /// Console.WriteLine(VectorOp.Permute4(y, 3, 2, 1, 0));  // <3, 2, 1, 0, 7, 6, 5, 4>
    /// Console.WriteLine(VectorOp.Permute4(y, 4, 5, 6, 7));  // <0, 1, 2, 3, 4, 5, 6, 7> : b7-b2 are ignored
    /// ]]></code>
    /// </remarks>
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
            return Permute4_Emulate(v, m0, m1, m2, m3);
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
            v1 = Permute4_Emulate(v1, m0, m1, m2, m3);
            v2 = Permute4_Emulate(v2, m0, m1, m2, m3);
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
    }
}
